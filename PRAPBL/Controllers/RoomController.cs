using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRAPBL.Data;
using PRAPBL.DTOs;
using PRAPBL.Models;
namespace PRAPBL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly PRAPBLContext _context;

    public RoomController(PRAPBLContext context)
    {
        _context = context;
    }
    

    //CREATE ROOM
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomDto dto)
    {
        var room = new Room
        {
            Name = dto.Name,
            Location = dto.Location
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return Ok(room);
    }

    //GET ALL ROOMS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _context.Rooms
            .AsNoTracking()
            .ToListAsync();

        return Ok(rooms);
    }

    //GET ROOM BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var room = await _context.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
            return NotFound();

        return Ok(room);
    }
    //UPDATE ROOM
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateRoomDto dto)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            return NotFound();

        room.Name = dto.Name;
        room.Location = dto.Location;

        await _context.SaveChangesAsync();

        return Ok(room);
    }
    //DELETE ROOM
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            return NotFound();
        
        var hasBookings = await _context.Bookings
            .AnyAsync(b => b.RoomId == id && !b.IsDeleted);
        if (hasBookings)
            return BadRequest("Cannot delete room with existing bookings.");


        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}