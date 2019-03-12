using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Interfaces;

namespace WcfService.CurrencyToWords
{
    public class ConverterHelper : IConverterHelper
    {
        private Dictionary<int, string> PowerName = new Dictionary<int, string>
            {
                {0, "" },
                {2, " hundred " },
                {3, " thousand " },
                {6, " million " }
            };

        public ConverterHelper()
        {

        }
        public string GetPowerName(int power)
        {
            return PowerName[power];
        }
        public string ConvertDigitToWord(int digit)
        {
            switch (digit)
            {
                case 0: return string.Empty;
                case 1: return "one";
                case 2: return "two";
                case 3: return "three";
                case 4: return "four";
                case 5: return "five";
                case 6: return "six";
                case 7: return "seven";
                case 8: return "eight";
                case 9: return "nine";
                default:
                    throw new ArgumentOutOfRangeException();
            };
        }

        public string ConvertTeensToWord(int number)
        {
            switch (number)
            {
                case 10: return "ten";
                case 11: return "eleven";
                case 12: return "twelve";
                case 13: return "thirteen";
                case 14: return "fourteen";
                case 15: return "fiveteen";
                case 16: return "sixteen";
                case 17: return "seventeen";
                case 18: return "eighteen";
                case 19: return "nineteen";
                case 20: return "twenty";
                case 30: return "thirty";
                case 40: return "forty";
                case 50: return "fifty";
                case 60: return "sixty";
                case 70: return "seventy";
                case 80: return "eighty";
                case 90: return "ninety";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}