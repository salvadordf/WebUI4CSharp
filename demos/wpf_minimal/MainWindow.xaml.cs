using System.Windows;
using WebUI4CSharp;

namespace wpf_minimal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebUIWindow window = new WebUIWindow();
            window.Show("<html><head><script src=\"webui.js\"></script></head> Hello World ! </html>");
        }
    }
}