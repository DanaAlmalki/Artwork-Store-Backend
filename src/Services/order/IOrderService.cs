using Backend_Teamwork.src.Utils;
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

        Task<List<OrderReadDto>> GetOrdersByPage(PaginationOptions paginationOptions);
    }
}
