using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ConcurrentBankingServer;
using ConcurrentBankingServer.Data;
using ConcurrentBankingServer.Model;
using System.Threading;

namespace ConcurrentBankingServer.Service
{
    public class AccoutService
    {
        public delegate void UpdateProgress(object sender, ProgressChangedEventArgs e);

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
                logger("Thread : " + Thread.CurrentThread.Name + 
                        " : Waiting till the lock in Account released to do the " + 
                            tr.Type + " transaction for Ac : " + accNo);

                accountDAO.getAccountByAccNo(accNo)._isAvailableLockedData.WaitOne();
                    
                logger("Thread : " + Thread.CurrentThread.Name 
                    + " : Signal received to confirm that the lock has been released. Doing "
                                + tr.Type + " transaction for  Ac : " + accNo);
                
                tr = accountDAO.getAccountByAccNo(accNo).executeTransaction(tr);

                logger("Thread : " + Thread.CurrentThread.Name
                   + " : Completed  "
                               + tr.Type + " transaction for  Ac : " + accNo);
                if (!tr.Success) {
                    logger("Thread : " + Thread.CurrentThread.Name +
                        " : Error while excuting the transaction for Account : " + accNo);
                }
            }
            //throw new Exception("Failed to authenticate");

            return tr;
        }

        public void executeTransaction2(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _bw = sender as BackgroundWorker;
            BackgroundWorkerArg args = e.Argument as BackgroundWorkerArg;
            ExeTransacBackgResult result = new ExeTransacBackgResult();

            if (authenticateTransaction(args.CardNumber, args.Pin))
            {
                logger("Thread : " + Thread.CurrentThread.Name + " : Waiting till the lock in Account released to do the " 
                                    + args.Transaction.Type + " transaction for Ac : " + args.AccountNumber);

                logger("Thread");
                //_bw.ReportProgress(33);
                
                // Wait till the lock is being released
                accountDAO.getAccountByAccNo(args.AccountNumber)._isAvailableLockedData.WaitOne();
                logger("Thread");
                // Abort the transaction if a cancel reqeust issued by user, Else proceed. Operation canoot be canceled after this point    
                if (_bw.CancellationPending) { e.Cancel = true; return; }

                //_bw.ReportProgress(44);
                
                // Execute the transaction on acccount
                result.Transaction = accountDAO.getAccountByAccNo(args.AccountNumber).executeTransaction(args.Transaction);
                logger("Thread");
                logger("Thread");
                //_bw.ReportProgress(100);
                if (!result.Transaction.Success)
                {
                    logger("Thread : " + Thread.CurrentThread.Name +
                        " : Error while excuting the transaction for Account : " + args.AccountNumber);

                    result.Success = false;
                    result.Msg = " : Error while excuting the transaction for Account : " + args.AccountNumber;

                }
                result.Success = true;
            }
            else{
                //_bw.ReportProgress(100);
                result.Success = false;
                result.Msg = "Failed to authenticate ";
            }
            e.Result = result;
        }

        public double getBalance(String cardNo, String pin, String accNo) {

            if (authenticateTransaction(cardNo, pin)) {

                logger("Thread : " + Thread.CurrentThread.Name + 
                        " : Waiting till the lock in Account released to read balance from Ac : " + accNo);

                accountDAO.getAccountByAccNo(accNo)._isAvailableLockedData.WaitOne();

                logger("Thread : " + Thread.CurrentThread.Name +
                        " : Signal received to confirm that the lock has been released. Reading the balance from Ac : " + accNo);
                
                return accountDAO.getAccountByAccNo(accNo).Balance;
            }

            return -1;

        }

        public bool authenticateTransaction(string cardNo, string pin)
        {

            DebitCard ac = accountDAO.getCardByCardNo(cardNo);

            if (ac != null && ac.Pin == pin)
            {
                logger("Authentication for Card : " + cardNo + " successfull.");
                return true;
            }

            logger("Authentication for Card : " + cardNo + " failed. Invalid Pin number");
            return false;
        }

        public List<String> getAccountsByCard(String cardNo) {

            return accountDAO.getCardByCardNo(cardNo).getAccounts();
        }

    }
}
