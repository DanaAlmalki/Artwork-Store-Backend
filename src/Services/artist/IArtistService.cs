using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Backend_Teamwork.src.DTO.ArtistDTO;

namespace Backend_Teamwork.src.Services.artist
{
    public interface IArtistService
    {
        Task<ArtistReadDto> CreateOneAsync(ArtistCreateDto createDto);
        Task<List<ArtistReadDto>> GetAllAsync();
        Task<ArtistReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ArtistUpdateDto updateDto);
        // Task CreateAsync(ArtistCreateDto artistDTO);
        Task<ArtistReadDto> GetByEmailAsync(string email);
    }
}