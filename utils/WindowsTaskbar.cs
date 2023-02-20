using System.Runtime.InteropServices;

public static class WindowsTaskbar
{
  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
  private static extern IntPtr FindWindow(
      string lpClassName,
      string lpWindowName);

  // import the stuff to hide the window
  [DllImport("user32.dll", SetLastError = true)]
  static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
  const int SW_HIDE = 0;
  const int SW_SHOW = 5;

  private const string taskbarClassName = "Shell_TrayWnd";
  private const string taskbarSecondaryClassName = "Shell_SecondaryTrayWnd";

  private static bool _isHidden = true;

  public static void Show()
  {
    var window = FindWindow(taskbarClassName, "");
    var secondWindow = FindWindow(taskbarSecondaryClassName, "");
    ShowWindow(window, SW_SHOW);
    ShowWindow(secondWindow, SW_SHOW);
    _isHidden = false;
  }

  public static void Hide()
  {
    var window = FindWindow(taskbarClassName, "");
    var secondWindow = FindWindow(taskbarSecondaryClassName, "");
    ShowWindow(window, SW_HIDE);
    ShowWindow(secondWindow, SW_HIDE);
    _isHidden = true;
  }

  public static void Toggle()
  {
    if (_isHidden)
      Show();
    else
      Hide();
  }
}