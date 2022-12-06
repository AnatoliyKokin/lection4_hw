using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;


var builder = new ConfigurationBuilder();
// установка пути к текущему каталогу
builder.SetBasePath(Directory.GetCurrentDirectory());
// получаем конфигурацию из файла appsettings.json
builder.AddJsonFile("appsettings.json");
// создаем конфигурацию
var config = builder.Build();

// получаем строку подключения
string? connectionString = config.GetConnectionString("DefaultConnection");
if (connectionString==null)
{
    Console.WriteLine("неверная строка подключения к бд в настройках appsettings.json");
}

var optionsBuilder = new DbContextOptionsBuilder<lection4_hw.DAL.Testdb1Context>();

var options = optionsBuilder
            .UseNpgsql(connectionString)
            .Options;

var commandLineParseResult = CommandLine.Parser.Default.ParseArguments<lection4_hw.CommandLineOptions>(args);

int commandLineErrorCount = 0;
foreach(var error in commandLineParseResult.Errors)
{
    //Console.WriteLine(error.ToString());
    commandLineErrorCount++;
}

if (commandLineErrorCount>0) return;

await using var ctx = new lection4_hw.DAL.Testdb1Context(options);

string? addFilePath = commandLineParseResult.Value.AddCurrencyFileName;
if (!string.IsNullOrEmpty(addFilePath))
{
    try
    {
       string jsonStr = File.ReadAllText(addFilePath);

       try
       {   
           lection4_hw.DAL.Entities.Currency? newCurrency = JsonSerializer.Deserialize<lection4_hw.DAL.Entities.Currency>(jsonStr);
           if (newCurrency!=null)
           {
                try
                {
                    await ctx.AddAsync(newCurrency); 
                    var result = await ctx.SaveChangesAsync();
                    if (result==1)
                    {
                        Console.WriteLine("Добавлена запись: "+newCurrency.ShortTitle+" "+newCurrency.LongTitle+" "+newCurrency.Country);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Ошибка добавления записи: "+newCurrency.ShortTitle+" "+newCurrency.LongTitle+" "+newCurrency.Country);
                    Console.WriteLine(e.ToString());
                }
           }

       }
       catch(Exception e)
       {
           Console.WriteLine("Не удалось получить объект типа currency из json");
           Console.WriteLine(e.ToString());
       }

    }
    catch(Exception e)
    {
           Console.WriteLine("Не удалось прочитать файл "+addFilePath);
           Console.WriteLine(e.ToString());
    }
}

Console.WriteLine("Persons");
foreach(var it in ctx.Persons)
{
    Console.WriteLine(it.FirstName+" "+it.LastName+" "+it.Passport);
}

Console.WriteLine("Deposits:");
foreach(var it in ctx.Deposits)
{
    Console.WriteLine(it.DepoNumber+" "+it.Person+" "+it.Currency+" "+it.Balance);
}

Console.WriteLine("Currencies:");
foreach(var it in ctx.Currencies)
{
    Console.WriteLine(it.ShortTitle+" "+it.LongTitle+" "+it.Country);
}



