using System.Diagnostics;
using System.Runtime.InteropServices;

public static class ProcessUtils
{
  public static void ListProcess()
  {
    Console.WriteLine("List of process:");
    Console.WriteLine(String.Join(Environment.NewLine,
                                    Process.GetProcesses()
                                    .Where(x => x.MainWindowHandle != IntPtr.Zero)
                                    .Select(x => x.ProcessName)
                                    .Distinct()));
  }

  public static void ShowProcess(string processName)
  {
    var processes = Process.GetProcessesByName(processName);
    foreach (var process in processes)
    {
      ShowWindow(process.MainWindowHandle, SW_SHOW);
      SwitchToThisWindow(process.MainWindowHandle);
    }
  }

  public static void HideProcess(string processName)
  {
    var processes = Process.GetProcessesByName(processName);
    foreach (var process in processes)
    {
      ShowWindow(process.MainWindowHandle, SW_MINIMIZE);
    }
  }

  public static void RemoveProcessFromTaskbar(string processName)
  {
    var processes = Process.GetProcessesByName(processName);
    foreach (var process in processes)
    {
      RemoveFromAltTab(process.MainWindowHandle);
    }
  }

  public static void AddProcessToTaskbar(string processName)
  {
    var processes = Process.GetProcessesByName(processName);
    foreach (var process in processes)
    {
      AddToAltTab(process.MainWindowHandle);
    }
  }

  #region Windows API
  // import the stuff to hide the window
  [DllImport("user32.dll", SetLastError = true)]
  static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
  const int SW_HIDE = 0;
  const int SW_SHOW = 5;
  private const int SW_MINIMIZE = 6;


  [DllImport("user32.dll")]
  public static extern int SetWindowLong(IntPtr window, int index, int value);
  [DllImport("user32.dll")]
  public static extern int GetWindowLong(IntPtr window, int index);


  const int GWL_EXSTYLE = -20;
  const int WS_EX_TOOLWINDOW = 0x00000080;
  const int WS_EX_APPWINDOW = 0x00040000;

  [DllImport("user32.dll", SetLastError = true)]
  static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

  public static void SwitchToThisWindow(IntPtr hWnd)
  {
    SwitchToThisWindow(hWnd, true);
  }

  public static void RemoveFromAltTab(IntPtr Handle)
  {
    //Make it gone from the ALT+TAB
    int windowStyle = GetWindowLong(Handle, GWL_EXSTYLE);
    SetWindowLong(Handle, GWL_EXSTYLE, windowStyle | WS_EX_TOOLWINDOW);
  }

  public static void AddToAltTab(IntPtr Handle)
  {
    //Make it visible in the ALT+TAB
    int windowStyle = GetWindowLong(Handle, GWL_EXSTYLE);
    SetWindowLong(Handle, GWL_EXSTYLE, windowStyle & ~WS_EX_TOOLWINDOW);
  }
  #endregion
}