using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cromulent.Server.Domain
{
    public class ProgressEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
