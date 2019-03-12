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
        public CurrencyToWordsConverter()
        {
            helper = new ConverterHelper();
        }

        public string ConvertCurrencyToWords(decimal value)
        {
            if (value == 0)
                return "zero";                     
                        
            var wholeNumber = decimal.Truncate(value);
            var points = (value - wholeNumber) * 100;

            StringBuilder numberBuilder = new StringBuilder();
            if (wholeNumber > 0)
            {
                int power = 6;
                while (power >= 0)
                {
                    int partNumber = decimal.ToInt32(wholeNumber % (int)Math.Pow(10, power + 3) / (int)Math.Pow(10, power));
                    if (partNumber > 0)
                    {
                        numberBuilder.Append(ConvertNumberToWords(partNumber));
                        numberBuilder.Append(helper.GetPowerName(power));
                    }
                    power -= 3;
                }

                numberBuilder.Append(" dollars");
            }
            
            if(points > 0)
            {
                numberBuilder.Append(" and ");
                numberBuilder.Append(ConvertNumberToWords((int)points));
                numberBuilder.Append(" cents");
            }
            
            return numberBuilder.ToString();
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
                sb.Append(" hundred ");
            }

            if (tens == 0)
            {
                sb.Append(helper.ConvertDigitToWord(unity));
            }
            else if (tens > 0 && tens < 2)
            {
                sb.Append(helper.ConvertTeensToWord(tens*10 + unity));
                
            }
            else if(tens >= 2)
            {
                sb.Append(helper.ConvertTeensToWord(tens*10));
                sb.Append(unity > 0 ? "-" : "");
                sb.Append(helper.ConvertDigitToWord(unity));

            }

            return sb.ToString();
        }

        
    }
}