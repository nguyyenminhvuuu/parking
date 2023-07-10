using User.DTO;
using User.Repositories.ConfigFirebase;
using User.Repositories.Repository.Interface;
using User.Repository;

namespace User.Repositories.Repository.Implements
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ConfigFireBaseClient firebase) : base(firebase)
        {
        }
    }
}
