namespace Domain.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void RollBack()
        {
            throw new System.NotImplementedException();
        }
    }
}
