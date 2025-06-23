using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;

namespace MiniCoreMultiCliente.MiniCore.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Vendedor> Vendedores => Set<Vendedor>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<ReglaComision> ReglasComision => Set<ReglaComision>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Vendedor>().ToTable("Vendedores");
            modelBuilder.Entity<Venta>().ToTable("Ventas");
            modelBuilder.Entity<ReglaComision>().ToTable("ReglasComision");

            modelBuilder.Entity<Vendedor>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Vendedores)
                .HasForeignKey(v => v.ClienteId);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Vendedor)
                .WithMany(ven => ven.Ventas)
                .HasForeignKey(v => v.VendedorId);

            modelBuilder.Entity<ReglaComision>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reglas)
                .HasForeignKey(r => r.ClienteId);
        }
    }
}