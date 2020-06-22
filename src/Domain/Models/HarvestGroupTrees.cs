using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class HarvestGroupTrees : Harvest
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int? GroupId { get; set; }

        public GroupTrees Group { get; private set; }
    }
}
