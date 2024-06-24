using WebUI4CSharp;

namespace console_text_editor
{
    public static class WebUI_Events
    {
        public static void close_app(ref webui_event_t e)
        {
            WebUI.Exit();
        }
    }
}
