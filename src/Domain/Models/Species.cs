using Domain.Models.Primitives;

namespace Domain.Models
{
    public class Species : Entity
    {
        public Code Code { get; private set; }
        public Description Description { get; private set; }
    }
}
