using System.Windows;
using WebUI4CSharp;

namespace wpf_text_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebUIWindow _window = new WebUIWindow();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void close_app(WebUIEvent e)
        {
            WebUI.Exit();
        }

        private void Window_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            close_app(e.BindEvent);
        }

        private void ShowBrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            _window.OnWebUIEvent += Window_OnWebUIEvent;

            // Set the root folder for the UI
            string absPath = System.IO.Path.GetFullPath("..\\..\\..\\..\\..\\assets\\text_editor");
            _window.SetRootFolder(absPath);

            // Bind HTML elements with the specified ID to C functions
            _window.Bind("close_app");

            // Show the window, preferably in a chromium based browser
            if (!_window.ShowBrowser("index.html", webui_browser.ChromiumBased))
            {
                _window.Show("index.html");
            }
        }
    }
}