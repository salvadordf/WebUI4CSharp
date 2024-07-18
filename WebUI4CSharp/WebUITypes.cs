using System.Runtime.InteropServices;

namespace WebUI4CSharp
{
    /// <summary>
    /// Supported web browsers
    /// </summary>
    public enum webui_browser
    {
        NoBrowser = 0,  // 0. No web browser
        AnyBrowser = 1, // 1. Default recommended web browser
        Chrome,         // 2. Google Chrome
        Firefox,        // 3. Mozilla Firefox
        Edge,           // 4. Microsoft Edge
        Safari,         // 5. Apple Safari
        Chromium,       // 6. The Chromium Project
        Opera,          // 7. Opera Browser
        Brave,          // 8. The Brave Browser
        Vivaldi,        // 9. The Vivaldi Browser
        Epic,           // 10. The Epic Browser
        Yandex,         // 11. The Yandex Browser
        ChromiumBased,  // 12. Any Chromium based browser
        Webview,        // 13. WebView (Non-web-browser)
    };

    /// <summary>
    /// Supported runtimes
    /// </summary>
    public enum webui_runtime
    {
        /// <summary>
        /// Prevent WebUI from using any runtime for .js and .ts files.
        /// </summary>
        None = 0, 
        /// <summary>
        /// Use Deno runtime for .js and .ts files.
        /// </summary>
        Deno,
        /// <summary>
        /// Use Nodejs runtime for .js files.
        /// </summary>
        NodeJS,
        /// <summary>
        /// Use Bun runtime for .js and .ts files
        /// </summary>
        Bun,      
    };

    /// <summary>
    /// WebUI event types
    /// </summary>
    public enum webui_event
    {
        /// <summary>
        /// Window disconnection event.
        /// </summary>
        WEBUI_EVENT_DISCONNECTED = 0,
        /// <summary>
        /// Window connection event.
        /// </summary>
        WEBUI_EVENT_CONNECTED,
        /// <summary>
        /// Mouse click event.
        /// </summary>
        WEBUI_EVENT_MOUSE_CLICK,
        /// <summary>
        /// Window navigation event.
        /// </summary>
        WEBUI_EVENT_NAVIGATION,
        /// <summary>
        /// Function call event.
        /// </summary>
        WEBUI_EVENT_CALLBACK,
    };

    /// <summary>
    /// WebUI configuration.
    /// </summary>
    public enum webui_config
    {
        /// <summary>
        /// Control if `webui_show()`, `webui_show_browser()` and
        /// `webui_show_wv()` should wait for the window to connect
        /// before returns or not. Default: True.
        /// </summary>
        show_wait_connection = 0,
        /// <summary>
        /// Control if WebUI should block and process the UI events
        /// one a time in a single thread `True`, or process every
        /// event in a new non-blocking thread `False`. This updates
        /// all windows. You can use `webui_set_event_blocking()` for
        /// a specific single window update. Default: False.
        /// </summary>
        ui_event_blocking,
        /// <summary>
        /// Automatically refresh the window UI when any file in the
        /// root folder gets changed. Default: False.
        /// </summary>
        folder_monitor,
        /// <summary>
        /// Allow multiple clients to connect to the same window,
        /// This is helpful for web apps (non-desktop software),
        /// Please see the documentation for more details. Default: False.
        /// </summary>
        multi_client,
        /// <summary>
        /// Allow multiple clients to connect to the same window,
        /// This is helpful for web apps (non-desktop software),
        /// Please see the documentation for more details. Default: False.
        /// </summary>
        use_cookies,
    }

    /// <summary>
    /// Structure with event information
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct webui_event_t
    {
        /// <summary>
        /// The window object number or ID.
        /// </summary>
        public UIntPtr window;
        /// <summary>
        /// Event type. See webui_events.
        /// </summary>
        public UIntPtr event_type;   
        /// <summary>
        /// HTML element ID.
        /// </summary>
        public IntPtr element;       
        /// <summary>
        /// Event number or ID.
        /// </summary>
        public UIntPtr event_number; 
        /// <summary>
        /// Bind ID.
        /// </summary>
        public UIntPtr bind_id;
        /// <summary>
        /// Client's unique ID.
        /// </summary>
        public UIntPtr client_id;
        /// <summary>
        /// Client's connection ID.
        /// </summary>
        public UIntPtr connection_id;
        /// <summary>
        /// Client's full cookies.
        /// </summary>
        public IntPtr cookies;          
    }

    /// <summary>
    /// Callback used by WebUIWindow.Bind
    /// </summary>
    /// <param name="e">Event information</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BindCallback(ref webui_event_t e);

    /// <summary>
    /// Callback used by WebUIWindow.SetFileHandler
    /// </summary>
    /// <param name="filename">Requested file name.</param>
    /// <param name="length">Length of the buffer returned by this function.</param>
    /// <returns>Pointer to the buffer with the requested file contents.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr FileHandlerCallback(IntPtr filename, out int length);

    /// <summary>
    /// Callback used by WebUILibFunctions.webui_interface_bind
    /// </summary>
    /// <param name="window">The window object number or ID.</param>
    /// <param name="event_type">Event type. See webui_events.</param>
    /// <param name="element">HTML element ID.</param>
    /// <param name="event_number">Event number or ID.</param>
    /// <param name="bind_id">Bind ID.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void InterfaceEventCallback(UIntPtr window, UIntPtr event_type, IntPtr element, UIntPtr event_number, UIntPtr bind_id);

}
