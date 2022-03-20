using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class CalorieCounterContext : DbContext
    {
        public DbSet<Dan> Dani { get; set; }
        public DbSet<Hrana> Hrana { get; set; }
        public DbSet<Obrok> Obroci { get; set; }
        public DbSet<Osoba> Osobe { get; set; }
        public DbSet<Porcija> Porcije { get; set; }
        
        public CalorieCounterContext(DbContextOptions options):base(options)
        {
            
        }
    }
}