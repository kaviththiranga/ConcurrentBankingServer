﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Data;

namespace ConcurrentBankingServer.Service
{
    public class AuthenticationService
    {
        protected AccountDAO accountDAO;


        public AuthenticationService(AccountDAO dao) {
            accountDAO = dao;
        }
        public bool authenticateTransaction(string accNo, string pin) {

            return true;
        }
    }
}
