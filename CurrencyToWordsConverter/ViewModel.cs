using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using CurrencyToWordsConverter.Validator;

namespace CurrencyToWordsConverter
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
            var validation = _inputValidator.Validate(InputNumber);
            if (validation.IsValid)
            {
                try
                {
                    OutputText = _wcfConvertService.ConvertNumberToWords(InputNumber);
                    OnPropertyChanged(nameof(OutputText));
                }
                catch (EndpointNotFoundException e)
                {
                    MessageBox.Show($"Could not connect to the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (Exception e)
                {
                    MessageBox.Show($"{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                ShowErrorMessageBox(validation.ValidationErrors);
            }
            
        }

        private static void ShowErrorMessageBox(IEnumerable<string> errors)
        {
            var message = string.Join("\n", errors);
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}