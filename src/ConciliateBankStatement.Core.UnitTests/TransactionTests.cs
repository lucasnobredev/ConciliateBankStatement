using ConciliateBankStatement.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ConciliateBankStatement.Core.UnitTests
{
    public class TransactionTests
    {
        [Fact]
        public void Test1()
        {
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(x => x.GetTransactionsByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Transaction>());
            var fileReaderService = new FileReaderService(transactionRepositoryMock.Object);

            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
            var qteTransactionsInFile = 31;
            var transactions = fileReaderService.Import(filePath);

            Assert.True(transactions != null);
            Assert.True(transactions.Count == qteTransactionsInFile);
        }

        [Fact]
        public void Test2()
        {
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(x => x.GetTransactionsByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Transaction>() { new Transaction("credit", new DateTime(2014, 2, 4), 12, "teste") });
            var fileReaderService = new FileReaderService(transactionRepositoryMock.Object);

            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
            var qteTransactionsInFile = 31;
            var qteTransactionsInFileWithDatePostedInMock = 5;
            var transactions = fileReaderService.Import(filePath);

            Assert.True(transactions != null);
            Assert.True(transactions.Count == (qteTransactionsInFile - qteTransactionsInFileWithDatePostedInMock));
        }
    }
}
