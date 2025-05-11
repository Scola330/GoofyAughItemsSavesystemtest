using System;
using System.Runtime.InteropServices;

public static class ConsoleUtils
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleDisplayMode(IntPtr hConsoleOutput, uint dwFlags, out COORD lpNewScreenBufferDimensions);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    private const int STD_OUTPUT_HANDLE = -11;
    private const uint CONSOLE_FULLSCREEN_MODE = 1;

    [StructLayout(LayoutKind.Sequential)]
    private struct COORD
    {
        public short X;
        public short Y;
    }

    public static void SetFullscreen()
    {
        IntPtr consoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);
        if (SetConsoleDisplayMode(consoleHandle, CONSOLE_FULLSCREEN_MODE, out _))
        {
            Console.Clear(); // Clear the console after switching to fullscreen
        }
        else
        {
            Console.WriteLine("Unable to set fullscreen mode.");
        }
    }
}
