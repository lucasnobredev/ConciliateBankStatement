using System;

namespace ConciliateBankStatement.Core
{
    public class Transaction
    {
        public int Id { get; private set; }
        public string Type { get; private set; }
        public DateTime DatePosted { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }

        public Transaction(string type, DateTime datePosted, decimal amount, string description)
        {
            this.Type = type;
            this.DatePosted = datePosted;
            this.Amount = amount;
            this.Description = description;
        }
    }
}
