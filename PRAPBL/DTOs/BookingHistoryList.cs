public class BookingHistoryList
{
    public int Id { get; set; }
    public string BorrowerName { get; set; }
    public string RoomName { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; }
}