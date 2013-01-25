using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrentBankingServer.Model
{
    [Serializable()]
    public class DebitCard
    {

        private List<String> allAcounts;

        private String cardNumber;

        public String CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }

        private String pinCode;

        public String Pin
        {
            get { return pinCode; }
            set { pinCode = value; }
        }

        public DebitCard(String cN, String pin) {
            CardNumber = cN;
            Pin = pin;
            allAcounts = new List<string>();
        }

        public void addAccount(String accountNum) {

            allAcounts.Add(accountNum);
        }

        public void setAccounts(List<String> allAcounts)
        {
            this.allAcounts = allAcounts;
        }

        public List<String> getAccounts() {

            return allAcounts;
        }



    }
}
