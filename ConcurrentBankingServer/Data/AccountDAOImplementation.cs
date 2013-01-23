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

            return allAcounts;  
            
        }

        public Account getAccountByAccNo(String accNo) {

           foreach(Account a in allAcounts){
               if (a.AccountNumber.Equals(accNo)) {
                   return a;
               }
           }
            return null;
        }

        public void loadAccounts() {
            /*allAcounts = new List<Account>();
            Account ac = new Account("AC00001", "1001");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);

            ac = new Account("AC00002", "1002");
            ac.executeTransaction(new Transaction("credit", 1000));
            ac.executeTransaction(new Transaction("debit", 555));
            allAcounts.Add(ac);
            ac = new Account("AC00003", "1003");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);
            ac = new Account("AC00004", "1004");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);
            ac = new Account("AC00005", "1005");
            ac.executeTransaction(new Transaction("credit", 1000));
            allAcounts.Add(ac);*/

            StreamReader _wr = new StreamReader(filePath);

            while (!_wr.EndOfStream)
            {
                //Read one line at a time
                string objectStream = _wr.ReadLine();
                //Convert the Base64 string into byte array
                byte[] memorydata = Convert.FromBase64String(objectStream);
                MemoryStream rs = new MemoryStream(memorydata);
                BinaryFormatter sf = new BinaryFormatter();
                //Create object using BinaryFormatter
                Account objResult = (Account)sf.Deserialize(rs);

                allAcounts.Add(objResult);
            }
        
        }

        public void readFromFile() {

        }
        public void saveAccounts() {

            /*Stream stream = File.Open(filePath, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();

            for (int i = 0; i < allAcounts.Count; i++)
            {

                bFormatter.Serialize(stream, allAcounts.ElementAt(i));

            }
            stream.Close();*/
            StreamWriter _wr = new StreamWriter(filePath);
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            for (int i = 0; i < allAcounts.Count; i++)
            {
                binaryFormatter.Serialize(memoryStream, allAcounts.ElementAt(i));
                string str = System.Convert.ToBase64String(memoryStream.ToArray());

                _wr.WriteLine(str);
            }
        }
    }
}
