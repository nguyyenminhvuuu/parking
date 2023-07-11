using Microsoft.AspNetCore.Mvc;
using User.Service.Interface;

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingDetailController : ControllerBase
    {
        private IParkingDetailServices _service;

        public ParkingDetailController(IParkingDetailServices parkingService)
        {
            _service = parkingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rs = await _service.GetAll();
                return rs != null ? Ok(rs) : StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("parking-id/{id}")]
        public async Task<IActionResult> GetParkingDetailByIdParking([FromRoute] Guid id)
        {
            try
            {
                var rs = await _service.GetParkingDetailByIdParking(id);
                return (rs != null) ? Ok(rs) : NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetParkingDetailById([FromRoute] Guid id)
        {
            try
            {
                var rs = await _service.GetParkingDetailById(id.ToString());
                return rs != null ? Ok(rs) : NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
