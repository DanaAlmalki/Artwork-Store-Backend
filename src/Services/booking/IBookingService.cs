using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Backend_Teamwork.src.DTO.BookingDTO;

namespace Backend_Teamwork.src.Services.booking
{
    public interface IBookingService
    {
        Task<List<BookingReadDto>> GetAllAsync();
        Task<BookingReadDto> GetByIdAsync(Guid id,Guid userId, string userRole);
        Task<List<BookingReadDto>> GetByUserIdAsync(Guid userId);
        Task<List<BookingReadDto>> GetByStatusAsync(string status);
        Task<List<BookingReadDto>> GetByUserIdAndStatusAsync(Guid userId, string status);
        Task<List<BookingReadDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<List<BookingReadDto>> GetByUserIdWithPaginationAsync(
            Guid userId,
            int pageNumber,
            int pageSize
        );
        Task<BookingReadDto> CreateAsync(BookingCreateDto booking, Guid userId);
        Task<BookingReadDto> ConfirmAsync(Guid id);
        Task<BookingReadDto> RejectAsync(Guid id);
        Task<BookingReadDto> CancelAsync(Guid id, Guid userId);
    }
}