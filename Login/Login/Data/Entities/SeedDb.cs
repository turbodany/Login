using Login.Enums;
using Login.Helpers;

namespace Login.Data.Entities
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); // crea la base de datos y aplica las migraciones pendientes
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Daniel", "Lobo", "wolf@yopmail.com", "3132422876", "Mz E15 lote 7", UserType.Admin);
        }

        private async Task<Usuario> CheckUserAsync(
            string document, 
            string firsName, 
            string lastName, 
            string email, 
            string phoneNumber, 
            string address, 
            UserType userType)
        {
            Usuario user = await _userHelper.AddUserAsync(email);
            if(user == null)
            {
                user = new Usuario
                {
                    FirstName = firsName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }
    }
}
