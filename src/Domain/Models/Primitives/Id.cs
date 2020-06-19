using System;

namespace Domain.Models.Primitives
{
    public class Id
    {
        public Guid Value { get; private set; }
        public Id() => Value = Guid.NewGuid();
    }
}