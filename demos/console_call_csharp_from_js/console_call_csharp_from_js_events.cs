using WebUI4CSharp;

namespace console_call_csharp_from_js
{
    public static class WebUI_Events
    {
        public static void my_function_string(ref webui_event_t e)
        {
            // JavaScript:
            // webui.call('MyID_One', 'Hello', 'World`);

            WebUIEvent lEvent = new WebUIEvent(e);
            string? str_1 = lEvent.GetString(); 
            string? str_2 = lEvent.GetStringAt(1);

            Console.WriteLine("my_function_string 1: {0}", str_1); // Hello
            Console.WriteLine("my_function_string 2: {0}", str_2); // World
        }


        public static void my_function_integer(ref webui_event_t e)
        {

            // JavaScript:
            // webui.call('MyID_Two', 123, 456, 789);

            WebUIEvent lEvent = new WebUIEvent(e);
            long number_1 = lEvent.GetInt(); 
            long number_2 = lEvent.GetIntAt(1);
            long number_3 = lEvent.GetIntAt(2);
            
            Console.WriteLine("my_function_integer 1: {0}", number_1); // 123
            Console.WriteLine("my_function_integer 2: {0}", number_2); // 456
            Console.WriteLine("my_function_integer 3: {0}", number_3); // 789
        }

        public static void my_function_boolean(ref webui_event_t e)
        {

            // JavaScript:
            // webui.call('MyID_Three', true, false);

            WebUIEvent lEvent = new WebUIEvent(e);
            bool status_1 = lEvent.GetBool();
            bool status_2 = lEvent.GetBoolAt(1);

            Console.WriteLine("my_function_boolean 1: {0}", status_1 ? "True" : "False"); // True
            Console.WriteLine("my_function_boolean 2: {0}", status_2 ? "True" : "False"); // False
        }

        public static void my_function_raw_binary(ref webui_event_t e)
        {

            // JavaScript:
            // webui.call('MyID_RawBinary', new Uint8Array([0x41,0x42,0x43]), big_arr);

            WebUIEvent lEvent = new WebUIEvent(e);
            MemoryStream? stream = lEvent.GetStream();
            if (stream != null)
            {
                string hexstring = Convert.ToHexString(stream.ToArray());
                Console.WriteLine("my_function_raw_binary: " + hexstring);
            }
        }

        public static void my_function_with_response(ref webui_event_t e)
        {

            // JavaScript:
            // webui.call('MyID_Four', number, 2).then(...)

            WebUIEvent lEvent = new WebUIEvent(e);
            long number = lEvent.GetInt(); 
            long times = lEvent.GetIntAt(1);

            long res = number * times;
            Console.WriteLine("my_function_with_response: {0} * {1} = {2}", number, times, res);

            // Send back the response to JavaScript
            lEvent.ReturnInt(res);
        }
    }
}
