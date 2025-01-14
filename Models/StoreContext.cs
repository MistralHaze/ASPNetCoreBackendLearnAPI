using Microsoft.EntityFrameworkCore;

namespace BackendLearnUdemy.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext>options): base(options) { }

        //Esto si he entendido bien representa las tablas a las que me contecto
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
