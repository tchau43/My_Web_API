using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly string _secretKey;
        private readonly AppSetting _appSetting;

        public UserController(MyDbContext context, IOptionsMonitor<AppSetting> optionsMonitor, IConfiguration configuration)
        {
            _context = context;
            _appSetting = optionsMonitor.CurrentValue;
            //another method to get secretkey
            //_secretKey = configuration["AppSetting:SecretKey"];
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.Users.SingleOrDefault(c => c.userName == model.userName && c.userPassword == model.userPassword);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = " Authenticate success",
                Data = token
            });
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []{
                    new Claim(ClaimTypes.Email, user.userEmail),
                    new Claim(ClaimTypes.Name, user.userName),
                    new Claim("UserName", user.userName),
                    new Claim("Id", user.userId.ToString()),

                    new Claim("TokenId", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
            };
        }
    }
}
