using WebUI4CSharp;

namespace winforms_text_editor
{
    public partial class MainForm : Form
    {
        public MainForm()
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

        private void ShowBrowserBtn_Click(object sender, EventArgs e)
        {
            WebUIWindow window = new WebUIWindow();
            window.OnWebUIEvent += Window_OnWebUIEvent;

            // Set the root folder for the UI
            string absPath = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\text_editor");
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
