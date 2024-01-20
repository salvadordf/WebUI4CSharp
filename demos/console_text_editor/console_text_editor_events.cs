using WebUI4CSharp;

namespace console_text_editor
{
    public static class WebUI_Events
    {
        public static void Close(ref webui_event_t e)
        {
            WebUI.Exit();
        }
    }
}
