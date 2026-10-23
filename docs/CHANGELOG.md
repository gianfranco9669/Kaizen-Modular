# Historial de cambios

Registrar cada modificación funcional, cambio de datos o despliegue importante en el mismo PR.

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
