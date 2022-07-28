using Microsoft.AspNetCore.Identity;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Services;

namespace productMgtApi.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            AppUser user = await FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<Response<AppUser>> CreateUserAsync(AppUser user, string password, UserRoles role)
        {
            AppUser existingUser = await FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return new Response<AppUser>(false, "Email already in use.", null);
            }
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return new Response<AppUser>(false, "Server error", null);
            }

            string roleName = role.ToString();

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);

            return new Response<AppUser>(true, "Success", user);
        }

        public Task<AppUser> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetRoleAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}