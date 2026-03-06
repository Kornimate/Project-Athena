using Athena.Interfaces;
using Athena.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Athena.Services
{
    public class DataWriter : IDataWriter
    {
        public event EventHandler? DataChanged;

        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        async public Task<bool> SaveWordsDataAsync(List<WordModel>? words)
        {
            if (words is null)
                return false;

            string localPath = Path.Combine(FileSystem.AppDataDirectory, AppConfigurations.WordsFileName);

            if (!File.Exists(localPath))
            {
                using var packageStream = await FileSystem.OpenAppPackageFileAsync(AppConfigurations.WordsFileName);
                using var localStream = File.Create(localPath);
            }

            using var writer = new StreamWriter(File.OpenWrite(localPath));
            string jsonString = JsonSerializer.Serialize(words, _options);

            await writer.WriteAsync(jsonString);

            return true;
        }
    }
}
