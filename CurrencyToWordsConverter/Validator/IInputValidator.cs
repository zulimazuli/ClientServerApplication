namespace CurrencyToWordsConverter.Validator
{
    public interface IInputValidator
    {
        ValidatorResult Validate(string input);
    }
}