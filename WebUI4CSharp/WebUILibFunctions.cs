using System.Runtime.InteropServices;

namespace WebUI4CSharp
{
    public static class WebUILibFunctions
    {
#if WEBUIDEMO
        // The demos define the WEBUIDEMO conditional for convenience
        // Enable the right library depending on the selected platform target in each demo
        private const string _LibName = "..\\..\\..\\..\\..\\WebUI_binaries\\64bits\\webui-2.dll";  // Any CPU
        //private const string _LibName = "..\\..\\..\\..\\..\\..\\WebUI_binaries\\64bits\\webui-2.dll"; // x64 
        //private const string _LibName = "..\\..\\..\\..\\..\\..\\WebUI_binaries\\64bits\\webui-2_debug.dll"; // x64 (verbose logging) 
        //private const string _LibName = "..\\..\\..\\..\\..\\..\\WebUI_binaries\\32bits\\webui-2.dll"; // x32 
        //private const string _LibName = "..\\..\\..\\..\\..\\..\\WebUI_binaries\\32bits\\webui-2_debug.dll"; // x32 (verbose logging)
#else
        private const string _LibName = "webui-2";    
#endif

        /**
         * @brief Create a new WebUI window object.
         *
         * @return Returns the window number.
         *
         * @example size_t myWindow = webui_new_window();
         */
        [DllImport(_LibName, CallingConvention=CallingConvention.Cdecl)]
        public static extern UIntPtr webui_new_window();

        /**
         * @brief Create a new webui window object using a specified window number.
         *
         * @param window_number The window number (should be > 0, and < WEBUI_MAX_IDS)
         *
         * @return Returns the window number.
         *
         * @example size_t myWindow = webui_new_window_id(123);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_new_window_id(UIntPtr window_number);

        /**
         * @brief Get a free window number that can be used with
         * `webui_new_window_id()`.
         *
         * @return Returns the first available free window number. Starting from 1.
         *
         * @example size_t myWindowNumber = webui_get_new_window_id();
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_get_new_window_id();

        /**
         * @brief Bind a specific html element click event with a function. Empty
         * element means all events.
         *
         * @param window The window number
         * @param element The HTML ID
         * @param func The callback function
         *
         * @return Returns a unique bind ID.
         *
         * @example webui_bind(myWindow, "myID", myFunction);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_bind(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string element, BindCallback func);

        /**
         * @brief Show a window using embedded HTML, or a file. If the window is already
         * open, it will be refreshed.
         *
         * @param window The window number
         * @param content The HTML, URL, Or a local file
         *
         * @return Returns True if showing the window is successed.
         *
         * @example webui_show(myWindow, "<html>...</html>"); | webui_show(myWindow,
         * "index.html"); | webui_show(myWindow, "http://...");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_show(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string content);

        /**
         * @brief Same as `webui_show()`. But using a specific web browser.
         *
         * @param window The window number
         * @param content The HTML, Or a local file
         * @param browser The web browser to be used
         *
         * @return Returns True if showing the window is successed.
         *
         * @example webui_show_browser(myWindow, "<html>...</html>", Chrome); |
         * webui_show(myWindow, "index.html", Firefox);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_show_browser(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string content, UIntPtr browser);

        /**
         * @brief Set the window in Kiosk mode (Full screen)
         *
         * @param window The window number
         * @param status True or False
         *
         * @example webui_set_kiosk(myWindow, true);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_kiosk(UIntPtr window, [MarshalAs(UnmanagedType.I1)] bool status);

        /**
         * @brief Wait until all opened windows get closed.
         *
         * @example webui_wait();
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_wait();

        /**
         * @brief Close a specific window only. The window object will still exist.
         *
         * @param window The window number
         *
         * @example webui_close(myWindow);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_close(UIntPtr window);

        /**
         * @brief Close a specific window and free all memory resources.
         *
         * @param window The window number
         *
         * @example webui_destroy(myWindow);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_destroy(UIntPtr window);

        /**
         * @brief Close all open windows. `webui_wait()` will return (Break).
         *
         * @example webui_exit();
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_exit();

        /**
         * @brief Set the web-server root folder path for a specific window.
         *
         * @param window The window number
         * @param path The local folder full path
         *
         * @example webui_set_root_folder(myWindow, "/home/Foo/Bar/");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_set_root_folder(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

        /**
         * @brief Set the web-server root folder path for all windows. Should be used
         * before `webui_show()`.
         *
         * @param path The local folder full path
         *
         * @example webui_set_default_root_folder("/home/Foo/Bar/");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_set_default_root_folder([MarshalAs(UnmanagedType.LPUTF8Str)] string path);

        /**
         * @brief Set a custom handler to serve files.
         *
         * @param window The window number
         * @param handler The handler function: `void myHandler(const char* filename,
         * int* length)`
         *
         * @return Returns a unique bind ID.
         *
         * @example webui_set_file_handler(myWindow, myHandlerFunction);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_file_handler(UIntPtr window, FileHandlerCallback handler);

        /**
         * @brief Check if the specified window is still running.
         *
         * @param window The window number
         *
         * @example webui_is_shown(myWindow);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_is_shown(UIntPtr window);

        /**
         * @brief Set the maximum time in seconds to wait for the browser to start.
         *
         * @param second The timeout in seconds
         *
         * @example webui_set_timeout(30);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_timeout(UIntPtr second);

        /**
         * @brief Set the default embedded HTML favicon.
         *
         * @param window The window number
         * @param icon The icon as string: `<svg>...</svg>`
         * @param icon_type The icon type: `image/svg+xml`
         *
         * @example webui_set_icon(myWindow, "<svg>...</svg>", "image/svg+xml");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_icon(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string icon, [MarshalAs(UnmanagedType.LPUTF8Str)] string icon_type);

        /**
         * @brief Base64 encoding. Use this to safely send text based data to the UI. If
         * it fails it will return NULL.
         *
         * @param str The string to encode (Should be null terminated)
         *
         * @example webui_encode("Hello");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        public static extern string webui_encode([MarshalAs(UnmanagedType.LPUTF8Str)] string str);

        /**
         * @brief Base64 decoding. Use this to safely decode received Base64 text from
         * the UI. If it fails it will return NULL.
         *
         * @param str The string to decode (Should be null terminated)
         *
         * @example webui_decode("SGVsbG8=");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        public static extern string webui_decode([MarshalAs(UnmanagedType.LPUTF8Str)] string str);

        /**
         * @brief Safely free a buffer allocated by WebUI using `webui_malloc()`.
         *
         * @param ptr The buffer to be freed
         *
         * @example webui_free(myBuffer);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_free(IntPtr ptr);

        /**
         * @brief Safely allocate memory using the WebUI memory management system. It
         * can be safely freed using `webui_free()` at any time.
         *
         * @param size The size of memory in bytes
         *
         * @example char* myBuffer = (char*)webui_malloc(1024);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr webui_malloc(UIntPtr size);

        /**
         * @brief Safely send raw data to the UI.
         *
         * @param window The window number
         * @param function The JavaScript function to receive raw data: `function
         * myFunc(myData){}`
         * @param raw The raw data buffer
         * @param size The raw data size in bytes
         *
         * @example webui_send_raw(myWindow, "myJavascriptFunction", myBuffer, 64);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_send_raw(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string function, UIntPtr raw, UIntPtr size);

        /**
         * @brief Set a window in hidden mode. Should be called before `webui_show()`.
         *
         * @param window The window number
         * @param status The status: True or False
         *
         * @example webui_set_hide(myWindow, True);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_hide(UIntPtr window, [MarshalAs(UnmanagedType.I1)] bool status);

        /**
         * @brief Set the window size.
         *
         * @param window The window number
         * @param width The window width
         * @param height The window height
         *
         * @example webui_set_size(myWindow, 800, 600);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_size(UIntPtr window, uint width, uint height);

        /**
         * @brief Set the window position.
         *
         * @param window The window number
         * @param x The window X
         * @param y The window Y
         *
         * @example webui_set_position(myWindow, 100, 100);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_position(UIntPtr window, uint x, uint y);

        /**
         * @brief Set the web browser profile to use. An empty `name` and `path` means
         * the default user profile. Need to be called before `webui_show()`.
         *
         * @param window The window number
         * @param name The web browser profile name
         * @param path The web browser profile full path
         *
         * @example webui_set_profile(myWindow, "Bar", "/Home/Foo/Bar"); |
         * webui_set_profile(myWindow, "", "");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_profile(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

        /**
         * @brief Set the web browser proxy_server to use. Need to be called before `webui_show()`.
         *
         * @param window The window number
         * @param proxy_server The web browser proxy_server
         *
         * @example webui_set_proxy(myWindow, "http://127.0.0.1:8888"); 
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_proxy(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string proxy_server);

        /**
         * @brief Get the full current URL.
         *
         * @param window The window number
         *
         * @return Returns the full URL string
         *
         * @example const char* url = webui_get_url(myWindow);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        public static extern string webui_get_url(UIntPtr window);

        /**
         * @brief Allow a specific window address to be accessible from a public network
         *
         * @param window The window number
         * @param status True or False
         *
         * @example webui_set_public(myWindow, true);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_public(UIntPtr window, [MarshalAs(UnmanagedType.I1)] bool status);

        /**
         * @brief Navigate to a specific URL
         *
         * @param window The window number
         * @param url Full HTTP URL
         *
         * @example webui_navigate(myWindow, "http://domain.com");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_navigate(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string url);

        /**
         * @brief Free all memory resources. Should be called only at the end.
         *
         * @example
         * webui_wait();
         * webui_clean();
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_clean();

        /**
         * @brief Delete all local web-browser profiles folder. It should called at the
         * end.
         *
         * @example
         * webui_wait();
         * webui_delete_all_profiles();
         * webui_clean();
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_delete_all_profiles();

        /**
         * @brief Delete a specific window web-browser local folder profile.
         *
         * @param window The window number
         *
         * @example
         * webui_wait();
         * webui_delete_profile(myWindow);
         * webui_clean();
         *
         * @note This can break functionality of other windows if using the same
         * web-browser.
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_delete_profile(UIntPtr window);

        /**
         * @brief Get the ID of the parent process (The web browser may re-create
         * another new process).
         *
         * @param window The window number
         *
         * @return Returns the the parent process id as integer
         *
         * @example size_t id = webui_get_parent_process_id(myWindow);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_get_parent_process_id(UIntPtr window);

        /**
         * @brief Get the ID of the last child process.
         *
         * @param window The window number
         *
         * @return Returns the the child process id as integer
         *
         * @example size_t id = webui_get_child_process_id(myWindow);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_get_child_process_id(UIntPtr window);

        /**
         * @brief Set a custom web-server network port to be used by WebUI.
         * This can be useful to determine the HTTP link of `webui.js` in case
         * you are trying to use WebUI with an external web-server like NGNIX
         *
         * @param window The window number
         * @param port The web-server network port WebUI should use
         *
         * @return Returns True if the port is free and usable by WebUI
         *
         * @example bool ret = webui_set_port(myWindow, 8080);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_set_port(UIntPtr window, UIntPtr port);

        /**
         * @brief Set the SSL/TLS certificate and the private key content, both in PEM
         * format. This works only with `webui-2-secure` library. If set empty WebUI
         * will generate a self-signed certificate.
         *
         * @param certificate_pem The SSL/TLS certificate content in PEM format
         * @param private_key_pem The private key content in PEM format
         *
         * @return Returns True if the certificate and the key are valid.
         *
         * @example bool ret = webui_set_tls_certificate("-----BEGIN
         * CERTIFICATE-----\n...", "-----BEGIN PRIVATE KEY-----\n...");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_set_tls_certificate([MarshalAs(UnmanagedType.LPUTF8Str)] string certificate_pem, [MarshalAs(UnmanagedType.LPUTF8Str)] string private_key_pem);

        /**
         * @brief Run JavaScript without waiting for the response.
         *
         * @param window The window number
         * @param script The JavaScript to be run
         *
         * @example webui_run(myWindow, "alert('Hello');");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_run(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string script);

        /**
         * @brief Run JavaScript and get the response back.
         * Make sure your local buffer can hold the response.
         *
         * @param window The window number
         * @param script The JavaScript to be run
         * @param timeout The execution timeout
         * @param buffer The local buffer to hold the response
         * @param buffer_length The local buffer size
         *
         * @return Returns True if there is no execution error
         *
         * @example bool err = webui_script(myWindow, "return 4 + 6;", 0, myBuffer, myBufferSize);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_script(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string script, UIntPtr timeout, IntPtr buffer, UIntPtr buffer_length);

        /**
         * @brief Chose between Deno and Nodejs as runtime for .js and .ts files.
         *
         * @param window The window number
         * @param runtime Deno | Nodejs
         *
         * @example webui_set_runtime(myWindow, Deno);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_set_runtime(UIntPtr window, UIntPtr runtime);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long webui_get_int_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get the first argument as integer
         *
         * @param e The event struct
         *
         * @return Returns argument as integer
         *
         * @example long long int myNum = webui_get_int(e);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long webui_get_int(ref webui_event_t e);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr webui_get_string_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get the first argument as string
         *
         * @param e The event struct
         *
         * @return Returns argument as string
         *
         * @example const char* myStr = webui_get_string(e);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr webui_get_string(ref webui_event_t e);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_get_bool_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get the first argument as boolean
         *
         * @param e The event struct
         *
         * @return Returns argument as boolean
         *
         * @example bool myBool = webui_get_bool(e);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_get_bool(ref webui_event_t e);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_get_size_at(ref webui_event_t e, UIntPtr index);

        /**
         * @brief Get size in bytes of the first argument
         *
         * @param e The event struct
         *
         * @return Returns size in bytes
         *
         * @example size_t argLen = webui_get_size(e);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_get_size(ref webui_event_t e);

        /**
         * @brief Return the response to JavaScript as integer.
         *
         * @param e The event struct
         * @param n The integer to be send to JavaScript
         *
         * @example webui_return_int(e, 123);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_return_int(ref webui_event_t e, long n);

        /**
         * @brief Return the response to JavaScript as string.
         *
         * @param e The event struct
         * @param s The string to be send to JavaScript
         *
         * @example webui_return_string(e, "Response...");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_return_string(ref webui_event_t e, [MarshalAs(UnmanagedType.LPUTF8Str)] string s);

        /**
         * @brief Return the response to JavaScript as a buffer.
         *
         * @param e The event struct
         * @param buffer The buffer to be send to JavaScript
         *
         * @example webui_return_string(e, "Response...");
         */
        [DllImport("webui-2", EntryPoint = "webui_return_string")]
        public static extern void webui_return_buffer(ref webui_event_t e, ref byte[] buffer);

        /**
         * @brief Return the response to JavaScript as boolean.
         *
         * @param e The event struct
         * @param n The boolean to be send to JavaScript
         *
         * @example webui_return_bool(e, true);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_return_bool(ref webui_event_t e, [MarshalAs(UnmanagedType.I1)] bool b);

        /**
         * @brief Bind a specific HTML element click event with a function. Empty element means all events.
         *
         * @param window The window number
         * @param element The element ID
         * @param func The callback as myFunc(Window, EventType, Element, EventNumber, BindID)
         *
         * @return Returns unique bind ID
         *
         * @example size_t id = webui_interface_bind(myWindow, "myID", myCallback);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_interface_bind(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string element, InterfaceEventCallback func);

        /**
         * @brief When using `webui_interface_bind()`, you may need this function to easily set a response.
         *
         * @param window The window number
         * @param event_number The event number
         * @param response The response as string to be send to JavaScript
         *
         * @example webui_interface_set_response(myWindow, e->event_number, "Response...");
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void webui_interface_set_response(UIntPtr window, UIntPtr event_number, [MarshalAs(UnmanagedType.LPUTF8Str)] string response);

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
        public static extern void webui_interface_set_buffer_response(UIntPtr window, UIntPtr event_number, ref byte[] buffer);

        /**
         * @brief Check if the app still running.
         *
         * @return Returns True if app is running
         *
         * @example bool status = webui_interface_is_app_running();
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_interface_is_app_running();

        /**
         * @brief Get a unique window ID.
         *
         * @param window The window number
         *
         * @return Returns the unique window ID as integer
         *
         * @example size_t id = webui_interface_get_window_id(myWindow);
         */
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_interface_get_window_id(UIntPtr window);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr webui_interface_get_string_at(UIntPtr window, UIntPtr event_number, UIntPtr index);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long webui_interface_get_int_at(UIntPtr window, UIntPtr event_number, UIntPtr index);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool webui_interface_get_bool_at(UIntPtr window, UIntPtr event_number, UIntPtr index);

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
        [DllImport(_LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr webui_interface_get_size_at(UIntPtr window, UIntPtr event_number, UIntPtr index);
    }
}
