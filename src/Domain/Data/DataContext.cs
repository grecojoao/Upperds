using Domain.Models;
using Domain.Models.Login;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tree> Trees { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<GroupTrees> GroupTrees { get; set; }
        public DbSet<HarvestTree> HarvestTree { get; set; }
        public DbSet<HarvestGroupTrees> HarvestGroupTrees { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HarvestTree>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<HarvestGroupTrees>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<GroupTrees>().HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "Upperds", Password = "q1w2e3r4!1@2#3$4", Role = Role.manager });
        }
    }
}
