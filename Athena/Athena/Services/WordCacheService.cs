using Athena.Interfaces;
using Athena.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Athena.Services
{
    public class WordCacheService : IWordCacheService
    {
        public event EventHandler? DataChanged;

        public List<WordModel> CachedWords { get; private set; }

        private readonly IDataLoader _dataLoader;
        private readonly IDataWriter _dataWriter;

        public WordCacheService(IDataLoader dataLoader, IDataWriter dataWriter)
        {
            _dataLoader = dataLoader;
            _dataWriter = dataWriter;

            CachedWords = [];
        }

        public async Task InitializeAsync()
        {
            CachedWords = await LoadWordsData();
        }

        public async Task<bool> UpdateWords(Action<List<WordModel>, WordModel> crudAction, WordModel word)
        {
            if (CachedWords is null || CachedWords.Count == 0)
                return false;

            crudAction(CachedWords, word);

            await _dataWriter.SaveWordsDataAsync(CachedWords);

            DataChanged?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public void RemoveWord(WordModel word)
        {
            if (CachedWords.Remove(word))
            {
                _dataWriter.SaveWordsDataAsync(CachedWords);
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private async Task<List<WordModel>> LoadWordsData()
        {
            return await _dataLoader.LoadWordsDataAsync();
        }
    }
}
