using Kaizen.Dominio.Administracion;
using Kaizen.Dominio.Gastronomia;
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

    public DbSet<CategoriaGastronomia> CategoriasGastronomia => Set<CategoriaGastronomia>();
    public DbSet<ProductoGastronomia> ProductosGastronomia => Set<ProductoGastronomia>();
    public DbSet<InsumoGastronomia> InsumosGastronomia => Set<InsumoGastronomia>();
    public DbSet<RecetaProducto> RecetasProducto => Set<RecetaProducto>();
    public DbSet<RecetaItem> RecetasItem => Set<RecetaItem>();
    public DbSet<PedidoGastronomia> PedidosGastronomia => Set<PedidoGastronomia>();
    public DbSet<PedidoItemGastronomia> PedidosItemGastronomia => Set<PedidoItemGastronomia>();
    public DbSet<MovimientoStockGastronomia> MovimientosStockGastronomia => Set<MovimientoStockGastronomia>();

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

            entity.HasOne(m => m.Socio).WithMany(s => s.Membresias).HasForeignKey(m => m.SocioId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(m => m.Plan).WithMany().HasForeignKey(m => m.PlanId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RegistroAcceso>(entity =>
        {
            entity.ToTable("gim_registro_acceso");
            entity.Property(r => r.Resultado).HasMaxLength(20);
            entity.Property(r => r.Motivo).HasMaxLength(250);
        });

        modelBuilder.Entity<CategoriaGastronomia>(entity =>
        {
            entity.ToTable("gas_categoria");
            entity.HasIndex(x => x.Nombre).IsUnique();
            entity.Property(x => x.Nombre).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Descripcion).HasMaxLength(250);
        });

        modelBuilder.Entity<ProductoGastronomia>(entity =>
        {
            entity.ToTable("gas_producto");
            entity.Property(x => x.Nombre).HasMaxLength(140).IsRequired();
            entity.Property(x => x.PrecioVenta).HasPrecision(12, 2);
            entity.HasOne(x => x.Categoria).WithMany().HasForeignKey(x => x.CategoriaGastronomiaId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InsumoGastronomia>(entity =>
        {
            entity.ToTable("gas_insumo");
            entity.Property(x => x.Nombre).HasMaxLength(140).IsRequired();
            entity.Property(x => x.Unidad).HasMaxLength(30).IsRequired();
            entity.Property(x => x.CostoUnitario).HasPrecision(12, 2);
            entity.Property(x => x.StockActual).HasPrecision(12, 3);
            entity.Property(x => x.StockMinimo).HasPrecision(12, 3);
        });

        modelBuilder.Entity<RecetaProducto>(entity =>
        {
            entity.ToTable("gas_receta_producto");
            entity.HasIndex(x => x.ProductoGastronomiaId).IsUnique();
            entity.HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.ProductoGastronomiaId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RecetaItem>(entity =>
        {
            entity.ToTable("gas_receta_item");
            entity.Property(x => x.Cantidad).HasPrecision(12, 3);
            entity.HasOne(x => x.RecetaProducto).WithMany(x => x.Items).HasForeignKey(x => x.RecetaProductoId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Insumo).WithMany().HasForeignKey(x => x.InsumoGastronomiaId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PedidoGastronomia>(entity =>
        {
            entity.ToTable("gas_pedido");
            entity.Property(x => x.Canal).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Estado).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Cliente).HasMaxLength(120);
            entity.Property(x => x.Observaciones).HasMaxLength(250);
            entity.Property(x => x.Total).HasPrecision(12, 2);
        });

        modelBuilder.Entity<PedidoItemGastronomia>(entity =>
        {
            entity.ToTable("gas_pedido_item");
            entity.Property(x => x.PrecioUnitario).HasPrecision(12, 2);
            entity.Property(x => x.Subtotal).HasPrecision(12, 2);
            entity.HasOne(x => x.Pedido).WithMany(x => x.Items).HasForeignKey(x => x.PedidoGastronomiaId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Producto).WithMany().HasForeignKey(x => x.ProductoGastronomiaId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MovimientoStockGastronomia>(entity =>
        {
            entity.ToTable("gas_movimiento_stock");
            entity.Property(x => x.TipoMovimiento).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Cantidad).HasPrecision(12, 3);
            entity.Property(x => x.Referencia).HasMaxLength(100).IsRequired();
            entity.HasOne(x => x.Insumo).WithMany().HasForeignKey(x => x.InsumoGastronomiaId).OnDelete(DeleteBehavior.Restrict);
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
