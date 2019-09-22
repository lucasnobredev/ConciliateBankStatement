using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Models
{
    public class FileImportedModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public IList<TransactionFileImportedModel> Transactions { get; set; }

        public FileImportedModel()
        {
            this.Transactions = new List<TransactionFileImportedModel>();
        }
    }
}
