using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentity.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asddotnetcore.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {

        private UserManager<Admin> _userManager;
        private readonly MyIdentityContext _identityContext;

        public IdentityController(UserManager<Admin> userManager, MyIdentityContext ctx)
        {
            _userManager = userManager;
            _identityContext = ctx;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/identity
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // map info from model to userIdentity
            //var userIdentity = _mapper.Map(model);

            var userIdentity = GenerateAdmin(model);

            var result = await _userManager.CreateAsync(userIdentity, GenerateSaltedHash(model.password));

            if (!result.Succeeded) return new BadRequestObjectResult(error: "Error!");

            await _identityContext.Admins.AddAsync(new Admin { });
            await _identityContext.SaveChangesAsync();
            return Ok("Registration complete!");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

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
            return new Admin { UserName = model.username };
        }
    }
}
