using System.ComponentModel.DataAnnotations;
namespace PRAPBL.DTOs;
public class CreateBookingDto
{
    [Required]
    public String BorrowerName { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }

    public int RoomId { get; set; }
}