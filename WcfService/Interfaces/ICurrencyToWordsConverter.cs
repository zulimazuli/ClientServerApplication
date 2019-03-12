using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService.Interfaces
{
    public interface ICurrencyToWordsConverter
    {
        string ConvertCurrencyToWords(decimal value);
    }
}
