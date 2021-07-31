using System;
using System.Collections.Generic;

namespace CurrencyToWordsConverter.Validator
{
    public class InputValidator : IInputValidator
    {
        private const decimal MaxNumber = 999999999.99M;
        private List<string> _validationErrors;

        public ValidatorResult Validate(string input)
        {
            _validationErrors = new List<string>();

            var isValid = IsDecimalNumber(input) && HasNoMoreThenTwoDecimals(input) && IsInMaxMinRange(input);
            
            var result = new ValidatorResult
            {
                IsValid = isValid,
                ValidationErrors = _validationErrors
            };

            return result;
        }

        private bool IsDecimalNumber(string input)
        {
            var valid = decimal.TryParse(input, out _);

            if (!valid)
            {
                _validationErrors.Add("Input is not a number in a correct form.");
            }
            
            return valid;
        }

        private bool HasNoMoreThenTwoDecimals(string input)
        {
            var decimalPointLength = input.Substring(input.IndexOf(".", StringComparison.Ordinal) + 1).Length;

            if (decimalPointLength > 2)
            {
                _validationErrors.Add("Input has too many digits after the decimal point.");
                return false;
            }

            return true;
        }

        private bool IsInMaxMinRange(string input)
        {
            var number = decimal.Parse(input);
            var valid = number <= MaxNumber && number >= 0;

            if (!valid)
            {
                _validationErrors.Add($"Input is out of range. Must be non-negative and less than {MaxNumber}.");
            }
            
            return valid;
        }       
    }
}
