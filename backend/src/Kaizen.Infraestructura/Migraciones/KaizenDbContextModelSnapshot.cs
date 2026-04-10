using System;
using Kaizen.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Kaizen.Infraestructura.Migraciones;

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
