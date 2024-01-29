using System.Windows;
using WebUI4CSharp;

namespace wpf_text_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Close(WebUIEvent e)
        {
            WebUI.Exit();
        }

        private void Window_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            Close(e.BindEvent);
        }

        private void ShowBrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            WebUIWindow window = new WebUIWindow();
            window.OnWebUIEvent += Window_OnWebUIEvent;

            // Set the root folder for the UI
            string absPath = System.IO.Path.GetFullPath("..\\..\\..\\..\\..\\assets\\text_editor");
            window.SetRootFolder(absPath);

            // Bind HTML elements with the specified ID to C functions
            window.Bind("__close-btn");

            // Show the window, preferably in a chromium based browser
            if (!window.ShowBrowser("index.html", webui_browsers.ChromiumBased))
            {
                window.Show("index.html");
            }
        }
    }
}