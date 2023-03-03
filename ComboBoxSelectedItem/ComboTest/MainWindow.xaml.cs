using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComboTest.Models;
using ComboTest.ViewModels;

namespace ComboTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var model = new MainWindowModel();
            var vm = new MainWindowViewModel(model);
            DataContext = vm;

            Loaded += (sender, args) =>
            {
                model.Init();
            };

            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        this.Dispatcher.Invoke(() =>
            //        {
            //            Debug.WriteLine($"Count: {Invalid.Items.Count} Selected: {Invalid.SelectedItem ?? "NULL"}");
            //        });

            //        Task.Delay(1000);
            //    }
            //});
        }
    }
}
