using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppContext _context;
        private readonly JwtSettings _jwtSettings;
        public UsersController(UserManager<User> userManager,
            SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings, AppContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUser createUser)
        {
            if (await _userManager.FindByEmailAsync(createUser.Email) != null)
                return BadRequest(new { Message = "This user exists." });
            var user = new User
            {
                Email = createUser.Email,
                UserName = createUser.Email,
                RegistrationDate = DateTime.UtcNow,
                EmailConfirmed = true,
                Name = createUser.Name
            };
            var result = await _userManager.CreateAsync(user, createUser.Password);
            if (result.Succeeded)
                return Ok(new { Message = "User created successfully." });
            return BadRequest(new { Message = "Failed to create user." });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return BadRequest(new { Message = "This user does not exist." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);
            if (result.Succeeded)
                return Ok(new { token = await GenerateJWT(user), id = user.Id });
            
            return BadRequest(new { Message = "Failed to login." });
        }
        [HttpGet]
        public IActionResult Users()
        {
            var users = _context.Users.FromSqlRaw("select Id, Name, Email from AspNetUsers")
                .Select(c => new {id = c.Id, name = c.Name, email = c.Email });
            return Ok(users);
        }
        private async Task<string> GenerateJWT(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userRoles = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(type: "PhoneNumber", user.PhoneNumber ?? ""),
                    new Claim(type: "Email", user.Email ?? ""),
                    new Claim(type: "RegistrationDate", value: user.RegistrationDate.ToString()),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString())
                }.Union(userClaims).Union(userRoles)),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            await _context.SaveChangesAsync();
            return tokenHandler.WriteToken(token);
        }
    }
}
