using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace WebUI4CSharp
{
    /// <summary>
    /// Bind arguments used in OnWebUIEvent
    /// </summary>
    public class BindEventArgs : EventArgs
    {
        public BindEventArgs(WebUIEvent bindEvent)
        {
            BindEvent = bindEvent;
        }

        public WebUIEvent BindEvent { get; }
    }

    /// <summary>
    /// File handler arguments used in OnFileHandlerEvent
    /// </summary>
    public class FileHandlerEventArgs : EventArgs
    {
        public FileHandlerEventArgs(string filename)
        {
            FileName = filename;
            ReturnValue = string.Empty;
        }

        public string FileName { get; }
        public string ReturnValue { get; set; }
    }

    /// <summary>
    /// Window wrapper for Window objects in WebUI.
    /// </summary>
    public class WebUIWindow
    {
        private UIntPtr _id = 0;
        private FileHandlerCallback? _fileHandlerCallback;
        private static List<UIntPtr> _bindIdList = new List<UIntPtr>();
        private Object _lockObj = new Object();

        /// <summary>
        /// Create a new WebUI window object.
        /// </summary>
        public WebUIWindow()
        {
            _id = WebUILibFunctions.webui_new_window();
            WebUI.AddWindow(this);
            _fileHandlerCallback = DoFileHandlerEvent;
        }

        /// <summary>
        /// Create a new webui window object using a specified window number.
        /// </summary>
        public WebUIWindow(UIntPtr windowId)
        {
            if ((windowId > 0) && (windowId < WebUI.WEBUI_MAX_IDS))
            {
                _id = WebUILibFunctions.webui_new_window_id(windowId);
            }
            else
            {
                _id = WebUILibFunctions.webui_new_window();
            }
            WebUI.AddWindow(this);
            _fileHandlerCallback = DoFileHandlerEvent;
        }

        /// <summary>
        /// Window number or Window ID.
        /// </summary>
        public UIntPtr Id { get => _id; }

        /// <summary>
        /// Returns true if the Window was created successfully.
        /// </summary>
        public bool Initialized { get => Id > 0; }

        /// <summary>
        /// Get the full current URL.
        /// </summary>
        public string Url
        {
            get
            {
                if (Initialized)
                {
                    return WebUILibFunctions.webui_get_url(_id);
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
                    return WebUILibFunctions.webui_get_parent_process_id(_id);
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
                    return WebUILibFunctions.webui_get_child_process_id(_id);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Event triggered after binding a webui event.
        /// </summary>
        public event EventHandler<BindEventArgs>? OnWebUIEvent;

        /// <summary>
        /// Event triggered when using a custom file handler.
        /// </summary>
        public event EventHandler<FileHandlerEventArgs>? OnFileHandlerEvent;

        /// <summary>
        /// Add the id to the list of Bind IDs handled by this window.
        /// </summary>
        private void AddBindID(UIntPtr id)
        {
            if (id > 0)
            {
                lock (_lockObj)
                {
                    _bindIdList.Add(id);
                }
            }
        }

        /// <summary>
        /// Returns true if the bind id belongs to this window.
        /// </summary>
        public Boolean HasBindID(UIntPtr id)
        {
            if (id > 0)
            {
                lock (_lockObj)
                {
                    return _bindIdList.IndexOf(id) >= 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Trigger the OnWebUIEvent event.
        /// </summary>
        /// <param name="e"></param>
        public virtual void DoBindEvent(ref webui_event_t e)
        {
            EventHandler<BindEventArgs>? eventHandler = OnWebUIEvent;
            if (eventHandler != null)
            {
                WebUIEvent lEvent = new WebUIEvent(e);
                BindEventArgs lEventArgs = new BindEventArgs(lEvent);
                eventHandler(this, lEventArgs);
            }
        }

        /// <summary>
        /// Trigger the OnFileHandlerEvent event.
        /// </summary>
        /// <param name="filename">Requested file name.</param>
        /// <param name="length">Length of the buffer returned by this function.</param>
        /// <returns>Pointer to the buffer with the requested file contents.</returns>
        public virtual IntPtr DoFileHandlerEvent(IntPtr filename, out int length)
        {
            length = 0;
            EventHandler<FileHandlerEventArgs>? eventHandler = OnFileHandlerEvent;
            if (eventHandler != null)
            {
                string? lFilename = WebUI.WebUIStringToCSharpString(filename);
                if (lFilename != null)
                {
                    FileHandlerEventArgs lEventArgs = new FileHandlerEventArgs(lFilename);
                    eventHandler(this, lEventArgs);
                    IntPtr lReturnValue = WebUI.CSharpStringToWebUIString(lEventArgs.ReturnValue, out length);
                    return lReturnValue;
                }
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// Get a free window number that can be used with `webui_new_window_id()`.
        /// </summary>
        public static UIntPtr GetNewWindowId()
        {
            return WebUILibFunctions.webui_get_new_window_id();
        }

        /// <summary>
        /// Show a window using embedded HTML, or a file. If the window is already open, it will be refreshed.
        /// </summary>
        /// <param name="content">The HTML, URL, Or a local file.</param>
        /// <returns>Returns True if showing the window is successed.</returns>
        public bool Show(string content)
        {
            return Initialized && WebUILibFunctions.webui_show(_id, content);
        }

        /// <summary>
        /// Same as `webui_show()`. But using a specific web browser.
        /// </summary>
        /// <param name="content">The HTML, URL, Or a local file.</param>
        /// <param name="browser">The web browser to be used.</param>
        /// <returns>Returns True if showing the window is successed.</returns>
        public bool ShowBrowser(string content, webui_browsers browser)
        {
            return Initialized && WebUILibFunctions.webui_show_browser(_id, content, (UIntPtr)browser);
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
                UIntPtr lBindID = WebUILibFunctions.webui_bind(_id, element, func);
                AddBindID(lBindID);
                return lBindID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// <para>Bind a specific html element click event with a callback function. Empty element means all events.</para>
        /// <para>The OnWebUIEvent event will be triggered for each WebUI event.</para>
        /// </summary>
        /// <param name="element">The HTML element ID.</param>
        /// <returns>Returns a unique bind ID.</returns>
        /// <remarks>
        /// <para>The callback function will always be executed in a background thread!</para>
        /// </remarks>
        public UIntPtr Bind(string element)
        {
            if (Initialized)
            {
                UIntPtr lBindID = WebUILibFunctions.webui_bind(_id, element, WebUI.CommonBindCallback);
                AddBindID(lBindID);
                return lBindID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// <para>Bind all browser events with a callback function.</para>
        /// </summary>
        /// <param name="func">The callback function.</param>
        /// <returns>Returns a unique bind ID.</returns>
        public UIntPtr BindAllEvents(BindCallback func)
        {
            if (Initialized)
            {
                UIntPtr lBindID = WebUILibFunctions.webui_bind(_id, string.Empty, func);
                AddBindID(lBindID);
                return lBindID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// <para>Bind all browser events with a callback function.</para>
        /// <para>The OnWebUIEvent event will be triggered for each WebUI event.</para>
        /// </summary>
        /// <returns>Returns a unique bind ID.</returns>
        public UIntPtr BindAllEvents()
        {
            if (Initialized)
            {
                UIntPtr lBindID = WebUILibFunctions.webui_bind(_id, string.Empty, WebUI.CommonBindCallback);
                AddBindID(lBindID);
                return lBindID;
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
                WebUILibFunctions.webui_set_kiosk(_id, status);
            }
        }

        /// <summary>
        /// Close a specific window only. The window object will still exist.
        /// </summary>
        public void Close()
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_close(_id);
            }
        }

        /// <summary>
        /// Close the window and free all memory resources.
        /// </summary>
        public void Destroy() {
            if (Initialized)
            {
                WebUILibFunctions.webui_destroy(_id);
                _id = 0;
            }
        }

        /// <summary>
        /// Set the web-server root folder path for a specific window.
        /// </summary>
        /// <param name="path">The local folder full path.</param>
        public bool SetRootFolder(string path)
        {
            return Initialized && WebUILibFunctions.webui_set_root_folder(_id, path);
        }

        /// <summary>
        /// Set a custom handler to serve files.
        /// </summary>
        /// <param name="handler">The handler function: `void myHandler(const char* filename, * int* length)`.</param>
        public void SetFileHandler(FileHandlerCallback handler)
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_set_file_handler(_id, handler);
            }
        }

        /// <summary>
        /// Set a custom handler to serve files. The OnFileHandlerEvent event will be triggered for each file.
        /// </summary>
        public void SetFileHandler()
        {
            if (Initialized && (_fileHandlerCallback != null))
            {
                WebUILibFunctions.webui_set_file_handler(_id, _fileHandlerCallback);
            }
        }

        /// <summary>
        /// Check if the specified window is still running.
        /// </summary>
        public bool IsShown()
        {
            return Initialized && WebUILibFunctions.webui_is_shown(_id);
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
                WebUILibFunctions.webui_set_icon(_id, icon, icon_type);
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
                WebUILibFunctions.webui_send_raw(_id, function, raw, size);
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
                WebUILibFunctions.webui_set_hide(_id, status);
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
                WebUILibFunctions.webui_set_size(_id, width, height);
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
                WebUILibFunctions.webui_set_position(_id, x, y);
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
                WebUILibFunctions.webui_set_profile(_id, name, path);
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
                WebUILibFunctions.webui_set_proxy(_id, proxy_server);
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
                WebUILibFunctions.webui_set_public(_id, status);
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
                WebUILibFunctions.webui_navigate(_id, url);
            }
        }

        /// <summary>
        /// Delete a specific window web-browser local folder profile.
        /// </summary>
        public void DeleteProfile()
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_delete_profile(_id);
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
            return Initialized && WebUILibFunctions.webui_set_port(_id, port);
        }

        /// <summary>
        /// Run JavaScript without waiting for the response.
        /// </summary>
        /// <param name="script_">The JavaScript to be run.</param>
        public void Run(string script_)
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_run(_id, script_);
            }
        }

        /// <summary>
        /// Run JavaScript and get the response back.
        /// Make sure response_length is big enough to hold the response.
        /// </summary>
        /// <param name="script_">The JavaScript to be run.</param>
        /// <param name="timeout">The execution timeout.</param>
        /// <param name="response">The response in string format.</param>
        /// <param name="response_length">The response size.</param>
        /// <returns>Returns True if there is no execution error.</returns>
        public bool Script(string script_, UIntPtr timeout, out string response, UIntPtr response_length)
        {
            if (Initialized)
            {
                IntPtr buffer = WebUI.Malloc(response_length);
                string? tempResponse = null;

                if (WebUILibFunctions.webui_script(_id, script_, timeout, buffer, response_length))
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

        /// <summary>
        /// Chose between Deno and Nodejs as runtime for .js and .ts files.
        /// </summary>
        /// <param name="runtime">Deno or Nodejs.</param>
        public void SetRuntime(webui_runtimes runtime)
        {
            if (Initialized)
            {
                WebUILibFunctions.webui_set_runtime(_id, (UIntPtr)runtime);
            }
        }
    }
}
