using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CurrencyConverter.ConvertServiceReference;

namespace CurrencyConverter
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public string InputNumber { get; set; }
        public string OutputText { get; set; }

        private readonly ConvertServiceClient _wcfConvertServiceClient;

        private readonly DelegateCommand _convertNumberToWordsCommand;
        public ICommand ConvertNumberToWordsCommand => _convertNumberToWordsCommand;

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public ViewModel()
        {
            _wcfConvertServiceClient = new ConvertServiceClient();
            _convertNumberToWordsCommand = new DelegateCommand(ConvertNumberToWords);
        }    
        
        private void ConvertNumberToWords(object commandParameter)
        {
            try
            {
                //VALIDATION
                //var prasingResult = decimal.Parse(InputNumber);

                //wcf service goes here
                OutputText = _wcfConvertServiceClient.ConvertNumberToCurrencyWords(InputNumber);
                OnPropertyChanged(nameof(OutputText));
            }
            catch(Exception e)
            {
                MessageBoxResult result = MessageBox.Show($"{e.Message}",
                                          "Error",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
            }
            
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

        public event EventHandler CanExecuteChanged;
        //{
        //    add { throw new NotSupportedException(); }
        //    remove { }
        //}
    }


}