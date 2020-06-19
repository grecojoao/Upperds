using System;
using Domain.Models.Primitives;

namespace Domain.Models
{
    public class Harvest : Entity
    {
        public string Information { get; private set; }
        public DateTime Date { get; private set; }
        public decimal GrossWeight { get; private set; }
        public Tree Tree { get; private set; }
    }
}
