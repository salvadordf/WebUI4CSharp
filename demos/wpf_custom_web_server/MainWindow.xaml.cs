using System.Windows;
using WebUI4CSharp;

namespace wpf_custom_web_server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Object _lockObj = new Object();
        List<String> _LogStrings = new List<String>();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        WebUIWindow _window = new WebUIWindow();

        public MainWindow()
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

        public void my_backend_func(WebUIEvent e)
        {
            // JavaScript:
            // my_backend_func(123, 456, 789);
            // or webui.my_backend_func(...);
            long number_1 = e.GetIntAt(0);
            long number_2 = e.GetIntAt(1);
            long number_3 = e.GetIntAt(2);

            AddLog($"my_backend_func 1: {number_1}"); // 123
            AddLog($"my_backend_func 2: {number_2}"); // 456
            AddLog($"my_backend_func 3: {number_3}"); // 789
        }

        private void Window_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            if (e.BindEvent.Element == "my_backend_func")
            {
                my_backend_func(e.BindEvent);
            }
            else
            {
                events(e.BindEvent);
            }
        }

        private void PythonBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "python.exe";
            startInfo.Arguments = System.IO.Path.GetFullPath("..\\..\\..\\..\\..\\assets\\custom_web_server\\simple_web_server.py");
            startInfo.WorkingDirectory = System.IO.Path.GetFullPath("..\\..\\..\\..\\..\\assets\\custom_web_server\\");
            process.StartInfo = startInfo;
            if (process.Start())
            {
                PythonBtn.IsEnabled = false;
                ShowBrowserBtn.IsEnabled = true;
            }
        }

        private void ShowBrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            // Bind all events
            _window.BindAllEvents();

            // Bind HTML elements with C functions
            _window.Bind("my_backend_func");

            // Set web server network port WebUI should use
            // this mean `webui.js` will be available at:
            // http://localhost:8081/webui.js
            _window.SetPort(8081);

            _window.OnWebUIEvent += Window_OnWebUIEvent;

            // Show a new window and show our custom web server
            // Assuming the custom web server is running on port
            // 8080...
            // Run the \assets\custom_web_server\simple_web_server.py script to create a simple web server
            _window.Show("http://localhost:8080/");

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500, 0);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WebUI.IsAppRunning())
            {
                WebUI.Exit();
            }
        }
    }
}