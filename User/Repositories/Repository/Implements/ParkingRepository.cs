using User.Repositories.ConfigFirebase;
using User.Repository.Interface;
namespace User.Repository.Implements
{
    public class ParkingRepository : Repository<DTO.Parking>, IParkingRepository
    {
        public ParkingRepository(ConfigFireBaseClient firebase) : base(firebase)
        {
        }
    }
}
