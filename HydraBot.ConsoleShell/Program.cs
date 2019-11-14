using HydraBot.ConsoleShell.Services;
using System;

namespace HydraBot.ConsoleShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hydra bot Shell...");
            Console.Title = "Hydra bot";

            var configLoager = new ConfigLoaderService();
            var config = configLoager.LoadConfig();

            var main = new Main(config.IdGroupVKontakte, config.TokenGroupVKontakte, config.TokenBotTelegram);
            main.Start();
            Console.ReadLine();
        }
    }
}
