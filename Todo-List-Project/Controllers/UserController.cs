using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo_List_Project.Data;
using Todo_List_Project.Models;

namespace Todo_List_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        // Constructor to inject AppDbContext
        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<ActionResult<List<Users>>> Register(Users newUser)
        {
            // Check if newUser is not null
            if (newUser != null)
            {
                // Add newUser
                _appDbContext.User.Add(newUser);
                await _appDbContext.SaveChangesAsync();
                return Ok(newUser);
            }
            return BadRequest("Object instance not set");
        }

        // POST: api/user/login
        [HttpPost("login")]
        public ActionResult Login(Users loginUser)
        {
            // Check if loginUser is null
            if (loginUser == null)
            {
                return BadRequest("Invalid login credentials");
            }

            // Find user in database by email and password
            var user = _appDbContext.User
                .FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

            if (user != null)
            {
                // Successful login, return user details
                return Ok(user);
            }

            // Invalid login credentials
            return Unauthorized();
        }
    }
}

 