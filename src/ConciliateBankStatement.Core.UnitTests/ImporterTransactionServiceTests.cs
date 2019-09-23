using ConciliateBankStatement.Core.Interfaces;
using ConciliateBankStatement.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ConciliateBankStatement.Core.UnitTests
{
    [Trait("Importer", "Transaction")]
    public class ImporterTransactionServiceTests
    {
        [Fact(DisplayName = "Should Import Transaction When There Is Not No One Persisted")]
        public void ShouldImportTransactionWhenThereIsNotNoOnePersisted()
        {
            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(x => x.GetTransactionsByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(() => null);

            var importerFileServiceMock = new Mock<IFileImporterService>();
            var importedFileModel = new ImportedFileModel()
            {
                Transactions = new List<TransactionImportedFileModel>()
                {
                    new TransactionImportedFileModel()
                    {
                        Amount = 100,
                        DatePosted = DateTime.Now.AddDays(-3).Date,
                        Description = "teste",
                        Type = "CREDIT"
                    }
                }
            };
            //{ new Transaction("CREDIT", DateTime.Now.AddDays(-3).Date, 100, "teste") }

            importerFileServiceMock.Setup(x => x.Import(It.IsAny<string>())).Returns(importedFileModel);

            var service = new TransactionImporterService(transactionRepositoryMock.Object, importerFileServiceMock.Object);

            var sut = service.Import(filePath);

            Assert.True(sut.Success);
            Assert.True(sut.Error == null);
            Assert.True(sut.TransactionsImportedQuantity == 1);
        }

        [Fact(DisplayName = "Should Import Transaction When There Is Transaction Persisted But It is not the same")]
        public void ShouldImportTransactionWhenThereIsTransactionPersistedButItIsNotTheSame()
        {
            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(x => x.GetTransactionsByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Transaction> { new Transaction("CREDIT", DateTime.Now.AddDays(-2).Date, 100, "teste") });

            var importerFileServiceMock = new Mock<IFileImporterService>();
            var importedFileModel = new ImportedFileModel()
            {
                Transactions = new List<TransactionImportedFileModel>()
                {
                    new TransactionImportedFileModel()
                    {
                        Amount = 100,
                        DatePosted = DateTime.Now.AddDays(-3).Date,
                        Description = "teste",
                        Type = "CREDIT"
                    }
                }
            };

            importerFileServiceMock.Setup(x => x.Import(It.IsAny<string>())).Returns(importedFileModel);

            var service = new TransactionImporterService(transactionRepositoryMock.Object, importerFileServiceMock.Object);

            var sut = service.Import(filePath);

            Assert.True(sut.Success);
            Assert.True(sut.Error == null);
            Assert.True(sut.TransactionsImportedQuantity == 1);
        }

        [Fact(DisplayName = "Should NOT Import Transaction When There Is Transaction Persisted And It is the same")]
        public void ShouldNotImportTransactionWhenThereIsTransactionPersistedAndItIsTheSame()
        {
            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(x => x.GetTransactionsByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<Transaction> { new Transaction("CREDIT", DateTime.Now.AddDays(-3).Date, 100, "teste") });

            var importerFileServiceMock = new Mock<IFileImporterService>();
            var importedFileModel = new ImportedFileModel()
            {
                Transactions = new List<TransactionImportedFileModel>()
                {
                    new TransactionImportedFileModel()
                    {
                        Amount = 100,
                        DatePosted = DateTime.Now.AddDays(-3).Date,
                        Description = "teste",
                        Type = "CREDIT"
                    }
                }
            };

            importerFileServiceMock.Setup(x => x.Import(It.IsAny<string>())).Returns(importedFileModel);

            var service = new TransactionImporterService(transactionRepositoryMock.Object, importerFileServiceMock.Object);

            var sut = service.Import(filePath);

            Assert.True(sut.Success);
            Assert.True(sut.Error == null);
            Assert.True(sut.TransactionsImportedQuantity == 0);
        }

        [Fact(DisplayName = "Should Not Import Transaction When There Is An Unexpected Error In Flow")]
        public void ShouldNotImportTransactionWhenThereIsAnUnexpectedErrorInFlow()
        {
            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(x => x.GetTransactionsByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(() => throw new Exception());

            var importerFileServiceMock = new Mock<IFileImporterService>();
            var importedFileModel = new ImportedFileModel()
            {
                Transactions = new List<TransactionImportedFileModel>()
                {
                    new TransactionImportedFileModel()
                    {
                        Amount = 100,
                        DatePosted = DateTime.Now.AddDays(-3).Date,
                        Description = "teste",
                        Type = "CREDIT"
                    }
                }
            };

            importerFileServiceMock.Setup(x => x.Import(It.IsAny<string>())).Returns(importedFileModel);

            var service = new TransactionImporterService(transactionRepositoryMock.Object, importerFileServiceMock.Object);

            var sut = service.Import(filePath);

            Assert.True(sut.Success == false);
            Assert.True(sut.Error != null);
            Assert.True(sut.TransactionsImportedQuantity == 0);
        }
    }
}
