namespace WcfService.Interfaces
{
    public interface IConverterHelper
    {
        string GetPowerName(int power);
        string ConvertDigitToWord(int digit);
        string ConvertTeensToWord(int number);
    }
}