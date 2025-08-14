using Athena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Interfaces
{
    public interface IDataLoader
    {
        Task<List<WordModel>> LoadDataFromJson(string fileName);
    }
}
