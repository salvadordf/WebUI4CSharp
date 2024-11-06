﻿using console_call_js_from_csharp;
using WebUI4CSharp;

string my_html = "<!DOCTYPE html>" +
                          "<html>" +
                          "  <head>" +
                          "    <meta charset=\"UTF-8\">" +
                          "    <script src=\"webui.js\"></script>" +
                          "    <title>Call JavaScript from C Example</title>" +
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
                          "    <h1>WebUI - Call JavaScript from C</h1>" +
                          "    <br>" +
                          "    <h1 id=\"count\">0</h1>" +
                          "    <br>" +
                          "    <button OnClick=\"my_function_count();\">Manual Count</button>" +
                          "    <br>" +
                          "    <button id=\"MyTest\" OnClick=\"AutoTest();\">Auto Count (Every 500ms)</button>" +
                          "    <br>" +
                          "    <button OnClick=\"my_function_exit();\">Exit</button>" +
                          "    <script>" +
                          "      let count = 0;" +
                          "      function GetCount() {" +
                          "        return count;" +
                          "      }" +
                          "      function SetCount(number) {" +
                          "        document.getElementById('count').innerHTML = number;" +
                          "        count = number;" +
                          "      }" +
                          "      function AutoTest(number) {" +
                          "        setInterval(function(){ my_function_count(); }, 500);" +
                          "      }" +
                          "    </script>" +
                          "  </body>" +
                          "</html>";

WebUIWindow window = new WebUIWindow();
window.Bind("my_function_count", WebUI_Events.my_function_count);
window.Bind("my_function_exit", WebUI_Events.my_function_exit);
window.Show(my_html);
WebUI.Wait();
WebUI.Clean();