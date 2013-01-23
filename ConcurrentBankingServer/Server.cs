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

        private AccountDAO accountDAO;


        public Server() {
            accountDAO = new AccountDAOImplementation();
            accountDAO.loadAccounts();
            authService = new AuthenticationService(accountDAO);
            accountService = new AccoutService(accountDAO);
        }

        public Server(Log logger) {
            this.logger = logger;
            logger("Server Started");
        }

        public void terminate() {
            logger("Saving all the accounts back to file.");
            accountDAO.saveAccounts();
            logger("Saving accounts successfull.");
            logger("Server terminated sucessfully.");
        
        }
    }
}
