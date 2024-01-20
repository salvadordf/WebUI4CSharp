using System.Runtime.InteropServices;
using console_text_editor;
using WebUI4CSharp;

WebUIWindow window = new WebUIWindow();

// Set the root folder for the UI
string absPath = Path.GetFullPath("..\\..\\..\\assets\\text_editor");
window.SetRootFolder(absPath);

// Bind HTML elements with the specified ID to C functions
window.Bind("__close-btn", WebUI_Events.Close);

// Show the window, preferably in a chromium based browser
if (!window.ShowBrowser("index.html", webui_browsers.ChromiumBased))
{
    window.Show("index.html");
}

WebUI.Wait();
WebUI.Clean();
