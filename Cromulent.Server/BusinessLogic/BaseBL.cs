using Cromulent.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cromulent.Server.BusinessLogic
{
    public class BaseBL
    {
        public static event EventHandler<ProgressEventArgs> UpdateProgress;

        public static void UpdateProgressMessage(string message)
        {
            ProgressEventArgs e = new ProgressEventArgs();
            e.Message = message;

            UpdateProgress.Invoke(null, e);
        }
    }
}
