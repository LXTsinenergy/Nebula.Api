using Microsoft.AspNetCore.Mvc;
using Nebula.BLL.Services.Interfaces;
using Nebula.Domain.Dto;
using Nebula.Domain.Models;

namespace Nebula.Api.Controllers
{
    [Route("/constellation-map")]
    [ApiController]
    public class ConstellationMapController : Controller
    {
        private readonly IConstellationMapService _constellationMapService;
        private readonly ICityService _cityService;

        public ConstellationMapController(
            IConstellationMapService constellationMapService,
            ICityService cityService)
        {
            _constellationMapService = constellationMapService;
            _cityService = cityService;
        }

        [HttpPost("/constellation-map/create")]
        public async Task<IActionResult> GetConstellationImageAsync(ConstellationMapOptionsDto mapOptions, string cityName)
        {
            var options = await CreateConstellationMapOptionsAsync(mapOptions, cityName);

            var constellationMap = await _constellationMapService.GetConstellationMap(options);
            var image = constellationMap.Image;
            return Ok(image);
        }

        [HttpGet("/constellation-map/cities")]
        public async Task<IActionResult> GetCitiesAsync()
        {
            var cityList = await _cityService.GetCitiesAsync();
            return Ok(cityList);
        }

        private async Task<City> GetCityAsync(string cityName)
        {
            var cityList = await _cityService.GetCitiesAsync();
            return cityList.Find(c => c.Name == cityName);
        }

        private async Task<ConstellationMapOptions> CreateConstellationMapOptionsAsync(ConstellationMapOptionsDto mapOptionsDto, string cityName)
        {
            ConstellationMapOptions options = new()
            {
                Date = mapOptionsDto.Date,
                City = await GetCityAsync(cityName),
                StarSize = mapOptionsDto.StarSize,
                CoordinatesAreVisible = mapOptionsDto.CoordinatesAreVisible,
                ConstellationBorderIsVisible = mapOptionsDto.ConstellationBorderIsVisible,
                StarsAreColored = mapOptionsDto.StarsAreColored,
                ConstellationsAreNamed = mapOptionsDto.ConstellationsAreNamed,
                PlanetsAreNamed = mapOptionsDto.PlanetsAreNamed,
                ThemeVariant = mapOptionsDto.ThemeVariant,
                Language = mapOptionsDto.Language,
            };

            return options;
        }
    }
}
