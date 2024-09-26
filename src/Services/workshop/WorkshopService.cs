using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using static Backend_Teamwork.src.DTO.WorkshopDTO;

namespace Backend_Teamwork.src.Services.workshop
{
    public class WorkshopService : IWorkshopService
    {
        protected readonly WorkshopRepository _workshopRepo;
        protected readonly IMapper _mapper;

        public WorkshopService(WorkshopRepository workshopRepo, IMapper mapper)
        {
            _workshopRepo = workshopRepo;
            _mapper = mapper;
        }

        public async Task<WorkshopReadDTO> CreateOneAsync(WorkshopCreateDTO createworkshopDto)
        {
            var workshop = _mapper.Map<WorkshopCreateDTO, Payment>(createworkshopDto);
            var workshopCreated = await _workshopRepo.CreateOneAsync(workshop);
            return _mapper.Map<Payment, WorkshopReadDTO>(workshopCreated);

        }

        public async Task<List<WorkshopReadDTO>> GetAllAsync()
        {
            var workshopList = await _workshopRepo.GetAllAsync();
            return _mapper.Map<List<Payment>, List<WorkshopReadDTO>>(workshopList);
        }

        public async Task<WorkshopReadDTO> GetByIdAsync(Guid id)
        {
            var foundworkshop = await _workshopRepo.GetByIdAsync(id);
            return _mapper.Map<Payment, WorkshopReadDTO>(foundworkshop);

        }
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundworkshop = await _workshopRepo.GetByIdAsync(id);
            bool isDeleted = await _workshopRepo.DeleteOneAsync(foundworkshop);

            if (isDeleted)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOneAsync(Guid id, WorkshopUpdateDTO workshopupdateDto)
        {
            var foundworkshop = await _workshopRepo.GetByIdAsync(id);
            if (foundworkshop == null)
            {
                return false;
            }
            _mapper.Map(workshopupdateDto, foundworkshop);
            return await _workshopRepo.UpdateOneAsync(foundworkshop);

        }


    }
}