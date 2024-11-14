using Airbnb.Data;
using Airbnb.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Test.Factory
{
    public class DbContextFactory
    {
        public static AirbnbDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AirbnbDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new AirbnbDbContext(options);
        }
        public static AirbnbDbContext CreateNewInMemoryDb()
        {
            AirbnbDbContext airbnbDbContext = GetMemoryContext();
            airbnbDbContext.Database.EnsureCreated();
            //SeedData(airbnbDbContext);
            return airbnbDbContext;
        }
        public static void SeedData(AirbnbDbContext airbnbDbContext)
        {
            airbnbDbContext.Countries.Add(new Data.Models.Country()
            {
                Name = "USA",
                Id = 1
            });
            airbnbDbContext.SaveChanges();
            airbnbDbContext.Provinces.AddRange(new List<Province>()
            {
                new Province()
                {
                    Id = 1,
                    Name = "Texas",
                    CountryId = 1
                },
                new Province()
                {
                    Id = 2,
                    Name = "Florida",
                    CountryId = 1
                }
            });
            airbnbDbContext.SaveChanges();
            airbnbDbContext.Currencies.Add(new Currency()
            {
                Id = 1,
                Code = "USD",
                Name = "US Dollar",
                Symbol = "$"
            });
            airbnbDbContext.SaveChanges();
            airbnbDbContext.Categories.AddRange(new List<Category>() 
            {
                new Category()
                {
                    Id = 1,
                    Name = "Camping"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Icons"
                },
                new Category()
                {
                    Id = 3,
                    Name = "CountrySide"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Amazing Views"
                }
            });
            airbnbDbContext.SaveChanges();
            long sellerId = 12345678999999999;
            airbnbDbContext.Users.AddRange(new List<User>() 
            {
                new User()
                {
                    Id = sellerId,
                    Name = "Seller",
                    Password = "1b4f0e9851971998e732078544c96b36c3d01cedf7caa332359d6f1d83567014"
                },
                new User()
                {
                    Id = 12345678999999998,
                    Name = "Buyer",
                    Password = "1b4f0e9851971998e732078544c96b36c3d01cedf7caa332359d6f1d83567014"
                }
            });
            airbnbDbContext.SaveChanges();
            sellerId = airbnbDbContext.Users.First().Id;
            Random random = new Random();
            airbnbDbContext.Rooms.AddRange(new List<Room>() 
            {
                new Room()
                {
                    Name = "Garden Views",
                    CurrencyId = 1,
                    Id = random.NextInt64(0, sellerId),
                    Adults = 2,
                    CategoryId = 1,
                    CheckinDate = DateTime.UtcNow.AddDays(1),
                    CheckoutDate = DateTime.UtcNow.AddDays(10),
                    CreatedBy = sellerId,
                    ProvinceId = 1,
                    Guests = 2,
                    Price = 0.3m,
                    Rating = 4.82m,
                    UserId = sellerId,
                    IsDeleted = false
                },
                new Room()
                {
                    Name = "River Views",
                    CurrencyId = 1,
                    Id = random.NextInt64(0, sellerId),
                    Adults = 5,
                    CategoryId = 2,
                    CheckinDate = DateTime.UtcNow.AddDays(2),
                    CheckoutDate = DateTime.UtcNow.AddDays(8),
                    CreatedBy = sellerId,
                    ProvinceId = 2,
                    Guests = 10,
                    Price = 2m,
                    Rating = 3.54m,
                    UserId = sellerId,
                    IsDeleted = false
                },
                new Room()
                {
                    Name = "Street Views",
                    CurrencyId = 1,
                    Id = random.NextInt64(0, sellerId),
                    Adults = 2,
                    CategoryId = 3,
                    CheckinDate = DateTime.UtcNow.AddDays(1),
                    CheckoutDate = DateTime.UtcNow.AddDays(20),
                    CreatedBy = sellerId,
                    ProvinceId = 1,
                    Guests = 5,
                    Price = 3.33m,
                    Rating = 2.53m,
                    UserId = sellerId,
                    IsDeleted = false
                },
                new Room()
                {
                    Name = "City Views",
                    CurrencyId = 1,
                    Id = random.NextInt64(0, sellerId),
                    Adults = 3,
                    CategoryId = 4,
                    CheckinDate = DateTime.UtcNow.AddDays(5),
                    CheckoutDate = DateTime.UtcNow.AddDays(9),
                    CreatedBy = sellerId,
                    ProvinceId = 1,
                    Guests = 4,
                    Price = 5.12m,
                    Rating = 3.56m,
                    UserId = sellerId,
                    IsDeleted = false
                },
                new Room()
                {
                    Name = "Ocean Views",
                    CurrencyId = 1,
                    Id = random.NextInt64(0, sellerId),
                    Adults = 6,
                    CategoryId = 1,
                    CheckinDate = DateTime.UtcNow.AddDays(9),
                    CheckoutDate = DateTime.UtcNow.AddDays(20),
                    CreatedBy = sellerId,
                    ProvinceId = 2,
                    Guests = 12,
                    Price = 9.34m,
                    Rating = 1.82m,
                    UserId = sellerId,
                    IsDeleted = false
                }
            });
            airbnbDbContext.SaveChanges();
        }
    }
}
