using AutoMapper;
using Backend_Teamwork.src.Entities;
using static Backend_Teamwork.src.DTO.ArtworkDTO;
using static Backend_Teamwork.src.DTO.CustomerDTO;
using static Backend_Teamwork.src.DTO.ArtistDTO;

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
            
      
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
                
            // Artist 
            CreateMap<Artist, ArtistReadDto>();
            CreateMap<ArtistCreateDto, Artist>();
            CreateMap<ArtistUpdateDto, Artist>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
        }
    }
}
