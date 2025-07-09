using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_Project_Management_API.Common;
using Simple_Project_Management_API.Data;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Models;

namespace Simple_Project_Management_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(AppDBContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Check if user already exists
                var existingUser = _context.Users.FirstOrDefault(e => e.Email == model.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "User already registered." });
                }

                var user = _mapper.Map<User>(model);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userData = new JWTModel
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    Email = user.Email
                };

                var token = JWTManager.GenerateJwtToken(userData, _config);

                return Ok(new { message = "Registration successful", token, userData });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.Users.Where(e => e.Email == Model.Email && e.Password == Model.Password).FirstOrDefault();
            if (user != null)
            {
                var userData = new JWTModel()
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    Email = user.Email
                };

                var token = JWTManager.GenerateJwtToken(userData, _config);
                return Ok(new { token, userData });
            }
            return Unauthorized();
        }

    }
}
