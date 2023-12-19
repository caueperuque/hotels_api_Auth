using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;

namespace TrybeHotel.Repository;
public class TrybeHotelContext : DbContext, ITrybeHotelContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DbSet<Booking> Bookings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public TrybeHotelContext(DbContextOptions<TrybeHotelContext> options) : base(options) {
        Seeder.SeedUserAdmin(this);
    }
    public TrybeHotelContext() { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=HotelsDB;User=SA;Password=SenhaSuperSecreta12!;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
                    .HasKey(h => h.HotelId);

        modelBuilder.Entity<Hotel>()
                    .HasOne(h => h.City)
                    .WithMany(c => c.Hotels)
                    .HasForeignKey(h => h.CityId);

        modelBuilder.Entity<City>()
                    .HasMany(c => c.Hotels)
                    .WithOne(h => h.City)
                    .HasForeignKey(h => h.CityId);

        modelBuilder.Entity<Room>()
                    .HasKey(r => r.RoomId);
        modelBuilder.Entity<Room>()
                    .HasOne(r => r.Hotel)
                    .WithMany(h => h.Rooms)
                    .HasForeignKey(r => r.HotelId);
    }

}