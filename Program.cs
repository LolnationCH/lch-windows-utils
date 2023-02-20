using System.CommandLine;

var rootCommand = CommandLineOptions.GetRootCommand();

return await rootCommand.InvokeAsync(args);
