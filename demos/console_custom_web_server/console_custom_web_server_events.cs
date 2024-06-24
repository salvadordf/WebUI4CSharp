using WebUI4CSharp;

namespace console_custom_web_server
{
    public static class WebUI_Events
    {
        public static void events(ref webui_event_t e)
        {
            // This function gets called every time
            // there is an event
            WebUIEvent lEvent = new WebUIEvent(e);
            switch (lEvent.EventType)
            {
                case webui_event.WEBUI_EVENT_CONNECTED:
                    Console.WriteLine("connected.");
                    break;

                case webui_event.WEBUI_EVENT_DISCONNECTED:
                    Console.WriteLine("disconnected.");
                    break;

                case webui_event.WEBUI_EVENT_MOUSE_CLICK:
                    Console.WriteLine("click.");
                    break;

                case webui_event.WEBUI_EVENT_NAVIGATION:
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

        public static void my_backend_func(ref webui_event_t e)
        {
            // JavaScript:
            // my_backend_func(123, 456, 789);
            // or webui.my_backend_func(...);
            WebUIEvent lEvent = new WebUIEvent(e);

            long number_1 = lEvent.GetIntAt(0);
            long number_2 = lEvent.GetIntAt(1);
            long number_3 = lEvent.GetIntAt(2);

            Console.WriteLine($"my_backend_func 1: {number_1}"); // 123
            Console.WriteLine($"my_backend_func 2: {number_2}"); // 456
            Console.WriteLine($"my_backend_func 3: {number_3}"); // 789
        }
    }
}
