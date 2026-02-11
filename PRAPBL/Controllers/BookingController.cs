using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRAPBL.Data;
using PRAPBL.DTOs;
using PRAPBL.Models;
namespace PRAPBL.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly PRAPBLContext _context;

    public BookingController(PRAPBLContext context)
    {
        _context = context;
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var booking = new Booking
        {
            BorrowerName = dto.BorrowerName,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            RoomId = dto.RoomId
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var statusHistory = new BookingStatusHistory
        {
            BookingId = booking.Id,
            Status = booking.Status,
            ChangedAt = DateTime.Now
        };

        _context.BookingStatusHistories.Add(statusHistory);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll(
        string? search = "",
        DateTime? startDate = null,
        DateTime? endDate = null,
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _context.Bookings
            .AsNoTracking()
            .Where(b => !b.IsDeleted);

        
        //SEARCH
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(b => b.BorrowerName.Contains(search));
        }

        //TOTALDATA
        var totalItems = await query.CountAsync();

        //PAGINATION
        var data = await query
            .OrderByDescending(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        //Filter DATE
        if (startDate.HasValue)
        {
            data = data.Where(b => b.StartTime.Date >= startDate.Value.Date).ToList();
        }
        if (endDate.HasValue)
        {
            data = data.Where(b => b.EndTime.Date <= endDate.Value.Date).ToList();
        }
        return Ok(new
        {
            page,
            pageSize,
            totalItems,
            totalpages = (int)Math.Ceiling((double)totalItems / pageSize),
            data
        });
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await _context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

        if (booking == null)
            return NotFound();

        return Ok(booking);
    }
    //HISTORY + LOG
    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetHistory(int id)
    {
        var histories = await _context.BookingStatusHistories
            .AsNoTracking()
            .Where(h => h.BookingId == id)
            .OrderByDescending(h => h.ChangedAt)
            .ToListAsync();

        return Ok(histories);
    }

    [HttpGet("history")]
        public async Task<IActionResult> GetAllHistory()
        {
            var data = await _context.BookingStatusHistories
                .Include(h => h.BookingId)
                .OrderByDescending(h => h.ChangedAt)
                .ToListAsync();

            return Ok(data);
        }

    [HttpGet("log")]
    public async Task<IActionResult> GetBookingLogs()
    {
        var data = await _context.Bookings
            .Include(b => b.Room)
            .Where(b => !b.IsDeleted)
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BookingHistoryList
            {
                Id = b.Id,
                BorrowerName = b.BorrowerName,
                RoomName = b.Room.Name,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                CreatedAt = b.CreatedAt,
                Status = b.Status
            })
            .ToListAsync();

        return Ok(data);
    }
    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateBookingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null || booking.IsDeleted)
            return NotFound();

        booking.BorrowerName = dto.BorrowerName;
        booking.StartTime = dto.StartTime;
        booking.EndTime = dto.EndTime;
        booking.RoomId = dto.RoomId;

        await _context.SaveChangesAsync();

        return Ok(booking);
    }

    //UPDATE STATUS
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null || booking.IsDeleted)
            return NotFound();
        
        booking.Status = dto.Status;

        var statusHistory = new BookingStatusHistory
        {
            BookingId = booking.Id,
            Status = dto.Status,
            ChangedAt = DateTime.Now
        };

        _context.BookingStatusHistories.Add(statusHistory);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Status updated successfully",
            booking.Status
        });
    }

    // DELETE (soft delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking == null || booking.IsDeleted)
            return NotFound();

        booking.IsDeleted = true;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
