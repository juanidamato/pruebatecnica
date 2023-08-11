using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TekusWebAPI.Models.auth;
using TekusWebAPI.Utils;
using TekusCore.Domain.Commons;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TekusWebAPI.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generate access token with special claim for api access
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/auth/token")]
        public async Task<ActionResult> GetToken(CreateTokenInputDto input)
        {
            HttpMapperResultUtil mapperResultUtil = new();
            OperationResultModel<CreateTokenOutputDto> result = new OperationResultModel<CreateTokenOutputDto>();
            string access_token;

            //in real life you have to do a real implementation to check credentials
            await Task.Delay(500);

            if (input.User == "admin" && input.Password == "admin")
            {
                result.code = OperationResultCodes.OK;
                result.message = "Generated Token";
                access_token = GenerateToken();
                result.payload = new CreateTokenOutputDto();
                result.payload.access_token = access_token;
                return mapperResultUtil.MapToActionResult(result);
            }
            else
            {
                result.code = OperationResultCodes.NOT_AUTHORIZED;
                result.message = "Invalid credentials (use admin,admin for sample code)";
                return mapperResultUtil.MapToActionResult(result);
            }
        }

        private string GenerateToken()
        {
            string? Key = "";

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, "admin"),
                    new Claim(ClaimTypes.Name, "Super Administrator"),
                    new Claim(ClaimTypes.GivenName, $"Super Administrator DAmato"),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Role, "superadmin"),
                    new Claim("scp", "api.access")
                };
            Key = _config["Jwt:Key"];
            if (Key is not null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(720),
                    signingCredentials: credentials);

                var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return jwt;
            }
            return "";
        }
    }
}
