using Athena.Interfaces;
using Athena.Models;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Athena.Services
{
    public class DataLoader : IDataLoader
    {
        public async Task<List<WordModel>> LoadWordsDataAsync()
        {
            string localPath = Path.Combine(FileSystem.AppDataDirectory, AppConfigurations.WordsFileName);

            if (!File.Exists(localPath))
            {
                using var packageStream = await FileSystem.OpenAppPackageFileAsync(AppConfigurations.WordsFileName);
                using var localStream = File.Create(localPath);

                await packageStream.CopyToAsync(localStream);
            }

            using var reader = new StreamReader(File.OpenRead(localPath));
            string json = await reader.ReadToEndAsync();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<WordModel>>(json, options)!;
        }
    }
}
