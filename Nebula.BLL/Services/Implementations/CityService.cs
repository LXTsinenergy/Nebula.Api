using Microsoft.Extensions.Caching.Memory;
using Nebula.BLL.Services.Interfaces;
using Nebula.Domain.Models;
using System.Globalization;

namespace Nebula.BLL.Services.Implementations
{
    public class CityService : ICityService
    {
        private const string dataUrl = "https://raw.githubusercontent.com/epogrebnyak/ru-cities/main/geocoding/coord_dadata.csv";
        private IMemoryCache _memoryCache;

        public CityService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            _memoryCache.TryGetValue(0, out List<City> cityList);
            if (cityList == null)
            {
                cityList = await CreateCitiesList();
                cityList.Sort((a, b) => string.Compare(a.Name, b.Name));

                TimeSpan cachingTime = TimeSpan.FromMinutes(5);
                _memoryCache.Set(0, cityList, cachingTime);
            }

            return cityList;
        }

        private async Task<List<City>> CreateCitiesList()
        {
            var stream = await GetDataStream();
            using var reader = new StreamReader(stream);

            List<City> cities = new();
            bool skipped = false;

            #region Data parse
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!skipped)
                {
                    skipped = true;
                    continue;
                }
                var row = line.Split(',');
                if (string.IsNullOrWhiteSpace(row[1])) continue;
                cities.Add(new City
                {
                    Name = row[1].Trim(),
                    Location = new()
                    {
                        Longitude = -float.Parse(row[2], CultureInfo.InvariantCulture),
                        Latitude = float.Parse(row[3], CultureInfo.InvariantCulture)
                    }
                });
            } 
            #endregion

            return cities;
        }

        private async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(dataUrl, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
