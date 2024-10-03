using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Utils;
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

        // Retrieves all orders
        public async Task<List<OrderReadDto>> GetAllAsync()
        {
            var OrderList = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<Order>, List<OrderReadDto>>(OrderList);
        }

        // Creates a new order
        public async Task<OrderReadDto> CreateOneAsync(OrderCreateDto createDto)
        {
            if (createDto == null)
            {
                throw CustomException.BadRequest("Order data cannot be null.");
            }
            createDto.CreatedAt = DateTime.UtcNow; // Set to UTC
            var OrderCreated = await _orderRepository.CreateOneAsync(
                _mapper.Map<OrderCreateDto, Order>(createDto)
            );
            return _mapper.Map<Order, OrderReadDto>(OrderCreated);
        }

        // Retrieves a order by their ID
        public async Task<OrderReadDto> GetByIdAsync(Guid id)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
            {
                throw CustomException.NotFound("Order not found.");
            }
            return _mapper.Map<Order, OrderReadDto>(foundOrder);
        }

        // Deletes a order by their ID
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
            {
                throw CustomException.NotFound("Order not found.");
            }
            return await _orderRepository.DeleteOneAsync(foundOrder);
        }

        // Updates a order by their ID
        public async Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto updateDto)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
            {
                throw CustomException.NotFound("Order not found.");
            }

            // Map the update DTO to the existing Order entity
            _mapper.Map(updateDto, foundOrder);

            return await _orderRepository.UpdateOneAsync(foundOrder);
            ;
        }

        public async Task<List<OrderReadDto>> GetOrdersByPage(PaginationOptions paginationOptions)
        {
            // Validate pagination options
            if (paginationOptions.Limit <= 0)
            {
                throw CustomException.BadRequest("Limit should be greater than 0.");
            }

            if (paginationOptions.Offset < 0)
            {
                throw CustomException.BadRequest("Offset should be 0 or greater.");
            }
            var OrderList = await _orderRepository.GetAllAsync(paginationOptions);
            return _mapper.Map<List<Order>, List<OrderReadDto>>(OrderList);
        }
    }
}
