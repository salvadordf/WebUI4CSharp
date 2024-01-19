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

    public delegate void BindCallback(ref webui_event_t e);

    [return: MarshalAs(UnmanagedType.LPUTF8Str)]
    public delegate string FileHandlerCallback([MarshalAs(UnmanagedType.LPUTF8Str)] string filename, out int length);

    public delegate void InterfaceEventCallback(UIntPtr window, UIntPtr event_type, [MarshalAs(UnmanagedType.LPUTF8Str)] string element, UIntPtr event_number, UIntPtr bind_id);


    public static class WebUI
    {
        public const string WEBUI_VERSION = "2.4.2";

        /// <summary>
        /// Max windows, servers and threads
        /// </summary>
        public const int WEBUI_MAX_IDS = 256;

        /// <summary>
        /// Max allowed argument's index
        /// </summary>
        public const int WEBUI_MAX_ARG = 16;

        private static object _lockObj = new object();
        private static List<WeakReference<WebUIWindow>> _windowList = new List<WeakReference<WebUIWindow>>();

        /**
         * @brief Wait until all opened windows get closed.
         *
         * @example webui_wait();
         */
        [DllImport("webui-2")]
        private static extern void webui_wait();

        /**
         * @brief Close all open windows. `webui_wait()` will return (Break).
         *
         * @example webui_exit();
         */
        [DllImport("webui-2")]
        private static extern void webui_exit();

        /**
         * @brief Set the web-server root folder path for all windows. Should be used
         * before `webui_show()`.
         *
         * @param path The local folder full path
         *
         * @example webui_set_default_root_folder("/home/Foo/Bar/");
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_set_default_root_folder([MarshalAs(UnmanagedType.LPUTF8Str)] string path);

        /**
         * @brief Set the maximum time in seconds to wait for the browser to start.
         *
         * @param second The timeout in seconds
         *
         * @example webui_set_timeout(30);
         */
        [DllImport("webui-2")]
        private static extern void webui_set_timeout(UIntPtr second);

        /**
         * @brief Safely free a buffer allocated by WebUI using `webui_malloc()`.
         *
         * @param ptr The buffer to be freed
         *
         * @example webui_free(myBuffer);
         */
        [DllImport("webui-2")]
        private static extern void webui_free(IntPtr ptr);

        /**
         * @brief Safely allocate memory using the WebUI memory management system. It
         * can be safely freed using `webui_free()` at any time.
         *
         * @param size The size of memory in bytes
         *
         * @example char* myBuffer = (char*)webui_malloc(1024);
         */
        [DllImport("webui-2")]
        private static extern IntPtr webui_malloc(UIntPtr size);

        /**
         * @brief Free all memory resources. Should be called only at the end.
         *
         * @example
         * webui_wait();
         * webui_clean();
         */
        [DllImport("webui-2")]
        private static extern void webui_clean();

        /**
         * @brief Delete all local web-browser profiles folder. It should called at the
         * end.
         *
         * @example
         * webui_wait();
         * webui_delete_all_profiles();
         * webui_clean();
         */
        [DllImport("webui-2")]
        private static extern void webui_delete_all_profiles();

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
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_set_tls_certificate([MarshalAs(UnmanagedType.LPUTF8Str)] string certificate_pem, [MarshalAs(UnmanagedType.LPUTF8Str)] string private_key_pem);

        /**
         * @brief Check if the app still running.
         *
         * @return Returns True if app is running
         *
         * @example bool status = webui_interface_is_app_running();
         */
        [DllImport("webui-2")]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool webui_interface_is_app_running();

        /// <summary>
        /// Wait until all opened windows get closed.
        /// </summary>
        public static void Wait()
        {
            webui_wait();
        }

        /// <summary>
        /// Close all open windows. `webui_wait()` will return (Break).
        /// </summary>
        public static void Exit()
        {
            webui_exit();
        }

        /// <summary>
        /// Set the web-server root folder path for all windows. Should be used before `webui_show()`.
        /// </summary>
        /// <param name="path">The local folder full path.</param>
        /// <returns>Returns True if the function was successful.</returns>
        public static bool SetDefaultRootFolder(string path)
        {
            return webui_set_default_root_folder(path);
        }

        /// <summary>
        /// Timeout in seconds before the browser starts. 0 means wait forever.
        /// </summary>
        public static void SetTimeout(UIntPtr second)
        {
            webui_set_timeout(second);
        }

        /// <summary>
        /// Safely free a buffer allocated by WebUI using `webui_malloc()`.
        /// </summary>
        /// <param name="ptr">The buffer to be freed.</param>
        public static void Free(IntPtr ptr)
        {
            webui_free(ptr);
        }

        /// <summary>
        /// Safely allocate memory using the WebUI memory management system. It can be safely freed using `webui_free()` at any time.
        /// </summary>
        /// <param name="size">The size of memory in bytes.</param>
        /// <returns></returns>
        public static IntPtr Malloc(UIntPtr size)
        {
            return webui_malloc(size);
        }

        /// <summary>
        /// Free all memory resources. Should be called only at the end.
        /// </summary>
        public static void Clean()
        {
            webui_clean();     
        }

        /// <summary>
        /// Delete all local web-browser profiles folder. It should called at the end.
        /// </summary>
        public static void DeleteAllProfiles() 
        {
            webui_delete_all_profiles();
        }

        /// <summary>
        /// Set the SSL/TLS certificate and the private key content, both in PEM
        /// format. This works only with `webui-2-secure` library. If set empty WebUI
        /// will generate a self-signed certificate.
        /// </summary>
        /// <param name="certificate_pem">The SSL/TLS certificate content in PEM format.</param>
        /// <param name="private_key_pem">The private key content in PEM format.</param>
        /// <returns>Returns True if the certificate and the key are valid.</returns>
        public static bool SetTLSCertificate(string certificate_pem, string private_key_pem)
        {
            return webui_set_tls_certificate(certificate_pem, private_key_pem);
        }

        /// <summary>
        /// Check if the app still running.
        /// </summary>
        /// <returns>Returns True if app is running.</returns>
        public static bool IsAppRunning()
        {
            return webui_interface_is_app_running();
        }

        public static void AddWindow(WebUIWindow newWindow)
        {
            lock(_lockObj)
            {
                _windowList.Add(new WeakReference<WebUIWindow>(newWindow));

                WebUIWindow? window;
                WeakReference<WebUIWindow> windowRef;
                for (int i = _windowList.Count - 1; i >= 0; i--)
                {
                    windowRef = _windowList[i];
                    if (! windowRef.TryGetTarget(out window))
                    {
                        _windowList.RemoveAt(i);
                    }
                }
            }
        }

        public static WebUIWindow? SearchWindow(UIntPtr windowId)
        {
            if (windowId > 0)
            {
                lock (_lockObj)
                {
                    WebUIWindow? window;
                    WeakReference<WebUIWindow> windowRef;
                    for (int i = 0; i < _windowList.Count; i++)
                    {
                        windowRef = _windowList[i];
                        if (windowRef.TryGetTarget(out window) && (window.Id == windowId))
                        {
                            return window;
                        }
                    }
                }
            }
            return null;
        }
    }
}
