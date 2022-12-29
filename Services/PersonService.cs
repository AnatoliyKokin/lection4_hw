using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lection4_hw.Services;

public class PersonService :  Abstractions.IPersonService
{
    private readonly DAL.Testdb1Context mContext;
    public PersonService(DAL.Testdb1Context ctx)
    {
        mContext = ctx;
    }

    public Task<List<DAL.Entities.Person>> GetAllAsync()
    {
        return mContext.Persons.ToListAsync();
    }
    
}