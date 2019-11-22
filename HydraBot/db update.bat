set /p name = "name:":
dotnet ef migrations add %name% -s C:\Users\Fooxboy\source\repos\HydraBot\HydraBot.ConsoleShell
dotnet ef database update -s C:\Users\Fooxboy\source\repos\HydraBot\HydraBot.ConsoleShell