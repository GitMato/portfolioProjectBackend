using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWebApi.Models;
using Newtonsoft.Json;

namespace asddotnetcore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class ProjectsController : ControllerBase
    {

        ILogger logger = new LoggerFactory().AddConsole().CreateLogger("ProjectsController");

        private MyWebApiContext _context;

        public ProjectsController(MyWebApiContext ctx)
        {
            _context = ctx;
        }



        // GET api/projects
        [HttpGet]
        public async Task<ActionResult<List<Project>>> Get()
        {
            
            logger.LogWarning("/api/projects GET");
            var projects = await _context.Projects.ToListAsync();
            if (projects == null)
            {
                logger.LogWarning("Projects not found...");
                return NotFound();
            }
            return projects;
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
        [Authorize]
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
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Project project)
        {
            // modify row in db
            var projectFromId = await _context.Projects.FindAsync(id);
            if (projectFromId == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors, (y, z) => z.Exception.Message);

                return BadRequest(errors);
            }
            //logger.LogCritical("Tools: " + project.Tools);

            projectFromId.Name = project.Name;
            projectFromId.ImgUrl = project.ImgUrl;
            projectFromId.ImgAlt = project.ImgAlt;
            projectFromId.Tools = project.Tools;
            projectFromId.Extraimg = project.Extraimg;
            projectFromId.Description = project.Description;
            projectFromId.Details = project.Details;

            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = projectFromId.Id }, project);

        }

        // DELETE api/projects/5
        // Need to return Task otherwise the DbContext is disposed before all actions are done. ( concurrency problem? )
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //Delete row
            logger.LogWarning("/api/projects/" + id + " DELETE");
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                logger.LogWarning("Project not found...");
                return NotFound();
            }
            else
            { 
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
