using Athena.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Athena.Interfaces
{
    public interface IWordCacheService
    {
        event EventHandler? DataChanged;

        Task InitializeAsync();
        List<WordModel> CachedWords { get; }
        Task<bool> UpdateWords(Action<List<WordModel>, WordModel> crudAction, WordModel word);
        void RemoveWord(WordModel word);
    }
}
