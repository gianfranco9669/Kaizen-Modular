using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Infraestructura.Migrations;

public partial class CorteInicial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "adm_impacto_comercial",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ModuloOrigen = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                TipoOperacion = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                ReferenciaExterna = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Descripcion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                Monto = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                FechaOperacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_adm_impacto_comercial", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "gas_categoria",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Nombre = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Descripcion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                Activa = table.Column<bool>(type: "boolean", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_gas_categoria", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "gas_insumo",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Nombre = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false),
                Unidad = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                CostoUnitario = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                StockActual = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                StockMinimo = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                Activo = table.Column<bool>(type: "boolean", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_gas_insumo", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "gim_plan",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Nombre = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Descripcion = table.Column<string>(type: "text", nullable: false),
                Precio = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                DuracionDias = table.Column<int>(type: "integer", nullable: false),
                PermiteAcceso = table.Column<bool>(type: "boolean", nullable: false),
                Activo = table.Column<bool>(type: "boolean", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_gim_plan", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "gim_socio",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                NumeroSocio = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                Nombre = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Apellido = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Documento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                Correo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Telefono = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Activo = table.Column<bool>(type: "boolean", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_gim_socio", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "gas_movimiento_stock",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                InsumoGastronomiaId = table.Column<Guid>(type: "uuid", nullable: false),
                TipoMovimiento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                Cantidad = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                Referencia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                FechaMovimientoUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_gas_movimiento_stock", x => x.Id);
                table.ForeignKey("FK_gas_movimiento_stock_gas_insumo_InsumoGastronomiaId", x => x.InsumoGastronomiaId, "gas_insumo", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "gas_producto",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Nombre = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false),
                CategoriaGastronomiaId = table.Column<Guid>(type: "uuid", nullable: false),
                PrecioVenta = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                Activo = table.Column<bool>(type: "boolean", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_gas_producto", x => x.Id);
                table.ForeignKey("FK_gas_producto_gas_categoria_CategoriaGastronomiaId", x => x.CategoriaGastronomiaId, "gas_categoria", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "gim_membresia",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                SocioId = table.Column<Guid>(type: "uuid", nullable: false),
                PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                EstadoVigencia = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                EstadoDeuda = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                MontoTotal = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                MontoAdeudado = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_gim_membresia", x => x.Id);
                table.ForeignKey("FK_gim_membresia_gim_plan_PlanId", x => x.PlanId, "gim_plan", "Id", onDelete: ReferentialAction.Restrict);
                table.ForeignKey("FK_gim_membresia_gim_socio_SocioId", x => x.SocioId, "gim_socio", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "gim_registro_acceso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                SocioId = table.Column<Guid>(type: "uuid", nullable: false),
                FechaHoraUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Resultado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                Motivo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_gim_registro_acceso", x => x.Id);
                table.ForeignKey("FK_gim_registro_acceso_gim_socio_SocioId", x => x.SocioId, "gim_socio", "Id", onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "gas_pedido",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Canal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                Estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                Cliente = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                Observaciones = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                FechaPedidoUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Total = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_gas_pedido", x => x.Id); });

        migrationBuilder.CreateTable(
            name: "gas_receta_producto",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ProductoGastronomiaId = table.Column<Guid>(type: "uuid", nullable: false),
                Activa = table.Column<bool>(type: "boolean", nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_gas_receta_producto", x => x.Id);
                table.ForeignKey("FK_gas_receta_producto_gas_producto_ProductoGastronomiaId", x => x.ProductoGastronomiaId, "gas_producto", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "gas_pedido_item",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                PedidoGastronomiaId = table.Column<Guid>(type: "uuid", nullable: false),
                ProductoGastronomiaId = table.Column<Guid>(type: "uuid", nullable: false),
                Cantidad = table.Column<int>(type: "integer", nullable: false),
                PrecioUnitario = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                Subtotal = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_gas_pedido_item", x => x.Id);
                table.ForeignKey("FK_gas_pedido_item_gas_pedido_PedidoGastronomiaId", x => x.PedidoGastronomiaId, "gas_pedido", "Id", onDelete: ReferentialAction.Cascade);
                table.ForeignKey("FK_gas_pedido_item_gas_producto_ProductoGastronomiaId", x => x.ProductoGastronomiaId, "gas_producto", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "gas_receta_item",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                RecetaProductoId = table.Column<Guid>(type: "uuid", nullable: false),
                InsumoGastronomiaId = table.Column<Guid>(type: "uuid", nullable: false),
                Cantidad = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                FechaCreacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaActualizacionUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_gas_receta_item", x => x.Id);
                table.ForeignKey("FK_gas_receta_item_gas_insumo_InsumoGastronomiaId", x => x.InsumoGastronomiaId, "gas_insumo", "Id", onDelete: ReferentialAction.Restrict);
                table.ForeignKey("FK_gas_receta_item_gas_receta_producto_RecetaProductoId", x => x.RecetaProductoId, "gas_receta_producto", "Id", onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(name: "IX_gas_categoria_Nombre", table: "gas_categoria", column: "Nombre", unique: true);
        migrationBuilder.CreateIndex(name: "IX_gas_insumo_Nombre", table: "gas_insumo", column: "Nombre");
        migrationBuilder.CreateIndex(name: "IX_gas_movimiento_stock_InsumoGastronomiaId", table: "gas_movimiento_stock", column: "InsumoGastronomiaId");
        migrationBuilder.CreateIndex(name: "IX_gas_pedido_item_PedidoGastronomiaId", table: "gas_pedido_item", column: "PedidoGastronomiaId");
        migrationBuilder.CreateIndex(name: "IX_gas_pedido_item_ProductoGastronomiaId", table: "gas_pedido_item", column: "ProductoGastronomiaId");
        migrationBuilder.CreateIndex(name: "IX_gas_producto_CategoriaGastronomiaId", table: "gas_producto", column: "CategoriaGastronomiaId");
        migrationBuilder.CreateIndex(name: "IX_gas_receta_item_InsumoGastronomiaId", table: "gas_receta_item", column: "InsumoGastronomiaId");
        migrationBuilder.CreateIndex(name: "IX_gas_receta_item_RecetaProductoId", table: "gas_receta_item", column: "RecetaProductoId");
        migrationBuilder.CreateIndex(name: "IX_gas_receta_producto_ProductoGastronomiaId", table: "gas_receta_producto", column: "ProductoGastronomiaId", unique: true);
        migrationBuilder.CreateIndex(name: "IX_gim_membresia_PlanId", table: "gim_membresia", column: "PlanId");
        migrationBuilder.CreateIndex(name: "IX_gim_membresia_SocioId", table: "gim_membresia", column: "SocioId");
        migrationBuilder.CreateIndex(name: "IX_gim_plan_Nombre", table: "gim_plan", column: "Nombre", unique: true);
        migrationBuilder.CreateIndex(name: "IX_gim_registro_acceso_SocioId", table: "gim_registro_acceso", column: "SocioId");
        migrationBuilder.CreateIndex(name: "IX_gim_socio_NumeroSocio", table: "gim_socio", column: "NumeroSocio", unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "adm_impacto_comercial");
        migrationBuilder.DropTable(name: "gas_movimiento_stock");
        migrationBuilder.DropTable(name: "gas_pedido_item");
        migrationBuilder.DropTable(name: "gas_receta_item");
        migrationBuilder.DropTable(name: "gim_membresia");
        migrationBuilder.DropTable(name: "gim_registro_acceso");
        migrationBuilder.DropTable(name: "gas_pedido");
        migrationBuilder.DropTable(name: "gas_receta_producto");
        migrationBuilder.DropTable(name: "gim_plan");
        migrationBuilder.DropTable(name: "gim_socio");
        migrationBuilder.DropTable(name: "gas_producto");
        migrationBuilder.DropTable(name: "gas_insumo");
        migrationBuilder.DropTable(name: "gas_categoria");
    }
}
