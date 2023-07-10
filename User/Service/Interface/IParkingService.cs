using Microsoft.AspNetCore.Mvc;
using User.DTO;
using User.Model.Parking;

namespace User.Service.Interface
{
    public interface IParkingService
    {
        public  Task<List<Parking>> GetAll();
        public  Task<List<Parking>> GetParkingByIdLocation(Guid idLocation);
        public Task<List<ParkingView>> GetParkingByKeyLocation(string? key);
        public Task<List<ParkingView>> GetParkingById(Guid idParking);
    }
}
