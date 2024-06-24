using System.Runtime.InteropServices;
using console_text_editor;
using WebUI4CSharp;

WebUIWindow window = new WebUIWindow();

// Set the root folder for the UI
string absPath = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\text_editor");
window.SetRootFolder(absPath);

// Bind HTML elements with the specified ID to C functions
window.Bind("close_app", WebUI_Events.close_app);

// Show the window, preferably in a chromium based browser
if (!window.ShowBrowser("index.html", webui_browser.ChromiumBased))
{
    window.Show("index.html");
}

WebUI.Wait();
WebUI.Clean();
