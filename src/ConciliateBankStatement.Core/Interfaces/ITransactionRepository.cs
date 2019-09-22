using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Interfaces
{
    public interface ITransactionRepository
    {
        IList<Transaction> GetTransactionsByPeriod(DateTime startAt, DateTime endAt);
    }
}
