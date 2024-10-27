// Controllers/PetController.cs
using Microsoft.AspNetCore.Mvc;
using PetAdoptionWithAzure.Data;
using PetAdoptionWithAzure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoptionWithAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly AnimalContext _context;

        public PetController(AnimalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAll()
        {
            return await _context.Animals.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetById(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Animal updatedAnimal)
        {
            if (id != updatedAnimal.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedAnimal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}
