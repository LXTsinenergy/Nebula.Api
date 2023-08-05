using Nebula.Domain.Models;

namespace Nebula.BLL.Services.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> GetCitiesAsync();
    }
}
