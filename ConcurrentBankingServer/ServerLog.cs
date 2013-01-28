using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ConcurrentBankingServer
{
    public partial class ServerLog : Form
    {
        public Server.Log logger;
        String logFile = "D:\\temp\\ServerLog.txt";

        public ServerLog()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Left = 500;
            Top = 0;
            logger += log;
            logger += logToFile;
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

        public void logToFile(string logMsg)
        {
            using (Mutex mutex = new Mutex(false, "Server Log File Lock"))
            {
                if (!mutex.WaitOne())
                {
                    log("Error Saving to log file");
                }

                TextWriter tw = new StreamWriter(logFile, true);
                tw.WriteLine((DateTime.Now).ToString() + " : " + logMsg);
                tw.Close();
                mutex.ReleaseMutex();
            }
        }
    }
}
