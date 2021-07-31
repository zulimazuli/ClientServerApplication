using System.Collections.Generic;

namespace CurrencyToWordsConverter.Validator
{
    public class ValidatorResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; } = new List<string>();
    }
}