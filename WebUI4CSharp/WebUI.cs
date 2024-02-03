using System.Runtime.InteropServices;
using System.Text;

namespace WebUI4CSharp
{
    /// <summary>
    /// Main WebUI class used to handle global properties, methods and the Window list.
    /// </summary>
    public static class WebUI
    {
        /// <summary>
        /// WebUI library version.
        /// </summary>
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


        /// <summary>
        /// Wait until all opened windows get closed.
        /// </summary>
        public static void Wait()
        {
            WebUILibFunctions.webui_wait();
        }

        /// <summary>
        /// Close all open windows. `webui_wait()` will return (Break).
        /// </summary>
        public static void Exit()
        {
            WebUILibFunctions.webui_exit();
        }

        /// <summary>
        /// Set the web-server root folder path for all windows. Should be used before `webui_show()`.
        /// </summary>
        /// <param name="path">The local folder full path.</param>
        /// <returns>Returns True if the function was successful.</returns>
        public static bool SetDefaultRootFolder(string path)
        {
            return WebUILibFunctions.webui_set_default_root_folder(path);
        }

        /// <summary>
        /// Timeout in seconds before the browser starts. 0 means wait forever.
        /// </summary>
        public static void SetTimeout(UIntPtr second)
        {
            WebUILibFunctions.webui_set_timeout(second);
        }

        /// <summary>
        /// Safely free a buffer allocated by WebUI using `webui_malloc()`.
        /// </summary>
        /// <param name="ptr">The buffer to be freed.</param>
        public static void Free(IntPtr ptr)
        {
            WebUILibFunctions.webui_free(ptr);
        }

        /// <summary>
        /// Safely allocate memory using the WebUI memory management system. It can be safely freed using `webui_free()` at any time.
        /// </summary>
        /// <param name="size">The size of memory in bytes.</param>
        /// <returns></returns>
        public static IntPtr Malloc(UIntPtr size)
        {
            return WebUILibFunctions.webui_malloc(size);
        }

        /// <summary>
        /// Free all memory resources. Should be called only at the end.
        /// </summary>
        public static void Clean()
        {
            WebUILibFunctions.webui_clean();     
        }

        /// <summary>
        /// Delete all local web-browser profiles folder. It should called at the end.
        /// </summary>
        public static void DeleteAllProfiles() 
        {
            WebUILibFunctions.webui_delete_all_profiles();
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
            return WebUILibFunctions.webui_set_tls_certificate(certificate_pem, private_key_pem);
        }

        /// <summary>
        /// Check if the app still running.
        /// </summary>
        /// <returns>Returns True if app is running.</returns>
        public static bool IsAppRunning()
        {
            return WebUILibFunctions.webui_interface_is_app_running();
        }

        /// <summary>
        /// Add an WebUIWindow instance to the list.
        /// </summary>
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

        /// <summary>
        /// Search a WebUIWindow instance in the list.
        /// </summary>
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

        /// <summary>
        /// Convert a pointer to a WebUI string in UTF8 format to a C# string.
        /// </summary>
        /// <param name="srcString">The pointer to the original UTF8 string.</param>
        /// <returns>Returns a C# string.</returns>
        public static string? WebUIStringToCSharpString(IntPtr srcString)
        {
            return Marshal.PtrToStringUTF8(srcString);
        }

        /// <summary>
        /// Converts a C# string to a WebUI string in UTF8 format.
        /// This function should only be used by the file handler callback.
        /// By allocating resources using webui_malloc() WebUI will automaticaly free the resources.         
        /// </summary>
        /// <param name="srcString">The original C# string.</param>
        /// <param name="rsltLength">The length of the result string.</param>
        /// <returns>Returns a pointer to a UTF8 string.</returns>
        public static IntPtr CSharpStringToWebUIString(string srcString, out int rsltLength)
        {
            if (srcString == string.Empty)
            {
                rsltLength = 0;
                return IntPtr.Zero;
            }
            else
            {
                byte[] arrayBuffer = Encoding.UTF8.GetBytes(srcString);
                rsltLength = arrayBuffer.Length + 1;
                IntPtr rsltBuffer = Malloc((UIntPtr)rsltLength);
                for (int i = 0; i < arrayBuffer.Length; i++)
                {
                    Marshal.WriteByte(rsltBuffer, i, arrayBuffer[i]);
                }
                Marshal.WriteByte(rsltBuffer, arrayBuffer.Length, 0);
                return rsltBuffer;
            }
        }

        public static void CommonBindCallback(ref webui_event_t e)
        {
            WebUIWindow? lWindow = SearchWindow(e.window);
            if ((lWindow != null) && lWindow.HasBindID(e.bind_id))
            {
                lWindow.DoBindEvent(ref e);
            }
        }
    }
}
