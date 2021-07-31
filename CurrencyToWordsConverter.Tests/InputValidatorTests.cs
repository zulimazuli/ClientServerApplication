using CurrencyToWordsConverter.Validator;
using NUnit.Framework;
using FluentAssertions;

namespace CurrencyToStringConverter.Tests
{
    [TestFixture]
    public class InputValidatorTests
    {
        private IInputValidator _sut;
        
        [SetUp]
        public void Setup()
        {
            _sut = new InputValidator();
        }

        [Test]
        public void Validate_NotNumber_ReturnFalse()
        {
            const string input = "testtest34";
            var validation = _sut.Validate(input);
            
            validation.IsValid.Should().BeFalse();
            validation.ValidationErrors.Should().ContainSingle();
        }

        [Test]
        public void Validate_TreeDecimals_ReturnFalse()
        {
            const string input = "34,553";
            var validation = _sut.Validate(input);

            validation.IsValid.Should().BeFalse();
            validation.ValidationErrors.Should().ContainSingle();

        }

        [Test]
        public void Validate_OutOfRangeMax_ReturnFalse()
        {
            const string input = "12312312399,55";
            var validation = _sut.Validate(input);
            
            validation.IsValid.Should().BeFalse();
            validation.ValidationErrors.Should().ContainSingle();
        }

        [Test]
        public void Validate_OutOfRangeMin_ReturnFalse()
        {
            const string input = "-2";
            var validation = _sut.Validate(input);
            
            validation.IsValid.Should().BeFalse();
            validation.ValidationErrors.Should().ContainSingle();
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
            var validation = _sut.Validate(input);
            
            validation.IsValid.Should().BeTrue();
            validation.ValidationErrors.Should().BeEmpty();
        }
    }
}