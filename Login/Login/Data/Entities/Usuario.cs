using Login.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Login.Data.Entities
{
    public class Usuario : IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres. ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string Document { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres. ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres. ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string LastName { get; set; }

        [Display(Name = "Direccíon")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres. ")]
        [Required(ErrorMessage = "El campo {0} es obligatorio. ")]
        public string Address { get; set; }


        [Display(Name = "Tipo de Usuario")]
        public UserType UserType { get; set; }


        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";


    }
}
