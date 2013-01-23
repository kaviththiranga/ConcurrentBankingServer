using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConcurrentBankingServer.Data;

namespace ConcurrentBankingServer.Service
{
    public class AccoutService
    {
        protected AccountDAO accountDAO;

        public AccoutService(AccountDAO dao)
        {
            accountDAO = dao;
        }
    }
}
