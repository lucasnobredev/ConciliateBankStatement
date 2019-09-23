using ConciliateBankStatement.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConciliateBankStatement.Core
{
    public class FileRecorderService : IFileRecorderService
    {
        public string Recorder(IFormFile formFile)
        {
            var fileName = Path.GetFileName(formFile.FileName);
            var filePath = Path.Combine("C:\\Projetos\\nibo_projeto\\ConciliateBankStatement\\src\\ConciliateBankStatement.Core\\uploads", fileName);
            using (var fileSteam = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileSteam);
            }

            return filePath;
        }
    }
}
