# REN — validación del Corte 1 y plan de pruebas del Corte 2 (23/10/2026)

## Confirmado por ejecución local del usuario

- Precheck de huellas C1: OK.
- Staging en copia independiente: OK.
- Build completo de solución .NET 10 en Release: OK.
- Prueba C# del calculador de combinados: 5/5 aprobada (fracciones, redondeo y fail closed).

No se ejecutaron migraciones, SQL ni cambios productivos con estos pasos.

## Preparado pero NO ejecutado todavía

- Corte 2: restaurar backup previamente verificado **solo en base descartable**; validar que la única migración pendiente es RevisionRecetaRen; ejecutar CRUD de revisiones y probar bloqueo de rollback destructivo.
- Se reportará éxito únicamente a partir de la salida verificable del usuario; los checks estáticos sobre el paquete no cuentan como prueba EF ni DB.

## Bloqueos productivos restantes

- Pruebas de API/integración, cambios reales de stock, ampliaciones y cancelaciones.
- Composición por receta de combinados y preparado de arroz.
- Gramajes pendientes, costos, nuevos precios y conteo físico.
- Prohibido sincronizar código empresarial a repositorio público.