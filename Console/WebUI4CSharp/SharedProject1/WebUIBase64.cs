using System.Runtime.InteropServices;

namespace WebUI4CSharp
{
    public static class WebUIBase64
    {
        /**
         * @brief Base64 encoding. Use this to safely send text based data to the UI. If
         * it fails it will return NULL.
         *
         * @param str The string to encode (Should be null terminated)
         *
         * @example webui_encode("Hello");
         */
        [DllImport("webui-2.dll")]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        private static extern string webui_encode([MarshalAs(UnmanagedType.LPUTF8Str)] string str);

        /**
         * @brief Base64 decoding. Use this to safely decode received Base64 text from
         * the UI. If it fails it will return NULL.
         *
         * @param str The string to decode (Should be null terminated)
         *
         * @example webui_decode("SGVsbG8=");
         */
        [DllImport("webui-2.dll")]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        private static extern string webui_decode([MarshalAs(UnmanagedType.LPUTF8Str)] string str);

        /// <summary>
        /// Base64 encoding. Use this to safely send text based data to the UI. If it fails it will return an empty string.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>Returns a encoded string.</returns>
        public static string Encode(string str) { return webui_encode(str); }

        /// <summary>
        /// Base64 decoding. Use this to safely decode received Base64 text from the UI. If it fails it will return an empty string.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>Returns a decoded string.</returns>
        public static string Decode(string str) { return webui_decode(str); }
    }
}
