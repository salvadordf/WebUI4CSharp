using WebUI4CSharp;

namespace text_editor
{
    public static class WebUI_Events
    {
        public static void Close(ref webui_event_t e)
        {
            WebUI.Exit();
        }
    }
}
