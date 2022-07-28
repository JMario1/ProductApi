using productMgtApi.Domain.Models;
using productMgtApi.Domain.Services;

namespace productMgtApi.Domain.Services
{
    public interface IAppUserService
    {
        Task<Response<AppUser>> CreateUserAsync(AppUser user, string password,  UserRoles role); 
        Task<AppUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<IList<string>> GetRoleAsync(AppUser user);
    }
}