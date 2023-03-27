using Microsoft.EntityFrameworkCore;

namespace RestApiProject.Entitis
{
    // When files containing the info refered to certian tables and file ...DbContext are created
    // then in nuget Packed Manager typ add-migration NAME to create a migration
    public class RestaurantDbContext: DbContext
    {

        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=True;";

        // Each table has its own DbSet property
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Dish> Dishes { get; set; }


        // Defining certain options that entity has to own
        // for example - here column Name in table Restaurant must not be empty and must have 40 chars in
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().Property(r => r.Name).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Restaurant>().HasOne(a => a.Address).WithOne(a => a.Restaurant).
                HasForeignKey<Address>(a => a.RestaurantId);

            modelBuilder.Entity<Dish>().Property(n => n.Name).IsRequired();

            modelBuilder.Entity<Address>().Property(n => n.City).HasMaxLength(50);
            modelBuilder.Entity<Address>().Property(n => n.Street).HasMaxLength(50); 

        }

        // Connection to database 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString); 
        }
    }
}
