using Microsoft.EntityFrameworkCore;
using FacturasAPI.Models;

namespace FacturasAPI.Data
{
    public class FacturasContext : DbContext
    {
        public FacturasContext(DbContextOptions<FacturasContext> options)
            : base(options)
        {
        }

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Factura
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Cliente)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NIT)
                    .HasMaxLength(20);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasDefaultValue("Activa");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.IVA)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(18,2)");

                // Relación uno a muchos con DetalleFactura
                entity.HasMany(e => e.Detalles)
                    .WithOne()
                    .HasForeignKey(d => d.FacturaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de DetalleFactura
            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Producto)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(18,2)");

                // Índice para búsquedas rápidas
                entity.HasIndex(e => e.FacturaId);
            });
        }
    }
}