using BackEnd_ЛР14_Воробьева_В.Д._241_333.Api.Currency.Service;
using BackEnd_ЛР14_Воробьева_В.Д._241_333.Api.Habits.Contract;
using BackEnd_ЛР14_Воробьева_В.Д._241_333.External.HttpClients;

namespace BackEnd_ЛР14_Воробьева_В.Д._241_333.Api.Habits.Service
{
    
    public sealed class CurrencyService : ICurrencyService
    {
        private readonly CurrencyHttpClient _client;

        public CurrencyService(CurrencyHttpClient client)
        {
            _client = client;
        }

        public async Task<CurrencyRatesResponseContract?> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken)
        {
            var data = await _client.GetLatestAsync(baseCurrency, cancellationToken);

            if (data == null || data.Result != "success")
                return null;

            return new CurrencyRatesResponseContract
            {
                BaseCurrency = data.BaseCode,
                LoadedAt = DateTimeOffset.UtcNow,
                Rates = data.ConversionRates
            };
        }
    }
    
}
