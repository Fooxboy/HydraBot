using HydraBot.ConsoleShell.Models;

namespace HydraBot.ConsoleShell.Commands
{
    public class SetAccess:IConsoleCommand
    {
        public string Trigger => "setaccess";
        public string Execute(string text)
        {
            return "команда недоступна";
        }

        public string Help => "Выдает доступ";
        public string HelpArguments => "";
    }
}