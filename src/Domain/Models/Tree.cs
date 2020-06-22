using System.ComponentModel.DataAnnotations;
using Domain.Models.Primitives;

namespace Domain.Models
{
    public class Tree : Entity
    {
        public int Age { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int? SpeciesId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int? GroupTreesId { get; set; }

        public Species Species { get; private set; }
        public GroupTrees GroupTrees { get; private set; }
    }
}
