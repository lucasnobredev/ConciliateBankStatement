using ConciliateBankStatement.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Interfaces
{
    public interface ITransactionImporterService
    {
        ImportResponse Import(IFormFile formFile);
    }
}
