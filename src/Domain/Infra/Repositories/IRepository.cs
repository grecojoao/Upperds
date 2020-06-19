using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Primitives;

namespace Domain.Infra.Repositories
{
    public interface IRepository
    {
        Task Save();
        Task<Id> Create(Entity entity);
        Task<Entity> Read(Id id);
        Task<IEnumerable<Entity>> ReadAll(Entity entity);
        Task<bool> Update(Entity entity);
        Task<bool> Delete(Entity entity);
    }
}
