using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Primitives
{
    public abstract class Entity : IId, ICode, IDescription
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres.")]
        [MinLength(1, ErrorMessage = "Este campo deve conter no minímo 1 caractere.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(1024, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres.")]
        [MinLength(1, ErrorMessage = "Este campo deve conter no mínimo 1 caractere.")]
        public string Description { get; set; }
    }
}
