using System;
using System.Threading;
using System.Windows.Forms;

namespace Reminder_for_Van
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            LeftMouseClick(100, 1040);
            Thread.Sleep(2000);
            LeftMouseClick(400, 980);
            Thread.Sleep(2000);
            while (DateTime.Now.Hour != 22 && DateTime.Now.Minute != 30)
            {
                SendKeys.SendWait("Shorert chmoranas equc");
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(180000);
            }

            Console.ReadKey();
        }
    }
}
