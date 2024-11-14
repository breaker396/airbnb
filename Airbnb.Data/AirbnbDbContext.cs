using Airbnb.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Airbnb.Data
{
    public class AirbnbDbContext : DbContext
    {
        public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomOrder> RoomOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Country_ID");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Category_ID");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Currency_ID");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Province_ID");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Country).WithMany(p => p.Provinces)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Province_CountryID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_User_ID");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Room_ID");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.User).WithMany(p => p.Rooms)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_UserID");
                entity.HasOne(d => d.Category).WithMany(p => p.Rooms)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_CategoryID");
                entity.HasOne(d => d.Currency).WithMany(p => p.Rooms)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Room_CurrencyID");
                entity.HasOne(d => d.Province).WithMany(p => p.Rooms)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Room_ProvinceID");
            });
            modelBuilder.Entity<RoomOrder>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_RoomOrder_ID");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.User).WithMany(p => p.RoomOrders)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_RoomOrder_UserID");
                entity.HasOne(d => d.Room).WithMany(p => p.RoomOrders)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_RoomOrder_RoomID");
            });
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    Name = "USA",
                    Id = 1
                });
            modelBuilder.Entity<Province>().HasData(
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
                });

            modelBuilder.Entity<Currency>().HasData(
                new Currency()
                {
                    Id = 1,
                    Code = "USD",
                    Name = "US Dollar",
                    Symbol = "$"
                });
            modelBuilder.Entity<Category>().HasData(
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
                });

            long sellerId = 12345678999999999;
            modelBuilder.Entity<User>().HasData(
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
                });
            Random random = new Random();
            modelBuilder.Entity<Room>().HasData(
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
            );
        }
    }
}
