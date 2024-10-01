using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using static Backend_Teamwork.src.DTO.OrderDTO;

namespace Backend_Teamwork.src.Services.order
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(OrderRepository OrderRepository, IMapper mapper)
        {
            _orderRepository = OrderRepository;
            _mapper = mapper;
        }

        // Get all
        public async Task<List<OrderReadDto>> GetAllAsync()
        {
            var OrderList = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<Order>, List<OrderReadDto>>(OrderList);
        }

        // Create
        public async Task<OrderReadDto> CreateOneAsync(OrderCreateDto createDto)
        {
            var OrderCreated = await _orderRepository.CreateOneAsync(
                _mapper.Map<OrderCreateDto, Order>(createDto)
            );
            return _mapper.Map<Order, OrderReadDto>(OrderCreated);
        }

        // Get by id
        public async Task<OrderReadDto> GetByIdAsync(Guid id)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<Order, OrderReadDto>(foundOrder);
        }

        // Delete
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
                return false;
            return await _orderRepository.DeleteOneAsync(foundOrder);
        }

        // Update
        public async Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto updateDto)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
                return false;

            // Map the update DTO to the existing Order entity
            _mapper.Map(updateDto, foundOrder);

            return await _orderRepository.UpdateOneAsync(foundOrder);
            ;
        }
    }
}
