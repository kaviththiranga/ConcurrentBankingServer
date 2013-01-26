using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentBankingServer.Model
{
    public class BackgroundWorkerArg
    {
        private String cardNum;

        public String CardNumber {
            get { return cardNum; }
            set { cardNum = value; }
        }

        private String pin;

        public String Pin {

            get { return pin; }
            set { pin = value; }
        }

        private String accNo;

        public String AccountNumber {

            get { return accNo; }
            set { accNo = value; }
        }

        private Transaction tr;

        public Transaction Transaction {

            get { return tr; }
            set { tr = value;}
        }
    }
}
