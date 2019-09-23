using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Models
{
    public class ImportResponse
    {
        public bool Success { get; private set; }
        public int TransactionsImportedQuantity { get; private set; }
        public string Error { get; private set; }

        public ImportResponse(int transactionsImportedQuantity)
        {
            Success = true;
            TransactionsImportedQuantity = transactionsImportedQuantity;
        }

        public ImportResponse(string error)
        {
            Error = error;
        }
    }
}
