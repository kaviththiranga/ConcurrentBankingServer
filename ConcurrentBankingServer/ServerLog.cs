using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConcurrentBankingServer
{
    public partial class ServerLog : Form
    {
        public Server.Log logger;

        public ServerLog()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Left = 500;
            Top = 0;
            logger = log;
        }

        private void ServerLog_Load(object sender, EventArgs e)
        {

        }

        public void log(String logMsg)
        {

            if (richTextBox1.InvokeRequired)
            {
                Invoke(new Action(
                        delegate()
                        {
                            richTextBox1.AppendText((DateTime.Now).ToString() + " : " + logMsg + "\n");
                        }));
            }
            else
            {
                richTextBox1.AppendText((DateTime.Now).ToString() + " : " + logMsg + "\n");
            }
        }
    }
}
