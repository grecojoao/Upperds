using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class HarvestTree : Harvest
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int? TreeId { get; set; }

        public Tree Tree { get; private set; }
    }
}
