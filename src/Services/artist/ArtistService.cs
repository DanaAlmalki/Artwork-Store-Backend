using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend_Teamwork.src.Utils;

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
            var foundArtistByEmail = await _artistRepo.GetByEmailAsync(createDto.Email);
            var foundArtistByPhoneNumber = await _artistRepo.GetByPhoneNumberAsync(createDto.PhoneNumber);

            if (foundArtistByEmail != null || foundArtistByPhoneNumber != null)
            {
                throw new InvalidOperationException("Email or phone number already in use.");
            }
            PasswordUtils.HashPassword(
                          createDto.Password,
                          out string hashedPassword,
                          out byte[] salt
                      );

            var artist = _mapper.Map<ArtistCreateDto, Artist>(createDto);
            artist.Password = hashedPassword;
            artist.Salt = salt;

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
            if (foundArtist == null)
                throw CustomException.BadRequest($"artist not found.");
            // Check if artist exists

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

                throw CustomException.BadRequest($"artist not found.");

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

        // public async Task<string> LoginAsync(ArtistCreateDto createDto)
        // {
        //     var foundArtist = await _artistRepo.GetByEmailAsync(createDto.Email);
        //     if (foundArtist == null)
        //     {
        //         return "NotFound";
        //     }

        //     //  تحقق من تطابق كلمة المرور
        //     bool isMatched = PasswordUtils.VerifyPassword(createDto.Password, foundArtist.Password, foundArtist.Salt);

        //     if (!isMatched)
        //     {
        //         return ("Invalid password.");
        //     }
        // }
        public async Task<ArtistReadDto> LoginAsync(ArtistCreateDto createDto)
        {
            var foundArtist = await _artistRepo.GetByEmailAsync(createDto.Email);

            if (foundArtist == null)
            {
                throw CustomException.NotFound($"Artist not found.");
            }

            // تحقق من تطابق كلمة المرور
            bool isMatched = PasswordUtils.VerifyPassword(createDto.Password, foundArtist.Password, foundArtist.Salt);

            if (!isMatched)
            {
                throw CustomException.BadRequest($"Invalid password.");
            }
            // في حال النجاح، ارجع الفنان
            return _mapper.Map<Artist, ArtistReadDto>(foundArtist);
        }

    }
}