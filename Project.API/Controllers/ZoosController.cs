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
        public async Task<ActionResult> Post(Zoo zoo)
        {
            _context.Add(zoo);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(zoo);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un zoológico con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

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
            try
            {

                await _context.SaveChangesAsync();
                return Ok(zoo);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un registro con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

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
