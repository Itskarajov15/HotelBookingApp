namespace HotelBooking.Data
{
    using HotelBooking.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class HotelBookingDbContext : IdentityDbContext
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<HotelImage> HotelImages { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<RoomImage> RoomImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<City>()
                .HasOne(x => x.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Hotel>()
                .HasOne(x => x.City)
                .WithMany(x => x.Hotels)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Hotel>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Hotels)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}