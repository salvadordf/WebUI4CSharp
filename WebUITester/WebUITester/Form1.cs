using System.Diagnostics;
using WebUI4CSharp;

namespace WebUITester
{
    public partial class Form1 : Form
    {
        WebUIWindow myWindow;
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
            myWindow = new WebUIWindow();
            if (myWindow.Show("<html><head><script src=\"webui.js\"></script></head> Hello World ! </html>"))

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
            label2.Text = myWindow.Url;
        }
    }
}
