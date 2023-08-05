using Microsoft.AspNetCore.Mvc;
using Nebula.BLL.Services.Interfaces;

namespace Nebula.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class MainPageController : Controller
    {
        private readonly ISpaceFactsService _spaceFactService;

        public MainPageController(ISpaceFactsService spaceFactsService)
        {
            _spaceFactService = spaceFactsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomSpaceFactAsync()
        {
            string fact = await _spaceFactService.GetRandomSpaceFact();
            return Ok(fact);
        }
    }
}
