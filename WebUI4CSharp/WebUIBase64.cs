using System;

namespace WebUI4CSharp
{
    /// <summary>
    /// Wrapper class for the Base64 conversion functions in WebUI.
    /// </summary>
    public static class WebUIBase64
    {
        /// <summary>
        /// Encode text to Base64. 
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>Returns the base64 encoded string.</returns>
        public static string? Encode(string str)
        {
            string? response = null;
            IntPtr buffer = WebUILibFunctions.webui_encode(str);
            if (buffer != IntPtr.Zero)
            {
                response = WebUI.WebUIStringToCSharpString(buffer);
                WebUI.Free(buffer);
            }
            return response;
        }

        /// <summary>
        /// Decode a Base64 encoded text.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>Returns the base64 decoded string.</returns>
        public static string? Decode(string str)
        {
            string? response = null;
            IntPtr buffer = WebUILibFunctions.webui_decode(str);
            if (buffer != IntPtr.Zero)
            {
                response = WebUI.WebUIStringToCSharpString(buffer);
                WebUI.Free(buffer);
            }
            return response;
        }
    }
}
