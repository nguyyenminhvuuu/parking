using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Service.Interface;

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private IParkingService _service;

        public ParkingController(IParkingService parkingService) {
            _service = parkingService;
         }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("locaiton-id/{id}")]
        public async Task<IActionResult> GetParkingByLocationId([FromRoute]Guid id)
        {
            try
            {
                var rs =await _service.GetParkingByIdLocation(id);
                return rs != null ? Ok(rs) : NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("parkings")]
        public async Task<IActionResult> GetParkingByKey(string? key)
        {
            try
            {
                var rs =await _service.GetParkingByKeyLocation(key);
                return rs!=null ? Ok(new JsonResult(rs).Value) : NotFound();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetParkingById([FromRoute] Guid id)
        {
            try
            {
                var rs = await _service.GetParkingById(id);
                return rs!=null? Ok(rs) : NotFound();   
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
