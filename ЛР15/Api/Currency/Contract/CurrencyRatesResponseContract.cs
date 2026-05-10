using System.Reflection;

namespace BackEnd_ЛР14_Воробьева_В.Д._241_333.Api.Habits.Contract
{
    public class CurrencyRatesResponseContract
    {
        public required string BaseCurrency { get; init; }

        public required DateTimeOffset LoadedAt { get; init; }

        public required Dictionary<string, decimal> Rates { get; init; }


    }
}
