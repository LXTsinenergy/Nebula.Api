using Nebula.Domain.Enums;
using Nebula.Domain.Models;

namespace Nebula.BLL.Services.Interfaces
{
    public interface IConstellationMapService
    {
        Task<ConstellationMap> GetConstellationMap(ConstellationMapOptions options);

        public ConstellationMapOptions SetConstellationMapOptions(
            DateTime date,
            City city,
            int starSize,
            bool coordinatesAreVisible,
            bool constellationBorderIsVisible,
            bool starsAreColored,
            bool constellationsAreNamed,
            bool planetsAreNamed,
            ConstellationMapTheme themeVariant,
            Language language);
    }
}
