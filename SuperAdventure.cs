global using Microsoft.EntityFrameworkCore;

namespace aspnetharjoitus
{
    public class SuperAdventure : DbContext
    {
        //Constructor
        public SuperAdventure(DbContextOptions<SuperAdventure> options) : base(options) { }

        //execute a LINQ query and save data
        public DbSet<Stat> Stats { get; set; }

    }
}
