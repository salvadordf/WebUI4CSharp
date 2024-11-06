using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WebUI4CSharp;

namespace console_web_app_multi_client
{
    public class WebUI_Events
    {
        public static WebUIWindow myWindow = new WebUIWindow();
        static string?[] privateInput_arr = new string[1024]; // One for each user
        static string? publicInput = null; // One for all users
        static UIntPtr users_count = 0;
        static UIntPtr tab_count = 0;

        public static void save(ref webui_event_t e)
        {
            WebUIEvent lEvent = new WebUIEvent(e);
            // Get input value and save it in the array
            privateInput_arr[e.client_id] = lEvent.GetString();
        }

        public static void saveAll(ref webui_event_t e)
        {
            WebUIEvent lEvent = new WebUIEvent(e);
            // Get input value and save it
            publicInput = lEvent.GetString();
            WebUIWindow? lWindow = lEvent.Window;
            lWindow?.Run($"document.getElementById(\"publicInput\").value = \"{publicInput}\";");
        }

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

            // Full web browser cookies
            string? cookies = lEvent.Cookies;

            // Static client (Based on web browser cookies)
            UIntPtr client_id = lEvent.ClientID;

            // Dynamic client connection ID (Changes on connect/disconnect events)
            UIntPtr connection_id = lEvent.ConnectionID;

            switch (lEvent.EventType)
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
            lEvent.RunClient("document.getElementById(\"status\").innerText = \"Connected!\";");

            // userNumber
            lEvent.RunClient($"document.getElementById(\"userNumber\").innerText = \"{client_id}\";");

            // connectionNumber
            lEvent.RunClient($"document.getElementById(\"connectionNumber\").innerText = \"{connection_id}\";");

            // privateInput
            lEvent.RunClient($"document.getElementById(\"privateInput\").value = \"{privateInput_arr[client_id]}\";");

            // publicInput
            lEvent.RunClient($"document.getElementById(\"publicInput\").value = \"{publicInput}\";");

            // Update all connected users

            // userCount
            lEvent.RunClient($"document.getElementById(\"userCount\").innerText = \"{users_count}\";");

            // tabCount
            lEvent.RunClient($"document.getElementById(\"tabCount\").innerText = \"{tab_count}\";");

            if (tab_count == 0)
            {
                WebUI.Exit();
            }
        }
    }
}
