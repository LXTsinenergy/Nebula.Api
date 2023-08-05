using Microsoft.Extensions.Caching.Memory;
using Nebula.BLL.Services.Interfaces;

namespace Nebula.BLL.Services.Implementations
{
    public class SpaceFactsService : ISpaceFactsService
    {
        private IMemoryCache _memoryCache;

        public SpaceFactsService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<string> GetRandomSpaceFact()
        {
            _memoryCache.TryGetValue(0, out string[]? data);
            if (data == null)
            {
                var cachingTime = TimeSpan.FromMinutes(5);
                _memoryCache.Set(0, data = await ReadFile(), cachingTime);
            }

            string fact = ChooseRandomFactFromList(data);

            return fact;
        }

        private async Task<string[]> ReadFile()
        {
            string path = "C:\\Users\\USER\\source\\repos\\Nebula.Api\\Nebula.BLL\\Data\\SpaceFacts.txt";

            using StreamReader streamReader = new(path);
            string text = await streamReader.ReadToEndAsync();

            var list = text.Split('\n');
            return list;
        }

        private string ChooseRandomFactFromList(string[] list)
        {
            Random random = new();

            int number = random.Next(1, list.Length);
            return list[number];
        }
    }
}
