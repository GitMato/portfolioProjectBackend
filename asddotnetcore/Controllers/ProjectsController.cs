using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;
using Newtonsoft.Json;

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
        public async Task<ActionResult<Project[]>> Get()
        {
            
            logger.LogWarning("/api/projects GET");
            var projects = _context.Projects;
            if (projects == null)
            {
                logger.LogWarning("Projects not found...");
                return NotFound();
            }
            return projects.ToArray();
        }

        // GET api/projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            logger.LogWarning("/api/projects/"+id + " GET");
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                logger.LogWarning("Project not found...");
                return NotFound();
            }

            return project;
            //return "value";
        }

        // POST api/projects
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Project project)
        {
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors, (y, z) => z.Exception.Message);

                return BadRequest(errors);
            }

            //new row to db
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            logger.LogWarning("/api/projects POST : " + project.Name);
            //return Ok();
            return CreatedAtAction("Get", new { id = project.Id }, project);
        }

        // PUT api/projects/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] string value)
        {
            // modify row in db
        }

        // DELETE api/projects/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            //Delete row
            logger.LogWarning("/api/projects/" + id + " DELETE");
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                logger.LogWarning("Project not found...");
                //return NotFound();
            }
            else
            { 
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}
