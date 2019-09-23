using ConciliateBankStatement.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ConciliateBankStatement.Core.UnitTests
{
    [Trait("Importer", "File")]
    public class FileImporterServiceTests
    {
        [Fact(DisplayName = "Should Import A File With 31 Transactions")]
        public void ShouldImportAFileWith31Transactions()
        {
            var fileRecorderServiceMock = new Mock<IFileRecorderService>();
            var importerFileService = new FileImporterService(fileRecorderServiceMock.Object);
            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
            fileRecorderServiceMock.Setup(x => x.Recorder(It.IsAny<IFormFile>())).Returns(filePath);

            var qteTransactionsInFile = 31;
            var sut = importerFileService.Import(It.IsAny<IFormFile>());

            Assert.True(sut != null);
            Assert.True(sut.Transactions.Count == qteTransactionsInFile);
        }
    }
}
