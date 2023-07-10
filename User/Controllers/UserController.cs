using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Authentications;
using User.Model.User;
using User.Service.Interface;

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Authentication _authen;
        private IUserService _service;

        public UserController(IUserService userService, Authentication authentication)
        {
            _authen = authentication;
            _service = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AuthenticationUser(UserLogin userLogin)
        {
            try
            {
                var userResponse = (ResponseClient)await _service.LoginCheck(userLogin.Username, userLogin.Password);
                if (userResponse != null && userResponse.Data != null! && userResponse.Data != "")
                {
                    DTO.User user = (DTO.User)userResponse.Data;
                    string token = _authen.GeneratorToken(user);
                    return Ok((new JsonResult(new { Token = token })).Value);
                }
                return NotFound(userResponse);
            }
            catch
            {
                return BadRequest("Can't working");
            }
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

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequest userRequest)
        {
            try
            {
                return Ok(await _service.Add(userRequest));
            }
            catch
            {
                return BadRequest("Can't create user");
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserUpdate user)
        {
            try
            {
                return Ok(await _service.Update(user));
            }
            catch
            {
                return BadRequest("Can't update user");
            }
        }
    }
}
