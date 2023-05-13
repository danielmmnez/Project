using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using Project.Shared.Entities;


namespace Project.API.Controllers
{
    [ApiController]
    [Route("/api/zoos")]
    public class ZoosController : ControllerBase
    {
        private readonly DataContext _context;

        public ZoosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Zoos.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Zoo zoos)
        {
            _context.Add(zoos);
            await _context.SaveChangesAsync();
            return Ok(zoos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var zoo = await _context.Zoos.FirstOrDefaultAsync(x => x.Id == id);
            if (zoo is null)
            {
                return NotFound();
            }

            return Ok(zoo);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Zoo zoo)
        {
            _context.Update(zoo);
            await _context.SaveChangesAsync();
            return Ok(zoo);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Zoos
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
