using Microsoft.AspNetCore.Mvc;
using User.DTO;
using User.Repository;
using User.Service.Interface;

namespace User.Service.Implement
{
    public class LocationService : ILocationService
    {
        private static List<Location> listLocation = new List<Location>();
        private IUnitOfWork _uow;
        private static string _child = "Location";
        public LocationService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public async Task CreateLocation(Location location)
        {
           await _uow.LocationRepository.AddUpdateAsync(location, _child, location.Id.ToString());
        }

        public async Task<List<Location>> GetAll()
        {
        
            var dataSnapshot = await _uow.LocationRepository.GetAllAsync(_child);
            if (dataSnapshot != null)
            {
                if (listLocation != null)
                {
                    listLocation.Clear();
                }
                foreach (var item in dataSnapshot)
                {
                    listLocation.Add(item.Object);
                }
                return listLocation;
            }
            return null;
        }

        public async Task<Location> GetLocationById(Guid id)
        {
            return await _uow.LocationRepository.GetByPrimaryKeyAsync(id.ToString(), _child);
        }
    }
}
