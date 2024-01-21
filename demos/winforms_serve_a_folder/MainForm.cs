using WebUI4CSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace winforms_serve_a_folder
{
    public partial class MainForm : Form
    {
        Object _lockObj = new Object();
        List<String> _LogStrings = new List<String>();
        private WebUIWindow _Window = new WebUIWindow();
        private WebUIWindow _SecondWindow = new WebUIWindow();
        private int _Count = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void AddLog(String msg)
        {
            lock (_lockObj)
            {
                _LogStrings.Add(msg);
            }
        }

        public void exit_app(WebUIEvent e)
        {
            // Close all opened windows
            WebUI.Exit();
        }

        public void events(WebUIEvent e)
        {
            // This function gets called every time
            // there is an event
            switch (e.EventType)
            {
                case webui_events.WEBUI_EVENT_CONNECTED:
                    AddLog("connected.");
                    break;

                case webui_events.WEBUI_EVENT_DISCONNECTED:
                    AddLog("disconnected.");
                    break;

                case webui_events.WEBUI_EVENT_MOUSE_CLICK:
                    AddLog("click.");
                    break;

                case webui_events.WEBUI_EVENT_NAVIGATION:
                    string? lUrl = e.GetString();
                    string navmessage = $"Navigating to {lUrl}";
                    AddLog(navmessage);
                    WebUIWindow? window = e.Window;
                    if ((window != null) && (lUrl != null))
                    {
                        // Because we used `webui_bind(MyWindow, "", events);`
                        // WebUI will block all `href` link clicks and sent here instead.
                        // We can then control the behaviour of links as needed.
                        window.Navigate(lUrl);
                    }
                    break;
            }
        }

        public void switch_to_second_page(WebUIEvent e)
        {
            // This function gets called every
            // time the user clicks on "SwitchToSecondPage"

            // Switch to `/second.html` in the same opened window.
            WebUIWindow? window = e.Window;
            if (window != null)
            {
                window.Show("second.html");
            }
        }

        public void show_second_window(WebUIEvent e)
        {

            // This function gets called every
            // time the user clicks on "OpenNewWindow"

            // Show a new window, and navigate to `/second.html`
            // if it's already open, then switch in the same window
            _SecondWindow.Show("second.html");
        }

        private void ShowBrowserBtn_Click(object sender, EventArgs e)
        {
            _Window.OnFileHandlerEvent += SecondWindow_OnFileHandlerEvent;
            _Window.OnWebUIEvent += Window_OnWebUIEvent;
            _SecondWindow.OnWebUIEvent += SecondWindow_OnWebUIEvent;

            // Bind HTML element IDs with a C functions
            _Window.Bind("SwitchToSecondPage");
            _Window.Bind("OpenNewWindow");
            _Window.Bind("Exit");
            _SecondWindow.Bind("Exit");

            // Bind events
            _Window.BindAllEvents();

            // Make Deno as the `.ts` and `.js` interpreter
            _Window.SetRuntime(webui_runtimes.Deno);

            // Set a custom files handler
            _Window.SetFileHandler();

            // Set window size
            _Window.SetSize(800, 800);

            // Set window position
            _Window.SetPosition(200, 200);

            // Set the root folder for the UI
            string absPath = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\serve_a_folder");
            _Window.SetRootFolder(absPath);
            _SecondWindow.SetRootFolder(absPath);

            // Show a new window
            // webui_set_root_folder(MyWindow, "_MY_PATH_HERE_");
            // webui_show_browser(MyWindow, "index.html", Chrome);
            _Window.Show("index.html");
            timer1.Enabled = true;
        }

        private void Window_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            if (e.BindEvent.Element == "SwitchToSecondPage")
            {
                switch_to_second_page(e.BindEvent);
            }
            else if (e.BindEvent.Element == "OpenNewWindow")
            {
                show_second_window(e.BindEvent);
            }
            else if (e.BindEvent.Element == "Exit")
            {
                exit_app(e.BindEvent);
            }
            else
            {
                events(e.BindEvent);
            }
        }

        private void SecondWindow_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            exit_app(e.BindEvent);
        }

        private void SecondWindow_OnFileHandlerEvent(object? sender, FileHandlerEventArgs e)
        {
            string logMessage = $"File: {e.FileName}";
            AddLog(logMessage);

            if (e.FileName == "/test.txt")
            {
                // Const static file example
                // Note: The connection will drop if the content
                // does not have `<script src="webui.js"></script>`
                e.ReturnValue = "This is a embedded file content example.";
            }
            else if (e.FileName == "/dynamic.html")
            {
                // Dynamic file example
                ++_Count;
                e.ReturnValue = $"<html>This is a dynamic file content example.<br> Count: {_Count}" +
                                 " <a href=\"dynamic.html\">[Refresh]</a><br>" +
                                 "<script src=\"webui.js\"></script></html>"; // To keep connection with WebUI
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (_lockObj)
            {
                for (int i = 0; i < _LogStrings.Count; i++)
                {
                    Memo1.Text += _LogStrings[i] + Environment.NewLine;
                }
                _LogStrings.Clear();
            }
        }
    }
}
