using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace asddotnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();
        //private ILogger logger = loggerFactory.CreateLogger("ToolsController");

        ILogger logger = new LoggerFactory().AddConsole().CreateLogger("ProjectsController");

        //public ProjectsController(ILoggerFactory loggerFactory)
        //{
        //    var logger = loggerFactory.CreateLogger("ToolsController-getAll");
        //}



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
        public void Post([FromBody] string value)
        {
            //new row to db
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
