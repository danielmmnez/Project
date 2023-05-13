using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using Project.Shared.Entities;
using System;


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
        public async Task<ActionResult> Post(Specie specie)
        {
            _context.Add(specie);
            try
            {

                await _context.SaveChangesAsync();
            return Ok(specie);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
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
            try
            {

                await _context.SaveChangesAsync();
            return Ok(specie);
        }
    catch (DbUpdateException dbUpdateException)
    {
        if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
        {
            return BadRequest("Ya existe un país con el mismo nombre.");
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
