using console_public_network_access;
using WebUI4CSharp;

// Main Private Window HTML
string private_html = "<!DOCTYPE html>" +
        "<html>" +
        "  <head>" +
        "    <meta charset=\"UTF-8\">" +
        "    <script src=\"webui.js\"></script>" +
        "    <title>Public Network Access Example</title>" +
        "    <style>" +
        "       body {" +
        "            font-family: 'Arial', sans-serif;" +
        "            color: white;" +
        "            background: linear-gradient(to right, #507d91, #1c596f, #022737);" +
        "            text-align: center;" +
        "            font-size: 18px;" +
        "        }" +
        "        button, input {" +
        "            padding: 10px;" +
        "            margin: 10px;" +
        "            border-radius: 3px;" +
        "            border: 1px solid #ccc;" +
        "            box-shadow: 0 3px 5px rgba(0,0,0,0.1);" +
        "            transition: 0.2s;" +
        "        }" +
        "        button {" +
        "            background: #3498db;" +
        "            color: #fff; " +
        "            cursor: pointer;" +
        "            font-size: 16px;" +
        "        }" +
        "        h1 { text-shadow: -7px 10px 7px rgb(67 57 57 / 76%); }" +
        "        button:hover { background: #c9913d; }" +
        "        input:focus { outline: none; border-color: #3498db; }" +
        "    </style>" +
        "  </head>" +
        "  <body>" +
        "    <h1>WebUI - Public Network Access Example</h1>" +
        "    <br>" +
        "    The second public window is configured to be accessible from <br>" +
        "    any device in the public network. <br>" +
        "    <br>" +
        "    Second public window link: <br>" +
        "    <h1 id=\"urlSpan\" style=\"color:#c9913d\">...</h1>" +
        "    Second public window events: <br>" +
        "    <textarea id=\"Logs\" rows=\"4\" cols=\"50\" style=\"width:80%\"></textarea>" +
        "    <br>" +
        "    <button id=\"Exit\">Exit</button>" +
        "  </body>" +
        "</html>";

// Public Window HTML
string public_html = "<!DOCTYPE html>" +
        "<html>" +
        "  <head>" +
        "    <meta charset=\"UTF-8\">" +
        "    <script src=\"webui.js\"></script>" +
        "    <title>Welcome to Public UI</title>" +
        "  </head>" +
        "  <body>" +
        "    <h1>Welcome to Public UI!</h1>" +
        "  </body>" +
        "</html>";

// App
WebUI.SetTimeout(0); // Wait forever (never timeout)

// Public Window
WebUI_Events.pubWindow.SetPublic(true); // Make URL accessible from public networks
WebUI_Events.pubWindow.BindAllEvents(WebUI_Events.public_window_events); // Bind all events
WebUI_Events.pubWindow.ShowBrowser(public_html, webui_browsers.NoBrowser); // Set public window HTML
string public_win_url = WebUI_Events.pubWindow.Url; // Get URL of public window

// Main Private Window
WebUI_Events.prvWindow.Bind("Exit", WebUI_Events.app_exit); // Bind exit button
WebUI_Events.prvWindow.Show(private_html); // Show the window

// Set URL in the UI
string javascript = $"document.getElementById('urlSpan').innerHTML = '{public_win_url}';";
WebUI_Events.prvWindow.Run(javascript);

WebUI.Wait();
WebUI.Clean();