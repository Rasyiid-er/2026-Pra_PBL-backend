namespace PRAPBL.Models;

public class BookingStatusHistory
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public string Status { get; set; }
    public DateTime ChangedAt { get; set; }
}