using ConciliateBankStatement.Core.Interfaces;
using ConciliateBankStatement.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConciliateBankStatement.Core
{
    public class FileImporterService : IFileImporterService
    {
        public ImportedFileModel Import(string filePath)
        {
            try
            {
                string line = null;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    line = sr.ReadToEnd();

                    line = line.Replace("\r", "");
                    line = line.Replace("\n", "");
                }

                return MapTransactionByFile(line);
            }
            catch (IOException e)
            {
                throw new Exception("The file could not be read:");
            }
        }

        private ImportedFileModel MapTransactionByFile(string line)
        {
            var fileImportedModel = new ImportedFileModel
            {
                DateStart = GetDateFromTag(line.Split("<DTSTART>")[1].Split("<DTEND>")[0]),
                DateEnd = GetDateFromTag(line.Split("<DTEND>")[1].Split("<STMTTRN>")[0])
            };

            for (var i = 1; i < line.Split("<STMTTRN>").Length; i++)
            {
                var transactionFile = line.Split("<STMTTRN>")[i];
                var separacoes = transactionFile.Split('<', '>');

                var transactionFileImportedModel = new TransactionImportedFileModel();

                for (var j = 0; j < separacoes.Length; j++)
                {
                    if (string.IsNullOrEmpty(separacoes[j]) || separacoes[j][0] == '/')
                        continue;

                    switch (separacoes[j])
                    {
                        case "TRNTYPE":
                            transactionFileImportedModel.Type = separacoes[j + 1];
                            break;
                        case "MEMO":
                            transactionFileImportedModel.Description = separacoes[j + 1];
                            break;
                        case "DTPOSTED":
                            transactionFileImportedModel.DatePosted = GetDateFromTag(separacoes[j + 1].Substring(0, 8));
                            break;
                        case "TRNAMT":
                            transactionFileImportedModel.Amount = decimal.Parse(separacoes[j + 1]);
                            break;
                    }

                    j++;
                }

                fileImportedModel.Transactions.Add(transactionFileImportedModel);
            }

            return fileImportedModel;
        }

        private DateTime GetDateFromTag(string tag)
        {
            return new DateTime(int.Parse(tag.Substring(0, 4)), int.Parse(tag.Substring(4, 2)), int.Parse(tag.Substring(6, 2)));
        }
    }
}
