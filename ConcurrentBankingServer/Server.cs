using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Service;
using ConcurrentBankingServer.Data;

namespace ConcurrentBankingServer
{
    public class Server
    {
        //Delegate to be used in displaying log messages
        public delegate void Log(String msg);

        protected Log logger;

        private AuthenticationService authService;

        private AccoutService accountService;

        public AuthenticationService AuthService {

            get { return authService; }
        }

        public AccoutService AccountService {
            get { return accountService; }
        }

        public AccountDAO accountDAO;


        public Server(Log logger)
        {

            this.logger = logger;
            accountDAO = new AccountDAOImplementation(this.logger);
            accountDAO.loadAccounts();
            accountDAO.loadCards();
            authService = new AuthenticationService(accountDAO);
            accountService = new AccoutService(accountDAO, this.logger);

            this.logger("Server Started");
        }

        /*public Server(Log logger) : this() {
            this.logger += logger;
        }*/

        public void terminate() {
            logger("Saving all the accounts back to file.");
            accountDAO.saveAccounts();
            accountDAO.saveCards();
            logger("Saving accounts successfull.");
            logger("Server terminated sucessfully.");
        
        }

        public void log(String logMsg) {
            System.Console.WriteLine((DateTime.Now).ToString() + " : " + logMsg + "\n");
        }
    }
}
