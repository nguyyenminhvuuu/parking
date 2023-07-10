
using User.Repositories.Interface;
using User.Repositories.Repository.Interface;
using User.Repository.Interface;
using User.Service.Interface;

namespace User.Repository
{
    public interface IUnitOfWork
    {
        
        public IUserRepository UserRepository { get; }
        public IParkingRepository ParkingRepository { get; }
        public IParkingDetailRepository ParkingDetailRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public ILocationRepository LocationRepository { get; }
    }
}
