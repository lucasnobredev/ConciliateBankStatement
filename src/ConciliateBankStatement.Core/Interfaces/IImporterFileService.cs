﻿using ConciliateBankStatement.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Core.Interfaces
{
    public interface IImporterFileService
    {
        FileImportedModel Import(string filePath);
    }
}
