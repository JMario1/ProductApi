using AutoMapper;
using productMgtApi.Controllers.Resources;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Security;

namespace productMgtApi.Mapping
{
    public class ResourceToModel : Profile
    {
        public ResourceToModel()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(a => a.Disabled, opt => opt.MapFrom(a => false))
                .ForMember(a => a.CreatedAt, opt => opt.MapFrom(a => DateTime.UtcNow));
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<CreateUserRequest, AppUser>();
            CreateMap<AccessToken, TokenResponse>()
                .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
                .ForMember(a => a.RefreshToken, opt => opt.MapFrom(a => a.RefreshToken.Token))
                .ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration));
        }
    }
}