using System;

namespace WcfService.CurrencyToWords
{
    public static class ConverterHelper
    {
        public const string CurrencyName = "dollar";
        public const string CurrencyCentName = "cent";
        public const string CurrencyNamePlural = "dollars";
        public const string CurrencyCentPlural = "cents";
        
        public static string GetPowerName(int power)
        {
            return power switch
            {
                2 => "hundred",
                3 => "thousand",
                6 => "million",
                _ => string.Empty
            };
        }
        
        public static string ConvertDigitToWord(int digit)
        {
            return digit switch
            {
                0 => string.Empty,
                1 => "one",
                2 => "two",
                3 => "three",
                4 => "four",
                5 => "five",
                6 => "six",
                7 => "seven",
                8 => "eight",
                9 => "nine",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static string ConvertTensToWord(int number)
        {
            return number switch
            {
                10 => "ten",
                11 => "eleven",
                12 => "twelve",
                13 => "thirteen",
                14 => "fourteen",
                15 => "fifteen",
                16 => "sixteen",
                17 => "seventeen",
                18 => "eighteen",
                19 => "nineteen",
                20 => "twenty",
                30 => "thirty",
                40 => "forty",
                50 => "fifty",
                60 => "sixty",
                70 => "seventy",
                80 => "eighty",
                90 => "ninety",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}