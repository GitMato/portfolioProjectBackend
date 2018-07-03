using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

//using Microsoft.Extensions.DependencyInjection;

namespace asddotnetcore.Controllers
{
    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ToolsController : ControllerBase
    {
        ILogger logger = new LoggerFactory().AddConsole().CreateLogger("ToolsController");

        private readonly MyWebApiContext _context;

        public ToolsController(MyWebApiContext ctx)
        {
            _context = ctx;
        }



        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    logger.LogWarning("/api/tools GET");
        //    return new string[] { "Tools:", "testiTool" };
        //   
        //}

        // api/tools
        [HttpGet]
        public ActionResult<Tool[]> Get()
        {
            logger.LogWarning("/api/tools GET");
            var tools = _context.Tools;
            if (tools == null)
            {
                logger.LogWarning("Tools not found...");
                return NotFound();
            }
            return tools.ToArray();
        }

        // api/tools/{id}
        [HttpGet("{id}")]
        public ActionResult<Tool> GetTool(int id)
        {
            logger.LogWarning("/api/tools GET - GetTool with id of "+id+".");
            var tool = _context.Tools.Find(id);
            if (tool == null)
            {
                logger.LogWarning("Tool does not exist...");
                return NotFound();
            }
            return tool;
        }
        
        // api/tools
        [HttpPost]
        public IActionResult Post([FromBody] Tool tool)
        //public IActionResult Post([FromBody] string name)
        {
            logger.LogWarning("/api/tools POST with the name: "+ tool.Name);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var tool = new Tool { Name = name };
            _context.Tools.Add(tool);
            _context.SaveChanges();

            return CreatedAtAction("GetTool", new { id = tool.Id }, tool);
     
        }

        // PUT api/tools/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // modify row in db
        }

        // DELETE api/tools/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Delete row
        }
    }
}
