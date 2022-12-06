using CommandLine;

namespace lection4_hw;

class CommandLineOptions
{
    [Option('a', "add",
	HelpText = "[filename] Файл json с данными для добавления currency в таблицу")]
  public string? AddCurrencyFileName { get; set; }

}