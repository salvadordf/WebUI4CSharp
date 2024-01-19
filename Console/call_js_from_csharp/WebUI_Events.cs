using WebUI4CSharp;

namespace call_js_from_csharp
{
    public static class WebUI_Events
    {
        public static void my_function_exit(ref webui_event_t e)
        {
            // Close all opened windows
            WebUI.Exit();
        }

        public static void my_function_count(ref webui_event_t e)
        {
            // This function gets called every time the user clicks on "MyButton1"
            WebUIEvent lEvent = new WebUIEvent(e);
            WebUIWindow? window = lEvent.Window;

            if (window != null)
            {
                string response;

                if (!window.Script("return GetCount();", 0, out response, 64))
                {
                    if (!window.IsShown())
                    {
                        Console.WriteLine("Window closed.");
                    }
                    else
                    {
                        Console.WriteLine("JavaScript Error: %s", response);
                    }
                    return;
                }

                // Get the count
                int count = Convert.ToInt32(response);

                // Increment
                count++;

                // Generate a JavaScript
                string js = $"SetCount({count});";

                // Run JavaScript
                window.Run(js);
            }
        }
    }
}
