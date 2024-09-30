using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.DTO;
using static Backend_Teamwork.src.DTO.ArtistDTO;
using Backend_Teamwork.src.Entities;




namespace Backend_Teamwork.src.Services.artist
{
    public class ArtistService : IArtistService
    {
        private readonly ArtistRepository _artistRepo;
        private readonly IMapper _mapper;
        // DI
        public ArtistService(ArtistRepository artistRepo, IMapper mapper)
        {
            _artistRepo = artistRepo;
            _mapper = mapper;
        }

        public async Task<ArtistReadDto> CreateOneAsync(ArtistCreateDto createDto)
        {
            var artist = _mapper.Map<ArtistCreateDto, Artist>(createDto);


            var artistCreated = await _artistRepo.CreateOneAsync(artist);

            return _mapper.Map<Artist, ArtistReadDto>(artistCreated);
        }
        public async Task<List<ArtistReadDto>> GetAllAsync()
        {
            var artistList = await _artistRepo.GetAllAsync();
            return _mapper.Map<List<Artist>, List<ArtistReadDto>>(artistList);
        }
        public async Task<ArtistReadDto> GetByIdAsync(Guid id)
        {
            var foundArtist = await _artistRepo.GetByIdAsync(id);

            return _mapper.Map<Artist, ArtistReadDto>(foundArtist);
        }
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundArtist = await _artistRepo.GetByIdAsync(id);
            if (foundArtist == null) return false; // Check if artist exists

            bool isDeleted = await _artistRepo.DeleteOneAsync(foundArtist);
            if (isDeleted)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateOneAsync(Guid id, ArtistUpdateDto updateDto)
        {
            var foundArtist = await _artistRepo.GetByIdAsync(id);

            if (foundArtist == null)
            {
                return false;
            }

            // keep old one - update a part of it
            _mapper.Map(updateDto, foundArtist);
            return await _artistRepo.UpdateOneAsync(foundArtist);
        }


        public async Task<ArtistReadDto> GetByNameAsync(string name)
        {
            var artist = await _artistRepo.GetByNameAsync(name);
            return _mapper.Map<Artist, ArtistReadDto>(artist);
        }
        public async Task<ArtistReadDto> GetByEmailAsync(string email)
        {
            var artist = await _artistRepo.GetByEmailAsync(email);
            return _mapper.Map<Artist, ArtistReadDto>(artist);
        }

        public async Task<ArtistReadDto> GetByPhoneNumberAsync(string phoneNum)
        {
            var artist = await _artistRepo.GetByPhoneNumberAsync(phoneNum);
            return _mapper.Map<Artist, ArtistReadDto>(artist);
        }

    }
}