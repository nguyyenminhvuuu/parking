using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Model.Order;
using User.Service.Interface;
using User.Model.User;
namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _service;

        public OrderController(IOrderService order)
        {
            _service = order;
        }

        [HttpPost]
        public async Task<IActionResult> OrderBooking(OrderRequest order)
        {
            try
            {
                return Ok(await _service.Booking(order));

            }catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("check-in/{id}")]
        public async Task<IActionResult> CheckIn([FromRoute]Guid id)
        {
            try
            {

                return await _service.CheckIn(id) ?Ok("success"):BadRequest("Your action not support");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("check-out/{id}")]
        public async Task<IActionResult> CheckOut([FromRoute] Guid id)
        {
            try
            {

                return await _service.CheckOut(id) ? Ok("success") : BadRequest("Your action not support");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
