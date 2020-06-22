using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Primitives;

namespace Domain.Models
{
    public abstract class Harvest : IId
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public double GrossWeight { get; set; }
        public string Information { get; set; }

        public bool IsDeleted { get; private set; } = false;

        public void Delete() => IsDeleted = true;
    }
}
