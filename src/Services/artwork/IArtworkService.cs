using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Teamwork.src.Utils;
using static Backend_Teamwork.src.DTO.ArtworkDTO;

namespace Backend_Teamwork.src.Services.artwork
{
    public interface IArtworkService
    {
        Task<ArtworkReadDto> CreateOneAsync(Guid userId, ArtworkCreateDto artwork);
        Task<List<ArtworkReadDto>> GetAllAsync(PaginationOptions paginationOptions);
        Task<ArtworkReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ArtworkUpdateDTO updateArtwork);
    }
}