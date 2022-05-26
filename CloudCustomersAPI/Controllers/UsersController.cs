using CloudCustomersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomersAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService _usersService;

        ///private readonly ILogger<UsersController> _logger;

        //public UsersController(ILogger<UsersController> logger)
        //{
        //    _logger = logger;
        //}

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersService.GetAllUsers();

            if (users.Any())
            {
                return Ok(users);
            }

            return NotFound();
        }
    }
}