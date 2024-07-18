using System.IO;
using System.Windows;
using WebUI4CSharp;

namespace wpf_web_app_multi_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebUIWindow _Window = new WebUIWindow();
        static string?[] privateInput_arr = new string[1024]; // One for each user
        static string? publicInput = null; // One for all users
        static UIntPtr users_count = 0;
        static UIntPtr tab_count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowBrowserBtn_Click(object sender, EventArgs e)
        {
            // Allow multi-user connection
            WebUI.SetConfig(webui_config.multi_client, true);

            // Allow cookies
            WebUI.SetConfig(webui_config.use_cookies, true);

            _Window.OnWebUIEvent += Window_OnWebUIEvent;

            // Bind HTML element IDs with a C functions
            _Window.Bind("save");
            _Window.Bind("saveAll");
            _Window.Bind("exit_app");

            // Bind events
            _Window.BindAllEvents();

            string lAssetsDir = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\web_app_multi_client");
            string lFileAbsPath = Path.Combine(lAssetsDir, "index.html");

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
                // Start server only
                string? url = _Window.StartServer(lContent);

                if (!string.IsNullOrWhiteSpace(url))
                {
                    // Open a new page in the default native web browser
                    WebUI.OpenUrl(url);
                }
            }
        }

        private static void save(WebUIEvent e)
        {
            // Get input value and save it in the array
            privateInput_arr[e.ClientID] = e.GetString();
        }

        private static void saveAll(WebUIEvent e)
        {
            // Get input value and save it
            publicInput = e.GetString();
            WebUIWindow? lWindow = e.Window;
            lWindow?.Run($"document.getElementById(\"publicInput\").value = \"{publicInput}\";");
        }

        private void exit_app(WebUIEvent e)
        {
            // Close all opened windows
            WebUI.Exit();
        }
        private void Window_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            if (e.BindEvent.Element == "save")
            {
                save(e.BindEvent);
            }
            else if (e.BindEvent.Element == "saveAll")
            {
                saveAll(e.BindEvent);
            }
            else if (e.BindEvent.Element == "exit_app")
            {
                exit_app(e.BindEvent);
            }
            else
            {
                events(e.BindEvent);
            }
        }

        private static void events(WebUIEvent e)
        {
            // This function gets called every time
            // there is an event

            // Full web browser cookies
            string? cookies = e.Cookies;

            // Static client (Based on web browser cookies)
            UIntPtr client_id = e.ClientID;

            // Dynamic client connection ID (Changes on connect/disconnect events)
            UIntPtr connection_id = e.ConnectionID;

            switch (e.EventType)
            {
                case webui_event.WEBUI_EVENT_CONNECTED:
                    // New connection
                    if (users_count < (client_id + 1))
                    { // +1 because it start from 0
                        users_count = (client_id + 1);
                    }
                    tab_count++;
                    break;

                case webui_event.WEBUI_EVENT_DISCONNECTED:
                    // Disconnection
                    if (tab_count > 0)
                        tab_count--;
                    break;
            }

            // Update this current user only

            // status
            e.RunClient("document.getElementById(\"status\").innerText = \"Connected!\";");

            // userNumber
            e.RunClient($"document.getElementById(\"userNumber\").innerText = \"{client_id}\";");

            // connectionNumber
            e.RunClient($"document.getElementById(\"connectionNumber\").innerText = \"{connection_id}\";");

            // privateInput
            e.RunClient($"document.getElementById(\"privateInput\").value = \"{privateInput_arr[client_id]}\";");

            // publicInput
            e.RunClient($"document.getElementById(\"publicInput\").value = \"{publicInput}\";");

            // Update all connected users

            // userCount
            e.RunClient($"document.getElementById(\"userCount\").innerText = \"{users_count}\";");

            // tabCount
            e.RunClient($"document.getElementById(\"tabCount\").innerText = \"{tab_count}\";");

            if (tab_count == 0)
            {
                WebUI.Exit();
            }
        }
    }
}