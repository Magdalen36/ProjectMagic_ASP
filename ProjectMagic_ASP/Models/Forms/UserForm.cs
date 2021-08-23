using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Models.Forms
{
    public class UserForm
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
       

        [Column(TypeName = "Datetime2")]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Votre mot de passe doit contenir minimum 6 caractères")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Vos mots de passe ne correspondent pas")]
        [DataType(DataType.Password)]
        public string PasswordCheck { get; set; }


    }
}
