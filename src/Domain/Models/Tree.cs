using Domain.Models.Primitives;

namespace Domain.Models
{
    public class Tree : Entity
    {
        public Code Code { get; private set; }
        public Description Description { get; private set; }
        public double Age { get; private set; }
        public Species Species { get; private set; }
    }
}
