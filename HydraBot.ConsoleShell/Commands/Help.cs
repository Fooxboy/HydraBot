using HydraBot.ConsoleShell.Models;

namespace HydraBot.ConsoleShell.Commands
{
    public class Helpp:IConsoleCommand
    {
        private readonly  CommandProccessor _proccessor;
        public Helpp(CommandProccessor proccessor)
        {
            _proccessor = proccessor;
        }
        public string Trigger => "help";
        public string Execute(string text)
        {
            var str = string.Empty;
            var commands = _proccessor.Commands;
            foreach (var command in commands)
            {
                str += $"{command.Trigger} {command.HelpArguments} - {command.Help}\n";
            }
            return str;
        }

        public string Help => "Выводит список доступных команд";
        public string HelpArguments => "";
    }
}