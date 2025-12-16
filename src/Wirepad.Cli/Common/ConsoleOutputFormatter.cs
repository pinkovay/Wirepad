using System;
using Wirepad.Cli.Common.Contracts;

namespace Wirepad.Cli.Common;

public class ConsoleOutputFormatter : IOutputFormatter
{
    public void DisplayContent(string content)
    {
        Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");


        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n-------------- [ CONTENT ] ------------------\n");
        Console.ResetColor();
        Console.WriteLine(content);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n---------------------------------------------");
        Console.ResetColor();
    }

    public void DisplayWatchUpdate(string content)
    {
        Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n---------- [ {DateTime.Now:HH:mm:ss} | CONTENT ] -----------\n");
        Console.ResetColor();
        Console.WriteLine(content);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n---------------------------------------------");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Watching... (Ctrl+C to exit)");
        Console.ResetColor();
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor(); 
    }

    public void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void WriteWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
