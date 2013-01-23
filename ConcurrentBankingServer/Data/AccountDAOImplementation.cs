using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Model;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConcurrentBankingServer.Data
{
    class AccountDAOImplementation : AccountDAO
    {
        private List<Account> allAcounts;

        String filePath = "D:\\temp\\accounts.dat";

        public List<Account> getAccounts() {

            return new List<Account>();  
            
        }

        public Account getAccountByAccNo(String accNo) {

            return new Account();
        
        }

        public void loadAccounts() { 
        
        
        }
        public void saveAccounts() {

            Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();

            for (int i = 0; i < allAcounts.Count; i++ )
            {

                bFormatter.Serialize(stream, allAcounts.ElementAt(i));

            }
            stream.Close();
        }
    }
}
