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

        private readonly ArtworkRepository _artworkRepository;

        public OrderService(
            OrderRepository OrderRepository,
            IMapper mapper,
            ArtworkRepository artworkRepository
        )
        {
            _orderRepository = OrderRepository;
            _mapper = mapper;
            _artworkRepository = artworkRepository;
        }

        //-----------------------------------------------------

        // Retrieves all orders (Only Admin)
        public async Task<List<OrderReadDto>> GetAllAsync()
        {
            var OrderList = await _orderRepository.GetAllAsync();
            if (OrderList.Count == 0)
            {
                throw CustomException.NotFound($"Orders not found");
            }
            return _mapper.Map<List<Order>, List<OrderReadDto>>(OrderList);
        }

        // Retrieves all orders
        public async Task<List<OrderReadDto>> GetAllAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw CustomException.BadRequest("Invalid order ID");
            }
            var orders = await _orderRepository.GetOrdersByUserIdAsync(id);
            if (orders == null || !orders.Any())
            {
                throw CustomException.NotFound($"No orders found for user with id: {id}");
            }
            return _mapper.Map<List<Order>, List<OrderReadDto>>(orders);
        }

        //-----------------------------------------------------

        // Creates a new order
        public async Task<OrderReadDto> CreateOneAsync(Guid userId, OrderCreateDto createDto)
        {
            if (createDto == null)
            {
                throw CustomException.BadRequest("Order data cannot be null.");
            }

            // Ensure the order has at least one artwork to process
            if (createDto.OrderDetails == null || !createDto.OrderDetails.Any())
            {
                throw CustomException.BadRequest("No artworks provided in the order.");
            }

            // Loop through each artwork in the order
            foreach (var orderDetail in createDto.OrderDetails)
            {
                // Fetch the artwork from the repository by ID
                var artwork = await _artworkRepository.GetByIdAsync(orderDetail.Artwork.Id);

                if (artwork == null)
                {
                    throw CustomException.NotFound(
                        $"Artwork with ID: {orderDetail.Artwork.Id} not found."
                    );
                }

                // Check if the requested quantity is available
                if (artwork.Quantity < orderDetail.Quantity)
                {
                    throw CustomException.BadRequest(
                        $"Artwork {artwork.Title} does not have enough stock. Requested: {orderDetail.Quantity}, Available: {artwork.Quantity}."
                    );
                }

                // Reduce the artwork's available quantity
                artwork.Quantity -= orderDetail.Quantity;

                // Update the artwork in the repository
                await _artworkRepository.UpdateOneAsync(artwork);
            }

            // Set the order creation time to UTC
            createDto.CreatedAt = DateTime.UtcNow;

            // Map and save the order in the repository
            var newOrder = await _orderRepository.CreateOneAsync(
                _mapper.Map<OrderCreateDto, Order>(createDto)
            );

            // Return the created order as a DTO
            return _mapper.Map<Order, OrderReadDto>(newOrder);
        }

        //-----------------------------------------------------

        // Retrieves a order by their ID (Only Admin)
        public async Task<OrderReadDto> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw CustomException.BadRequest("Invalid order ID");
            }
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
            {
                throw CustomException.NotFound($"Order with ID {id} not found.");
            }
            return _mapper.Map<Order, OrderReadDto>(foundOrder);
        }

        // Retrieves a order by their ID
        public async Task<OrderReadDto> GetByIdAsync(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
            {
                throw CustomException.BadRequest("Invalid order ID");
            }
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
            {
                throw CustomException.NotFound($"Order with ID {id} not found.");
            }
            if (foundOrder.UserId != userId)
            {
                throw CustomException.Fotbidden("You are not authorized to view this order.");
            }

            return _mapper.Map<Order, OrderReadDto>(foundOrder);
        }

        //-----------------------------------------------------

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
