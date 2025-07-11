using Calculator.ViewModel;
using System.Windows;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // assign Viewmodel as Datacontext
            this.DataContext = new CalculatorViewModel();
        }
    }
}
