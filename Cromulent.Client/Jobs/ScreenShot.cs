using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cromulent.Client.Jobs
{
    public class ScreenShot
    {

        private static Timer timer = new Timer();

        public void Start(int seconds)
        {
            timer.Interval = seconds * 1000;           
            timer.Tick += timer_Tick;
            timer.Enabled = true;
            timer.Start();
            TakeScreenShot();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TakeScreenShot();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Tick -= timer_Tick;
        }

        public void TakeScreenShot()
        {
            using (Bitmap bmpScreenCapture = new Bitmap(Screen.AllScreens.Sum(x => x.Bounds.Width),
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);

                    bmpScreenCapture.Save(Settings.ScreenShotFileName);
                }
            }
        }
    }
}
