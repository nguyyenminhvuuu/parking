using User.DTO;
using User.Model.Order;

namespace User.Service.Interface
{
    public interface IOrderService
    {
        Task<Ticket> Booking(OrderRequest orderRequest);
        Task<bool> CheckIn(Guid idOrder);
        Task<bool> CheckOut(Guid idOrder);

    }
}
