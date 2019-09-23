using ConciliateBankStatement.Core.Interfaces;
using ConciliateBankStatement.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConciliateBankStatement.Core
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IFileImporterService _importerFileService;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IFileImporterService importerFileService)
        {
            _transactionRepository = transactionRepository;
            _importerFileService = importerFileService;
        }

        public ImportResponse Import(IFormFile formFile)
        {
            try
            {
                var importedFile = _importerFileService.Import(formFile);
                var transactions = _transactionRepository.GetTransactionsByPeriod(importedFile.DateStart, importedFile.DateEnd);
                int transactionsImportedQuantity = 0;

                foreach (var transactionImported in importedFile.Transactions)
                {
                    if (transactions != null && transactions.Any(x => x.DatePosted == transactionImported.DatePosted))
                        continue;

                    var transaction = new Transaction(transactionImported.Type, transactionImported.DatePosted, transactionImported.Amount, transactionImported.Description);

                    _transactionRepository.Save(transaction);

                    transactionsImportedQuantity++;
                }

                return new ImportResponse(transactionsImportedQuantity);
            }
            catch(Exception ex)
            {
                return new ImportResponse("Falha inesperada, tente novamente mais tarde.");
            }
        }

        public IList<Transaction> GetTransactions(DateTime startAt, DateTime endAt)
        {
            if(startAt == default(DateTime))
                startAt = DateTime.Now.AddMonths(-1);

            if (endAt == default(DateTime))
                endAt = DateTime.Now;
            
            return _transactionRepository.GetTransactionsByPeriod(startAt, endAt);
        }
    }
}
