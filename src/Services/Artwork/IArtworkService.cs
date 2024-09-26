using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Backend_Teamwork.src.DTO.ArtworkDTO;

namespace Backend_Teamwork.src.Services.Artwork
{
    public interface IArtworkService
    {
        Task<ArtworkReadDto> CreateOneAsync(ArtworkCreateDto artwork);
        Task<List<ArtworkReadDto>> GetAllAsync();
        Task<ArtworkReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ArtworkUpdateDTO updateArtwork);
    }
}