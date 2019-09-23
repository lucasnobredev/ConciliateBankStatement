using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Models
{
    public class ImportedFileModel
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public IList<TransactionImportedFileModel> Transactions { get; set; }

        public ImportedFileModel()
        {
            this.Transactions = new List<TransactionImportedFileModel>();
        }
    }
}
