using CurrencyConverter;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class InputValidatorTests
    {
        private IInputValidator _inputValidator;
        [SetUp]
        public void Setup()
        {
            _inputValidator = new InputValidator();
        }

        [Test]
        public void Validate_NotNumber_ReturnFalse()
        {
            var input = "testtest34";
            var result = _inputValidator.Validate(input);
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_TreeDecimals_ReturnFalse()
        {
            var input = "34,555";
            var result = _inputValidator.Validate(input);
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_OutOfRangeMax_ReturnFalse()
        {
            var input = "12312312399,55";
            var result = _inputValidator.Validate(input);
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_OutOfRangeMin_ReturnFalse()
        {
            var input = "-2";
            var result = _inputValidator.Validate(input);
            Assert.IsFalse(result);
        }

        [TestCase("0")]
        [TestCase("1")]
        [TestCase("41645")]
        [TestCase("132,4")]
        [TestCase("132,00")]
        [TestCase("00,45")]
        [TestCase("00,00")]
        public void Validate_DecimalNumber_ReturnTrue(string input)
        {
            var result = _inputValidator.Validate(input);
            Assert.IsTrue(result);
        }
    }
}