using Calculator.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Clipboard = System.Windows.Clipboard;

namespace Calculator.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _expression = "";
        private string _result = "";
        private readonly CalculatorModel _model = new CalculatorModel();

        public string Expression
        {
            get => _expression;
            set { _expression = value; OnPropertyChanged(); }
        }

        public string Result
        {
            get => _result;
            set { _result = value; OnPropertyChanged(); }
        }

        public ICommand InputCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand CalculateCommand { get; }
        public ICommand MemorySaveCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand BackspaceCommand { get; }

        public CalculatorViewModel()
        {
            InputCommand = new RelayCommand(param => Expression += param?.ToString());
            ClearCommand = new RelayCommand(_ => { Expression = ""; Result = ""; });
            CalculateCommand = new RelayCommand(_ => Result = _model.Evaluate(Expression));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
