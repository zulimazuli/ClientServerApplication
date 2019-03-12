using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var viewModel = new ViewModel();
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
