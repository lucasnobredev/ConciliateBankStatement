using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Models
{
    public class TransactionFileImportedModel
    {
        public string Type { get; set; }
        public DateTime DatePosted { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
