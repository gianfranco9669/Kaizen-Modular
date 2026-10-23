# Migración del recetario REN — plan validado contra el código local

**Estado:** propuesta de migración; SIN EJECUTAR en base de datos.

## Datos de cocina y catálogo

El manifest de trabajo contiene **27 preparaciones individuales y 7 combinados**. 15 preparaciones tienen al menos un ingrediente sin cantidad informada. El manifiesto no es equivalente a recetas 100% operativas.

Reglas: roll de 8 piezas, 120 g de arroz preparado; proteína estándar 60 g cuando aplique; Finlandia y palta 30 g sólo en recetas que los llevan salvo excepciones; Buenos Aires clásico = salmón/palta, New York = salmón/queso crema; Masarikado = 124 g salmón total; acompañamientos de 40 g por pedido; envío $3.000 como cargo.

## Hallazgos de arquitectura

- `RecetaProducto` contiene únicamente insumos directos.
- El motor de consumo obtiene la receta activa por ítem comercial, sin una expansión visible de componentes de combinados.
- `GastronomiaRepositorio.GuardarRecetaAsync` reemplaza los items existentes y no guarda versiones históricas.
- Los movimientos de egreso referencian pedidos, y las cancelaciones reintegran los **movimientos originales**; conservar esta propiedad.
- `StockInicializado == false` hace que el insumo no registre consumo. Con stock inicializado puede quedar saldo negativo.
- Los modificadores suman líneas/productos; deben probarse sustituciones de Pokes para evitar sumas duplicadas.

## Orden seguro

1. **Privatizar GitHub y sanear código** antes de subirlo.
2. Publicar la copia correcta en una rama baseline, con README, migraciones, scripts válidos, CI y documentación; sin secrets/backups/dependencias.
3. Auditoría de tablas PostgreSQL actuales en solo lectura y conciliación ID/SKU.
4. Crear versiones de recetas y componentes de recetas, elaboraciones y acompañamientos por pedido.
5. Importador idempotente con modo dry run y reporte de diferencias.
6. Tests sobre restore desechable; verificar Rappi, caja y administración.
7. Aplicación controlada a producción y CHANGELOG.

## Fuera de alcance actual

No se ha comparado la base activa, no se ha compilado ni probado la API; no se han modificado productos, recetas ni stock.

**Precaución:** scripts históricos de carta/limpieza contienen operaciones SQL que escriben/eliminan; no ejecutarlos en producción.
