using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.DTO;
using Backend_Teamwork.src.Repository;
using static Backend_Teamwork.src.DTO.OrderDTO;

namespace Backend_Teamwork.src.Services.order
{
    public class OrderService : IOrderService
    {
        private readonly OrderRespository _orderRepository;
        private readonly IMapper _mapper;

        public Task<OrderReadDto> CreateOneAsync(OrderCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderReadDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderReadDto> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<OrderReadDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
