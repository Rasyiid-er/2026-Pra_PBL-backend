using Microsoft.EntityFrameworkCore;
using PRAPBL.Models;
namespace PRAPBL.Data;
public class PRAPBLContext : DbContext
{
    public PRAPBLContext(DbContextOptions<PRAPBLContext> options) : base(options)
    {
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingStatusHistory> BookingStatusHistories { get; set; }
}