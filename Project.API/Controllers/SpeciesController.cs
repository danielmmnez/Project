using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using Project.Shared.Entities;


namespace Project.API.Controllers
{
    [ApiController]
    [Route("/api/species")]
    public class SpeciesController : ControllerBase
    {
        private readonly DataContext _context;

        public SpeciesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Species.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Specie species)
        {
            _context.Add(species);
            await _context.SaveChangesAsync();
            return Ok(species);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var specie = await _context.Species.FirstOrDefaultAsync(x => x.Id == id);
            if (specie is null)
            {
                return NotFound();
            }

            return Ok(specie);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Specie specie)
        {
            _context.Update(specie);
            await _context.SaveChangesAsync();
            return Ok(specie);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Species
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
