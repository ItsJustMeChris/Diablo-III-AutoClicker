using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace Diablo3Auto_Clicker
{
    class Clicker
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        //[enabled][delay]
        private int[] keys = { VK_1, VK_2, VK_3, VK_4 };
        private int[] keyToggles { get; set; }
        private int[] delays { get; set; }
        private int curDelay;

        private const int VK_1 = 0x31;
        private const int VK_2 = 0x32;
        private const int VK_3 = 0x33;
        private const int VK_4 = 0x34;
        private const UInt32 WM_KEYDOWN = 0x0100;

        public Clicker(int keyCount)
        {
            this.keyToggles = new int[keyCount];
            this.delays = new int[keyCount];
            this.curDelay = 0;

        }

        public static Process Diablo()
        {
            Process[] processes = Process.GetProcessesByName("Diablo III64");
            try
            {
                return processes[0];
            }
            catch
            {
                return null;
            }
        }

        public void toggleKey(int key)
        {
            if (this.keyToggles[key - 1] == 0)
                this.keyToggles[key - 1] = 1;
            else
                this.keyToggles[key - 1] = 0;
        }

        public void setKeyDelay(int key, int delay)
        {
            if (key >= 1 && key <= 4)
                this.delays[key-1] = delay;
        }

        public int getKeyDelay(int key)
        {
            if (key >= 1 && key <= 4)
                return this.delays[key - 1];
            else
                return 0;
        }

        public void clickKeys()
        {
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            myTimer.Interval = 1000;
            myTimer.Start();
        }

        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            if (this.curDelay < this.delays.Max())
                this.curDelay++;
            else
                this.curDelay = 0;

            for (var i = 0; i < keys.Length; i++)
            {
                if (this.delays[i] == this.curDelay && this.keyToggles[i] == 1)
                {
                    FakeKey(this.keys[i]);
                }
            }
        }

        public static void FakeKey(int key)
        {
            if (Diablo() != null)
            {
                PostMessage(Diablo().MainWindowHandle, WM_KEYDOWN, key, 0);
            }
        }
    }
}
