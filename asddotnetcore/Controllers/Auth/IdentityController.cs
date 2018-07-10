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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asddotnetcore.Controllers
{

    

    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : Controller
    {
        ILogger logger = new LoggerFactory().AddConsole().CreateLogger("IdentityController");

        private readonly SignInManager<Admin> _signInManager;
        private readonly UserManager<Admin> _userManager;
        private readonly MyIdentityContext _identityContext;
        private readonly IConfiguration _configuration;
        private readonly JwtIssuerOptions _jwtOptions;

        public IdentityController(UserManager<Admin> userManager, 
                                  MyIdentityContext ctx, 
                                  SignInManager<Admin> signInManager,
                                  IConfiguration configuration,
                                  IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _identityContext = ctx;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;
        }

        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/identity/register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // map info from model to userIdentity
            //var userIdentity = _mapper.Map(model);

            var userIdentity = GenerateAdmin(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(error: "Error! " + result.Errors.ToString());
            
            await _identityContext.SaveChangesAsync();

            //await _signInManager.SignInAsync(user, false);
            //return await GenerateJwtToken(model.Email, user);
            
            return Ok("Registration complete!");
            //return Cre
        }

        // POST api/identity/login
        [Produces("application/json")]
        [HttpPost]
        public async Task<object> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // map info from model to userIdentity
            //var userIdentity = _mapper.Map(model);

            //var userIdentity = GenerateAdmin(model);


            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(error: "Wrong username+password combination!");
            }

            var admin = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
            return await GenerateJwtToken(model.Username, admin);
        }

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        // no need for this
        // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-2.1
        public string GenerateSaltedHash(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //derive a 256-bit subkey
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;

        }

        public Admin GenerateAdmin(RegistrationViewModel model)
        {
            return new Admin { UserName = model.Username };
        }

        private async Task<object> GenerateJwtToken(string username, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                //new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
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
                //claims: claims,
                //notBefore: _jwtOptions.NotBefore,
                expires: expires
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
