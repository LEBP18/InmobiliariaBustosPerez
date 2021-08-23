using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaBustosPerez.Models
{
    public class Propietario
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio"),
           MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio"),
           MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Campo obligatorio"),
            StringLength(8, MinimumLength = 8, ErrorMessage = "Un DNI debe tener 8 dígitos")]
        public string Dni { get; set; }

        [StringLength(15, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 15 dígitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Campo obligatorio"),
            EmailAddress(ErrorMessage = "Dirección de correo inválida"),
            MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio"), DataType(DataType.Password),
        StringLength(8, MinimumLength = 8, ErrorMessage = "La contraseña debe tener mínimo 8 dígitos")]
        public string Clave { get; set; }
    }
}
