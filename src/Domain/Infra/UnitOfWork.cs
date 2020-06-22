using System.Threading.Tasks;
using Domain.Data;

namespace Domain.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context) => _context = context;

        public async Task Commit() => await _context.SaveChangesAsync();
    }
}
