using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lection4_hw.Services;

public class DepositService :  Abstractions.IDepositService
{
    private readonly DAL.Testdb1Context mContext;
    public DepositService(DAL.Testdb1Context ctx)
    {
        mContext = ctx;
    }

    public Task<List<DAL.Entities.Deposit>> GetAllAsync()
    {
        return mContext.Deposits.ToListAsync();
    }
    
}