using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using Project.Shared.Entities;


namespace Project.API.Controllers
{
    [ApiController]
    [Route("/api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly DataContext _context;

        public AnimalsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Animals.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Animal animals)
        {
            _context.Add(animals);
            await _context.SaveChangesAsync();
            return Ok(animals);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(x => x.Id == id);
            if (animal is null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Animal animal)
        {
            _context.Update(animal);
            await _context.SaveChangesAsync();
            return Ok(animal);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Animals
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
