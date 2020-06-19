using System.Collections.Generic;
using Domain.Models.Primitives;

namespace Domain.Models
{
    public class GroupOfTrees : Entity
    {
        public Code Code { get; private set; }
        public string Name { get; private set; }   
        public Description Description { get; private set; }
        public IEnumerable<Tree> Trees { get; private set; }
    }
}
