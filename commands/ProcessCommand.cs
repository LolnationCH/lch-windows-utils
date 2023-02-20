using System.CommandLine;

public static class ProcessCommand
{
  [Flags]
  private enum ProcessAction
  {
    Show = 0x1,
    Hide = 0x2,
    RemoveFromTaskbar = 0x4,
    AddToTaskbar = 0x8,
  }

  private static List<Action<string>>? GetActions(ProcessAction action)
  {
    var actions = new List<Action<string>>();
    if (action.HasFlag(ProcessAction.Show))
    {
      actions.Add(ProcessUtils.ShowProcess);
    }
    if (action.HasFlag(ProcessAction.Hide))
    {
      actions.Add(ProcessUtils.HideProcess);
    }
    if (action.HasFlag(ProcessAction.RemoveFromTaskbar))
    {
      actions.Add(ProcessUtils.RemoveProcessFromTaskbar);
    }
    if (action.HasFlag(ProcessAction.AddToTaskbar))
    {
      actions.Add(ProcessUtils.AddProcessToTaskbar);
    }
    return actions;
  }

  public static Command GetCommand()
  {
    var processNameOption = new Option<string>("--name", "The process name");
    processNameOption.AddAlias("-n");
    var processActionOption = new Option<ProcessAction>("--actions", "The actions to perform on the process");
    processActionOption.AddAlias("-a");

    var processCommand = new Command("process", "process actions")
    {
      processNameOption,
      processActionOption,
    };

    processCommand.SetHandler((name, processAction) =>
    {
      var action = GetActions(processAction);

      if (action != null)
        action.ForEach(x => x(name));
      else
        Console.WriteLine("No action specified");
    }, processNameOption, processActionOption);

    return processCommand;
  }
}