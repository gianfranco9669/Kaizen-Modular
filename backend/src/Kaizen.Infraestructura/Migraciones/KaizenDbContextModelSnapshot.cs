using System;
using Kaizen.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Kaizen.Infraestructura.Migrations;

[DbContext(typeof(KaizenDbContext))]
partial class KaizenDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

        modelBuilder.Entity("Kaizen.Dominio.Administracion.ImpactoComercial", b =>
        {
            b.Property<Guid>("Id");
            b.Property<string>("Descripcion").HasMaxLength(250).IsRequired();
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<DateTime>("FechaOperacionUtc");
            b.Property<string>("ModuloOrigen").HasMaxLength(50).IsRequired();
            b.Property<decimal>("Monto").HasPrecision(12, 2);
            b.Property<string>("ReferenciaExterna").HasMaxLength(120).IsRequired();
            b.Property<string>("TipoOperacion").HasMaxLength(80).IsRequired();
            b.HasKey("Id");
            b.ToTable("adm_impacto_comercial");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.CategoriaGastronomia", b =>
        {
            b.Property<Guid>("Id");
            b.Property<bool>("Activa");
            b.Property<string>("Descripcion").HasMaxLength(250).IsRequired();
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<string>("Nombre").HasMaxLength(120).IsRequired();
            b.HasKey("Id");
            b.HasIndex("Nombre").IsUnique();
            b.ToTable("gas_categoria");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.InsumoGastronomia", b =>
        {
            b.Property<Guid>("Id");
            b.Property<bool>("Activo");
            b.Property<decimal>("CostoUnitario").HasPrecision(12, 2);
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<string>("Nombre").HasMaxLength(140).IsRequired();
            b.Property<decimal>("StockActual").HasPrecision(12, 3);
            b.Property<decimal>("StockMinimo").HasPrecision(12, 3);
            b.Property<string>("Unidad").HasMaxLength(30).IsRequired();
            b.HasKey("Id");
            b.ToTable("gas_insumo");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.ProductoGastronomia", b =>
        {
            b.Property<Guid>("Id");
            b.Property<bool>("Activo");
            b.Property<Guid>("CategoriaGastronomiaId");
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<string>("Nombre").HasMaxLength(140).IsRequired();
            b.Property<decimal>("PrecioVenta").HasPrecision(12, 2);
            b.HasKey("Id");
            b.HasIndex("CategoriaGastronomiaId");
            b.ToTable("gas_producto");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.RecetaProducto", b =>
        {
            b.Property<Guid>("Id");
            b.Property<bool>("Activa");
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<Guid>("ProductoGastronomiaId");
            b.HasKey("Id");
            b.HasIndex("ProductoGastronomiaId").IsUnique();
            b.ToTable("gas_receta_producto");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.RecetaItem", b =>
        {
            b.Property<Guid>("Id");
            b.Property<decimal>("Cantidad").HasPrecision(12, 3);
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<Guid>("InsumoGastronomiaId");
            b.Property<Guid>("RecetaProductoId");
            b.HasKey("Id");
            b.HasIndex("InsumoGastronomiaId");
            b.HasIndex("RecetaProductoId");
            b.ToTable("gas_receta_item");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.PedidoGastronomia", b =>
        {
            b.Property<Guid>("Id");
            b.Property<string>("Canal").HasMaxLength(20).IsRequired();
            b.Property<string>("Cliente").HasMaxLength(120).IsRequired();
            b.Property<string>("Estado").HasMaxLength(30).IsRequired();
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<DateTime>("FechaPedidoUtc");
            b.Property<string>("Observaciones").HasMaxLength(250).IsRequired();
            b.Property<decimal>("Total").HasPrecision(12, 2);
            b.HasKey("Id");
            b.ToTable("gas_pedido");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.PedidoItemGastronomia", b =>
        {
            b.Property<Guid>("Id");
            b.Property<int>("Cantidad");
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<Guid>("PedidoGastronomiaId");
            b.Property<decimal>("PrecioUnitario").HasPrecision(12, 2);
            b.Property<Guid>("ProductoGastronomiaId");
            b.Property<decimal>("Subtotal").HasPrecision(12, 2);
            b.HasKey("Id");
            b.HasIndex("PedidoGastronomiaId");
            b.HasIndex("ProductoGastronomiaId");
            b.ToTable("gas_pedido_item");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gastronomia.MovimientoStockGastronomia", b =>
        {
            b.Property<Guid>("Id");
            b.Property<decimal>("Cantidad").HasPrecision(12, 3);
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<DateTime>("FechaMovimientoUtc");
            b.Property<Guid>("InsumoGastronomiaId");
            b.Property<string>("Referencia").HasMaxLength(100).IsRequired();
            b.Property<string>("TipoMovimiento").HasMaxLength(20).IsRequired();
            b.HasKey("Id");
            b.HasIndex("InsumoGastronomiaId");
            b.ToTable("gas_movimiento_stock");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gimnasio.Plan", b =>
        {
            b.Property<Guid>("Id");
            b.Property<bool>("Activo");
            b.Property<string>("Descripcion").IsRequired();
            b.Property<int>("DuracionDias");
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<string>("Nombre").HasMaxLength(120).IsRequired();
            b.Property<bool>("PermiteAcceso");
            b.Property<decimal>("Precio").HasPrecision(12, 2);
            b.HasKey("Id");
            b.HasIndex("Nombre").IsUnique();
            b.ToTable("gim_plan");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gimnasio.Socio", b =>
        {
            b.Property<Guid>("Id");
            b.Property<bool>("Activo");
            b.Property<string>("Apellido").HasMaxLength(120).IsRequired();
            b.Property<string>("Correo").HasMaxLength(120).IsRequired();
            b.Property<string>("Documento").HasMaxLength(30).IsRequired();
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<string>("Nombre").HasMaxLength(120).IsRequired();
            b.Property<string>("NumeroSocio").HasMaxLength(30).IsRequired();
            b.Property<string>("Telefono").HasMaxLength(50).IsRequired();
            b.HasKey("Id");
            b.HasIndex("NumeroSocio").IsUnique();
            b.ToTable("gim_socio");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gimnasio.Membresia", b =>
        {
            b.Property<Guid>("Id");
            b.Property<string>("EstadoDeuda").HasMaxLength(20).IsRequired();
            b.Property<string>("EstadoVigencia").HasMaxLength(20).IsRequired();
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<DateOnly>("FechaFin");
            b.Property<DateOnly>("FechaInicio");
            b.Property<decimal>("MontoAdeudado").HasPrecision(12, 2);
            b.Property<decimal>("MontoTotal").HasPrecision(12, 2);
            b.Property<Guid>("PlanId");
            b.Property<Guid>("SocioId");
            b.HasKey("Id");
            b.HasIndex("PlanId");
            b.HasIndex("SocioId");
            b.ToTable("gim_membresia");
        });

        modelBuilder.Entity("Kaizen.Dominio.Gimnasio.RegistroAcceso", b =>
        {
            b.Property<Guid>("Id");
            b.Property<DateTime?>("FechaActualizacionUtc");
            b.Property<DateTime>("FechaCreacionUtc");
            b.Property<DateTime>("FechaHoraUtc");
            b.Property<string>("Motivo").HasMaxLength(250).IsRequired();
            b.Property<string>("Resultado").HasMaxLength(20).IsRequired();
            b.Property<Guid>("SocioId");
            b.HasKey("Id");
            b.HasIndex("SocioId");
            b.ToTable("gim_registro_acceso");
        });
    }
}
