using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConciliateBankStatement.Presentation.Models;
using Microsoft.AspNetCore.Http;
using ConciliateBankStatement.Core;
using ConciliateBankStatement.Core.Interfaces;

namespace ConciliateBankStatement.Presentation.Controllers
{
    public class FileImporterController : Controller
    {
        private readonly ITransactionService _transactionService;
        public FileImporterController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile formFile)
        {
            var response = _transactionService.Import(formFile);

            return View(response);
        }
    }
}
