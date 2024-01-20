using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace WebUI4CSharp
{
    public class WebUIEvent
    {
        private webui_event_t _event;

        /**
         * @brief Get an argument as integer at a specific index
         *
         * @param e The event struct
         * @param index The argument position starting from 0
         *
         * @return Returns argument as integer
         *
         * @example long long int myNum = webui_get_int_at(e, 0);
         */
        [DllImport("webui-2")]
        private static extern long webui_get_int_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get the first argument as integer
         *
         * @param e The event struct
         *
         * @return Returns argument as integer
         *
         * @example long long int myNum = webui_get_int(e);
         */
        [DllImport("webui-2")]
        private static extern long webui_get_int(ref webui_event_t e);

        /**
         * @brief Get an argument as string at a specific index
         *
         * @param e The event struct
         * @param index The argument position starting from 0
         *
         * @return Returns argument as string
         *
         * @example const char* myStr = webui_get_string_at(e, 0);
         */
        [DllImport("webui-2")]
        private static extern IntPtr webui_get_string_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get the first argument as string
         *
         * @param e The event struct
         *
         * @return Returns argument as string
         *
         * @example const char* myStr = webui_get_string(e);
         */
        [DllImport("webui-2")]
        private static extern IntPtr webui_get_string(ref webui_event_t e);

        /**
         * @brief Get an argument as boolean at a specific index
         *
         * @param e The event struct
         * @param index The argument position starting from 0
         *
         * @return Returns argument as boolean
         *
         * @example bool myBool = webui_get_bool_at(e, 0);
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_get_bool_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get the first argument as boolean
         *
         * @param e The event struct
         *
         * @return Returns argument as boolean
         *
         * @example bool myBool = webui_get_bool(e);
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_get_bool(ref webui_event_t e);

        /**
         * @brief Get the size in bytes of an argument at a specific index
         *
         * @param e The event struct
         * @param index The argument position starting from 0
         *
         * @return Returns size in bytes
         *
         * @example size_t argLen = webui_get_size_at(e, 0);
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_get_size_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get size in bytes of the first argument
         *
         * @param e The event struct
         *
         * @return Returns size in bytes
         *
         * @example size_t argLen = webui_get_size(e);
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_get_size(ref webui_event_t e);

        /**
         * @brief Return the response to JavaScript as integer.
         *
         * @param e The event struct
         * @param n The integer to be send to JavaScript
         *
         * @example webui_return_int(e, 123);
         */
        [DllImport("webui-2")]
        private static extern void webui_return_int(ref webui_event_t e, long n);

        /**
         * @brief Return the response to JavaScript as string.
         *
         * @param e The event struct
         * @param s The string to be send to JavaScript
         *
         * @example webui_return_string(e, "Response...");
         */
        [DllImport("webui-2")]
        private static extern void webui_return_string(ref webui_event_t e, [MarshalAs(UnmanagedType.LPUTF8Str)] string s);

        /**
         * @brief Return the response to JavaScript as a buffer.
         *
         * @param e The event struct
         * @param buffer The buffer to be send to JavaScript
         *
         * @example webui_return_string(e, "Response...");
         */
        [DllImport("webui-2", EntryPoint = "webui_return_string")]
        private static extern void webui_return_buffer(ref webui_event_t e, ref byte[] buffer);

        /**
         * @brief Return the response to JavaScript as boolean.
         *
         * @param e The event struct
         * @param n The boolean to be send to JavaScript
         *
         * @example webui_return_bool(e, true);
         */
        [DllImport("webui-2")]
        private static extern void webui_return_bool(ref webui_event_t e, [MarshalAs(UnmanagedType.I1)] bool b);

        /**
         * @brief When using `webui_interface_bind()`, you may need this function to easily set a response.
         *
         * @param window The window number
         * @param event_number The event number
         * @param response The response as string to be send to JavaScript
         *
         * @example webui_interface_set_response(myWindow, e->event_number, "Response...");
         */
        [DllImport("webui-2")]
        private static extern void webui_interface_set_response(UIntPtr window, UIntPtr event_number, [MarshalAs(UnmanagedType.LPUTF8Str)] string response);

        /**
         * @brief When using `webui_interface_bind()`, you may need this function to easily set a response.
         *
         * @param window The window number
         * @param event_number The event number
         * @param buffer The response as a buffer to be send to JavaScript
         *
         * @example webui_interface_set_response(myWindow, e->event_number, "Response...");
         */
        [DllImport("webui-2", EntryPoint = "webui_interface_set_response")]
        private static extern void webui_interface_set_buffer_response(UIntPtr window, UIntPtr event_number, ref byte[] buffer);

        /**
         * @brief Get an argument as string at a specific index
         *
         * @param window The window number
         * @param event_number The event number
         * @param index The argument position
         *
         * @return Returns argument as string
         *
         * @example const char* myStr = webui_interface_get_string_at(myWindow, e->event_number, 0);
         */
        [DllImport("webui-2")]
        private static extern IntPtr webui_interface_get_string_at(UIntPtr window, UIntPtr event_number, UIntPtr index);

        /**
         * @brief Get an argument as integer at a specific index
         *
         * @param window The window number
         * @param event_number The event number
         * @param index The argument position
         *
         * @return Returns argument as integer
         *
         * @example long long int myNum = webui_interface_get_int_at(myWindow, e->event_number, 0);
         */
        [DllImport("webui-2")]
        private static extern long webui_interface_get_int_at(UIntPtr window, UIntPtr event_number, UIntPtr index);

        /**
         * @brief Get an argument as boolean at a specific index
         *
         * @param window The window number
         * @param event_number The event number
         * @param index The argument position
         *
         * @return Returns argument as boolean
         *
         * @example bool myBool = webui_interface_get_bool_at(myWindow, e->event_number, 0);
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_interface_get_bool_at(UIntPtr window, UIntPtr event_number, UIntPtr index);

        /**
         * @brief Get the size in bytes of an argument at a specific index
         *
         * @param window The window number
         * @param event_number The event number
         * @param index The argument position
         *
         * @return Returns size in bytes
         *
         * @example size_t argLen = webui_interface_get_size_at(myWindow, e->event_number, 0);
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_interface_get_size_at(UIntPtr window, UIntPtr event_number, UIntPtr index);

        /// <summary>
        /// Returns true if the Window was created successfully.
        /// </summary>
        public bool Initialized { get { return _event.window > 0; } }
        /// <summary>
        /// WebUI event struct.
        /// </summary>
        public webui_event_t Event { get { return _event; } }
        /// <summary>
        /// Window wrapper for the Window object of this event.
        /// </summary>
        public WebUIWindow? Window { get { return WebUI.SearchWindow(_event.window); } } 
        /// <summary>
        /// The window object number or ID.
        /// </summary>
        public UIntPtr WindowID { get { return _event.window; } }
        /// <summary>
        /// Event type.
        /// </summary>
        public webui_events EventType { get { return (webui_events)_event.event_type; } }
        /// <summary>
        /// HTML element ID.
        /// </summary>
        public string? Element { get { return WebUI.WebUIStringToCSharpString(_event.element); } }
        /// <summary>
        /// Event number or Event ID.
        /// </summary>
        public UIntPtr EventID { get { return _event.event_number; } }
        /// <summary>
        /// Bind ID.
        /// </summary>
        public UIntPtr BindID { get { return _event.bind_id; } }

        public WebUIEvent(webui_event_t e)
        {
            _event.window = e.window;
            _event.event_type = e.event_type;
            _event.element = e.element;
            _event.event_number = e.event_number;
            _event.bind_id = e.bind_id;
        }

        public WebUIEvent(UIntPtr window, UIntPtr event_type, IntPtr element, UIntPtr event_number, UIntPtr bind_id)
        {
            _event.window = window;
            _event.event_type = event_type;
            _event.element = element;
            _event.event_number = event_number;
            _event.bind_id = bind_id;
        }

        /// <summary>
        /// Get the first argument as integer.
        /// </summary>
        /// <returns>Returns argument as integer.</returns>
        public long GetInt()
        {
            if (Initialized)
            {
                return webui_get_int(ref _event);
            }
            else
            { 
                return 0;
            }
        }

        /// <summary>
        /// Get an argument as integer at a specific index.
        /// </summary>
        /// <param name="index">The argument position starting from 0.</param>
        /// <returns>Returns argument as integer.</returns>
        public long GetIntAt(UIntPtr index)
        {
            if (Initialized)
            {
                return webui_get_int_at(ref _event, index);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the first argument as string.
        /// </summary>
        /// <returns>Returns argument as string.</returns>
        public string? GetString() 
        {
            if (Initialized)
            {
                return WebUI.WebUIStringToCSharpString(webui_get_string(ref _event));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get an argument as string at a specific index.
        /// </summary>
        /// <param name="index">The argument position starting from 0.</param>
        /// <returns>Returns argument as string.</returns>
        public string? GetStringAt(UIntPtr index)
        {
            if (Initialized)
            {
                return WebUI.WebUIStringToCSharpString(webui_get_string_at(ref _event, index));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the first argument as a stream.
        /// </summary>
        /// <returns>Returns argument as a stream.</returns>
        public MemoryStream? GetStream()
        {
            if (Initialized)
            {
                IntPtr buffer = webui_get_string(ref _event);
                UIntPtr buffersize = webui_get_size(ref _event);
                MemoryStream stream = new MemoryStream((int)buffersize);
                for (UIntPtr i = 0; i < buffersize; i++)
                {
                    stream.WriteByte(Marshal.ReadByte(buffer, (int)i));
                }
                stream.Position = 0;
                return stream;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the first argument as a stream at a specific index.
        /// </summary>
        /// <param name="index">The argument position starting from 0.</param>
        /// <returns>Returns argument as a stream.</returns>
        public MemoryStream? GetStreamAt(UIntPtr index)
        {
            if (Initialized)
            {
                IntPtr buffer = webui_get_string_at(ref _event, index);
                UIntPtr buffersize = webui_get_size_at(ref _event, index);
                MemoryStream stream = new MemoryStream((int)buffersize);
                for (UIntPtr i = 0; i < buffersize; i++)
                {
                    stream.WriteByte(Marshal.ReadByte(buffer, (int)i));
                }
                stream.Position = 0;
                return stream;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the first argument as boolean.
        /// </summary>
        /// <returns>Returns argument as boolean.</returns>
        public bool GetBool()
        {
            return Initialized && webui_get_bool(ref _event);
        }

        /// <summary>
        /// Get an argument as boolean at a specific index.
        /// </summary>
        /// <param name="index">The argument position starting from 0.</param>
        /// <returns>Returns argument as boolean.</returns>
        public bool GetBoolAt(UIntPtr index)
        {
            return Initialized && webui_get_bool_at(ref _event, index);
        }

        /// <summary>
        /// Get size in bytes of the first argument.
        /// </summary>
        /// <returns>Returns size in bytes.</returns>
        public UIntPtr GetSize()
        {
            if (Initialized)
            {
                return webui_get_size(ref _event);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the size in bytes of an argument at a specific index.
        /// </summary>
        /// <param name="index">The argument position starting from 0.</param>
        /// <returns>Returns size in bytes.</returns>
        public UIntPtr GetSizeAt(UIntPtr index)
        {
            if (Initialized)
            {
                return webui_get_size_at(ref _event, index);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Return the response to JavaScript as integer.
        /// </summary>
        /// <param name="value">The integer to be send to JavaScript.</param>
        public void ReturnInt(long value)
        {
            if (Initialized)
            {
                webui_return_int(ref _event, value);
            }
        }

        /// <summary>
        /// Return the response to JavaScript as string.
        /// </summary>
        /// <param name="value">The string to be send to JavaScript.</param>
        public void ReturnString(string value)
        {
            if (Initialized)
            {
                webui_return_string(ref _event, value);
            }
        }

        /// <summary>
        /// Return the response to JavaScript as a stream.
        /// </summary>
        /// <param name="value">The stream to be send to JavaScript.</param>
        public void ReturnStream(MemoryStream value)
        {
            if (Initialized)
            {
                byte[] bytes = value.ToArray();
                webui_return_buffer(ref _event, ref bytes);
            }
        }

        /// <summary>
        /// Return the response to JavaScript as boolean.
        /// </summary>
        /// <param name="value">The boolean to be send to JavaScript.</param>
        public void ReturnBool(bool value)
        {
            if (Initialized)
            {
                webui_return_bool(ref _event, value);
            }
        }

        /// <summary>
        /// When using `webui_interface_bind()`, you may need this function to easily set a response.
        /// </summary>
        /// <param name="response">The response as string to be send to JavaScript.</param>
        public void SetResponse(string response)
        {
            if (Initialized)
            {
                webui_interface_set_response(_event.window, _event.event_number, response);
            }
        }

        /// <summary>
        /// When using `webui_interface_bind()`, you may need this function to easily set a response.
        /// </summary>
        /// <param name="response">The response as a stream to be send to JavaScript.</param>
        public void SetResponse(MemoryStream response)
        {
            if (Initialized)
            {
                byte[] buffer = response.ToArray();
                webui_interface_set_buffer_response(_event.window, _event.event_number, ref buffer);
            }
        }
    }
}
