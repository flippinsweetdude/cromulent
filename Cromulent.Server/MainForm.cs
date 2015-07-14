using Cromulent.Server.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cromulent.Server
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            AsynchronousSocketListener.StartListening();           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BaseBL.UpdateProgress += BaseBL_UpdateProgress;
        }

        void BaseBL_UpdateProgress(object sender, Domain.ProgressEventArgs e)
        {
            txtStatus.Text = string.Format("{0}\r\n{1}", e.Message, txtStatus.Text);
        }
    }
}
