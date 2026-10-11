using AutoMapper;
using SvaggerOpenIP.DataBase;
using SvaggerOpenIP.DTO;
using SvaggerOpenIP.DTO.SwaggerOpenApi.DTO;
namespace SvaggerOpenIP.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<CreateProductDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));
        }
    }
}
