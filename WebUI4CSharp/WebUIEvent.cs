using System.IO;
using System.Runtime.InteropServices;

namespace WebUI4CSharp
{
    /// <summary>
    /// Event wrapper for Event objects in WebUI.
    /// </summary>
    public class WebUIEvent
    {
        private webui_event_t _event;

        public WebUIEvent(webui_event_t e)
        {
            _event.window = e.window;
            _event.event_type = e.event_type;
            _event.element = e.element;
            _event.event_number = e.event_number;
            _event.bind_id = e.bind_id;
            _event.client_id = e.client_id;
            _event.connection_id = e.connection_id;
            _event.cookies = e.cookies;
        }

        public WebUIEvent(UIntPtr window, UIntPtr event_type, IntPtr element, 
            UIntPtr event_number, UIntPtr bind_id, UIntPtr client_id, 
            UIntPtr connection_id, IntPtr cookies)
        {
            _event.window = window;
            _event.event_type = event_type;
            _event.element = element;
            _event.event_number = event_number;
            _event.bind_id = bind_id;
            _event.client_id = client_id;
            _event.connection_id = connection_id;
            _event.cookies = cookies;
        }

        /// <summary>
        /// Returns true if the Window was created successfully.
        /// </summary>
        public bool Initialized { get => _event.window > 0; }

        /// <summary>
        /// WebUI event struct.
        /// </summary>
        public webui_event_t Event { get => _event; }

        /// <summary>
        /// Window wrapper for the Window object of this event.
        /// </summary>
        public WebUIWindow? Window { get => WebUI.SearchWindow(_event.window); } 

        /// <summary>
        /// The window object number or ID.
        /// </summary>
        public UIntPtr WindowID { get => _event.window; }

        /// <summary>
        /// Event type.
        /// </summary>
        public webui_event EventType { get => (webui_event)_event.event_type; }

        /// <summary>
        /// HTML element ID.
        /// </summary>
        public string? Element { get => WebUI.WebUIStringToCSharpString(_event.element); }

        /// <summary>
        /// Event number or Event ID.
        /// </summary>
        public UIntPtr EventID { get => _event.event_number; }

        /// <summary>
        /// Bind ID.
        /// </summary>
        public UIntPtr BindID { get => _event.bind_id; }

        /// <summary>
        /// Client's unique ID.
        /// </summary>
        public UIntPtr ClientID { get => _event.client_id; }

        /// <summary>
        /// Client's connection ID.
        /// </summary>
        public UIntPtr ConnectionID { get => _event.connection_id; }

        /// <summary>
        /// Client's full cookies.
        /// </summary>
        public string? Cookies { get => WebUI.WebUIStringToCSharpString(_event.cookies); }

        /// <summary>
        /// Get the first argument as integer.
        /// </summary>
        /// <returns>Returns argument as integer.</returns>
        public long GetInt()
        {
            if (Initialized)
            {
                return WebUILibFunctions.webui_get_int(ref _event);
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
                return WebUILibFunctions.webui_get_int_at(ref _event, index);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the first argument as float.
        /// </summary>
        /// <returns>Returns argument as float.</returns>
        public double GetFloat()
        {
            if (Initialized)
            {
                return WebUILibFunctions.webui_get_float(ref _event);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get an argument as float at a specific index.
        /// </summary>
        /// <param name="index">The argument position starting from 0.</param>
        /// <returns>Returns argument as float.</returns>
        public double GetFloatAt(UIntPtr index)
        {
            if (Initialized)
            {
                return WebUILibFunctions.webui_get_float_at(ref _event, index);
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
                return WebUI.WebUIStringToCSharpString(WebUILibFunctions.webui_get_string(ref _event));
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
                return WebUI.WebUIStringToCSharpString(WebUILibFunctions.webui_get_string_at(ref _event, index));
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
                IntPtr buffer = WebUILibFunctions.webui_get_string(ref _event);
                UIntPtr bufferSize = WebUILibFunctions.webui_get_size(ref _event);
                MemoryStream stream = new MemoryStream((int)bufferSize);
                for (UIntPtr i = 0; i < bufferSize; i++)
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
                IntPtr buffer = WebUILibFunctions.webui_get_string_at(ref _event, index);
                UIntPtr bufferSize = WebUILibFunctions.webui_get_size_at(ref _event, index);
                MemoryStream stream = new MemoryStream((int)bufferSize);
                for (UIntPtr i = 0; i < bufferSize; i++)
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
            return Initialized && WebUILibFunctions.webui_get_bool(ref _event);
        }

        /// <summary>
        /// Get an argument as boolean at a specific index.
        /// </summary>
        /// <param name="index">The argument position starting from 0.</param>
        /// <returns>Returns argument as boolean.</returns>
        public bool GetBoolAt(UIntPtr index)
        {
            return Initialized && WebUILibFunctions.webui_get_bool_at(ref _event, index);
        }

        /// <summary>
        /// Get size in bytes of the first argument.
        /// </summary>
        /// <returns>Returns size in bytes.</returns>
        public UIntPtr GetSize()
        {
            if (Initialized)
            {
                return WebUILibFunctions.webui_get_size(ref _event);
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
                return WebUILibFunctions.webui_get_size_at(ref _event, index);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get how many arguments there are in an event.
        /// </summary>
        /// <returns>Returns the arguments count.</returns>
        public UIntPtr GetCount()
        {
            if (Initialized)
            {
                return WebUILibFunctions.webui_get_count(ref _event);
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
                WebUILibFunctions.webui_return_int(ref _event, value);
            }
        }

        /// <summary>
        /// Return the response to JavaScript as float.
        /// </summary>
        /// <param name="value">The float number to be send to JavaScript.</param>
        public void ReturnFloat(double value)
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_return_float(ref _event, value);
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
                WebUILibFunctions.webui_return_string(ref _event, value);
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
                WebUILibFunctions.webui_return_buffer(ref _event, ref bytes);
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
                WebUILibFunctions.webui_return_bool(ref _event, value);
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
                WebUILibFunctions.webui_interface_set_response(_event.window, _event.event_number, response);
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
                WebUILibFunctions.webui_interface_set_buffer_response(_event.window, _event.event_number, ref buffer);
            }
        }

        /// <summary>
        /// Show a window using embedded HTML, or a file. If the window is already open, it will be refreshed.Single client.
        /// </summary>
        /// <param name="content">The HTML, URL, Or a local file.</param>
        /// <returns>Returns True if showing the window is successed.</returns>
        public bool ShowClient(string content)
        {
            return Initialized && WebUILibFunctions.webui_show_client(ref _event, content);
        }

        /// <summary>
        /// Close a specific client.
        /// </summary>
        public void CloseClient()
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_close_client(ref _event);
            }
        }

        /// <summary>
        /// Safely send raw data to the UI. Single client.
        /// </summary>
        /// <param name="function">The JavaScript function to receive raw data: `function * myFunc(myData){}`.</param>
        /// <param name="raw">The raw data buffer.</param>
        /// <param name="size">The raw data size in bytes.</param>
        public void SendRawClient(string function, UIntPtr raw, UIntPtr size)
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_send_raw_client(ref _event, function, raw, size);
            }
        }

        /// <summary>
        /// Navigate to a specific URL. Single client.
        /// </summary>
        /// <param name="url">Full HTTP URL.</param>
        public void NavigateClient(string url)
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_navigate_client(ref _event, url);
            }
        }

        /// <summary>
        /// Run JavaScript without waiting for the response. Single client.
        /// </summary>
        /// <param name="script_">The JavaScript to be run.</param>
        public void RunClient(string script_)
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_run_client(ref _event, script_);
            }
        }

        /// <summary>
        /// Run JavaScript and get the response back. Single client. 
        /// Make sure your local buffer can hold the response.
        /// </summary>
        /// <param name="e">The event struct.</param>
        /// <param name="script_">The JavaScript to be run.</param>
        /// <param name="timeout">The execution timeout in seconds.</param>
        /// <param name="response">The response.</param>
        /// <param name="response_length">The response size.</param>
        /// <returns>Returns True if there is no execution error.</returns>
        public bool ScriptClient(string script_, UIntPtr timeout, out string response, UIntPtr response_length)
        {
            if (Initialized)
            {
                IntPtr buffer = WebUI.Malloc(response_length);
                string? tempResponse = null;

                if (WebUILibFunctions.webui_script_client(ref _event, script_, timeout, buffer, response_length))
                {
                    tempResponse = WebUI.WebUIStringToCSharpString(buffer);
                }

                WebUI.Free(buffer);

                if (tempResponse != null)
                {
                    response = tempResponse;
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}
