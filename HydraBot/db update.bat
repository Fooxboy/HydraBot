set /p name = "name:":
dotnet ef migrations add  -s C:\Users\Fooxboy\RiderProjects\HydraBot\HydraBot.ConsoleShell
dotnet ef database update -s C:\Users\Fooxboy\RiderProjects\HydraBot\HydraBot.ConsoleShell