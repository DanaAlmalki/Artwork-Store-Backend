using AutoMapper;
using Backend_Teamwork.src.Entities;
using static Backend_Teamwork.src.DTO.ArtistDTO;
using static Backend_Teamwork.src.DTO.ArtworkDTO; // <<< add this

namespace Backend_Teamwork.src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Artwork, ArtworkReadDto>();
            CreateMap<ArtworkCreateDto, Category>();
            CreateMap<ArtworkUpdateDTO, Category>().
            ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));


            // Artist 
            CreateMap<Artist, ArtistReadDto>();
            CreateMap<ArtistCreateDto, Artist>();
            CreateMap<ArtistUpdateDto, Artist>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

        }
    }
}