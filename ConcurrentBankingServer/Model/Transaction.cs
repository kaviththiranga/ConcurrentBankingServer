using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentBankingServer.Model
{
    [Serializable()]
    public class Transaction
    {
        private String transactionType;

        public String Type {
            get { return transactionType; }
            set { transactionType = value; }
        }

        private double amount;

        public double Amount {
            get { return amount; }
            set { amount = value; }
        }

        private double remainingBalance;


        public double Balance
        {
            get { return remainingBalance; }
            set { remainingBalance = value; }
        }

        private bool sucess = false;

        public bool Success {
            get { return sucess; }
            set { sucess = value; }
        }

        private String notes;
        public String Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public Transaction(String type, double amount) {

            transactionType = type;
            this.amount = amount;
        }
    }
}
