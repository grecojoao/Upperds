namespace Domain.Infra.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
        void RollBack();
    }
}
