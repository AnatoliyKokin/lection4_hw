using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder().ConfigureAppConfiguration(
    cfg =>
    {
        cfg.SetBasePath(Directory.GetCurrentDirectory());
        cfg.AddJsonFile("appsettings.json");
    }
).ConfigureServices((context,services) => 
{
    services.AddDbContext<lection4_hw.DAL.Testdb1Context>( 
        options => 
    { options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")); });
    
    services.AddSingleton<lection4_hw.Options.IOptionsProvider>(new lection4_hw.Options.CommandLineOptionsProvider(args));
    services.AddScoped<lection4_hw.Services.Abstractions.IReader<lection4_hw.DAL.Entities.Currency>,lection4_hw.Services.CurrencyJsonReader>();
    services.AddSingleton(typeof(lection4_hw.Services.ObjectTextConverter<lection4_hw.DAL.Entities.Currency>));
    services.AddSingleton(typeof(lection4_hw.Services.ObjectTextConverter<lection4_hw.DAL.Entities.Person>));
    services.AddSingleton(typeof(lection4_hw.Services.ObjectTextConverter<lection4_hw.DAL.Entities.Deposit>));
    services.AddSingleton<lection4_hw.Services.Abstractions.IPrintService,lection4_hw.Services.ConsolePrintService>();
    services.AddSingleton<lection4_hw.Services.Abstractions.ICurrencyService,lection4_hw.Services.CurrencyService>();
    services.AddSingleton<lection4_hw.Services.Abstractions.IDepositService,lection4_hw.Services.DepositService>();
    services.AddSingleton<lection4_hw.Services.Abstractions.IPersonService,lection4_hw.Services.PersonService>();
    services.AddHostedService<lection4_hw.Services.StartupService>();
}).Build();

host.Run();
