using System.Windows;
using System.Windows.Controls;

namespace WebClientPool.ViewModels
{
    public class WindowService : IWindowService
    {
        public Window View { get; set; }
        public TextBox LogTextBox { get; set; }

        public void ScrollToEndLog()
        {
            View.Dispatcher.Invoke(() =>
            {
                LogTextBox.ScrollToEnd();
            });
        }
    }
}