using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lection4_hw.Services;

public class CurrencyService :  Abstractions.ICurrencyService
{
    private readonly DAL.Testdb1Context mContext;
    private readonly ILogger<CurrencyService> mLogger;
    public CurrencyService(DAL.Testdb1Context ctx, ILogger<CurrencyService> logger)
    {
        mContext = ctx;
        mLogger = logger;
    }

    public async Task<string?> AddCurrency(DAL.Entities.Currency currency)
    {
        await mContext.Currencies.AddAsync(currency);
        int added = await mContext.SaveChangesAsync();
        string? id = (added>0) ? currency.ShortTitle : null;
        return id;
    }

    public Task<List<DAL.Entities.Currency>> GetAllAsync()
    {
        return mContext.Currencies.ToListAsync();
    }

    private void Log(DAL.Entities.Currency entity, bool result)
    {
        if (result)
        {
            mLogger.LogInformation("Added entity with id = "+entity.ShortTitle);
            return;
        }
        mLogger.LogInformation("Error adding entity with id = "+entity.ShortTitle);
    }

    
}