using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;

namespace TrybeHotel.Repository;
public class TrybeHotelContext : DbContext, ITrybeHotelContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public TrybeHotelContext(DbContextOptions<TrybeHotelContext> options) : base(options) {
        Seeder.SeedUserAdmin(this);
    }
    public TrybeHotelContext() { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=TrybeHotel;User=SA;Password=TrybeHotel12!;TrustServerCertificate=True");
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

        modelBuilder.Entity<Room>()
            .HasMany(b => b.Bookings)
            .WithOne(r => r.Room)
            .HasForeignKey(b => b.RoomId);

        modelBuilder.Entity<User>()
                    .HasKey(u => u.UserId);
        modelBuilder.Entity<User>()
                    .HasMany(b => b.Bookings)
                    .WithOne(u => u.User);
        
        modelBuilder.Entity<Booking>()
                    .HasKey(b => b.BookingId);
        modelBuilder.Entity<Booking>()
                    .HasOne(u => u.User)
                    .WithMany(b => b.Bookings);
                    
    }

}