
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace lection4_hw.Services;




public class CurrencyJsonReader : Abstractions.IReader<DAL.Entities.Currency>
{
    private readonly ILogger<CurrencyJsonReader>? mLogger = null;
    
    public CurrencyJsonReader() {}
    public CurrencyJsonReader(ILogger<CurrencyJsonReader> logger) 
    {
        mLogger = logger;
    }

    public DAL.Entities.Currency? Read(string path)
    {
        try
        {
            string jsonStr = File.ReadAllText(path);
            DAL.Entities.Currency? newCurrency = JsonSerializer.Deserialize<lection4_hw.DAL.Entities.Currency>(jsonStr);
            return newCurrency;
        }
        catch(Exception e)
        {
            mLogger?.LogError(e.Message);
        }   
        return null;  
    }
}