using System.Diagnostics;
using WebUI4CSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace winforms_public_network_access
{
    public partial class MainForm : Form
    {
        private WebUIWindow pubWindow = new WebUIWindow();
        private WebUIWindow prvWindow = new WebUIWindow();
        private string public_win_url = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        public void public_window_events(WebUIEvent e)
        {
            // This function gets called every time
            // there is an event
            switch (e.EventType)
            {
                case webui_events.WEBUI_EVENT_CONNECTED:
                    prvWindow.Run("document.getElementById(\"Logs\").value += \"New connection.\\n\";");
                    break;

                case webui_events.WEBUI_EVENT_DISCONNECTED:
                    prvWindow.Run("document.getElementById(\"Logs\").value += \"Disconnected.\\n\";");
                    break;
            }
        }

        public void app_exit(WebUIEvent e)
        {
            WebUI.Exit();
        }

        private void OpenDefBrowserBtn_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo(public_win_url) { UseShellExecute = true });
        }

        private void ShowBrowserBtn_Click(object sender, EventArgs e)
        {
            // Main Private Window HTML
            string private_html = "<!DOCTYPE html>" +
                    "<html>" +
                    "  <head>" +
                    "    <meta charset=\"UTF-8\">" +
                    "    <script src=\"webui.js\"></script>" +
                    "    <title>Public Network Access Example</title>" +
                    "    <style>" +
                    "       body {" +
                    "            font-family: 'Arial', sans-serif;" +
                    "            color: white;" +
                    "            background: linear-gradient(to right, #507d91, #1c596f, #022737);" +
                    "            text-align: center;" +
                    "            font-size: 18px;" +
                    "        }" +
                    "        button, input {" +
                    "            padding: 10px;" +
                    "            margin: 10px;" +
                    "            border-radius: 3px;" +
                    "            border: 1px solid #ccc;" +
                    "            box-shadow: 0 3px 5px rgba(0,0,0,0.1);" +
                    "            transition: 0.2s;" +
                    "        }" +
                    "        button {" +
                    "            background: #3498db;" +
                    "            color: #fff; " +
                    "            cursor: pointer;" +
                    "            font-size: 16px;" +
                    "        }" +
                    "        h1 { text-shadow: -7px 10px 7px rgb(67 57 57 / 76%); }" +
                    "        button:hover { background: #c9913d; }" +
                    "        input:focus { outline: none; border-color: #3498db; }" +
                    "    </style>" +
                    "  </head>" +
                    "  <body>" +
                    "    <h1>WebUI - Public Network Access Example</h1>" +
                    "    <br>" +
                    "    The second public window is configured to be accessible from <br>" +
                    "    any device in the public network. <br>" +
                    "    <br>" +
                    "    Second public window link: <br>" +
                    "    <h1 id=\"urlSpan\" style=\"color:#c9913d\">...</h1>" +
                    "    Second public window events: <br>" +
                    "    <textarea id=\"Logs\" rows=\"4\" cols=\"50\" style=\"width:80%\"></textarea>" +
                    "    <br>" +
                    "    <button id=\"Exit\">Exit</button>" +
                    "  </body>" +
                    "</html>";

            // Public Window HTML
            string public_html = "<!DOCTYPE html>" +
                    "<html>" +
                    "  <head>" +
                    "    <meta charset=\"UTF-8\">" +
                    "    <script src=\"webui.js\"></script>" +
                    "    <title>Welcome to Public UI</title>" +
                    "  </head>" +
                    "  <body>" +
                    "    <h1>Welcome to Public UI!</h1>" +
                    "  </body>" +
                    "</html>";

            // App
            WebUI.SetTimeout(0); // Wait forever (never timeout)

            // Public Window
            pubWindow.SetPublic(true); // Make URL accessible from public networks
            pubWindow.BindAllEvents(); // Bind all events
            pubWindow.OnWebUIEvent += pubWindow_OnWebUIEvent;
            pubWindow.ShowBrowser(public_html, webui_browsers.NoBrowser); // Set public window HTML
            public_win_url = pubWindow.Url; // Get URL of public window

            // Main Private Window
            prvWindow.Bind("Exit"); // Bind exit button
            prvWindow.OnWebUIEvent += prvWindow_OnWebUIEvent;
            prvWindow.Show(private_html); // Show the window

            // Set URL in the UI
            string javascript = $"document.getElementById('urlSpan').innerHTML = '{public_win_url}';";
            prvWindow.Run(javascript);

            OpenDefBrowserBtn.Enabled = true;
            ShowBrowserBtn.Enabled = false;
        }

        private void pubWindow_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            public_window_events(e.BindEvent);
        }

        private void prvWindow_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            app_exit(e.BindEvent);
        }
    }
}

