
using BackEnd_ЛР14_Воробьева_В.Д._241_333.Api.Habits.Contract;

namespace BackEnd_ЛР14_Воробьева_В.Д._241_333.Api.Currency.Service
{
    public interface ICurrencyService
    {

        Task<CurrencyRatesResponseContract?> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken);
    }
}
