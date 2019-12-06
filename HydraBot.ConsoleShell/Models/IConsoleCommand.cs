namespace HydraBot.ConsoleShell.Models
{
    public interface IConsoleCommand
    {
        string Trigger { get; }
        string Execute(string text);
        string Help { get; }
        string HelpArguments { get; }
    }
}