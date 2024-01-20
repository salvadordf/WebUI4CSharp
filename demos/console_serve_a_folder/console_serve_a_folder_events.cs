using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WebUI4CSharp;

namespace console_serve_a_folder
{
    public static class WebUI_Events
    {
        public static WebUIWindow myWindow = new WebUIWindow();
        public static WebUIWindow mySecondWindow = new WebUIWindow();
        public static int myCount = 0;

        public static void exit_app(ref webui_event_t e)
        {
            // Close all opened windows
            WebUI.Exit();
        }

        public static void events(ref webui_event_t e)
        {
            // This function gets called every time
            // there is an event
            WebUIEvent lEvent = new WebUIEvent(e);
            switch (lEvent.EventType)
            {
                case webui_events.WEBUI_EVENT_CONNECTED:
                    Console.WriteLine("connected.");
                    break;

                case webui_events.WEBUI_EVENT_DISCONNECTED:
                    Console.WriteLine("disconnected.");
                    break;

                case webui_events.WEBUI_EVENT_MOUSE_CLICK:
                    Console.WriteLine("click.");
                    break;

                case webui_events.WEBUI_EVENT_NAVIGATION:
                    string? lUrl = lEvent.GetString();
                    string navmessage = $"Navigating to {lUrl}";
                    Console.WriteLine(navmessage);
                    WebUIWindow? window = lEvent.Window;
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

        public static void switch_to_second_page(ref webui_event_t e)
        {
            // This function gets called every
            // time the user clicks on "SwitchToSecondPage"

            // Switch to `/second.html` in the same opened window.
            WebUIEvent lEvent = new WebUIEvent(e);
            WebUIWindow? window = lEvent.Window;
            if (window != null) 
            {
                window.Show("second.html");
            }
        }

        public static void show_second_window(ref webui_event_t e)
        {

            // This function gets called every
            // time the user clicks on "OpenNewWindow"

            // Show a new window, and navigate to `/second.html`
            // if it's already open, then switch in the same window
            mySecondWindow.Show("second.html");
        }

        public static IntPtr my_files_handler(IntPtr filename, out int length) 
        {
            string? lFilename = WebUI.WebUIStringToCSharpString(filename);

            if (lFilename != null)
            {
                string logMessage = $"File: {lFilename}";
                Console.WriteLine(logMessage);

                if (lFilename == "/test.txt")
                {
                    // Const static file example
                    // Note: The connection will drop if the content
                    // does not have `<script src="webui.js"></script>`
                    return WebUI.CSharpStringToWebUIString("This is a embedded file content example.", out length);
                }
                else if (lFilename == "/dynamic.html")
                {
                    // Dynamic file example
                    ++myCount;
                    string rsltString = $"<html>This is a dynamic file content example.<br> Count: {myCount}" + 
                                         " <a href=\"dynamic.html\">[Refresh]</a><br>" + 
                                         "<script src=\"webui.js\"></script></html>"; // To keep connection with WebUI
                    return WebUI.CSharpStringToWebUIString(rsltString, out length);
                } 
            }

            length = 0;
            // Other files:
            // A NULL return will make WebUI
            // looks for the file locally.
            return IntPtr.Zero;
        }

    }
}
