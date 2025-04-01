using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using OuathAuthentication;
using OuathAuthentication.Entities;
using OuathAuthentication.Models;
using OuathAuthentication.Services.Interfaces;

namespace OuathAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public UsersController(AuthContext context, IUserService userService, IConfiguration config)
        {
            _context = context;
            _userService = userService;
            _config = config;
        }

        // GET: api/Users
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            if (!users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser([FromQuery] string email)
        {
            var user = await _userService.GetUser(email);


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
            }


           // POST: api/Users/Register
            [HttpPost("Register")]
            public async Task<IActionResult> Register([FromBody] UserRequestModel model)
            {
                var registerUser = await _userService.RegisterUser(model);
                if (registerUser == null)
                {
                    return BadRequest("Registration failed");
                }
                return Ok(registerUser);
            }

            // POST: api/Users/Login
            [HttpPost("Login")]
            public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
            {
                var login = await _userService.LoginUser(model);
                if (login == null)
                    return BadRequest("Login Failed");
                return Ok(login);
            }


        }
    }


