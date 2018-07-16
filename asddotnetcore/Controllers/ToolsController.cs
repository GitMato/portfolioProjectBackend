using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

//using Microsoft.Extensions.DependencyInjection;

namespace asddotnetcore.Controllers
{
    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
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
        public async Task<ActionResult<List<Tool>>> Get()
        {
            logger.LogWarning("/api/tools GET");
            var tools = await _context.Tools.ToListAsync();
            if (tools == null)
            {
                logger.LogWarning("Tools not found...");
                return NotFound();
            }
            return tools;
        }

        // api/tools/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tool>> GetTool(int id)
        {
            logger.LogWarning("/api/tools GET - GetTool with id of "+id+".");
            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                logger.LogWarning("Tool does not exist...");
                return NotFound();
            }
            return tool;
        }

        // api/tools
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tool tool)
        //public IActionResult Post([FromBody] string name)
        {
            logger.LogWarning("/api/tools POST with the name: "+ tool.Name);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var tool = new Tool { Name = name };
            await _context.Tools.AddAsync(tool);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTool", new { id = tool.Id }, tool);
     
        }

        // PUT api/tools/5
        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Tool tool)
        {
            // modify row in db
            var toolFromId = await _context.Tools.FindAsync(id);
            if (toolFromId == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors, (y, z) => z.Exception.Message);

                return BadRequest(errors);
            }

            toolFromId.Name = tool.Name;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTool", new { id = toolFromId.Id }, tool);
        }

        // DELETE api/tools/5
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            //Delete row
            logger.LogWarning("/api/tools/" + id + " DELETE");
            var tool = _context.Tools.Find(id);
            if (tool == null)
            {
                logger.LogWarning("Tool not found...");
                //return NotFound();
            }
            else
            {
                _context.Tools.Remove(tool);
                await _context.SaveChangesAsync();
            }
        }
    }
}
