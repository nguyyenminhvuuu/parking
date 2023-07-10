using User.DTO;

namespace User.Service.Interface
{
    public interface ILocationService
    {
        Task CreateLocation(Location location);
        Task<List<Location>> GetAll();
        Task<Location> GetLocationById(Guid id);
    }
}
