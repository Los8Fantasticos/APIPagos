using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace MinimalAPI_Pagos.Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected ApplicationDbContext()
        {

        }

        //public ApplicationDbContext()
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("No connection string set for database.");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



    }
}
