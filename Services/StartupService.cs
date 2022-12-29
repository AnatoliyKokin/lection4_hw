using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using lection4_hw.Services.Abstractions;

namespace lection4_hw.Services;

class StartupService : IHostedService
{
    private readonly DAL.Testdb1Context mContext;
    private readonly IServiceProvider mServiceProvider;
    private readonly IHostApplicationLifetime mLifeTime;

    public StartupService(DAL.Testdb1Context context, IServiceProvider provider, IHostApplicationLifetime lifeTime)
    {
        mContext = context;
        mServiceProvider = provider;
        mLifeTime = lifeTime;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            var currenciesService = ServiceProviderServiceExtensions.GetRequiredService<ICurrencyService>(mServiceProvider);

            var printService = ServiceProviderServiceExtensions.GetRequiredService<IPrintService>(mServiceProvider);

            var optionsProvider = ServiceProviderServiceExtensions.GetRequiredService<Options.IOptionsProvider>(mServiceProvider);


            optionsProvider.GetOptions(
            async (options) =>
            {
                await AddCurrencyEntity(options.AddCurrencyFileName, currenciesService, printService);
                await PrintEntities<DAL.Entities.Person, IPersonService>("Persons", printService);
                await PrintEntities<DAL.Entities.Deposit, IDepositService>("Deposits", printService);
                await PrintEntities<DAL.Entities.Currency>("Currencies", currenciesService, printService);
                mLifeTime.StopApplication();
            },
            options =>
            {
                printService.Print(options.ErrorText ?? "");
                printService.Println("");
                mLifeTime.StopApplication();
            });



        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task PrintEntities<T>(string header, IDbService<T> dbService, IPrintService printService) where T : class
    {
        var textConverter = ServiceProviderServiceExtensions.GetRequiredService<ObjectTextConverter<T>>(mServiceProvider);
        ICollection<T> entities = await dbService.GetAllAsync();
        printService.Println(header);
        printService.Print(textConverter.Text(entities));
    }

    private async Task PrintEntities<T, TService>(string header, IPrintService service)
    where T : class
    where TService : IDbService<T>
    {
        var dbService = ServiceProviderServiceExtensions.GetRequiredService<TService>(mServiceProvider);
        await PrintEntities<T>(header, dbService, service);
    }

    private async Task AddCurrencyEntity(string? path, ICurrencyService dbService, IPrintService printService)
    {
        if (path == null) return;
        var reader = ServiceProviderServiceExtensions.GetRequiredService<IReader<DAL.Entities.Currency>>(mServiceProvider);
        DAL.Entities.Currency? newCurrency = reader.Read(path);
        if (newCurrency == null) return;
        try
        {
            string? id = await dbService.AddCurrency(newCurrency);
            if (id != null)
            {
                printService.Println("Added new currency");
            }
        }
        catch(Exception)
        {
            printService.Println("error add currency");
        }
    }

    public void Dispose()
    {
        mContext.Dispose();
    }


}