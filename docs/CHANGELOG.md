# Historial de cambios

Registrar cada modificación funcional, cambio de datos o despliegue importante en el mismo PR.

## 2026-10-23 — C3-A compiló y 13 smoke tests pasaron; C3-B pendiente

- El usuario ejecutó C3-A en Windows y confirmó `PRECHECK OK`, `STAGING OK`, build completo Release .NET 10 y `13/13` smoke tests aprobados.
- Entre los escenarios validados están prorrateo 5/8 y 4/8, exclusión de receta plana al habilitar componentes, multiplicación y acumulación, más rechazos de composiciones inválidas.
- Se preparó un **paquete C3-B** para probar EF y PostgreSQL solo en base temporal: requiere backup identificado por SHA-256, valida C3-A, compila antes de acceder a PostgreSQL y rechaza destinos no temporales.
- C3-B **no se ha ejecutado aún**. No hay pruebas completas de API, Rappi, ampliaciones ni reintegro por cancelación.
- La base `kaizen_modular_dev` y el código local original no recibieron cambios por estas pruebas. Producción permanece bloqueada.
- No se publicaron scripts ni datos comerciales sensibles en el repositorio público.

## 2026-10-23 — C3-A desarrollado en copia aislada; validación local pendiente

- Se recibió el menú comercial actualizado de REN (4 páginas) y el recetario integrado v0.2 (13 páginas).
- El nuevo menú fija un precio único de rolls premium; se revisaron 34 preparaciones/combinados y siete composiciones con prorrateo por piezas.
- Se detectaron diferencias sustantivas entre ambas fuentes en nombres/rellenos, ciertas presentaciones y extras. Las decisiones se mantienen sin aplicar.
- Se preparó **código C3-A exclusivo de staging**, con entidades EF de configuración/composición opt-in, migración propuesta, resolvedor único de requerimientos por pedido y smoke de lógica.
- Se verificaron huellas de archivo, integridad del paquete, 12 controles estructurales y compatibilidad de parche `git apply --check`.
- **Compilación .NET 10, prueba real de migración C3 y pruebas operativas pendientes.** No se ejecutó SQL, no se aplicó al proyecto original ni se activó stock.
- No se publicó código empresarial ni se guardaron secretos en este repositorio público.

## 2026-10-23 — C2 V5 aprobado en PostgreSQL aislado; C3 especificado

- Informe local del usuario: `REN C2 OK`, `tests_ef_aprobados = true`, base temporal creada y eliminada, base original no modificada.
- La ejecución confirmó: migración EF 19→20, revisiones antes/después, reintentos idempotentes, preservación de IDs de recetas e ingredientes y bloqueo del Down con historial.
- Se completó análisis estático de integración de combinados/stock: pedidos normales, ampliaciones y Rappi reutilizan un mismo método de descuento; la cancelación reintegra por movimientos históricos.
- Corte 3: plan de persistencia de componentes, activación controlada sin doble consumo y matriz de 28 escenarios. **Solo especificación; implementación y pruebas C3 pendientes**.
- Ningún parche de C2/C3 fue aplicado al proyecto original ni a la base local activa. No se publicó código empresarial.

## 2026-10-23 — C2 V4 localiza UPDATE indebido; corrección V5 preparada

- El diagnóstico V4 encontró `DbUpdateConcurrencyException` en `RecetaItem`: estado EF `Modified` para un identificador inexistente en la base temporal.
- Análisis del código confirmó que la sustitución de elementos mediante navegación de EF estaba intentando actualizar registros nuevos con GUID preasignado.
- Se preparó un parche V5 **solo para staging**: actualizar los ingredientes existentes conservando sus IDs, insertar explícitamente los nuevos y eliminar únicamente los retirados.
- Se añadió una verificación en el smoke test para comprobar preservación de los IDs de ingredientes tras modificar cantidades.
- Integridad estática del paquete y hashes del origen C1 verificados. **Build V5 y smoke EF/DB pendientes de ejecución local**.
- Informe V4 confirmó eliminación de la base temporal y ausencia de modificaciones en `kaizen_modular_dev`. Migración productiva NO autorizada.

## 2026-10-23 — C2 V3 valida migración EF; excepción al actualizar receta

- El usuario ejecutó C2 V3: compilación correcta; restauración en base temporal; migración EF y cambio de 19 a 20 migraciones correctos; guardado sin cambios correcto.
- Al actualizar el detalle de una receta, EF Core devolvió `DbUpdateConcurrencyException`. Por ello, **el C2 funcional permanece INCOMPLETO**. No se afirma causa exacta sin conocer entidad, estado y fila afectada.
- El proceso eliminó la base temporal e informó que `kaizen_modular_dev` no fue alterada.
- Preparado **C2 V4 de diagnóstico controlado**, exclusivamente del harness de staging: registra tipo de entidad EF afectada, estado, clave y existencia en base temporal, sin secretos. V4 no ejecutado todavía.
- No se modifica lógica productiva en respuesta a un error cuyo origen no está comprobado.

## 2026-10-23 — C2 detenido en compilación de harness, corrección V3

- El usuario confirmó restauración OK del backup en una base temporal y posterior fallo de compilación del test EF: CS8602 en `Program.cs:17`, desreferencia posible de `NpgsqlConnectionStringBuilder.Database` (nullable).
- La prueba no llegó a ejecutar migración EF. El reporte confirma eliminación de la base temporal, sin afectar la base original.
- Se preparó **C2 V3** con chequeo explícito de Database y compilación previa al acceso PostgreSQL.
- El script solo puede reemplazar el `Program.cs` del test de staging con huella conocida; ningún archivo del proyecto original se modifica.
- **V3 sin ejecutar todavía**: aprobación EF/DB pendiente de nuevo resultado real.

## 2026-10-23 — C2 PostgreSQL interrumpido y corregido (sin cambios productivos)

- Ejecución local de Corte 2: precheck OK, restauración en base descartable OK, prueba EF detenida antes del inicio.
- Error `no existe la relación __efmigrationshistory`: identificador `__EFMigrationsHistory` perdió las comillas al pasar por `psql -c` desde Windows PowerShell.
- Informe del usuario confirma eliminación de la base temporal y ausencia de cambios en la original.
- Se preparó **C2 corregido** enviando consultas SQL por stdin. Se endureció la prueba del rollback para exigir el error PostgreSQL específico y no cualquier excepción.
- **Nueva ejecución pendiente**. No se proclama validación EF ni PostgreSQL hasta obtener `REN C2 OK`.

## 2026-10-23 — Corte 1 de desarrollo REN validado en staging

- El usuario informó ejecución exitosa de Precheck, Stage y compilación Release de la solución .NET 10 en carpeta de staging separada.
- Smoke test de cálculo de combinados aprobado (5/5): 5/8, 4/8, redondeo al final y fallos seguros.
- Preparación de Corte 2: test de migración EF, CRUD de revisiones y rechazo de Down destructivo sobre base PostgreSQL descartable. **Pendiente ejecución y resultados en Windows**.
- No se ha desplegado ni aplicado migración en la base original; Git local sin tocar; código empresarial no publicado.

## 2026-10-23 — Conciliación de carta contra PostgreSQL local

- Se recibió y analizó el inventario SQL generado por la auditoría de solo lectura.
- Se documentaron los riesgos de identidad de productos, inicialización del inventario, preparados, recetas, combinados y modificadores.
- Se agregó `docs/CONCILIACION_REN_2026-10-23.md` con hallazgos **sin publicar datos comerciales sensibles**.
- Se preparó una matriz privada de cambios para su verificación posterior.
- **No se ejecutaron migraciones ni cambios al código o base del usuario.**

## 2026-10-23 — Validación del respaldo y preparación de auditoría REN

- Se obtuvo un respaldo actual de la base local `kaizen_modular_dev`.
- Se confirmó `RESTORE OK EN BASE DESCARTABLE`: 51 tablas y 19 migraciones informadas.
- La base original y los servicios no fueron alterados por la verificación.
- Se preparó una herramienta de auditoría SQL READ ONLY.

## 2026-10-23 — Auditoría de la copia real y documentación técnica

- Se cotejó la copia completa de Kaizen Modular con la salida del recolector.
- Se verificó stack .NET 10, PostgreSQL/EF Core, Angular 22 y módulos.
- Se documentaron riesgos de recetas planas, combos, elaboraciones, salsas por pedido y stock flexible.
- No se publicó código empresarial ni se modificaron bases de datos o producción.

## 2026-10-23 — Corrección del recolector PowerShell REN

- Se corrige error de .Count bajo StrictMode y se actualiza guía Windows.

## 2026-10-23 — Inicio de documentación y preparación de migración

- Creación de estructura documental y control de documentación en Pull Requests.

## Plantilla para próximas entradas

### AAAA-MM-DD — Módulo / Cambio

- **Motivo:**
- **Cambios realizados:**
- **Base de datos / migraciones:**
- **Pruebas y resultados:**
- **Riesgos / reversión:**
- **Enlace al PR o commit:**
