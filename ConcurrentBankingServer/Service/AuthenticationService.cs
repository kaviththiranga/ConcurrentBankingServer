using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Data;
using ConcurrentBankingServer.Model;

namespace ConcurrentBankingServer.Service
{
    public class AuthenticationService
    {
        protected AccountDAO accountDAO;


        public AuthenticationService(AccountDAO dao) {
            accountDAO = dao;
        }
        public bool authenticateTransaction(string accNo, string pin) {

            Account ac = accountDAO.getAccountByAccNo(accNo);

            if (ac != null && ac.Pin.Equals(pin))
            {
                return true;
            }

            return false;
        }
    }
}
