using freecurrencyapi;
using Microsoft.Extensions.Configuration;

namespace CurrencyRates
{
    public class ApiService
    {
        private readonly IConfiguration _configuration;
        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetRates()
        {
            string apiKey = _configuration["Freecurrencyapi_ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("API Key is missing. Please set environment variable: Freecurrencyapi_ApiKey");
            }

            string currencies = _configuration["Freecurrencyapi_Currencies"];
            if (string.IsNullOrEmpty(currencies))
            {
                throw new Exception("Currencies are missing. Please set environment variable: Freecurrencyapi_Currencies (Currency codes separated by comma)");
            }
            
            string baseCurrency = _configuration["Freecurrencyapi_BaseCurrency"];
            if (string.IsNullOrEmpty(baseCurrency))
            {
                baseCurrency = "EUR";
            }

            var fx = new Freecurrencyapi(apiKey);
            var rates = await Task.Run(() =>
            {
                return fx.Latest(baseCurrency, currencies);
            });

            return rates;
        }
    }
}
