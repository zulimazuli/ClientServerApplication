using CurrencyToWordsConverter.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyToWordsConverter.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : ControllerBase
    {
        private readonly ILogger<ConvertController> _logger;
        private readonly ICurrencyToWordsConverter _converter;

        public ConvertController(ILogger<ConvertController> logger, ICurrencyToWordsConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        [HttpPost]
        public IActionResult Post(decimal value)
        {
            // todo: validate value
            
            return Ok(_converter.ConvertCurrencyToWords(value));
        }
    }
}