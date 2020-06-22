using System.Threading.Tasks;

namespace Domain.Infra
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
