using HydraBot.ConsoleShell.Services;
using System;
using HydraBot.ConsoleShell.Commands;

namespace HydraBot.ConsoleShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Refine bot Shell loading...");
            Console.Title = "Refine bot";

            var configLoader = new ConfigLoaderService();
            var config = configLoader.LoadConfig();

            var main = new Main(config.IdGroupVKontakte, config.TokenGroupVKontakte, config.TokenBotTelegram);
            main.Start();
            
            var consoleProccessor = new CommandProccessor();
            consoleProccessor.SetCommands(new Helpp(consoleProccessor));
            
            while (true)
            {
                Console.WriteLine();
                Console.Write("Введите команду: ");
                var text = Console.ReadLine();
                consoleProccessor.Start(text);
            }
        }
    }
}
