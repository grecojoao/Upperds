using System;

namespace Domain.Models
{
    public abstract class FilterHarvest
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
