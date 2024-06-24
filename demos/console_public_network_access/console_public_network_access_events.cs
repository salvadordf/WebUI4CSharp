using WebUI4CSharp;

namespace console_public_network_access
{
    public static class WebUI_Events
    {
        public static WebUIWindow prvWindow = new WebUIWindow();
        public static WebUIWindow pubWindow = new WebUIWindow();
        public static void public_window_events(ref webui_event_t e)
        {
            // This function gets called every time
            // there is an event
            switch ((webui_event)e.event_type)
            {
                case webui_event.WEBUI_EVENT_CONNECTED:
                    prvWindow.Run("document.getElementById(\"Logs\").value += \"New connection.\\n\";");
                    break;

                case webui_event.WEBUI_EVENT_DISCONNECTED:
                    prvWindow.Run("document.getElementById(\"Logs\").value += \"Disconnected.\\n\";");
                    break;
            }
        }

        public static void app_exit(ref webui_event_t e)
        {
            WebUI.Exit();
        }
    }
}
