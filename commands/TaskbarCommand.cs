using System.CommandLine;

public static class TaskbarCommand
{
  [Flags]
  private enum TaskbarAction
  {
    Show = 0x1,
    Hide = 0x2,
  }

  public static Command GetCommand()
  {
    var actionOption = new Option<TaskbarAction>("--action", "The action to perform on the taskbar");
    actionOption.AddAlias("-a");

    var taskbarCommand = new Command("taskbar", "taskbar actions")
    {
      actionOption,
    };

    taskbarCommand.SetHandler((action) =>
    {
      if (action.HasFlag(TaskbarAction.Show))
        WindowsTaskbar.Show();
      else if (action.HasFlag(TaskbarAction.Hide))
        WindowsTaskbar.Hide();

    }, actionOption);

    return taskbarCommand;
  }
}