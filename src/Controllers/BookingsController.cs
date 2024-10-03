using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.booking;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.BookingDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BookingReadDto>>> GetBookings()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin","Customer")]
        public async Task<ActionResult<BookingReadDto>> GetBookingById([FromRoute] Guid id)
        {
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userRole = authClaims.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;
            var convertedUserId = new Guid(userId);
            var booking = await _bookingService.GetByIdAsync(id, convertedUserId,userRole);
            return Ok(booking);
        }

        [HttpGet("search/{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BookingReadDto>>> GetByBookingsUserId(
            [FromRoute] Guid userId
        )
        {
            var booking = await _bookingService.GetByUserIdAsync(userId);
            return Ok(booking);
        }

        [HttpGet("my-bookings")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult<List<BookingReadDto>>> GetBookingsByUserId()
        {
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var convertedUserId = new Guid(userId);
            var booking = await _bookingService.GetByUserIdAsync(convertedUserId);
            return Ok(booking);
        }

        [HttpGet("search/{status:alpha}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BookingReadDto>>> GetBookingsByStatus(
            [FromRoute] string status
        )
        {
            var booking = await _bookingService.GetByStatusAsync(status);
            return Ok(booking);
        }

        [HttpGet("my-bookings/search/{status}")]
        //[Authorize(Roles = "customer")]
        public async Task<ActionResult<List<BookingReadDto>>> GetBookingsByUserIdAndStatus(
            [FromRoute] string status
        )
        {
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var convertedUserId = new Guid(userId);
            var booking = await _bookingService.GetByUserIdAndStatusAsync(convertedUserId, status);
            return Ok(booking);
        }

        [HttpGet("page")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BookingReadDto>>> GetBookingsWithPagination(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
        )
        {
            var bookings = await _bookingService.GetWithPaginationAsync(pageNumber, pageSize);
            return Ok(bookings);
        }

        [HttpGet("page/{userId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BookingReadDto>>> GetBookingsByUserIdWithPagination(
            [FromRoute] Guid userId,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
        )
        {
            var bookings = await _bookingService.GetByUserIdWithPaginationAsync(
                userId,
                pageNumber,
                pageSize
            );
            return Ok(bookings);
        }

        [HttpGet("my-bookings/page")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult<List<BookingReadDto>>> GetBookingsByUserIdWithPagination(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
        )
        {
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var convertedUserId = new Guid(userId);
            var bookings = await _bookingService.GetByUserIdWithPaginationAsync(
                convertedUserId,
                pageNumber,
                pageSize
            );
            return Ok(bookings);
        }

        [HttpPost]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult<BookingReadDto>> CreateBooking(
            [FromBody] BookingCreateDto bookingDTO
        )
        {
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var convertedUserId = new Guid(userId);
            var booking = await _bookingService.CreateAsync(bookingDTO, convertedUserId);
            return CreatedAtAction(nameof(CreateBooking), new { id = booking.Id }, booking);
        }

        [HttpPut("confirm/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookingReadDto>> ConfirmBooking([FromRoute] Guid id)
        {
            var booking = await _bookingService.ConfirmAsync(id);
            return Ok(booking);
        }

        [HttpPut("reject/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookingReadDto>> RejectBooking([FromRoute] Guid id)
        {
            var booking = await _bookingService.RejectAsync(id);
            return Ok(booking);
        }

        [HttpPut("my-bookings/cancel/{id}")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult<BookingReadDto>> CancelBooking([FromRoute] Guid id)
        {
            var authClaims = HttpContext.User;
            var userId = authClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var convertedUserId = new Guid(userId);
            var booking = await _bookingService.CancelAsync(id, convertedUserId);
            return Ok(booking);
        }
    }
}
