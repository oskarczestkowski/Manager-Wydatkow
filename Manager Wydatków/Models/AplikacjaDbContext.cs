using Microsoft.EntityFrameworkCore;

namespace Manager_Wydatkow.Models
{
    public class AplikacjaDbContext:DbContext
    {
        public AplikacjaDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Transakcje> Transakcjes { get; set;}
        public DbSet<Kategoria> Kategorias { get; set; }
    }
}
