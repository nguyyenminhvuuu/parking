using User.DTO;
using User.Repositories.ConfigFirebase;
using User.Repository.Interface;

namespace User.Repository.Implements
{
    public class ParkingDetailRepository : Repository<ParkingDetail>, IParkingDetailRepository
    {
        public ParkingDetailRepository(ConfigFireBaseClient firebase) : base(firebase)
        {
        }
    }
}
