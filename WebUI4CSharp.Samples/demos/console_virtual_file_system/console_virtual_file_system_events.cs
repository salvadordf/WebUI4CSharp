using WebUI4CSharp;

namespace console_virtual_file_system
{
    public static class WebUI_Events
    {
        public static WebUIWindow myWindow = new WebUIWindow();

        public static void exit_app(ref webui_event_t e)
        {
            // Close all opened windows
            WebUI.Exit();
        }

        public static IntPtr Vfs(IntPtr filename, out int length)
        {
            length = 0;
            string? lFilename = WebUI.WebUIStringToCSharpString(filename);
            
            // This function reads files in the drive but you can get the contents from
            // any other source like resources.

            if (lFilename != null)
            {
                string lOriginalFilename = lFilename;
                int i = lFilename.IndexOf('/');
                if (i >= 0)
                {
                    lFilename = lFilename.Substring(i + 1);
                }
                string lAssetsDir = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\virtual_file_system");
                string lFileAbsPath = Path.Combine(lAssetsDir, lFilename);

                try
                {
                    if (File.Exists(lFileAbsPath))
                    {
                        string lContent;
                        using (FileStream lFileStream = new FileStream(lFileAbsPath, FileMode.Open))
                        {
                            var lStreamReader = new StreamReader(lFileStream);
                            lContent = lStreamReader.ReadToEnd();
                            lStreamReader.Close();
                            lFileStream.Close();
                        }
                        string lMimeType = WebUI.GetMimeType(lFilename);
                        string lHTTPHeader = @"HTTP/1.1 200 OK" + Environment.NewLine +
                                              "Content-Type: " + lMimeType + Environment.NewLine +
                                              "Content-Length: " + lContent.Length.ToString() + Environment.NewLine +
                                              "Cache-Control: no-cache" + Environment.NewLine + Environment.NewLine +
                                              lContent;

                        return WebUI.CSharpStringToWebUIString(lHTTPHeader, out length);
                    }
                    else if (Directory.Exists(lFileAbsPath))
                    {
                        // Redirect requests to directories to the index.html file.
                        string lHTTPHeader = @"HTTP/1.1 302 Found" + Environment.NewLine +
                                              "Location: " + lOriginalFilename + @"\index.html" + Environment.NewLine +
                                              "Cache-Control: no-cache" + Environment.NewLine + Environment.NewLine;

                        return WebUI.CSharpStringToWebUIString(lHTTPHeader, out length);
                    }
                    else
                    {
                        string lContent = "<html><head><title>Resource Not Found</title></head>" +
                            "<body><p>The resource you requested has not been found at the specified address. " +
                            "Please check the spelling of the address.</p></body></html>";

                        string lHTTPHeader = @"HTTP/1.1 404 Not Found" + Environment.NewLine +
                                              "Content-Type: text/html" + Environment.NewLine +
                                              "Content-Length: " + lContent.Length.ToString() + Environment.NewLine +
                                              "Cache-Control: no-cache" + Environment.NewLine + Environment.NewLine +
                                              lContent;

                        return WebUI.CSharpStringToWebUIString(lHTTPHeader, out length);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There was a problem opening {lFilename}. {e.Message}");
                }
            }
            return IntPtr.Zero;
        }
    }
}
