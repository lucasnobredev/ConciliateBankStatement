using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Models
{
    public class TransactionImportedFileModel
    {
        public string Type { get; set; }
        public DateTime DatePosted { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
