using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Data;
using ConcurrentBankingServer.Model;

namespace ConcurrentBankingServer.Service
{
    public class AccoutService
    {
        protected AccountDAO accountDAO;

        public AccoutService(AccountDAO dao)
        {
            accountDAO = dao;
        }

        public void executeTransaction(String accNo, String pin, Transaction tr) {

            if (authenticateTransaction(accNo, pin))
            {

                accountDAO.getAccountByAccNo(accNo).executeTransaction(tr);
            }

            //throw new Exception("Failed to authenticate");
        }

        public double getBalance(String accNo, String pin) {

            if (authenticateTransaction(accNo, pin)) {

                return accountDAO.getAccountByAccNo(accNo).Balance;
            }

            return -1;

        }

        public bool authenticateTransaction(string accNo, string pin)
        {

            Account ac = accountDAO.getAccountByAccNo(accNo);

            if (ac != null && ac.Pin.Equals(pin))
            {
                return true;
            }

            return false;
        }
    }
}
