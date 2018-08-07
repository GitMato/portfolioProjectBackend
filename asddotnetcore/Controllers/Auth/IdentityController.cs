using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentity.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asddotnetcore.Controllers
{


    [Produces("application/json")]
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : Controller
    {
        ILogger logger = new LoggerFactory().AddConsole().CreateLogger("IdentityController");

        private readonly SignInManager<ApiUser> _signInManager;
        private readonly UserManager<ApiUser> _userManager;
        private readonly MyIdentityContext _identityContext;
        private readonly IConfiguration _configuration;
        private readonly JwtIssuerOptions _jwtOptions;

        public IdentityController(UserManager<ApiUser> userManager, 
                                  MyIdentityContext ctx, 
                                  SignInManager<ApiUser> signInManager,
                                  IConfiguration configuration,
                                  IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _identityContext = ctx;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;

            //_identityContext.Database.Migrate();
        }

        // POST api/identity/register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = GenerateUser(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(error: "Error! " + result.Errors.ToString());
            
            await _identityContext.SaveChangesAsync();
            
            return Ok("Registration complete!");
        }

        // POST api/identity/login
        //[Produces("application/json")]
        [HttpPost]
        public async Task<object> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(error: "Wrong username+password combination!");
            }

            var user = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
            return await GenerateJwtToken(model.Username, user);
        }

        // no need for this
        // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-2.1
        //public string GenerateSaltedHash(string password)
        //{
        //    // generate a 128-bit salt using a secure PRNG
        //    byte[] salt = new byte[128 / 8];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(salt);
        //    }

        //    //derive a 256-bit subkey
        //    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA1,
        //        iterationCount: 10000,
        //        numBytesRequested: 256 / 8));

        //    return hashed;

        //}

        public ApiUser GenerateUser(RegistrationViewModel model)
        {
            return new ApiUser { UserName = model.Username };
        }

        private async Task<object> GenerateJwtToken(string username, ApiUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                //new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Admin", user.IsAdmin.ToString())
            };

            
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));
            var expires = DateTime.Now.AddDays(1);
            //var expires = Convert.ToDateTime(_jwtOptions.ValidFor);

            var token = new JwtSecurityToken(
                //_configuration["JwtIssuer"],
                //_configuration["JwtAudience"],
                claims: claims,
                //expires: expires,
                signingCredentials: creds,

                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                //notBefore: _jwtOptions.NotBefore,
                //expires: expires
                expires: _jwtOptions.Expiration
                //signingCredentials: _jwtOptions.SigningCredentials
            );

            

            return new JwtSecurityTokenHandler().WriteToken(token);
             
        }
    }

    public class RegistrationViewModel
    {
        
        public string Username { get; set; }
        
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
