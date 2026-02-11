namespace PRAPBL.Models;

public class Booking
{
    public int Id { get; set; }

    public string BorrowerName { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; }

    public string Status { get; set; } = "Pending";

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
