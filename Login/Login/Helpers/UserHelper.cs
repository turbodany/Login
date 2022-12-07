using Login.Data;
using Login.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Login.Helpers
{
    public class UserHelper : IUserHelper

    {
        private readonly DataContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserHelper(DataContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddUserAsync(Usuario user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(Usuario user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName); //en el rol manager verifica si el rol existe
            if (!roleExists) //si no existe lo crea mediante el identity role
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName, //crea un rol con el nombre que el usuario mandó
                });
            }
        }

        public async Task<Usuario> GetUserAsinc(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsUserInRoleAsync(Usuario user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        Task<Usuario> IUserHelper.AddUserAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
