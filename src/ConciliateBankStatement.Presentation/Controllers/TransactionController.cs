using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConciliateBankStatement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConciliateBankStatement.Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(
            ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IActionResult Index(DateTime startAt, DateTime endAt)
        {
            return View(_transactionService.GetTransactions(startAt, endAt));
        }
    }
}