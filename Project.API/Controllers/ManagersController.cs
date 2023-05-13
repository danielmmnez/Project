using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using Project.Shared.Entities;


namespace Project.API.Controllers
{
    [ApiController]
    [Route("/api/managers")]
    public class ManagersController : ControllerBase
    {
        private readonly DataContext _context;

        public ManagersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Managers.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Manager managers)
        {
            _context.Add(managers);
            await _context.SaveChangesAsync();
            return Ok(managers);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(x => x.Id == id);
            if (manager is null)
            {
                return NotFound();
            }

            return Ok(manager);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Manager manager)
        {
            _context.Update(manager);
            await _context.SaveChangesAsync();
            return Ok(manager);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Managers
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

    }

}
