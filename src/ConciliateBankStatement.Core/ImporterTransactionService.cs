using ConciliateBankStatement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConciliateBankStatement.Core
{
    public class ImporterTransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IImporterFileService _importerFileService;

        public ImporterTransactionService(
            ITransactionRepository transactionRepository,
            IImporterFileService importerFileService)
        {
            _transactionRepository = transactionRepository;
            _importerFileService = importerFileService;
        }

        public void Import(string filePath)
        {
            var importedFile = _importerFileService.Import(filePath);
            var transactions = _transactionRepository.GetTransactionsByPeriod(importedFile.DateStart, importedFile.DateEnd);

            foreach(var transactionImported in importedFile.Transactions)
            {
                if (transactions != null && transactions.Any(x => x.DatePosted == transactionImported.DatePosted))
                    continue;

                var transaction = new Transaction(transactionImported.Type, transactionImported.DatePosted, transactionImported.Amount, transactionImported.Description);

                _transactionRepository.Save(transaction);
            }
        }
    }
}
