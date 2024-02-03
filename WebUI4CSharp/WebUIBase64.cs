namespace WebUI4CSharp
{
    /// <summary>
    /// Wrapper class for the Base64 conversion functions in WebUI.
    /// </summary>
    public static class WebUIBase64
    {
        /// <summary>
        /// Base64 encoding. Use this to safely send text based data to the UI. If it fails it will return an empty string.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>Returns a encoded string.</returns>
        public static string Encode(string str) { return WebUILibFunctions.webui_encode(str); }

        /// <summary>
        /// Base64 decoding. Use this to safely decode received Base64 text from the UI. If it fails it will return an empty string.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>Returns a decoded string.</returns>
        public static string Decode(string str) { return WebUILibFunctions.webui_decode(str); }
    }
}
