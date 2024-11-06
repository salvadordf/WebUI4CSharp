﻿using System.IO;
using System.Windows;
using WebUI4CSharp;

namespace wpf_call_csharp_from_js
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Object _lockObj = new Object();
        private List<String> _LogStrings = new List<String>();
        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private WebUIWindow _window = new WebUIWindow();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddLog(String msg)
        {
            lock (_lockObj)
            {
                _LogStrings.Add(msg);
            }
        }

        public void my_function_string(WebUIEvent e)
        {
            // JavaScript:
            // my_function_string('Hello', 'World`);

            string? str_1 = e.GetString();
            string? str_2 = e.GetStringAt(1);

            AddLog($"my_function_string 1: {str_1}"); // Hello
            AddLog($"my_function_string 2: {str_2}"); // World
        }


        public void my_function_integer(WebUIEvent e)
        {
            // JavaScript:
            // my_function_integer(123, 456, 789, 12345.6789);

            UIntPtr count = e.GetCount();
            AddLog($"my_function_integer: There are {count} arguments in this event"); // 4

            long number_1 = e.GetInt();
            long number_2 = e.GetIntAt(1);
            long number_3 = e.GetIntAt(2);
            double float_1 = e.GetFloatAt(3);

            AddLog($"my_function_integer 1: {number_1}"); // 123
            AddLog($"my_function_integer 2: {number_2}"); // 456
            AddLog($"my_function_integer 3: {number_3}"); // 789
            AddLog($"my_function_integer 4: {float_1}"); // 12345.6789
        }

        public void my_function_boolean(WebUIEvent e)
        {
            // JavaScript:
            // my_function_boolean(true, false);

            bool status_1 = e.GetBool();
            bool status_2 = e.GetBoolAt(1);

            AddLog($"my_function_boolean 1: {status_1}"); // True
            AddLog($"my_function_boolean 2: {status_2}"); // False
        }

        public void my_function_raw_binary(WebUIEvent e)
        {
            // JavaScript:
            // my_function_raw_binary(new Uint8Array([0x41]), new Uint8Array([0x42, 0x43]));

            MemoryStream? stream = e.GetStream();
            if (stream != null)
            {
                string hexstring = Convert.ToHexString(stream.ToArray());
                AddLog("my_function_raw_binary: " + hexstring);
            }
        }

        public void my_function_with_response(WebUIEvent e)
        {
            // JavaScript:
            // my_function_with_response(number, 2).then(...)

            long number = e.GetInt();
            long times = e.GetIntAt(1);

            long res = number * times;
            AddLog($"my_function_with_response: {number} * {times} = {res}");

            // Send back the response to JavaScript
            e.ReturnInt(res);
        }

        private void ShowBrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WebUI.IsAppRunning()) { return; }

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

            _window.Bind("my_function_string");
            _window.Bind("my_function_integer");
            _window.Bind("my_function_boolean");
            _window.Bind("my_function_with_response");
            _window.Bind("my_function_raw_binary");
            _window.OnWebUIEvent += new EventHandler<BindEventArgs>(Window_OnWebUIEvent);
            _window.Show(my_html);
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500, 0);
            dispatcherTimer.Start();
        }

        private void Window_OnWebUIEvent(object? sender, BindEventArgs e)
        {
            if (e.BindEvent.Element == "my_function_string")
            {
                my_function_string(e.BindEvent);
            }
            else if (e.BindEvent.Element == "my_function_integer")
            {
                my_function_integer(e.BindEvent);
            }
            else if (e.BindEvent.Element == "my_function_boolean")
            {
                my_function_boolean(e.BindEvent);
            }
            else if (e.BindEvent.Element == "my_function_with_response")
            {
                my_function_with_response(e.BindEvent);
            }
            else if (e.BindEvent.Element == "my_function_raw_binary")
            {
                my_function_raw_binary(e.BindEvent);
            }
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WebUI.IsAppRunning())
            {
                WebUI.Exit();
            }
        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            lock (_lockObj)
            {
                for (int i = 0; i < _LogStrings.Count; i++)
                {
                    Memo1.Text += _LogStrings[i] + Environment.NewLine;
                }
                _LogStrings.Clear();
            }
        }
    }
}