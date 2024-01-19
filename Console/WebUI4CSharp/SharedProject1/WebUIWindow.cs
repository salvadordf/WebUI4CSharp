using System.Runtime.InteropServices;

namespace WebUI4CSharp
{
    public class WebUIWindow
    {
        private UIntPtr _id = 0;

        /**
         * @brief Create a new WebUI window object.
         *
         * @return Returns the window number.
         *
         * @example size_t myWindow = webui_new_window();
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_new_window();

        /**
         * @brief Create a new webui window object using a specified window number.
         *
         * @param window_number The window number (should be > 0, and < WEBUI_MAX_IDS)
         *
         * @return Returns the window number.
         *
         * @example size_t myWindow = webui_new_window_id(123);
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_new_window_id(UIntPtr window_number);

        /**
         * @brief Get a free window number that can be used with
         * `webui_new_window_id()`.
         *
         * @return Returns the first available free window number. Starting from 1.
         *
         * @example size_t myWindowNumber = webui_get_new_window_id();
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_get_new_window_id();

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
        [DllImport("webui-2")]
        private static extern UIntPtr webui_bind(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string element, BindCallback func);

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
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_show(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string content);

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
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_show_browser(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string content, UIntPtr browser);

        /**
         * @brief Set the window in Kiosk mode (Full screen)
         *
         * @param window The window number
         * @param status True or False
         *
         * @example webui_set_kiosk(myWindow, true);
         */
        [DllImport("webui-2")]
        private static extern void webui_set_kiosk(UIntPtr window, [MarshalAs(UnmanagedType.I1)] bool status);

        /**
         * @brief Close a specific window only. The window object will still exist.
         *
         * @param window The window number
         *
         * @example webui_close(myWindow);
         */
        [DllImport("webui-2")]
        private static extern void webui_close(UIntPtr window);

        /**
         * @brief Close a specific window and free all memory resources.
         *
         * @param window The window number
         *
         * @example webui_destroy(myWindow);
         */
        [DllImport("webui-2")]
        private static extern void webui_destroy(UIntPtr window);

        /**
         * @brief Set the web-server root folder path for a specific window.
         *
         * @param window The window number
         * @param path The local folder full path
         *
         * @example webui_set_root_folder(myWindow, "/home/Foo/Bar/");
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_set_root_folder(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

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
        [DllImport("webui-2")]
        private static extern void webui_set_file_handler(UIntPtr window, FileHandlerCallback handler);

        /**
         * @brief Check if the specified window is still running.
         *
         * @param window The window number
         *
         * @example webui_is_shown(myWindow);
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_is_shown(UIntPtr window);

        /**
         * @brief Set the default embedded HTML favicon.
         *
         * @param window The window number
         * @param icon The icon as string: `<svg>...</svg>`
         * @param icon_type The icon type: `image/svg+xml`
         *
         * @example webui_set_icon(myWindow, "<svg>...</svg>", "image/svg+xml");
         */
        [DllImport("webui-2")]
        private static extern void webui_set_icon(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string icon, [MarshalAs(UnmanagedType.LPUTF8Str)] string icon_type);

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
        [DllImport("webui-2")]
        private static extern void webui_send_raw(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string function, UIntPtr raw, UIntPtr size);

        /**
         * @brief Set a window in hidden mode. Should be called before `webui_show()`.
         *
         * @param window The window number
         * @param status The status: True or False
         *
         * @example webui_set_hide(myWindow, True);
         */
        [DllImport("webui-2")]
        private static extern void webui_set_hide(UIntPtr window, [MarshalAs(UnmanagedType.I1)] bool status);

        /**
         * @brief Set the window size.
         *
         * @param window The window number
         * @param width The window width
         * @param height The window height
         *
         * @example webui_set_size(myWindow, 800, 600);
         */
        [DllImport("webui-2")]
        private static extern void webui_set_size(UIntPtr window, uint width, uint height);

        /**
         * @brief Set the window position.
         *
         * @param window The window number
         * @param x The window X
         * @param y The window Y
         *
         * @example webui_set_position(myWindow, 100, 100);
         */
        [DllImport("webui-2")]
        private static extern void webui_set_position(UIntPtr window, uint x, uint y);

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
        [DllImport("webui-2")]
        private static extern void webui_set_profile(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

        /**
         * @brief Set the web browser proxy_server to use. Need to be called before `webui_show()`.
         *
         * @param window The window number
         * @param proxy_server The web browser proxy_server
         *
         * @example webui_set_proxy(myWindow, "http://127.0.0.1:8888"); 
         */
        [DllImport("webui-2")]
        private static extern void webui_set_proxy(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string proxy_server);

        /**
         * @brief Get the full current URL.
         *
         * @param window The window number
         *
         * @return Returns the full URL string
         *
         * @example const char* url = webui_get_url(myWindow);
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        private static extern string webui_get_url(UIntPtr window);

        /**
         * @brief Allow a specific window address to be accessible from a public network
         *
         * @param window The window number
         * @param status True or False
         *
         * @example webui_set_public(myWindow, true);
         */
        [DllImport("webui-2")]
        private static extern void webui_set_public(UIntPtr window, [MarshalAs(UnmanagedType.I1)] bool status);

        /**
         * @brief Navigate to a specific URL
         *
         * @param window The window number
         * @param url Full HTTP URL
         *
         * @example webui_navigate(myWindow, "http://domain.com");
         */
        [DllImport("webui-2")]
        private static extern void webui_navigate(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string url);

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
        [DllImport("webui-2")]
        private static extern void webui_delete_profile(UIntPtr window);

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
        [DllImport("webui-2")]
        private static extern UIntPtr webui_get_parent_process_id(UIntPtr window);

        /**
         * @brief Get the ID of the last child process.
         *
         * @param window The window number
         *
         * @return Returns the the child process id as integer
         *
         * @example size_t id = webui_get_child_process_id(myWindow);
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_get_child_process_id(UIntPtr window);

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
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_set_port(UIntPtr window, UIntPtr port);


        /**
         * @brief Run JavaScript without waiting for the response.
         *
         * @param window The window number
         * @param script The JavaScript to be run
         *
         * @example webui_run(myWindow, "alert('Hello');");
         */
        [DllImport("webui-2")]
        private static extern void webui_run(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string script);

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
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_script(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string script, UIntPtr timeout, UIntPtr buffer, UIntPtr buffer_length);

        /**
         * @brief Chose between Deno and Nodejs as runtime for .js and .ts files.
         *
         * @param window The window number
         * @param runtime Deno | Nodejs
         *
         * @example webui_set_runtime(myWindow, Deno);
         */
        [DllImport("webui-2")]
        private static extern void webui_set_runtime(UIntPtr window, UIntPtr runtime);

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
        [DllImport("webui-2")]
        private static extern UIntPtr webui_interface_bind(UIntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string element, InterfaceEventCallback func);


        /**
         * @brief Get a unique window ID.
         *
         * @param window The window number
         *
         * @return Returns the unique window ID as integer
         *
         * @example size_t id = webui_interface_get_window_id(myWindow);
         */
        [DllImport("webui-2")]
        private static extern UIntPtr webui_interface_get_window_id(UIntPtr window);


        /// <summary>
        /// Window number or Window ID.
        /// </summary>
        public UIntPtr Id { get => _id; }

        /// <summary>
        /// Returns true if the Window was created successfully.
        /// </summary>
        public bool Initialized { get { return Id > 0; } }

        /// <summary>
        /// Get the full current URL.
        /// </summary>
        public string Url
        {
            get
            {
                if (Initialized)
                {
                    return webui_get_url(_id);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Get the ID of the parent process (The web browser may re-create another new process).
        /// </summary>
        public UIntPtr ParentProcessId { 
            get 
            {
                if (Initialized)
                {
                    return webui_get_parent_process_id(_id);
                }
                else
                {
                    return 0;
                }
            } 
        }

        /// <summary>
        /// Get the ID of the last child process.
        /// </summary>
        public UIntPtr ChildProcessId
        {
            get
            {
                if (Initialized)
                {
                    return webui_get_child_process_id(_id);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Create a new WebUI window object.
        /// </summary>
        public WebUIWindow()
        {
            _id = webui_new_window();
            WebUI.AddWindow(this);
        }

        /// <summary>
        /// Create a new webui window object using a specified window number.
        /// </summary>
        public WebUIWindow(UIntPtr windowId)
        {
            if ((windowId > 0) && (windowId < WebUI.WEBUI_MAX_IDS))
            {
                _id = webui_new_window_id(windowId);
            }
            else
            {
                _id = webui_new_window();
            }
            WebUI.AddWindow(this);
        }

        /// <summary>
        /// Get a free window number that can be used with `webui_new_window_id()`.
        /// </summary>
        public static UIntPtr GetNewWindowId()
        {
            return webui_get_new_window_id();
        }

        /// <summary>
        /// Show a window using embedded HTML, or a file. If the window is already open, it will be refreshed.
        /// </summary>
        /// <param name="content">The HTML, URL, Or a local file.</param>
        /// <returns>Returns True if showing the window is successed.</returns>
        public bool Show(string content)
        {
            return Initialized && webui_show(_id, content);
        }

        /// <summary>
        /// Same as `webui_show()`. But using a specific web browser.
        /// </summary>
        /// <param name="content">The HTML, URL, Or a local file.</param>
        /// <param name="browser">The web browser to be used.</param>
        /// <returns>Returns True if showing the window is successed.</returns>
        public bool ShowBrowser(string content, UIntPtr browser)
        {
            return Initialized && webui_show_browser(_id, content, browser);
        }

        /// <summary>
        /// Bind a specific html element click event with a callback function. Empty element means all events.
        /// </summary>
        /// <param name="element">The HTML element ID.</param>
        /// <param name="func">The callback function.</param>
        /// <returns>Returns a unique bind ID.</returns>
        /// <remarks>
        /// <para>The callback function will always be executed in a background thread!</para>
        /// </remarks>
        public UIntPtr Bind(string element, BindCallback func)
        {
            if (Initialized)
            {
                return webui_bind(_id, element, func);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Bind a specific HTML element click event with a callback function. Empty element means all events.
        /// </summary>
        /// <param name="element">The HTML element ID.</param>
        /// <param name="func">The callback as myFunc(Window, EventType, Element, EventNumber, BindID).</param>
        /// <returns>Returns unique bind ID.</returns>
        public UIntPtr Bind(string element, InterfaceEventCallback func)
        {
            if (Initialized)
            {
                return webui_interface_bind(_id, element, func);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Set the window in Kiosk mode (Full screen).
        /// </summary>
        /// <param name="status">True or False.</param>
        public void SetKiosk(bool status)
        {
            if (Initialized)
            {
                webui_set_kiosk(_id, status);
            }
        }

        /// <summary>
        /// Close a specific window only. The window object will still exist.
        /// </summary>
        public void Close()
        {
            if (Initialized)
            {
                webui_close(_id);
            }
        }

        /// <summary>
        /// Close the window and free all memory resources.
        /// </summary>
        public void Destroy() {
            if (Initialized)
            {
                webui_destroy(_id);
                _id = 0;
            }
        }

        /// <summary>
        /// Set the web-server root folder path for a specific window.
        /// </summary>
        /// <param name="path">The local folder full path.</param>
        public bool SetRootFolder(string path)
        {
            return Initialized && webui_set_root_folder(_id, path);
        }

        /// <summary>
        /// Set a custom handler to serve files.
        /// </summary>
        /// <param name="handler">The handler function: `void myHandler(const char* filename, * int* length)`.</param>
        public void SetFileHandler(FileHandlerCallback handler)
        {
            if (Initialized)
            {
                webui_set_file_handler(_id, handler);
            }
        }

        /// <summary>
        /// Check if the specified window is still running.
        /// </summary>
        public bool IsShown()
        {
            return Initialized && webui_is_shown(_id);
        }

        /// <summary>
        /// Set the default embedded HTML favicon.
        /// </summary>
        /// <param name="icon">The icon as string: `<svg>...</svg>`.</param>
        /// <param name="icon_type">The icon type: `image/svg+xml`.</param>
        public void SetIcon(string icon, string icon_type)
        {
            if (Initialized)
            {
                webui_set_icon(_id, icon, icon_type);
            }
        }

        /// <summary>
        /// Safely send raw data to the UI.
        /// </summary>
        /// <param name="function">The JavaScript function to receive raw data: `function * myFunc(myData){}`.</param>
        /// <param name="raw">The raw data buffer.</param>
        /// <param name="size">The raw data size in bytes.</param>
        public void SendRaw(string function, UIntPtr raw, UIntPtr size)
        {
            if (Initialized)
            {
                webui_send_raw(_id, function, raw, size);
            }
        }

        /// <summary>
        /// Set a window in hidden mode. Should be called before `webui_show()`.
        /// </summary>
        /// <param name="status">The status: True or False.</param>
        public void SetHide(bool status)
        {
            if (Initialized)
            {
                webui_set_hide(_id, status);
            }
        }

        /// <summary>
        /// Set the window size.
        /// </summary>
        /// <param name="width">The window width.</param>
        /// <param name="height">The window height.</param>
        public void SetSize(uint width, uint height) 
        { 
            if (Initialized) 
            { 
                webui_set_size(_id, width, height);
            } 
        }

        /// <summary>
        /// Set the window position.
        /// </summary>
        /// <param name="x">The window X.</param>
        /// <param name="y">The window Y.</param>
        public void SetPosition(uint x, uint y)
        {
            if (Initialized)
            {
                webui_set_position(_id, x, y);
            }
        }

        /// <summary>
        /// Set the web browser profile to use. An empty `name` and `path` means the default user profile. Need to be called before `webui_show()`.
        /// </summary>
        /// <param name="name">The web browser profile name.</param>
        /// <param name="path">The web browser profile full path.</param>
        public void SetProfile(string name, string path)
        {
            if (Initialized)
            {
                webui_set_profile(_id, name, path);
            }
        }

        /// <summary>
        /// Set the web browser proxy_server to use. Need to be called before 'webui_show()'.
        /// </summary>
        /// <param name="proxy_server">The web browser proxy_server. For example 'http://127.0.0.1:8888'</param>
        public void SetProxy(string proxy_server)
        {
            if (Initialized)
            {
                webui_set_proxy(_id, proxy_server);
            }
        }

        /// <summary>
        /// Allow a specific window address to be accessible from a public network.
        /// </summary>
        /// <param name="status">True or False.</param>
        public void SetPublic(bool status)
        {
            if (Initialized)
            {
                webui_set_public(_id, status);
            }
        }

        /// <summary>
        /// Navigate to a specific URL.
        /// </summary>
        /// <param name="url">Full HTTP URL.</param>
        public void Navigate(string url)
        {
            if (Initialized)
            {
                webui_navigate(_id, url);
            }
        }

        /// <summary>
        /// Delete a specific window web-browser local folder profile.
        /// </summary>
        public void DeleteProfile()
        {
            if (Initialized)
            {
                webui_delete_profile(_id);
            }
        }

        /// <summary>
        /// Set a custom web-server network port to be used by WebUI.
        /// This can be useful to determine the HTTP link of `webui.js` in case
        /// you are trying to use WebUI with an external web-server like NGNIX
        /// </summary>
        /// <param name="port">The web-server network port WebUI should use.</param>
        /// <returns>Returns True if the port is free and usable by WebUI.</returns>
        public bool SetPort(UIntPtr port)
        {
            return Initialized && webui_set_port(_id, port);
        }

        /// <summary>
        /// Run JavaScript without waiting for the response.
        /// </summary>
        /// <param name="script_">The JavaScript to be run.</param>
        public void Run(string script_)
        {
            if (Initialized)
            {
                webui_run(_id, script_);
            }
        }

        /// <summary>
        /// Run JavaScript and get the response back.
        /// Make sure your local buffer can hold the response.
        /// </summary>
        /// <param name="script_">The JavaScript to be run.</param>
        /// <param name="timeout">The execution timeout.</param>
        /// <param name="buffer">The local buffer to hold the response.</param>
        /// <param name="buffer_length">The local buffer size.</param>
        /// <returns>Returns True if there is no execution error.</returns>
        public bool Script(string script_, UIntPtr timeout, UIntPtr buffer, UIntPtr buffer_length)
        {
            return Initialized && webui_script(_id, script_, timeout, buffer, buffer_length);
        }

        /// <summary>
        /// Chose between Deno and Nodejs as runtime for .js and .ts files.
        /// </summary>
        /// <param name="runtime">Deno or Nodejs.</param>
        public void SetRuntime(UIntPtr runtime)
        {
            if (Initialized)
            {
                webui_set_runtime(_id, runtime);
            }
        }


    }
}
