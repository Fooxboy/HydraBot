using System;
using System.Collections.Generic;
using System.Linq;
using HydraBot.ConsoleShell.Models;

namespace HydraBot.ConsoleShell
{
    public class CommandProccessor
    {
        public List<IConsoleCommand> Commands { get; set; }

        public void SetCommands(params IConsoleCommand[] commands)
        {
            Commands = commands.ToList();
        }

        public void Start(string text)
        {
            var com = text.Split(" ");
            try
            {
                var command = Commands.Single(c => c.Trigger == com[0]);
                try
                {
                    var response = command.Execute(text);
                    Console.WriteLine($"[CONSOLE OK]: {response}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[CONSOLE ERROR]: Произошла ошибка при выполнении команды {com}: \n {e}");
                }
            }
            catch
            {
                Console.WriteLine("[CONSOLE ERROR]: Неизвестная команда.");
            }
        }
    }
}