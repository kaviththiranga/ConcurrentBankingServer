using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConcurrentBankingServer.Model
{
    [Serializable()]
    public class Account
    {
        // This is used to signal the service classes once the lock on the
        // Balance mutator and getter methods is being released
        [NonSerialized()]
        public AutoResetEvent _isAvailableLockedData;

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
                double balance;
                // Prevents modification of balance while reading the balance
                lock (this)
                {
                    balance = currentBalance;
                }

                //Signals waiting threads
                _isAvailableLockedData.Set();

                return balance;
            }
        }

        private List<Transaction> recentTransactions;

        public List<Transaction> Transaction {
            get { return recentTransactions; }
        }

        public Account() {
            recentTransactions = new List<Transaction>();
            // Creates AutoResetEvent and sets initial state to signalled
            _isAvailableLockedData = new AutoResetEvent(true);
        }
        public Account(String aH, String aN, String pin) : this() {

            accountHolder = aH;
            accountNumber = aN;
            this.pinCode = pin;
        }

        public bool executeTransaction(Transaction t) {
            
            if (t.Type.Equals("debit")) {
                if (debitAccount(t.Amount))
                {
                    t.Balance = Balance;
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

                t.Balance = Balance;
                addToTransactions(t);
                
                return true;
            }

            return false;
        }

        private bool debitAccount(double amount) {

            bool success = false;
            lock (this)
            {
                if (currentBalance > amount)
                {
                    Thread.Sleep(1000);
                    currentBalance -= amount;
                    success = true;
                }
            }

            // Signals waiting threads
           _isAvailableLockedData.Set();

            return success;
        }

        private void creditAccount(double amount) {
            lock (this)
            {
                Thread.Sleep(1000);
                currentBalance += amount;
            }

            // Signals waiting threads
            _isAvailableLockedData.Set();
        }
        private void addToTransactions(Transaction t) {

            if (! (recentTransactions.Count < 25)){
                recentTransactions.RemoveAt(0);
            }
            recentTransactions.Add(t);
        }

    }
}
