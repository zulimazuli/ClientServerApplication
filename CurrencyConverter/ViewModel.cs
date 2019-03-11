using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CurrencyConverter
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public decimal InputNumber { get; set; }
        public string OutputText { get; set; }

        private readonly DelegateCommand _convertNumberToWordsCommand;
        public ICommand ConvertNumberToWordsCommand => _convertNumberToWordsCommand;

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public ViewModel()
        {
            _convertNumberToWordsCommand = new DelegateCommand(ConvertNumberToWords);
        }    
        
        private void ConvertNumberToWords(object commandParameter)
        {
            //wcf service goes here
            OutputText = "Changes were made.";
            OnPropertyChanged(nameof(OutputText));
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeAction;

        public DelegateCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
        }

        public void Execute(object parameter) => _executeAction(parameter);

        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged
        {
            add { throw new NotSupportedException(); }
            remove { }
        }
    }


}