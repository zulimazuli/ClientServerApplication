using System;
using NUnit.Framework;
using WcfService.CurrencyToWords;
using WcfService.Interfaces;

namespace WcfService.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private ICurrencyToWordsConverter _currencyToWordsConverter;

        [SetUp]
        public void Setup()
        {
            _currencyToWordsConverter = new CurrencyToWordsConverter();
        }

        [TestCase(0, ExpectedResult = "zero dollars")]
        [TestCase(1, ExpectedResult = "one dollar")]
        [TestCase(25.1, ExpectedResult = "twenty-five dollars and ten cents")]
        [TestCase(0.01, ExpectedResult = "zero dollars and one cent")]
        [TestCase(45100, ExpectedResult = "forty-five thousand one hundred  dollars")] //modified
        [TestCase(999999999.99, ExpectedResult = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public string ConvertCurrencyToWords_ExampleTests(decimal input)
        {
            return _currencyToWordsConverter.ConvertCurrencyToWords(input);
        }

        [Test]
        public void ConvertCurrencyToWords_0_zero()
        {
            var result = _currencyToWordsConverter.ConvertCurrencyToWords(0);
            Assert.AreEqual(result, "zero dollars");
        }

    }
}
