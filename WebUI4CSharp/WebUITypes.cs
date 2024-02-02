using System.Runtime.InteropServices;

namespace WebUI4CSharp
{
    public enum webui_browsers
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
    };

    public enum webui_runtimes
    {
        None = 0, // 0. Prevent WebUI from using any runtime for .js and .ts files
        Deno,     // 1. Use Deno runtime for .js and .ts files
        NodeJS,   // 2. Use Nodejs runtime for .js files
    };

    public enum webui_events
    {
        WEBUI_EVENT_DISCONNECTED = 0, // 0. Window disconnection event
        WEBUI_EVENT_CONNECTED,        // 1. Window connection event
        WEBUI_EVENT_MOUSE_CLICK,      // 2. Mouse click event
        WEBUI_EVENT_NAVIGATION,       // 3. Window navigation event
        WEBUI_EVENT_CALLBACK,         // 4. Function call event
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct webui_event_t
    {
        public UIntPtr window;       // The window object number
        public UIntPtr event_type;   // Event type
        public IntPtr element;       // HTML element ID   
        public UIntPtr event_number; // Internal WebUI
        public UIntPtr bind_id;      // Bind ID
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BindCallback(ref webui_event_t e);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr FileHandlerCallback(IntPtr filename, out int length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void InterfaceEventCallback(UIntPtr window, UIntPtr event_type, IntPtr element, UIntPtr event_number, UIntPtr bind_id);

}
