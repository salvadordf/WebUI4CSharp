using WebUI4CSharp;

WebUIWindow window = new WebUIWindow();
window.Show("<html><head><script src=\"webui.js\"></script></head> Hello World ! </html>");
WebUI.Wait();
