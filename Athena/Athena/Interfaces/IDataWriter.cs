using Athena.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Athena.Interfaces
{
    public interface IDataWriter
    {
        Task<bool> SaveWordsDataAsync(List<WordModel>? words);
    }
}
