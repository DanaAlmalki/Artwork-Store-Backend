using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using AutoMapper;
using static Backend_Teamwork.src.DTO.PaymentDTO;
namespace Backend_Teamwork.src.Services.payment
{
    public class PaymentService : IPaymentService
    {
        protected readonly PaymentRepository _paymentRepo;
        protected readonly IMapper _mapper;


        public PaymentService(PaymentRepository paymentRepo, IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task<PaymentReadDTO> CreateOneAsync(PaymentCreateDTO createpaymentDto)
        {
            var payment = _mapper.Map<PaymentCreateDTO, Payment>(createpaymentDto);
            var paymentCreated = await _paymentRepo.CreateOneAsync(payment);
            return _mapper.Map<Payment, PaymentReadDTO>(paymentCreated);

        }

        public async Task<List<PaymentReadDTO>> GetAllAsync()
        {
            var paymentList = await _paymentRepo.GetAllAsync();
            return _mapper.Map<List<Payment>, List<PaymentReadDTO>>(paymentList);
        }

        public async Task<PaymentReadDTO> GetByIdAsync(Guid id)
        {
            var foundpayment = await _paymentRepo.GetByIdAsync(id);
            return _mapper.Map<Payment, PaymentReadDTO>(foundpayment);

        }
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundpayment = await _paymentRepo.GetByIdAsync(id);
            bool isDeleted = await _paymentRepo.DeleteOneAsync(foundpayment);

            if (isDeleted)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOneAsync(Guid id, PaymentUpdateDTO paymentupdateDto)
        {
            var foundpayment = await _paymentRepo.GetByIdAsync(id);
            if (foundpayment == null)
            {
                return false;
            }
            _mapper.Map(paymentupdateDto, foundpayment);
            return await _paymentRepo.UpdateOneAsync(foundpayment);

        }



    }
}