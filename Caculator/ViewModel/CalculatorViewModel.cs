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
            MemorySaveCommand = new RelayCommand(_ =>
            {
                if (double.TryParse(Result, out var value))
                    _model.SaveToMemory(value);
            });
            MemoryRecallCommand = new RelayCommand(_ =>
            {
                //if (!string.IsNullOrWhiteSpace(Result) && double.TryParse(Result, out var value) && value == _model.Memory)
                //    Expression += _model.Memory.ToString();
                if (_model.HasMemory)
                    Expression += _model.Memory.ToString();
            });
            CopyCommand = new RelayCommand(_ => Clipboard.SetText(Expression));
            PasteCommand = new RelayCommand(_ =>
            {
                var clipboard = Clipboard.GetText();
                if (!string.IsNullOrWhiteSpace(clipboard))
                    Expression += clipboard;
            });
            BackspaceCommand = new RelayCommand(_ =>
            {
                if (!string.IsNullOrEmpty(Expression))
                    Expression = Expression.Substring(0, Expression.Length - 1);
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
