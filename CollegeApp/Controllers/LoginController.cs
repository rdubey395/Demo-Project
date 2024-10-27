using CollegeApp.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("Login")]
        public  ActionResult LoginRoute(LoginModelDTO loginModelDTO)
        {
            if (!ModelState.IsValid) {
                BadRequest("Please provide Username and Password");
            }
            LoginResDTO loginResDTO = new LoginResDTO();    
            if (loginModelDTO.Username == "RajDubey" && loginModelDTO.Password == "Raj123")
            {
                string validIssuer = "";
                string validAudience = "";
                byte[] key = null ;
                if(loginModelDTO.PolicyName == "LocalHost")
                {
                    key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));
                    validIssuer = _configuration.GetValue<string>("LocalIssuer");
                    validAudience = _configuration.GetValue<string>("LocalAudience");
                }
               else if (loginModelDTO.PolicyName == "Demo")
                {
                    key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretDemo"));
                    validIssuer = _configuration.GetValue<string>("DemoIssuer");
                    validAudience = _configuration.GetValue<string>("DemoAudience");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokendescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = validIssuer,
                    Audience = validAudience,
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, loginModelDTO.Username),
                            new Claim(ClaimTypes.Role, "Admin")
                        }
                        ),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokendescriptor);
                var tokenGenerated = tokenHandler.WriteToken(token);
                loginResDTO.Username = loginModelDTO.Username;
                loginResDTO.token = tokenGenerated;
            }
            else
            {
                BadRequest("Invalid Username and Password");
            }

            return Ok(loginResDTO);
        }
        
    }
}
