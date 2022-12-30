using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagementWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public static User usr= new User();

        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser(UserRegistrationDto userRegistrationDto )
        {
            CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            usr.Username= userRegistrationDto.Username;
            usr.PasswordHash= passwordHash;
            usr.PasswordSalt= passwordSalt;

            return Ok(usr);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserRegistrationDto userRegistrationDto)
        {
            if(usr.Username!=userRegistrationDto.Username)
            {
                return BadRequest("user name not found");
            }

            if (!VerifyPasswordHash(userRegistrationDto.Password, usr.PasswordHash,usr.PasswordSalt))
            { 
                return BadRequest("user password not correct");
            }

            string token = CreateToken(usr);
            return Ok(token);
        }

        private string CreateToken(User usr)
        {
            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usr.Username)

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey")
                .Value));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
           
            var token = new JwtSecurityToken(
                claims:claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:credential);

            var jsonWebToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jsonWebToken;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hm= new HMACSHA512())
            {
                passwordSalt = hm.Key;
                passwordHash = hm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hm = new HMACSHA512(usr.PasswordSalt))
            {
                var hash = hm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return hash.SequenceEqual(passwordHash);
            }
        }
    }
}
