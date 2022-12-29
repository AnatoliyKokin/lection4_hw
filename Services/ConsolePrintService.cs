

using System.Reflection;

namespace lection4_hw.Services;

public class ConsolePrintService : Abstractions.IPrintService
{

    public ConsolePrintService()
    {

    }

    public void Print(string text)
    {
        Console.Write(text);
    }

    public void Println(string text)
    {
        Console.WriteLine(text);
    }

    
}