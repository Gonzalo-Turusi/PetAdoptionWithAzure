// Data/AnimalContext.cs
using Microsoft.EntityFrameworkCore;
using PetAdoptionWithAzure.Models;

namespace PetAdoptionWithAzure.Data
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
    }
}
