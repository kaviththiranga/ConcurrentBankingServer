using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Model;

namespace ConcurrentBankingServer.Data
{
    interface AccountDAO
    {
        public List<Account> getAccounts();

        public bool athenticate(String accNo, String pin);

        public Account getAccountByAccNo(String accNo);
    }
}
