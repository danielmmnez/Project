﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Data;
using Project.Shared.Entities;


namespace Project.API.Controllers
{
    [ApiController]
    [Route("/api/diseases")]
    public class DiseasesController : ControllerBase
    {
        private readonly DataContext _context;

        public DiseasesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Diseases.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Disease diseases)
        {
            _context.Add(diseases);
            await _context.SaveChangesAsync();
            return Ok(diseases);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var disease = await _context.Diseases.FirstOrDefaultAsync(x => x.Id == id);
            if (disease is null)
            {
                return NotFound();
            }

            return Ok(disease);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Disease disease)
        {
            _context.Update(disease);
            await _context.SaveChangesAsync();
            return Ok(disease);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Diseases
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
