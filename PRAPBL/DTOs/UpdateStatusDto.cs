using System.ComponentModel.DataAnnotations;

namespace PRAPBL.DTOs;

public class UpdateStatusDto
{
    [Required]
    public string Status { get; set; }
}
