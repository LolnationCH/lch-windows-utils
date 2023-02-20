using System.CommandLine;

static class CommandLineOptions
{
  public static RootCommand GetRootCommand()
  {
    var rootCommand = new RootCommand
    {
      Description = "LCH windows utilities"
    };


    var listOption = new Option<bool>("--list-process", "List all the process with a Window")
    {
      IsRequired = false
    };

    rootCommand.AddOption(listOption);
    rootCommand.AddCommand(ProcessCommand.GetCommand());
    rootCommand.AddCommand(TaskbarCommand.GetCommand());

    rootCommand.SetHandler((bool listProcess) =>
    {
      if (listProcess)
      {
        ProcessUtils.ListProcess();
        return;
      }
    }, listOption);

    return rootCommand;
  }
}
