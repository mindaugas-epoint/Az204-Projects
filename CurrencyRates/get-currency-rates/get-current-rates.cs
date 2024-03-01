using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CurrencyRates
{
    public class get_current_rates
    {
        private readonly ILogger<get_current_rates> _logger;
        private readonly ApiService _apiService;

        public get_current_rates(ILogger<get_current_rates> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [Function("get_current_rates")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Getting currency rates");

            var rates = await _apiService.GetRates();

            return new JsonResult(rates);
        }
    }
}
