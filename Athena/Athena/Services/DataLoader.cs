using Athena.Interfaces;
using Athena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Athena.Services
{
    public class DataLoader : IDataLoader
    {
        public async Task<List<WordModel>> LoadDataFromJson(string fileName)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("example.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<WordModel>>(json, options)!;
        }
    }
}
