using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Backend_Teamwork.src.DTO.OrderDTO;

namespace Backend_Teamwork.src.Services.order
{
    public interface IOrderService
    {
        // Get all
        Task<List<OrderReadDto>> GetAllAsync();

        // create
        Task<OrderReadDto> CreateOneAsync(OrderCreateDto createDto);

        // Get by id
        Task<OrderReadDto> GetByIdAsync(Guid id);

        // delete
        Task<bool> DeleteOneAsync(Guid id);

        Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto updateDto);

        Task<OrderReadDto> GetByEmailAsync(string email);
    }
}
