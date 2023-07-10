using User.DTO;
using User.Model.Order;
using User.Repository;
using User.Service.Interface;

namespace User.Service.Implement
{

    public class OrderService : IOrderService
    {
        private IUnitOfWork _context;
        private static string _childOrder = "Order";
        private static string _childTicket = "Ticket";
        private static string _childPkd = "ParkingDetail";
        private static double _discount =0;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork;
        }
        public async Task<Ticket> Booking(OrderRequest orderRequest)
        {
            ParkingDetail pkd = await _context.ParkingDetailRepository.GetByPrimaryKeyAsync(orderRequest.IdParkingDetail.ToString(), _childPkd);
            if (pkd.Status.Equals("None"))
            {
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    IdUser = orderRequest.IdUser,
                    IdParking = orderRequest.IdParking,
                    IdParkingDetail = orderRequest.IdParkingDetail,
                    Discount = _discount,
                    Price = pkd.Price,
                    Fee = orderRequest.Fee,
                    TotalMoney = pkd.Price,
                    LicensePlates = orderRequest.LicensePlates,
                    Time = orderRequest.Time,
                    DateCreate = DateTime.Now,
                    Status = "Booking"
                };
                Ticket ticket = new Ticket
                {
                    IdParking = order.Id,
                    IdParkingDetail = order.IdParkingDetail,
                    IdUser = orderRequest.IdUser,
                    LicensePlates = orderRequest.LicensePlates,
                    IdOrder = order.Id,
                    DateCreate = DateTime.Now,
                    Status = "Booking"
                };
                await _context.TicketRepository.AddUpdateAsync(ticket, _childTicket, ticket.IdOrder.ToString());
                await _context.OrderRepository.AddUpdateAsync(order, _childOrder, order.Id.ToString());
                return ticket;
            }return null;
        }

        public async Task<bool> CheckIn(Guid idOrder)
        {
            Order order =await _context.OrderRepository.GetByPrimaryKeyAsync(idOrder.ToString(), _childOrder);
            if (order.Status.Equals("Booking"))
            {
                Ticket ticket = await _context.TicketRepository.GetByPrimaryKeyAsync(idOrder.ToString(), _childTicket);
                ParkingDetail pkd = await _context.ParkingDetailRepository.GetByPrimaryKeyAsync(order.IdParkingDetail.ToString(), _childPkd);
                order.CheckIn = DateTime.Now;
                order.Status = "CheckIn";
                ticket.Status = "CheckIn";
                pkd.Status = "Busy";
                await _context.TicketRepository.AddUpdateAsync(ticket, _childTicket, ticket.IdOrder.ToString());
                await _context.ParkingDetailRepository.AddUpdateAsync(pkd, _childPkd, pkd.Id.ToString());
                await _context.OrderRepository.AddUpdateAsync(order, _childOrder, order.Id.ToString());
                return true;
            }return false;
        }

        public async Task<bool> CheckOut(Guid idOrder)
        {
            Order order =await _context.OrderRepository.GetByPrimaryKeyAsync(idOrder.ToString(), _childOrder);
            if (order.Status.Equals("CheckIn"))
            {
                ParkingDetail pkd = await _context.ParkingDetailRepository.GetByPrimaryKeyAsync(order.IdParkingDetail.ToString(), _childPkd);
                DateTime CheckOut = DateTime.Now;
                DateTime CheckIn = order.CheckIn;
                int hoursCheckOut = CheckOut.Hour;
                int hoursCheckIn = CheckIn.Hour;
                int hourOwn = hoursCheckOut - hoursCheckIn;
                if (hourOwn >= 6)
                {
                    order.Fee += order.Price;
                }

                if ((hoursCheckOut >= 0 && hoursCheckOut < 6) || (hoursCheckIn >= 0 && hoursCheckIn < 6))
                {
                    order.Fee = order.Price * 0.5;
                }
                pkd.Status = "None";
                order.TotalMoney = order.Price + order.Fee - order.Discount;
                order.CheckOut = DateTime.Now;
                order.Status = "CheckOut";
                await _context.OrderRepository.AddUpdateAsync(order, _childOrder, order.Id.ToString());
                await _context.ParkingDetailRepository.AddUpdateAsync(pkd, _childPkd, pkd.Id.ToString());
                return true;
            }return false;
        }
    }
}
