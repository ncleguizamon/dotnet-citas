using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using DatingApp.API.models;
using DatingApp.API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
// variables de entorno y configuración 
        private readonly IAuthRepository _repo;
     
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo ,  IConfiguration config)
        {
            _config = config;
            
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto )
        {
            if(!string.IsNullOrEmpty(userForRegisterDto.Username))
               userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.Username)) 
           ModelState.AddModelError("Username" , "Nombre de usuario ya existe.");
        if (!ModelState.IsValid)
        return BadRequest(ModelState);
            //register users 
           
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };
            var createUser= await _repo.register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody]UserForLoginDto userForloginDto)
        {
            
            var userFromRepo = await _repo.login(userForloginDto.Username.ToLower(), userForloginDto.Password);
            if(userFromRepo == null)
            return Unauthorized();
            //generete token 
            var tokenHandler= new JwtSecurityTokenHandler();
            var key =Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor= new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username),
                    new Claim(ClaimTypes.Role, userFromRepo.rol.ToString())
                }),

                 Expires = DateTime.Now.AddDays(1),
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)

            };
            var token= tokenHandler.CreateToken(tokenDescriptor);
            var tokenString  = tokenHandler.WriteToken(token);
            return Ok(new {tokenString});
            
           
        }
    }
}
