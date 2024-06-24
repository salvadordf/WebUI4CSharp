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
        public static string Encode(string str) { return WebUILibFunctions.webui_encode(str); }

        /// <summary>
        /// Decode a Base64 encoded text.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>Returns the base64 decoded string.</returns>
        public static string Decode(string str) { return WebUILibFunctions.webui_decode(str); }
    }
}
