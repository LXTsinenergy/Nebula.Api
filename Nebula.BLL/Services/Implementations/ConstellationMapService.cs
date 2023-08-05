using Nebula.BLL.Services.Interfaces;
using Nebula.Domain.Enums;
using Nebula.Domain.Models;
using System.Drawing;

namespace Nebula.BLL.Services.Implementations
{
    public class ConstellationMapService : IConstellationMapService
    {
        public async Task<ConstellationMap> GetConstellationMap(ConstellationMapOptions options)
        {
            var map = new ConstellationMap();

            map.Options = options;
            map.Image = await DownloadConstellationMapImage(options);

            return map;
        }

        public ConstellationMapOptions SetConstellationMapOptions(
            DateTime date, 
            City location, 
            int starSize, 
            bool coordinatesAreVisible,
            bool constellationBorderIsVisible,
            bool starsAreColored,
            bool constellationsAreNamed,
            bool planetsAreNamed,
            ConstellationMapTheme themeVariant,
            Language language)
        {
            ConstellationMapOptions options = new()
            {
                #region Properties initialization
                Date = date,
                City = location,
                StarSize = starSize,
                CoordinatesAreVisible = coordinatesAreVisible,
                ConstellationBorderIsVisible = constellationBorderIsVisible,
                StarsAreColored = starsAreColored,
                ConstellationsAreNamed = constellationsAreNamed,
                PlanetsAreNamed = planetsAreNamed,
                ThemeVariant = themeVariant,
                Language = language 
                #endregion
            };
            
            return options;
        }

        private async Task<Bitmap> DownloadConstellationMapImage(ConstellationMapOptions options)
        {
            string url = $"http://www.astronet.ru/cgi-bin/skyc.cgi?ut={options.Date.Hour}&day={options.Date.Day}&month={options.Date.Month}&year={options.Date.Year}&longitude={options.City.Location.Longitude}&latitude={options.City.Location.Latitude}&azimuth=0&height=90&m={options.StarSize}&dgrids={Convert.ToInt32(options.CoordinatesAreVisible)}&dcbnd={Convert.ToInt32(options.ConstellationBorderIsVisible)}&dfig=1&colstars={Convert.ToInt32(options.StarsAreColored)}&names={Convert.ToInt32(options.ConstellationsAreNamed)}&xs=1024&theme={(int)options.ThemeVariant}&dpl={Convert.ToInt32(options.PlanetsAreNamed)}&drawmw=1&pdf=0&lang={(int)options.Language}";

            var stream = await GetStream(url);
            
            Bitmap constellationMapImage = new(stream);
            return constellationMapImage;
        }

        private async Task<Stream> GetStream(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var stream = await response.Content.ReadAsStreamAsync();

            return stream;
        }
    }
}
