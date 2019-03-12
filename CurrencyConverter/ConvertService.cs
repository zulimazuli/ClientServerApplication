namespace CurrencyConverter
{
    public class ConvertService : IConvertService
    {
        private readonly ConvertServiceReference.ConvertServiceClient _wcfConvertServiceClient;

        public ConvertService()
        {
            _wcfConvertServiceClient = new ConvertServiceReference.ConvertServiceClient();
        }

        public string ConvertNumberToWords(string input)
        {
            return _wcfConvertServiceClient.ConvertNumberToCurrencyWords(input);
        }
    }

    public interface IConvertService
    {
        string ConvertNumberToWords(string input);
    }
}
