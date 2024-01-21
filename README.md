# WebUI4CSharp [![Tweet](https://img.shields.io/twitter/url/http/shields.io.svg?style=social)](https://twitter.com/intent/tweet?text=Add%20WebUI4Delphi%20to%20your%20applications%20to%20use%20any%20web%20browser%20as%20a%20GUI%20in%20your%20application&url=https://github.com/salvadordf/WebUI4CSharp&via=briskbard&hashtags=WebUI4CSharp,csharp,webui)
WebUI4CSharp is a [WebUI](https://github.com/webui-dev/webui) wrapper, which allows you to use any web browser as a GUI, with C# in the backend and HTML5 in the frontend. 

WebUI allows you to link your console, WinForms or WPF application with a web app that runs in a web browser installed in the operating system. Originally WebUI was created to have all the UI code in the web browser and the rest of the code in your hidden C# application.
However, you can also decide to have a visible C# application communicating with a HTML5 app. You can get web browser events in your desktop application, call C# functions from JS, call JS functions from C# code, execute JavaScript, etc.

WebUI4CSharp can be used in 64 bit console, WinForms or WPF applications for Windows. 

WebUI doesn't embed a web browser in your application. It's used as a bridge between a desktop application and the web browser running an HTML5 app. 


## Features

- Fully Independent (*No need for any third-party runtimes*)
- Lightweight & Small memory footprint
- Fast binary communication protocol between WebUI and the browser (*Instead of JSON*)
- Multi-platform & Multi-Browser
- Using private profile for safety
- Original library written in Pure C
- XML documentation.


## Minimal Example

```cs
﻿using WebUI4CSharp;

WebUIWindow window = new WebUIWindow();
window.Show("<html><head><script src=\"webui.js\"></script></head> Hello World ! </html>");
WebUI.Wait();
```

[More examples](https://github.com/salvadordf/WebUI4CSharp/tree/main/demos)


## Text editor

This [text_editor](https://github.com/salvadordf/WebUI4CSharp/tree/main/demos/console_text_editor) is a lightweight and portable example written in C# and JavaScript using WebUI as the GUI.

![text_editor](https://github.com/salvadordf/WebUI4Delphi/assets/17946341/306533de-5885-4bab-9c05-1627ea9b9bc8)


## Installation

* Open the file WebUI4CSharp.sln.
* 


## Links
* [Developer Forums](https://www.briskbard.com/forum)
* [WebUI project](https://github.com/webui-dev/webui) 
* [C API documentation](https://webui.me/docs/#/c_api)

## Support
If you find this project useful, please consider making a donation.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=FTSD2CCGXTD86)

You can also support this project with Patreon.

<a href="https://patreon.com/salvadordf"><img src="https://c5.patreon.com/external/logo/become_a_patron_button.png" alt="Patreon donate button" /></a>

You can also support this project with Liberapay.

<a href="https://liberapay.com/salvadordf/donate"><img alt="Donate using Liberapay" src="https://liberapay.com/assets/widgets/donate.svg"></a>

## Related projects 
* [CEF4Delphi](https://github.com/salvadordf/CEF4Delphi) 
* [WebView4Delphi](https://github.com/salvadordf/WebView4Delphi)
* [WebUI4Delphi](https://github.com/salvadordf/WebUI4Delphi)
