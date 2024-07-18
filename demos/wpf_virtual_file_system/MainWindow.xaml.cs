using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using WebUI4CSharp;

namespace wpf_virtual_file_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebUIWindow _Window = new WebUIWindow();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void exit_app(WebUIEvent e)
        {
            // Close all opened windows
            WebUI.Exit();
        }

        private void ShowBrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            _Window.OnFileHandlerEvent += Window_OnFileHandlerEvent;
            _Window.OnWebUIEvent += Window_OnWebUIEvent;

            // Bind HTML element IDs with a C functions
            _Window.Bind("Exit");

            // Set a custom files handler
            _Window.SetFileHandler();

            // Show a new window
            _Window.Show("index.html");
        }

        private void Window_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            exit_app(e.BindEvent);
        }

        private void Window_OnFileHandlerEvent(object? sender, FileHandlerEventArgs e)
        {
            string lFilename = e.FileName;

            // This function reads files in the drive but you can get the contents from
            // any other source like resources.

            int i = lFilename.IndexOf('/');
            if (i >= 0)
            {
                lFilename = lFilename.Substring(i + 1);
            }
            string lAssetsDir = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\virtual_file_system");
            string lFileAbsPath = Path.Combine(lAssetsDir, lFilename);

            try
            {
                if (File.Exists(lFileAbsPath))
                {
                    string lContent;
                    using (FileStream lFileStream = new FileStream(lFileAbsPath, FileMode.Open))
                    {
                        var lStreamReader = new StreamReader(lFileStream);
                        lContent = lStreamReader.ReadToEnd();
                        lStreamReader.Close();
                        lFileStream.Close();
                    }
                    string lMimeType = WebUI.GetMimeType(lFilename);
                    string lHTTPHeader = @"HTTP/1.1 200 OK" + Environment.NewLine +
                                          "Content-Type: " + lMimeType + Environment.NewLine +
                                          "Content-Length: " + lContent.Length.ToString() + Environment.NewLine +
                                          "Cache-Control: no-cache" + Environment.NewLine + Environment.NewLine +
                                          lContent;

                    e.ReturnValue = lHTTPHeader;
                }
                else if (Directory.Exists(lFileAbsPath))
                {
                    // Redirect requests to directories to the index.html file.
                    string lHTTPHeader = @"HTTP/1.1 302 Found" + Environment.NewLine +
                                          "Location: " + e.FileName + @"\index.html" + Environment.NewLine +
                                          "Cache-Control: no-cache" + Environment.NewLine + Environment.NewLine;

                    e.ReturnValue = lHTTPHeader;
                }
                else
                {
                    string lContent = "<html><head><title>Resource Not Found</title></head>" +
                        "<body><p>The resource you requested has not been found at the specified address. " +
                        "Please check the spelling of the address.</p></body></html>";

                    string lHTTPHeader = @"HTTP/1.1 404 Not Found" + Environment.NewLine +
                                          "Content-Type: text/html" + Environment.NewLine +
                                          "Content-Length: " + lContent.Length.ToString() + Environment.NewLine +
                                          "Cache-Control: no-cache" + Environment.NewLine + Environment.NewLine +
                                          lContent;

                    e.ReturnValue = lHTTPHeader;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"There was a problem opening {lFilename}. {exc.Message}");
            }
        }
    }
}