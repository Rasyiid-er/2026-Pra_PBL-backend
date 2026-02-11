using System.ComponentModel.DataAnnotations;
namespace PRAPBL.DTOs;

public class CreateRoomDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }
}