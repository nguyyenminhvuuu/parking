using User.Repositories.ConfigFirebase;
using User.Repository.Implements;
using User.Repositories.Implements;
using User.Service.Interface;
using User.Repository.Interface;
using User.Repositories.Interface;
using User.Repositories.Repository.Interface;
using User.Repositories.Repository.Implements;

namespace User.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private  ConfigFireBaseClient _configFireBaseClient;
     
       

        public IUserRepository UserRepository { get; private set; }

        public IParkingRepository ParkingRepository { get; private set; }

        public IParkingDetailRepository ParkingDetailRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public ITicketRepository TicketRepository { get; private set; }

        public ILocationRepository LocationRepository { get; private set; }

        public UnitOfWork(ConfigFireBaseClient configFireBaseClient)
        {
            _configFireBaseClient=configFireBaseClient;
            ParkingRepository = new ParkingRepository(_configFireBaseClient);
            UserRepository = new UserRepository(_configFireBaseClient);
            ParkingDetailRepository = new ParkingDetailRepository(_configFireBaseClient);
            OrderRepository= new OrderRepository(_configFireBaseClient);
            TicketRepository= new TicketRepository(_configFireBaseClient);
            LocationRepository= new LocationRepository(_configFireBaseClient);
        }
    }
}
