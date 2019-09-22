using ConciliateBankStatement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConciliateBankStatement.Core
{
    public class FileReaderService
    {
        private readonly ITransactionRepository _transactionRepository;

        public FileReaderService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IList<Transaction> Import(string filePath)
        {
            var transactions = new List<Transaction>();
            try
            {
                string line = null;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    line = sr.ReadToEnd();

                    line = line.Replace("\r", "");
                    line = line.Replace("\n", "");
                }

                MapTransactionByFile(transactions, line);

                Console.WriteLine(line);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return transactions;
        }

        private void MapTransactionByFile(List<Transaction> transactionsFile, string line)
        {
            var dateStart = GetDateFromTag(line.Split("<DTSTART>")[1].Split("<DTEND>")[0]);
            var dateEnd = GetDateFromTag(line.Split("<DTEND>")[1].Split("<STMTTRN>")[0]);

            var transactions = _transactionRepository.GetTransactionsByPeriod(dateStart, dateEnd);

            for (var i = 1; i < line.Split("<STMTTRN>").Length; i++)
            {
                var transactionFile = line.Split("<STMTTRN>")[i];
                var separacoes = transactionFile.Split('<', '>');

                string type = string.Empty;
                DateTime? createAt = null;
                decimal amount = 0;
                string description = string.Empty;
                bool endFor = false;

                for (var j = 0; j < separacoes.Length; j++)
                {
                    if (string.IsNullOrEmpty(separacoes[j]) || separacoes[j][0] == '/')
                        continue;

                    switch (separacoes[j])
                    {
                        case "TRNTYPE":
                            type = separacoes[j + 1];
                            break;
                        case "MEMO":
                            description = separacoes[j + 1];
                            break;
                        case "DTPOSTED":
                            createAt = GetDateFromTag(separacoes[j + 1].Substring(0, 8));
                            if (transactions.Any(x => x.DatePosted == createAt))
                                endFor = true;

                            break;
                        case "TRNAMT":
                            amount = decimal.Parse(separacoes[j + 1]);
                            break;
                    }

                    if (endFor)
                        break;

                    j++;
                }

                if(!endFor)
                {
                    var transaction = new Transaction(type, createAt.Value, amount, description);
                    transactionsFile.Add(transaction);
                }
            }
        }

        private DateTime GetDateFromTag(string tag)
        {
            return new DateTime(int.Parse(tag.Substring(0, 4)), int.Parse(tag.Substring(4, 2)), int.Parse(tag.Substring(6, 2)));
        }
    }
}
