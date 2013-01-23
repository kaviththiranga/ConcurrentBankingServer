using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentBankingServer.Model
{
    [Serializable()]
    public class Account
    {
        private String accountNumber;

        public String AccountNumber
        {
            get { return accountNumber; }
            set { AccountNumber = value; }
        }

        private String pinCode;

        public String Pin
        {
            get { return pinCode; }
            set { pinCode = value; }
        }


        private String accountHolder;

        public String Holder
        {
            get { return accountHolder;}
            set { accountHolder = value;}
        }

        private double currentBalance;

        public double Balance {
            get 
            {
                lock (this)
                {
                    return currentBalance;
                }
            }
        }

        private List<Transaction> recentTransactions;

        public List<Transaction> Transaction {
            get { return recentTransactions; }
        }

        public Account() {
            recentTransactions = new List<Transaction>();
        }
        public Account(String aH, String aN) {

            accountHolder = aH;
            accountNumber = aN;
            recentTransactions = new List<Transaction>();
        }

        public bool executeTransaction(Transaction t) {
            
            if (t.Type.Equals("debit")) {
                if (debitAccount(t.Amount))
                {
                    addToTransactions(t);
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (t.Type.Equals("credit"))
            {
                creditAccount(t.Amount);
                addToTransactions(t);
                return true;
            }

            return false;
        }

        private bool debitAccount(double amount) {

            lock (this)
            {
                if (currentBalance > amount)
                {
                    currentBalance -= amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void creditAccount(double amount) {
            lock (this)
            {
                currentBalance += amount;
            }
        }
        private void addToTransactions(Transaction t) {

            if (! (recentTransactions.Count < 25)){
                recentTransactions.RemoveAt(0);
            }
            recentTransactions.Add(t);
        }

    }
}
