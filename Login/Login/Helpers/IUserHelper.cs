using Login.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Login.Helpers
{
    public interface IUserHelper
    {
        Task<Usuario>GetUserAsinc(string email);  // se le pasa el email del usuario y me devuelve el usuario, al ser un metodo asincrono me devuelve un task de user
        Task<IdentityResult> AddUserAsync(Usuario user,string password); //el metodo adduser sirve para crear el usuario y de paso le pasamos el password
        Task CheckRoleAsync(string roleName); //este metodo verifica si existe un rol, en caso contrario lo crea 
        Task AddUserToRoleAsync(Usuario user,string roleName); // asigna cada usuario a un tipo de rol
        Task<bool> IsUserInRoleAsync(Usuario user, string roleName);//devuelve un bool dependiendo del usuario, si es admin o user
        Task<Usuario> AddUserAsync(string email);
    }
}
