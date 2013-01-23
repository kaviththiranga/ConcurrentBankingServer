using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Model;

namespace ConcurrentBankingServer.Data
{
    public interface AccountDAO
    {
        List<Account> getAccounts();

        Account getAccountByAccNo(String accNo);

        void saveAccounts();

        void loadAccounts();
    }
}
