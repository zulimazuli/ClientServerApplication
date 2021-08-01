using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurrencyToWordsConverter.API.Helpers;
using CurrencyToWordsConverter.API.Interfaces;

namespace CurrencyToWordsConverter.API.Converter
{
    public class CurrencyToWordsConverter : ICurrencyToWordsConverter
    {
        private const int MaxPower = 6;

        public string ConvertCurrencyToWords(decimal value)
        {
            if (value == 0)
            {
                return GetZeroDollars();
            }
            
            var words = new List<string>();

            var wholeNumber = decimal.Truncate(value);
            var fraction = (value - wholeNumber) * 100;
            
            if (wholeNumber > 0)
            {
                var power = MaxPower;
                while (power >= 0)
                {
                    var partNumber = decimal.ToInt32(wholeNumber % (int)Math.Pow(10, power + 3) / (int)Math.Pow(10, power));
                    if (partNumber > 0)
                    {
                        words.Add(ConvertNumberToWords(partNumber));
                        words.Add(ConverterHelper.GetPowerName(power));
                    }
                    power -= 3;
                }

                var currency = wholeNumber == 1 ? ConverterHelper.CurrencyName : ConverterHelper.CurrencyNamePlural;
                words.Add(currency);
            }

            if (fraction > 0)
            {
                if (words.Count > 0)
                {
                    words.Add("and");
                }
                
                words.Add(ConvertNumberToWords((int)fraction));

                var currencyCent = fraction == 1 ? ConverterHelper.CurrencyCentName : ConverterHelper.CurrencyCentPlural;
                words.Add(currencyCent);
            }

            var result = string.Join(" ", words.Where(w => !string.IsNullOrEmpty(w)));
            
            return result;
        }

        private static string GetZeroDollars()
        {
            return $"zero {ConverterHelper.CurrencyNamePlural}";
        }

        private static string ConvertNumberToWords(int number)
        {
            var hundreds = (number % 1000) / 100;
            var tens = (number % 100) / 10;
            var unity = number % 10;

            var sb = new StringBuilder();

            if(hundreds > 0)
            {
                sb.Append(ConverterHelper.ConvertDigitToWord(hundreds));
                sb.Append($" {ConverterHelper.GetPowerName(2)} ");
            }

            if (tens == 0)
            {
                sb.Append(ConverterHelper.ConvertDigitToWord(unity));
            }
            else if (tens > 0 && tens < 2)
            {
                sb.Append(ConverterHelper.ConvertTensToWord(10 + unity));
                
            }
            else if(tens >= 2)
            {
                sb.Append(ConverterHelper.ConvertTensToWord(tens * 10));
                sb.Append(unity > 0 ? "-" : string.Empty);
                sb.Append(ConverterHelper.ConvertDigitToWord(unity));

            }

            return sb.ToString().Trim();
        }        
    }
}