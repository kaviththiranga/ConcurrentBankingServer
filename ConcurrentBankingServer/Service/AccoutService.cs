using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer;
using ConcurrentBankingServer.Data;
using ConcurrentBankingServer.Model;
using System.Threading;

namespace ConcurrentBankingServer.Service
{
    public class AccoutService
    {
        protected AccountDAO accountDAO;

        private Server.Log logger;

        public AccoutService(AccountDAO dao, Server.Log logger)
        {
            accountDAO = dao;
            this.logger = logger;
        }

        public void executeTransaction(String accNo, String pin, Transaction tr) {

            if (authenticateTransaction(accNo, pin))
            {

                logger("Thread : " + Thread.CurrentThread.Name + " : Waiting till the lock in Account released to do the transaction for Ac : " + accNo);

                accountDAO.getAccountByAccNo(accNo)._isAvailableLockedData.WaitOne();

                logger("Thread : " + Thread.CurrentThread.Name + " : Signal received to confirm that the lock has been released. Doing transaction for  Ac : " + accNo);
                
                accountDAO.getAccountByAccNo(accNo).executeTransaction(tr);
            }


            //throw new Exception("Failed to authenticate");
        }

        public double getBalance(String accNo, String pin) {

            if (authenticateTransaction(accNo, pin)) {

                logger("Thread : " + Thread.CurrentThread.Name + " : Waiting till the lock in Account released to read balance from Ac : " + accNo);

                accountDAO.getAccountByAccNo(accNo)._isAvailableLockedData.WaitOne();

                logger("Thread : " + Thread.CurrentThread.Name + " : Signal received to confirm that the lock has been released. Reading the balance from Ac : " + accNo);
                
                return accountDAO.getAccountByAccNo(accNo).Balance;
            }

            return -1;

        }

        public bool authenticateTransaction(string accNo, string pin)
        {

            Account ac = accountDAO.getAccountByAccNo(accNo);

            if (ac != null && ac.Pin == pin)
            {
                return true;
            }

            logger("Authentication for Ac : " + accNo + " failed. Invalid Pin number");
            return false;
        }
    }
}
