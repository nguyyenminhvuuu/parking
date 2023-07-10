using User.DTO;
using User.Repositories.ConfigFirebase;
using User.Repositories.Interface;
using User.Repository;

namespace User.Repositories.Implements
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ConfigFireBaseClient firebase) : base(firebase)
        {
        }
    }
}
