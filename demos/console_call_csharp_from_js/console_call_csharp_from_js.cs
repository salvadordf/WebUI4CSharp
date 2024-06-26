﻿using console_call_csharp_from_js;
using WebUI4CSharp;

string my_html = 
        "<!DOCTYPE html>" +
        "<html>" +
        "  <head>" +
        "    <meta charset=\"UTF-8\">" +
        "    <script src=\"webui.js\"></script>" +
        "    <title>Call C from JavaScript Example</title>" +
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
        "    <h1>WebUI - Call C from JavaScript</h1>" +
        "    <p>Call C functions with arguments (<em>See the logs in your terminal</em>)</p>" +
        "    <button onclick=\"my_function_string('Hello', 'World');\">Call my_function_string()</button>" +
        "    <br>" +
        "    <button onclick=\"my_function_integer(123, 456, 789, 12345.6789);\">Call my_function_integer()</button>" +
        "    <br>" +
        "    <button onclick=\"my_function_boolean(true, false);\">Call my_function_boolean()</button>" +
        "    <br>" +
        "    <button onclick=\"my_function_raw_binary(new Uint8Array([0x41,0x42,0x43]), big_arr);\"> " +
        "     Call my_function_raw_binary()</button>" +
        "    <br>" +
        "    <p>Call a C function that returns a response</p>" +
        "    <button onclick=\"MyJS();\">Call my_function_with_response()</button>" +
        "    <div>Double: <input type=\"text\" id=\"MyInputID\" value=\"2\"></div>" +
        "    <script>" +
        "      const arr_size = 512 * 1000;" +
        "      const big_arr = new Uint8Array(arr_size);" +
        "      big_arr[0] = 0xA1;" +
        "      big_arr[arr_size - 1] = 0xA2;" +
        "      function MyJS() {" +
        "        const MyInput = document.getElementById('MyInputID');" +
        "        const number = MyInput.value;" +
        "        my_function_with_response(number, 2).then((response) => {" +
        "            MyInput.value = response;" +
        "        });" +
        "      }" +
        "    </script>" +
        "  </body>" +
        "</html>";

WebUIWindow window = new WebUIWindow();
window.Bind("my_function_string", WebUI_Events.my_function_string);
window.Bind("my_function_integer", WebUI_Events.my_function_integer);
window.Bind("my_function_boolean", WebUI_Events.my_function_boolean);
window.Bind("my_function_with_response", WebUI_Events.my_function_with_response);
window.Bind("my_function_raw_binary", WebUI_Events.my_function_raw_binary);
window.Show(my_html);
WebUI.Wait();
WebUI.Clean();
