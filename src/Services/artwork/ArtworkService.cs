using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Repository;
using Backend_Teamwork.src.Services.artwork;
using Backend_Teamwork.src.Utils;
using Microsoft.EntityFrameworkCore.Update.Internal;
using static Backend_Teamwork.src.DTO.ArtworkDTO;

namespace Backend_Teamwork.src.Services.artwork
{
    public class ArtworkService : IArtworkService
    {
        private readonly ArtworkRepository _artworkRepo;
        private readonly IMapper _mapper;

        public ArtworkService(ArtworkRepository artworkRepo, IMapper mapper)
        {
            _artworkRepo = artworkRepo;
            _mapper = mapper;
        }

        public async Task<ArtworkReadDto> CreateOneAsync(Guid artistId, ArtworkCreateDto createDto)
        {
            if (createDto == null)
            {
                throw CustomException.BadRequest("Artwork creation data cannot be null.");
            }
            var artwork = _mapper.Map<ArtworkCreateDto, Artwork>(createDto);
            artwork.ArtistId = artistId;
            var createdArtwork = await _artworkRepo.CreateOneAsync(artwork);
            return _mapper.Map<Artwork, ArtworkReadDto>(createdArtwork);
        }

        public async Task<List<ArtworkReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var artworkList = await _artworkRepo.GetAllAsync(paginationOptions);
            if (artworkList == null || !artworkList.Any())
            {
                throw CustomException.NotFound("No artworks found.");
            }
            return _mapper.Map<List<Artwork>, List<ArtworkReadDto>>(artworkList);
        }

        public async Task<ArtworkReadDto> GetByIdAsync(Guid id)
        {
            var artwork = await _artworkRepo.GetByIdAsync(id);
            if (artwork == null)
            {
                throw CustomException.NotFound($"Artwork with ID {id} not found.");
            }
            return _mapper.Map<Artwork, ArtworkReadDto>(artwork);
        }

        public async Task<List<ArtworkReadDto>> GetByArtistIdAsync(Guid id)
        {
            var artworks = await _artworkRepo.GetByArtistIdAsync(id);
            if (artworks == null || !artworks.Any())
            {
                throw CustomException.NotFound($"No artworks found for artist with ID {id}.");
            }
            var artworkList = _mapper.Map<List<Artwork>, List<ArtworkReadDto>>(artworks);
            return artworkList;
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundArtwork = await _artworkRepo.GetByIdAsync(id);
            if (foundArtwork == null)
            {
                throw CustomException.NotFound($"Artwork with ID {id} not found.");
            }
            bool isDeleted = await _artworkRepo.DeleteOneAsync(foundArtwork);

            return isDeleted;
        }

        public async Task<bool> UpdateOneAsync(Guid id, ArtworkUpdateDTO updateDto)
        {
            var foundArtwork = await _artworkRepo.GetByIdAsync(id);
            if (foundArtwork == null)
            {
                throw CustomException.NotFound($"Artwork with ID {id} not found.");
            }

            // keep old one - update a part of it
            _mapper.Map(updateDto, foundArtwork);
            return await _artworkRepo.UpdateOneAsync(foundArtwork);
        }
    }
}
