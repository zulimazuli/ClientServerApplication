using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WcfService.Interfaces;

namespace WcfService.CurrencyToWords
{
    public class CurrencyToWordsConverter : ICurrencyToWordsConverter
    {
        private readonly IConverterHelper helper;
        private const int _maxPower = 6;

        private const string _currencyName = "dollar";
        private const string _currencyCent = "cent";
        private const string _currencyNamePlural = "dollars";
        private const string _currencyCentPlural = "cents";

        public CurrencyToWordsConverter()
        {


            helper = new ConverterHelper();
        }

        public string ConvertCurrencyToWords(decimal value)
        {
            if (value == 0)
                return GetZeroDollars();

            var wholeNumber = decimal.Truncate(value);
            var points = (value - wholeNumber) * 100;

            StringBuilder wholeBuilder = new StringBuilder();
            if (wholeNumber > 0)
            {
                int power = _maxPower;
                while (power >= 0)
                {
                    int partNumber = decimal.ToInt32(wholeNumber % (int)Math.Pow(10, power + 3) / (int)Math.Pow(10, power));
                    if (partNumber > 0)
                    {
                        wholeBuilder.Append(ConvertNumberToWords(partNumber));
                        wholeBuilder.Append(helper.GetPowerName(power));
                    }
                    power -= 3;
                }

                var currency = wholeNumber == 1 ? _currencyName : _currencyNamePlural;
                wholeBuilder.Append(" ").Append(currency);
            }
            else
            {
                wholeBuilder.Append(GetZeroDollars());
            }

            StringBuilder pointsBuilder = new StringBuilder();
            if (points > 0)
            {
                pointsBuilder.Append(" and ");
                pointsBuilder.Append(ConvertNumberToWords((int)points));

                var currencyCent = points == 1 ? _currencyCent : _currencyCentPlural;
                pointsBuilder.Append(" ").Append(currencyCent);
            }

            var result = wholeBuilder.ToString() + pointsBuilder.ToString();
            
            //todo: to fix - ugly workaround
            result = result.Replace("  ", " ");

            return result;
        }

        private string GetZeroDollars()
        {
            return string.Format("{0} {1}", "zero", _currencyNamePlural);
        }

        private string ConvertNumberToWords(int number)
        {
            if (number > 999)
                throw new ArgumentOutOfRangeException();

            var hundreds = (int)((number % 1000) / 100);
            var tens = (int)((number % 100) / 10);
            var unity = (int)(number % 10);

            StringBuilder sb = new StringBuilder();

            if(hundreds > 0)
            {
                sb.Append(helper.ConvertDigitToWord(hundreds));
                sb.Append(helper.GetPowerName(2));
            }

            if (tens == 0)
            {
                sb.Append(helper.ConvertDigitToWord(unity));
            }
            else if (tens > 0 && tens < 2)
            {
                sb.Append(helper.ConvertTensToWord(tens*10 + unity));
                
            }
            else if(tens >= 2)
            {
                sb.Append(helper.ConvertTensToWord(tens*10));
                sb.Append(unity > 0 ? "-" : "");
                sb.Append(helper.ConvertDigitToWord(unity));

            }

            return sb.ToString();
        }        
    }
}