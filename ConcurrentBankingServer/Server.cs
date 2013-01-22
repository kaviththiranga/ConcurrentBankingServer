using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentBankingServer
{
    public class Server
    {
        //Delegate to be used in displaying log messages
        public delegate void Log(String msg);

        protected Log logger;

        public Server() { }

        public Server(Log logger) {
            this.logger = logger;

        }

    }
}
