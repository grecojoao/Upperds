using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Primitives;

namespace Domain.Infra.Repositories
{
    public class TreeRepository : IRepository
    {
        public Task<Id> Create(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entity> Read(Id id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entity>> ReadAll(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
