using ConciliateBankStatement.Core;
using ConciliateBankStatement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConciliateBankStatement.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private StatementContext _context;
        public TransactionRepository(StatementContext context)
        {
            _context = context;
        }
        
        public IList<Transaction> GetTransactionsByPeriod(DateTime startAt, DateTime endAt)
        {
            return _context.Transactions.Where(x => x.DatePosted >= startAt && x.DatePosted <= endAt).ToList();
        }

        public void Save(Transaction transaction)
        {
            _context.Add(transaction);
            _context.SaveChanges();
        }
    }
}
