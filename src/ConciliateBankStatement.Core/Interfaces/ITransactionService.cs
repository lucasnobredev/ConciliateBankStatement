using ConciliateBankStatement.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Interfaces
{
    public interface ITransactionService
    {
        ImportResponse Import(IFormFile formFile);
        IList<Transaction> GetTransactions(DateTime startAt, DateTime endAt);
    }
}
