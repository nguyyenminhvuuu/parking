using User.DTO;

namespace User.Service.Interface
{
    public interface IParkingDetailServices
    {
        Task<List<ParkingDetail>> GetAll();
        Task<List<ParkingDetail>> GetParkingDetailByIdParking(Guid? idParking);
        Task<ParkingDetail> GetParkingDetailById(string? idParkingDetail);
    }
}
