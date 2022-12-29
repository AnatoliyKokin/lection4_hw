using CommandLine;
using CommandLine.Text;


namespace lection4_hw.Options;


public class CommandLineOptionsProvider : IOptionsProvider
{
    private readonly string[] mArgs;
    public CommandLineOptionsProvider(string[] args)
    {
        mArgs = args;
    }

    public void GetOptions(Action<AppOptions> successAction, Action<AppOptions> errorAction)
    {
        CommandLineOptions clo = new CommandLineOptions();
        var parseResult = CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(mArgs).
        WithParsed<CommandLineOptions>(opts =>
        {
            AppOptions appOptions =new AppOptions();
            appOptions.AddCurrencyFileName = opts.AddCurrencyFileName;
            appOptions.ErrorText = null;
            successAction.Invoke(appOptions);
        });

        if (parseResult.Tag == ParserResultType.NotParsed)
        {
            AppOptions appOptions =new AppOptions();
            appOptions.AddCurrencyFileName = null;
            appOptions.ErrorText =  HelpText.AutoBuild<CommandLineOptions>(parseResult, x => x, x => x);
            errorAction.Invoke(appOptions);
        }
        
    }

    /*CommandLineOptions GetOptions()
    {
        var commandLineParseResult = 

        int commandLineErrorCount = 0;
        foreach (var error in commandLineParseResult.Errors)
        {
            //Console.WriteLine(error.ToString());
            commandLineErrorCount++;
        }

        if (commandLineErrorCount > 0) return;
    }*/

    internal class CommandLineOptions
    {
        [Option('a', "add",
        HelpText = "[filename] Файл json с данными для добавления currency в таблицу")]
        public string? AddCurrencyFileName { get; set; }

    }

}