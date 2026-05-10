using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace BackEnd_ЛР14_Воробьева_В.Д._241_333.Api.Currency.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet("rates")]
        public async Task<IActionResult> GetRates([FromQuery] string? code)
        {
            var currencySelected = (code ?? Request.Cookies["currencySelected"] ?? "USD").ToUpper();
            var currencyHistorySessionKey = "currencyHistory";

        var apiUrl = _configuration["CurrencyApi:ExchangeRateUrl"];

            if (string.IsNullOrWhiteSpace(apiUrl))
                return BadRequest("URL внешнего API не задан в конфигурации.");

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                    return StatusCode(500, "Не удалось получить данные из внешнего API");

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);

                if (!doc.RootElement.TryGetProperty("conversion_rates", out var ratesElement))
                    return StatusCode(500, "В ответе API отсутствует conversion_rates");

                var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(ratesElement.GetRawText());

                if (rates == null)
                    return StatusCode(500, "Не удалось распарсить conversion_rates");

                // если такой валюты нет
                if (!rates.TryGetValue(currencySelected, out var rate))
                    return NotFound($"Валюта {currencySelected} не найдена.");

                if (code != null)
                {
                    Response.Cookies.Append("currencySelected", currencySelected, new CookieOptions
                    {
                        MaxAge = TimeSpan.FromDays(30),
                        HttpOnly = true

                    });


                    var historyJson = HttpContext.Session.GetString(currencyHistorySessionKey);
                    var history = historyJson is null ? [] : (JsonSerializer.Deserialize<string[]>(historyJson) ?? []);
                    HttpContext.Session.SetString("currencyHistory", JsonSerializer.Serialize(history.Append(currencySelected)));
                }

                // возвращаем только одну валюту
                var result = new
                {
                    date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    code = currencySelected,
                    rate = rate
                };

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Ошибка подключения к внешнему API");
            }
        }

        [HttpGet("cookie")]
        public IActionResult GetCookie()
        {
            var historyJson = HttpContext.Session.GetString("currencyHistory");
            var history = historyJson is null ? [] : (JsonSerializer.Deserialize<string[]>(historyJson) ?? []); 
            return Ok(new
            {

                CurrencyFromCookie = Request.Cookies["currencySelected"],
                RecentCurrencyFromSession = history

            });
        }

    }
}
/*{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet("rates")]
        public async Task<IActionResult> GetRates([FromQuery] string? code)
        {
            var currencySelected = code ?? Request.Cookies["currencySelected"] ?? "USD";

            var apiUrl = _configuration["CurrencyApi:ExchangeRateUrl"];

            if (string.IsNullOrWhiteSpace(apiUrl))
                return BadRequest("URL внешнего API не задан в конфигурации.");

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                    return StatusCode(500, "Не удалось получить данные из внешнего API");

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);

                if (!doc.RootElement.TryGetProperty("conversion_rates", out var ratesElement))
                    return StatusCode(500, "В ответе API отсутствует conversion_rates");

                
                var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(ratesElement.GetRawText());
                


                var result = new
                {
                    date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    code = currencySelected,
                    rates = rates

                };

                if (code != null)
                {
                    Response.Cookies.Append("currencySelected", code, new CookieOptions { MaxAge = TimeSpan.FromDays(30),
                    Path = "/",
                        //SameSite = SameSiteMode.Lax,
                        HttpOnly = false,
                        Secure = true,

                    });
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Ошибка подключения к внешнему API");
            }
        }
    }
} */
