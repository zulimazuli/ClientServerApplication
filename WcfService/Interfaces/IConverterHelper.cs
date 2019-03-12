namespace WcfService.Interfaces
{
    public interface IConverterHelper
    {
        string GetPowerName(int power);
        string ConvertDigitToWord(int digit);
        string ConvertTensToWord(int number);
    }
}