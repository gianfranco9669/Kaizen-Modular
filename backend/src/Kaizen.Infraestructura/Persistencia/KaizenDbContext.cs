using Kaizen.Dominio.Administracion;
using Kaizen.Dominio.Gimnasio;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infraestructura.Persistencia;

public class KaizenDbContext : DbContext
{
    public KaizenDbContext(DbContextOptions<KaizenDbContext> options) : base(options)
    {
    }

    public DbSet<Socio> Socios => Set<Socio>();
    public DbSet<Plan> Planes => Set<Plan>();
    public DbSet<Membresia> Membresias => Set<Membresia>();
    public DbSet<RegistroAcceso> RegistrosAcceso => Set<RegistroAcceso>();
    public DbSet<ImpactoComercial> ImpactosComerciales => Set<ImpactoComercial>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Socio>(entity =>
        {
            entity.ToTable("gim_socio");
            entity.HasIndex(s => s.NumeroSocio).IsUnique();
            entity.Property(s => s.NumeroSocio).HasMaxLength(30).IsRequired();
            entity.Property(s => s.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(s => s.Apellido).HasMaxLength(120).IsRequired();
            entity.Property(s => s.Documento).HasMaxLength(30).IsRequired();
            entity.Property(s => s.Correo).HasMaxLength(120);
            entity.Property(s => s.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.ToTable("gim_plan");
            entity.HasIndex(p => p.Nombre).IsUnique();
            entity.Property(p => p.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Precio).HasPrecision(12, 2);
        });

        modelBuilder.Entity<Membresia>(entity =>
        {
            entity.ToTable("gim_membresia");
            entity.Property(m => m.EstadoVigencia).HasMaxLength(20);
            entity.Property(m => m.EstadoDeuda).HasMaxLength(20);
            entity.Property(m => m.MontoTotal).HasPrecision(12, 2);
            entity.Property(m => m.MontoAdeudado).HasPrecision(12, 2);

            entity.HasOne(m => m.Socio)
                .WithMany(s => s.Membresias)
                .HasForeignKey(m => m.SocioId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.Plan)
                .WithMany()
                .HasForeignKey(m => m.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RegistroAcceso>(entity =>
        {
            entity.ToTable("gim_registro_acceso");
            entity.Property(r => r.Resultado).HasMaxLength(20);
            entity.Property(r => r.Motivo).HasMaxLength(250);
        });

        modelBuilder.Entity<ImpactoComercial>(entity =>
        {
            entity.ToTable("adm_impacto_comercial");
            entity.Property(i => i.ModuloOrigen).HasMaxLength(50).IsRequired();
            entity.Property(i => i.TipoOperacion).HasMaxLength(80).IsRequired();
            entity.Property(i => i.ReferenciaExterna).HasMaxLength(120).IsRequired();
            entity.Property(i => i.Descripcion).HasMaxLength(250);
            entity.Property(i => i.Monto).HasPrecision(12, 2);
        });

        base.OnModelCreating(modelBuilder);
    }
}
