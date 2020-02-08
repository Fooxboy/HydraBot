set /p name = "name:":
dotnet ef migrations add  -s C:\Users\Fooxboy\Documents\Projects\HydraBot\HydraBot.ConsoleShell
dotnet ef database update -s C:\Users\Fooxboy\Documents\Projects\HydraBot\HydraBot.ConsoleShell