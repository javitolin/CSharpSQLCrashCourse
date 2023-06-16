using Microsoft.AspNetCore.Mvc;
using MySqlWebApp.Entities;
using MySqlWebApp.Entities.Requests.User;
using MySqlWebApp.Services.Interfaces;

namespace MySqlWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                return Ok(user);
            }
            catch (Exception)
            {
                return NotFound(id);
            }
        }

        [HttpPost]
        public ActionResult<User> CreateUser(CreateRequest createRequest)
        {
            try
            {
                var user = _userService.CreateUser(createRequest);
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest(createRequest);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            try
            {
                var deletedUser = _userService.Delete(id);
                return Ok(deletedUser);
            }
            catch (Exception)
            {
                return NotFound(id);
            }
        }
    }
}