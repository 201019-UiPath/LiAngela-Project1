using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using StoreDB.Models;

namespace StoreDB
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
        
        public DbSet<ProductStock> ProductStocks { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString("ChiYDB");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ProductStock>().HasKey("Id");

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Commuter Doe", PhoneNumber = "(123) 456 7890", EmailAddress = "commuter@doe.com", Password = "predictable" },
                new Customer { CustomerId = 2, Name = "Architect Doe", PhoneNumber = "(123) 456 7891", EmailAddress = "architect@doe.com", Password = "guessable" },
                new Customer { CustomerId = 3, Name = "Tourist Doe", PhoneNumber = "(123) 456 7892", EmailAddress = "tourist@doe.com", Password = "insecure" },
                new Customer { CustomerId = 4, Name = "Elder Doe", PhoneNumber = "(123) 456 7893", EmailAddress = "elder@doe.com", Password = "weak" },
                new Customer { CustomerId = 5, Name = "Junior Doe", PhoneNumber = "(123) 456 7894", EmailAddress = "jr@doe.com", Password = "compromisable" });

            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Riverwalk Station", PhoneNumber = "(312) 111 1111", Address = "100 Wolf Pt" },
                new Location { LocationId = 2, Name = "State St Station", PhoneNumber = "(312) 222 2222", Address = "200 State St" },
                new Location { LocationId = 3, Name = "North Branch Station", PhoneNumber = "(312) 333 3333", Address = "300 Halsted St" },
                new Location { LocationId = 4, Name = "Chinatown Station", PhoneNumber = "(312) 444 4444", Address = "400 Cermak Rd" },
                new Location { LocationId = 5, Name = "West Loop Station", PhoneNumber = "(312) 555 5555", Address = "500 Madison St" });

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, TripType = "Regular", TicketType = "One-Way", PassengerType = "Child", Price = 3 },
                new Product { ProductId = 2, TripType = "Regular", TicketType = "Week Pass", PassengerType = "Child", Price = 15 },
                new Product { ProductId = 3, TripType = "Regular", TicketType = "Month Pass", PassengerType = "Child", Price = 50 },
                new Product { ProductId = 4, TripType = "Regular + tour guide describing buildings", TicketType = "One-Way", PassengerType = "Child", Price = 5 },
                new Product { ProductId = 5, TripType = "Lake", TicketType = "One-Way", PassengerType = "Child", Price = 5 },
                new Product { ProductId = 6, TripType = "Regular", TicketType = "One-Way", PassengerType = "Adult", Price = 6 },
                new Product { ProductId = 7, TripType = "Regular", TicketType = "Week Pass", PassengerType = "Adult", Price = 30 },
                new Product { ProductId = 8, TripType = "Regular", TicketType = "Month Pass", PassengerType = "Adult", Price = 100 },
                new Product { ProductId = 9, TripType = "Regular + tour guide describing buildings", TicketType = "One-Way", PassengerType = "Adult", Price = 10 },
                new Product { ProductId = 10, TripType = "Lake", TicketType = "One-Way", PassengerType = "Adult", Price = 10 },
                new Product { ProductId = 11, TripType = "Regular", TicketType = "One-Way", PassengerType = "Senior", Price = 5 },
                new Product { ProductId = 12, TripType = "Regular", TicketType = "Week Pass", PassengerType = "Senior", Price = 25 },
                new Product { ProductId = 13, TripType = "Regular", TicketType = "Month Pass", PassengerType = "Senior", Price = 75 },
                new Product { ProductId = 14, TripType = "Regular + tour guide describing buildings", TicketType = "One-Way", PassengerType = "Senior", Price = 7 },
                new Product { ProductId = 15, TripType = "Lake", TicketType = "One-Way", PassengerType = "Senior", Price = 7 });

            int i = 1;
            modelBuilder.Entity<ProductStock>().HasData(
                new ProductStock { Id = i++, LocationId = 1, ProductId = 1, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 2, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 3, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 4, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 5, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 6, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 7, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 8, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 9, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 10, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 11, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 12, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 13, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 14, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 1, ProductId = 15, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 1, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 2, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 3, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 4, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 5, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 6, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 7, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 8, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 9, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 10, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 11, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 12, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 13, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 14, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 2, ProductId = 15, QuantityStocked = 50 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 1, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 2, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 3, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 6, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 7, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 8, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 11, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 12, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 3, ProductId = 13, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 1, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 2, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 3, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 6, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 7, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 8, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 11, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 12, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 4, ProductId = 13, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 1, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 2, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 3, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 6, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 7, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 8, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 11, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 12, QuantityStocked = 10 },
                new ProductStock { Id = i++, LocationId = 5, ProductId = 13, QuantityStocked = 10 });
        }
    }
}