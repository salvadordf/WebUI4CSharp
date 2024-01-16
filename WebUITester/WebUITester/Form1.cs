using System.Diagnostics;
using WebUI4CSharp;

namespace WebUITester
{
    public partial class Form1 : Form
    {
        UIntPtr myWindow = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myWindow = WebUI.webui_new_window();
            if (WebUI.webui_show_browser(myWindow, "<html><head><script src=\"webui.js\"></script></head> Hello World ! </html>", (UIntPtr)WebUI.webui_browsers.Edge))

            {
                label1.Text = "true";
            }
            else
            {
                label1.Text = "false";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = WebUI.webui_get_url(myWindow);
        }
    }
}
