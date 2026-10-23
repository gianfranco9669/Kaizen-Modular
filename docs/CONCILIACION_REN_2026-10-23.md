# Conciliación REN — 23/10/2026

**Estado: auditoría completada; migración productiva NO autorizada.**

Se verificó el inventario actual de PostgreSQL mediante una transacción READ ONLY en una base local respaldada y restaurada previamente en una base temporal. Los datos comerciales detallados de productos, SKU, precios, recetas y existencias se mantienen **fuera de este repositorio público**.

## Hallazgos confirmados

- El catálogo existente y el nuevo recetario requieren conciliación por **identidad semántica y categoría**, no solo nombre. Hay por lo menos una colisión de nombre entre un extra y un nuevo roll, que impide reutilizar IDs automáticamente.
- Todas las existencias llevan un estado de inicialización de inventario no validado; **no** habilitar consumos automáticamente sin conteo y normalización.
- El sistema tiene recetas activas para los productos actualmente activos; esto no significa que la información nutricional/costo/stock coincida con el recetario revisado.
- Existen variaciones relevantes en cantidades por unidad, ingredientes, preparados, combinados y reglas de salsas incluidas.
- Se necesita conservar SKU e IDs históricos, introducir tratamiento de componentes de combinados y revisar el flujo de anulación.
- Hay presentaciones y precios pendientes de confirmación comercial y cocina.
- El esquema incluye columnas con nombres que solo difieren por mayúsculas/minúsculas: analizar migraciones y relaciones antes de refactorizarlo.

## Próximo paso controlado

1. Resolver baseline de Git local sin destruir el trabajo sin seguimiento.
2. Convertir el repositorio a privado antes de subir código y datos propios.
3. Aprobar mapeo de catálogo/precios y gramajes, manteniendo en pausa recetas incompletas.
4. Crear migración y pruebas en base descartable con rollback, sin sustituir recetas históricas por simple actualización.
5. Verificar compilación, integración y servicio antes de tocar el entorno operativo.

**Este documento no contiene comandos de actualización de base ni secretos.** La matriz detallada solo se entrega como archivo privado en el contexto de trabajo.
