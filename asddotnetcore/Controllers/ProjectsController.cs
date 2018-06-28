using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;

namespace asddotnetcore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ProjectsController : ControllerBase
    {

        ILogger logger = new LoggerFactory().AddConsole().CreateLogger("ProjectsController");

        private readonly MyWebApiContext _context;

        public ProjectsController(MyWebApiContext ctx)
        {
            _context = ctx;
        }



        // GET api/projects
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            
            logger.LogWarning("/api/projects GET");
            return new string[] { "Projects:", "value2" };
        }

        // GET api/projects/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/projects
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors, (y, z) => z.Exception.Message);

                return BadRequest(errors);
            }


            _context.Projects.Add(project);
            _context.SaveChanges();
            //Newtonsoft.Json.JsonConvert.DeserializeObject<MyWebApi.Models.Project>(project);
            
            //new row to db
            logger.LogWarning("/api/projects POST : " + project.Name);
            return Ok();
        }

        // PUT api/projects/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // modify row in db
        }

        // DELETE api/projects/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Delete row
        }
    }
}
