using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Infraestructura.Migraciones;

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
                table.ForeignKey(name: "FK_gim_membresia_gim_plan_PlanId", column: x => x.PlanId, principalTable: "gim_plan", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
                table.ForeignKey(name: "FK_gim_membresia_gim_socio_SocioId", column: x => x.SocioId, principalTable: "gim_socio", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
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
                table.ForeignKey(name: "FK_gim_registro_acceso_gim_socio_SocioId", column: x => x.SocioId, principalTable: "gim_socio", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(name: "IX_gim_membresia_PlanId", table: "gim_membresia", column: "PlanId");
        migrationBuilder.CreateIndex(name: "IX_gim_membresia_SocioId", table: "gim_membresia", column: "SocioId");
        migrationBuilder.CreateIndex(name: "IX_gim_plan_Nombre", table: "gim_plan", column: "Nombre", unique: true);
        migrationBuilder.CreateIndex(name: "IX_gim_registro_acceso_SocioId", table: "gim_registro_acceso", column: "SocioId");
        migrationBuilder.CreateIndex(name: "IX_gim_socio_NumeroSocio", table: "gim_socio", column: "NumeroSocio", unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "adm_impacto_comercial");
        migrationBuilder.DropTable(name: "gim_membresia");
        migrationBuilder.DropTable(name: "gim_registro_acceso");
        migrationBuilder.DropTable(name: "gim_plan");
        migrationBuilder.DropTable(name: "gim_socio");
    }
}
