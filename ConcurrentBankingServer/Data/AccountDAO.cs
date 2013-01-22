using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Model;

namespace ConcurrentBankingServer.Data
{
    interface AccountDAO
    {
        List<Account> getAccounts();

        Account getAccountByAccNo(String accNo);

    }
}
