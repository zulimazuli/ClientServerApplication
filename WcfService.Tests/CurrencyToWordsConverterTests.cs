using System;
using NUnit.Framework;
using WcfService.CurrencyToWords;
using WcfService.Interfaces;

namespace WcfService.Tests
{
    [TestFixture]
    public class CurrencyToWordsConverterTests
    {
        private ICurrencyToWordsConverter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CurrencyToWordsConverter();
        }

        [TestCase(0, ExpectedResult = "zero dollars")]
        [TestCase(1, ExpectedResult = "one dollar")]
        [TestCase(25.1, ExpectedResult = "twenty-five dollars and ten cents")]
        [TestCase(0.01, ExpectedResult = "one cent")]
        [TestCase(0.43, ExpectedResult = "forty-three cents")]
        [TestCase(45100, ExpectedResult = "forty-five thousand one hundred dollars")]
        [TestCase(999999999.99, ExpectedResult = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public string ConvertCurrencyToWords_WithValidInputs_ReturnsCorrectString(decimal input)
        {
            return _sut.ConvertCurrencyToWords(input);
        }
    }
}
