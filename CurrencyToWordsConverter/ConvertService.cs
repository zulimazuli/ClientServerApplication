namespace CurrencyToWordsConverter
{
    public class ConvertService : IConvertService
    {
        public string ConvertNumberToWords(string input)
        {
            using var client = new CurrencyConverter.ConvertServiceReference.ConvertServiceClient();
            return client.ConvertNumberToCurrencyWords(input);
        }
    }
}
