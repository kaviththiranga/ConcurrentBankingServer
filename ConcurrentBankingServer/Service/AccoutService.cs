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

        public Transaction executeTransaction(String cardNo, String pin, String accNo, Transaction tr) {

            if (authenticateTransaction(cardNo, pin))
            {

                logger("Thread : " + Thread.CurrentThread.Name + " : Waiting till the lock in Account released to do the transaction for Ac : " + accNo);

                accountDAO.getAccountByAccNo(accNo)._isAvailableLockedData.WaitOne();

                logger("Thread : " + Thread.CurrentThread.Name + " : Signal received to confirm that the lock has been released. Doing transaction for  Ac : " + accNo);
                
                return accountDAO.getAccountByAccNo(accNo).executeTransaction(tr);
            }
            //throw new Exception("Failed to authenticate");
        }

        public double getBalance(String cardNo, String pin, String accNo) {

            if (authenticateTransaction(cardNo, pin)) {

                logger("Thread : " + Thread.CurrentThread.Name + " : Waiting till the lock in Account released to read balance from Ac : " + accNo);

                accountDAO.getAccountByAccNo(accNo)._isAvailableLockedData.WaitOne();

                logger("Thread : " + Thread.CurrentThread.Name + " : Signal received to confirm that the lock has been released. Reading the balance from Ac : " + accNo);
                
                return accountDAO.getAccountByAccNo(accNo).Balance;
            }

            return -1;

        }

        public bool authenticateTransaction(string cardNo, string pin)
        {

            DebitCard ac = accountDAO.getCardByCardNo(cardNo);

            if (ac != null && ac.Pin == pin)
            {
                return true;
            }

            logger("Authentication for Card : " + cardNo + " failed. Invalid Pin number");
            return false;
        }

        public List<String> getAccountsByCard(String cardNo) {

            return accountDAO.getCardByCardNo(cardNo).getAccounts();
        }

        public bool authenticateTransaction2(string accNo, string pin)
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
