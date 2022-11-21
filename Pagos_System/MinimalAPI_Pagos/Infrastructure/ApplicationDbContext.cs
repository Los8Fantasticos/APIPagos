using Microsoft.EntityFrameworkCore;
using MinimalAPI_Pagos.Models.ApplicationModel;
using System.Reflection;

namespace MinimalAPI_Pagos.Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        // protected ApplicationDbContext()
        //{

        // }


        public DbSet<PagosModel>? Factura { get; set; } = null!;

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
            modelBuilder.Entity<PagosModel>(entity =>
            {
                entity.HasKey(e => e.IdFactura);
                entity.Property(e => e.Patente).HasMaxLength(10).HasColumnName("nvarchar").IsRequired();
                entity.Property(e => e.Monto).HasMaxLength(10).HasColumnName("nvarchar").IsRequired();
                entity.Property(e => e.Fecha).HasDefaultValueSql("getdate()").IsRequired();
                entity.Property(e => e.Active).HasDefaultValue(true).IsRequired();

            });

            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



    }
}
