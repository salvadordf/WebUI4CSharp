using console_virtual_file_system;
using WebUI4CSharp;

// Bind HTML element IDs with a C functions
WebUI_Events.myWindow.Bind("Exit", WebUI_Events.exit_app);

// Set a custom files handler
WebUI_Events.myWindow.SetFileHandler(WebUI_Events.Vfs);

// Show a new window
WebUI_Events.myWindow.Show("index.html");

WebUI.Wait();
WebUI.Clean();