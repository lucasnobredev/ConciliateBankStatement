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
            var sut = new FileImporterService(fileRecorderServiceMock.Object);
            var filePath = GetFilePath();
            fileRecorderServiceMock.Setup(x => x.Recorder(It.IsAny<IFormFile>())).Returns(filePath);

            var qteTransactionsInFile = 31;
            var result = sut.Import(It.IsAny<IFormFile>());

            Assert.True(result != null);
            Assert.True(result.Transactions.Count == qteTransactionsInFile);
        }
        
        private string GetFilePath()
        {
            return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\ConciliateBankStatement.Core.UnitTests", "teste.ofx");
        }
    }
}
