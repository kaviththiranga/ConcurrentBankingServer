using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Model;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ConcurrentBankingServer;

namespace ConcurrentBankingServer.Data
{
    class AccountDAOImplementation : AccountDAO
    {
        private List<Account> allAcounts;

        private Server.Log logger;

        String filePath = "D:\\temp\\accounts.dat";

        public AccountDAOImplementation(Server.Log logger)
        {
            allAcounts = new List<Account>();
            this.logger = logger;
        }

        public List<Account> getAccounts() {

            return allAcounts;  
            
        }

        public Account getAccountByAccNo(String accNo) {

           //logger("Searching for Account with AccountNum : " + accNo);

           foreach(Account a in allAcounts){
               
               if (a.AccountNumber.Equals(accNo)) {

                  // logger("Found Account with AccountNum : " + accNo);
                   return a;
               }
           }
           return null;
        }

        public void loadAccounts() {
            allAcounts = new List<Account>();

            
           /*Account ac = new Account("Kavith Thiranga","AC00001", "1001");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);

            ac = new Account("Kavith Thiranga", "AC00002", "1002");
            ac.executeTransaction(new Transaction("credit", 1000));
            ac.executeTransaction(new Transaction("debit", 555));
            allAcounts.Add(ac);
            ac = new Account("Kavith Thiranga", "AC00003", "1003");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);
            ac = new Account("Kavith Thiranga", "AC00004", "1004");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);
            ac = new Account("Kavith Thiranga", "AC00005", "1005");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);*/

            Stream stream = File.Open(filePath, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();

            allAcounts = bformatter.Deserialize(stream) as List<Account>;
            logger("Number of Accounts loaded: " + allAcounts.Count);
            foreach (Account a in allAcounts)
            {
                a._isAvailableLockedData = new System.Threading.AutoResetEvent(true);
                logger("Account with Account Number : " + a.AccountNumber+" loaded.");
            }

            stream.Close();

        
        }

        public void saveAccounts() {
            logger("Number of Accounts loaded: " + allAcounts.Count);
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, allAcounts);
            logger("Number of Accounts loaded: " + allAcounts.Count);
            stream.Close();
        
        }
    }
}
