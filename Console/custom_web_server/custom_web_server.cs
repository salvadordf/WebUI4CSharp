using custom_web_server;
using WebUI4CSharp;

WebUIWindow window = new WebUIWindow();

// Bind all events
window.BindAllEvents(WebUI_Events.events);

// Bind HTML elements with C functions
window.Bind("my_backend_func", WebUI_Events.my_backend_func);

// Set web server network port WebUI should use
// this mean `webui.js` will be available at:
// http://localhost:8081/webui.js
window.SetPort(8081);

// Show a new window and show our custom web server
// Assuming the custom web server is running on port
// 8080...
// Run the \assets\custom_web_server\simple_web_server.py script to create a simple web server
window.Show("http://localhost:8080/");

WebUI.Wait();
WebUI.Clean();