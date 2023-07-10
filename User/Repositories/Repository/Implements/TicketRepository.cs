using User.DTO;
using User.Repositories.ConfigFirebase;
using User.Repositories.Interface;
using User.Repository;

namespace User.Repositories.Implements
{
    public class TicketRepository : Repository<Ticket>,ITicketRepository
    {
        public TicketRepository(ConfigFireBaseClient firebase) : base(firebase)
        {
        }
    }
}
