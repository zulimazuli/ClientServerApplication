using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CurrencyConverter
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public string InputNumber { get; set; }
        public string OutputText { get; set; }

        private readonly IConvertService _wcfConvertService;
        private readonly IInputValidator _inputValidator;

        private readonly DelegateCommand _convertCommand;
        public ICommand ConvertCommand => _convertCommand;

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public ViewModel()
        {
            _wcfConvertService = new ConvertService();
            _inputValidator = new InputValidator();
            _convertCommand = new DelegateCommand(ConvertToWordsCommand);
        }    
        
        private void ConvertToWordsCommand(object commandParameter)
        {
            if (_inputValidator.Validate(InputNumber))
            {
                try
                {
                    OutputText = _wcfConvertService.ConvertNumberToWords(InputNumber);
                    OnPropertyChanged(nameof(OutputText));
                }
                catch (Exception e)
                {
                    MessageBoxResult result = MessageBox.Show($"{e.Message}", "Error",
                                                MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                ShowIncorrectInputMessageBox();
            }
            
        }

        private void ShowIncorrectInputMessageBox()
        {
            var message = string.Join("\n", _inputValidator.GetValidationErrors());
            MessageBoxResult result = MessageBox.Show(message, "Error",
                                                        MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }


}