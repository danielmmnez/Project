using System.ComponentModel.DataAnnotations;

namespace Project.Shared.Entities
{
    public class Manager
    {
        public int Id { get; set; }

        [Display(Name = "Gerente")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
    }
}
