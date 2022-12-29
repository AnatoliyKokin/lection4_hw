

namespace lection4_hw.Services.Abstractions;

public interface ICurrencyService : IDbService<DAL.Entities.Currency>
{
    Task<string?> AddCurrency(DAL.Entities.Currency currency);
}