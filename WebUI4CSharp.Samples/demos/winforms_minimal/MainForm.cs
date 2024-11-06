using WebUI4CSharp;

namespace winforms_minimal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowBrowserBtn_Click(object sender, EventArgs e)
        {
            WebUIWindow window = new WebUIWindow();
            window.Show("<html><head><script src=\"webui.js\"></script></head> Hello World ! </html>");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WebUI.IsAppRunning()) 
            {
                WebUI.Exit();
            }
        }
    }
}
