using System.Windows;
using WebClientPool.ViewModels;

namespace WebClientPool.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var model = new Models.MainWindowModel();
            var viewModel = new MainWindowViewModel(new WindowService
            {
                View = this,
                LogTextBox = LogTextBox
            }, model);
            DataContext = viewModel;
        }
    }
}
