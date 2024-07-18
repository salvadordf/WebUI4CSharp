using console_web_app_multi_client;
using WebUI4CSharp;

// Allow multi-user connection
WebUI.SetConfig(webui_config.multi_client, true);

// Allow cookies
WebUI.SetConfig(webui_config.use_cookies, true);

// Bind HTML element IDs with a C functions
WebUI_Events.myWindow.Bind("save", WebUI_Events.save);
WebUI_Events.myWindow.Bind("saveAll", WebUI_Events.saveAll);
WebUI_Events.myWindow.Bind("exit_app", WebUI_Events.exit_app);

WebUI_Events.myWindow.BindAllEvents(WebUI_Events.events);

string lAssetsDir = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\web_app_multi_client");
string lFileAbsPath = Path.Combine(lAssetsDir, "index.html");

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
    // Start server only
    string? url = WebUI_Events.myWindow.StartServer(lContent);

    if (!string.IsNullOrWhiteSpace(url))
    {
        // Open a new page in the default native web browser
        WebUI.OpenUrl(url);
    }
}

// Wait until all windows get closed
WebUI.Wait();

// Free all memory resources (Optional)
WebUI.Clean();
