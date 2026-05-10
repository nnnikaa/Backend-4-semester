using System.Text.Json.Serialization;

namespace BackEnd_ЛР14_Воробьева_В.Д._241_333.External.HttpClients.Models
{
    public class ExchangeRateApiResponse
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("base_code")]
        public string BaseCode { get; set; }

        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, decimal> ConversionRates { get; set; }
    }
}
