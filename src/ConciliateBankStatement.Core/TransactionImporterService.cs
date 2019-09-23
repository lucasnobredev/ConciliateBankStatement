using ConciliateBankStatement.Core.Interfaces;
using ConciliateBankStatement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConciliateBankStatement.Core
{
    public class TransactionImporterService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IFileImporterService _importerFileService;

        public TransactionImporterService(
            ITransactionRepository transactionRepository,
            IFileImporterService importerFileService)
        {
            _transactionRepository = transactionRepository;
            _importerFileService = importerFileService;
        }

        public ImportResponse Import(string filePath)
        {
            try
            {
                var importedFile = _importerFileService.Import(filePath);
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
    }
}
