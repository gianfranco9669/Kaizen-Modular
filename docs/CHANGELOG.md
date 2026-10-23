# Historial de cambios

Todas las modificaciones funcionales, cambios de datos y despliegues importantes deben registrarse aquí, idealmente en el mismo Pull Request.

## 2026-10-23 — Corrección del recolector PowerShell REN

- **Problema:** el recolector original fallaba en PowerShell con StrictMode al consultar `.Count` sobre un resultado individual de `Where-Object`.
- **Cambio:** se fuerza el resultado a colección mediante `@(...)`, se muestra la carpeta inspeccionada y se detecta una extracción sin archivos.
- **Archivos:** `scripts/REN_RECOLECTAR_CODIGO_SOLO_LECTURA.ps1` y `docs/GUIA_WINDOWS.md`.
- **Impacto:** solo herramienta local de auditoría; no se cambió código aplicativo, base de datos ni sistema productivo.
- **Verificación:** inspección estática del script; ejecución en Windows pendiente de la prueba del usuario.

## 2026-10-23 — Inicio de documentación y preparación de migración

- Se crea estructura documental para migrar el proyecto local a GitHub de forma controlada.
- Se agrega control de actualización de documentación en Pull Requests.
- Se describe el plan de auditoría de productos, recetas, inventario e integraciones de REN.
- **Sin modificaciones al código productivo ni a la base actual.**
- **Pendiente:** recibir y cotejar el código local vigente.

## Plantilla para próximas entradas

### AAAA-MM-DD — Módulo / Cambio

- **Motivo:**
- **Cambios realizados:**
- **Base de datos / migraciones:**
- **Pruebas y resultados:**
- **Riesgos / reversión:**
- **Enlace al PR o commit:**
