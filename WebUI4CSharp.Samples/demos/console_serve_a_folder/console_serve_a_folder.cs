using console_serve_a_folder;
using WebUI4CSharp;

// Bind HTML element IDs with a C functions
WebUI_Events.myWindow.Bind("SwitchToSecondPage", WebUI_Events.switch_to_second_page);
WebUI_Events.myWindow.Bind("OpenNewWindow", WebUI_Events.show_second_window);
WebUI_Events.myWindow.Bind("Exit", WebUI_Events.exit_app);
WebUI_Events.mySecondWindow.Bind("Exit", WebUI_Events.exit_app);

// Bind events
WebUI_Events.myWindow.BindAllEvents(WebUI_Events.events);

// Make Deno as the `.ts` and `.js` interpreter
WebUI_Events.myWindow.SetRuntime(webui_runtime.Deno);

// Set a custom files handler
WebUI_Events.myWindow.SetFileHandler(WebUI_Events.my_files_handler);

// Set window size
WebUI_Events.myWindow.SetSize(800, 800);

// Set window position
WebUI_Events.myWindow.SetPosition(200, 200);

// Set the root folder for the UI
string absPath = Path.GetFullPath("..\\..\\..\\..\\..\\assets\\serve_a_folder");
WebUI_Events.myWindow.SetRootFolder(absPath);
WebUI_Events.mySecondWindow.SetRootFolder(absPath);

// Show a new window
// webui_set_root_folder(MyWindow, "_MY_PATH_HERE_");
// webui_show_browser(MyWindow, "index.html", Chrome);
WebUI_Events.myWindow.Show("index.html");

WebUI.Wait();
WebUI.Clean();