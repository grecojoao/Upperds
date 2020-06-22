using System.ComponentModel.DataAnnotations;
using Domain.Models.Primitives;

namespace Domain.Models
{
    public class GroupTrees : Entity
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Name { get; set; }

        public bool IsDeleted { get; private set; } = false;

        public void Delete() => IsDeleted = true;
    }
}
