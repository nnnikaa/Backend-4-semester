using BackEnd_ЛР14_Воробьева_В.Д._241_333.External.HttpClients.Models;

namespace BackEnd_ЛР14_Воробьева_В.Д._241_333.External.HttpClients
{
    public sealed class CurrencyHttpClient
    {
        private readonly HttpClient _httpClient;

        public CurrencyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRateApiResponse?> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken)
        {
            baseCurrency = baseCurrency.ToUpper();

            var response = await _httpClient.GetAsync($"latest/{baseCurrency}", cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ExchangeRateApiResponse>(cancellationToken: cancellationToken);
        }

    }
}
