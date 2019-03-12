using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyConverter
{
    public interface IInputValidator
    {
        bool Validate(string input);
        List<string> GetValidationErrors();
    }

    public class InputValidator : IInputValidator
    {
        private const decimal _maxNumber = 999999999.99M;
        private string _inputToValidate;
        private List<string> _validationErrors;

        public bool Validate(string input)
        {
            _inputToValidate = input;
            _validationErrors = new List<string>();

            var valid = IsDecimalNumber() 
                        && HasNoMoreThenTwoDecimals() 
                        && IsInMaxMinRange();
            return valid;
        }

        public List<string> GetValidationErrors()
        {
            return _validationErrors;
        }

        private bool IsDecimalNumber()
        {
            bool valid = decimal.TryParse(_inputToValidate, out decimal _);

            if (!valid) _validationErrors.Add("Input is not a number in a correct form.");
            return valid;
        }

        private bool HasNoMoreThenTwoDecimals()
        {
            bool valid = true;
            var array = _inputToValidate.ToString().Split(',');
            if(array.Length == 2)
            {
                valid = array[1].Length <= 2;
            }

            if (!valid) _validationErrors.Add("Input has too many digits after the decimal point.");
            return valid;
        }

        private bool IsInMaxMinRange()
        {
            decimal input = decimal.Parse(_inputToValidate);
            bool valid = input <= _maxNumber && input >= 0;

            if (!valid) _validationErrors.Add($"Input is out of range. Must be non-negative and less than {_maxNumber}.");
            return valid;
        }       
    }
}
