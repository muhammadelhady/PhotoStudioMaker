using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhotoMakerStudio.Data;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;

namespace PhotoMakerStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthrRpository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthrRpository repo, IConfiguration config)
        {

            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (userForRegisterDto == null)
                return BadRequest("data object is empty");

            userForRegisterDto.Name = userForRegisterDto.Name.ToLower();

            if (await _repo.UserIsExists(userForRegisterDto.Name))
                return BadRequest("user already exists");

            var userToCreate = new User
            {
                Name = userForRegisterDto.Name
            };

            var createdUser = await _repo.Resgister(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            if (userForLoginDto == null)
                return BadRequest("data object is empty");

            var userFromRepo = await _repo.Login(userForLoginDto.Name, userForLoginDto.Password);
            if ( userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.ID.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Name)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials= creds

            };


            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok ( new 
            { 
                token = tokenHandler.WriteToken(token)
            });

        }
        



    
    }
}
