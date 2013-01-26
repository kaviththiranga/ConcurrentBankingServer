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

        private List<DebitCard> allCards;

        private Server.Log logger;

        String filePath = "D:\\temp\\accounts.dat";

        String filePath2 = "D:\\temp\\cards.dat";

        public AccountDAOImplementation(Server.Log logger)
        {
            allCards = new List<DebitCard>();
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

        public DebitCard getCardByCardNo(String cardNo)
        {

            foreach (DebitCard a in allCards)
            {

                if (a.CardNumber.Equals(cardNo))
                {

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
                a._locker = new Object(); ;
                
            }

            stream.Close();

        
        }
        public void loadCards() {

            /*DebitCard card = new DebitCard("DC001", "1989");
            card.addAccount("AC00001");
            card.addAccount("AC00002");
            allCards.Add(card);

            card = new DebitCard("DC002", "1989");
            card.addAccount("AC00003");
            card.addAccount("AC00004");
            allCards.Add(card);*/



             Stream stream = File.Open(filePath2, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();

            allCards = bformatter.Deserialize(stream) as List<DebitCard>;
            logger("Number of Cards loaded: " + allCards.Count);
            foreach (DebitCard a in allCards)
            {
                
            }

            stream.Close();
        
        }

        public void saveAccounts() {
            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, allAcounts);
            stream.Close();
        }

        public void saveCards()
        {
            Stream stream = File.Open(filePath2, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, allCards);
            stream.Close();
        }
    }
}
