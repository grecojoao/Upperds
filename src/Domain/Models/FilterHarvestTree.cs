namespace Domain.Models
{
    public class FilterHarvestTree : FilterHarvest
    {
        public int TreeId { get; set; }
        public int SpeciesId { get; set; }
    }
}
