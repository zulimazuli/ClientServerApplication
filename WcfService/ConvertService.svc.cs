using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService.CurrencyToWords;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ConvertService : IConvertService
    {
        public string TestService(decimal value)
        {
            return string.Format("value: {0}", value);
        }

        public string ConvertNumberToCurrencyWords(string value)
        {
            decimal validNumber = decimal.Parse(value);

            CurrencyToWordsConverter converter = new CurrencyToWordsConverter();
            var result = converter.ConvertCurrencyToWords(validNumber);

            return string.Format(result);
        }
    }
}
