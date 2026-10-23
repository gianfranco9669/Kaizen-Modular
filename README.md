# Kaizen Modular

Sistema modular para operación de gastronomía y otras áreas de Kaizen.

> **Estado de este repositorio (23/10/2026):** se está preparando la migración de la copia local vigente a GitHub. **El código productivo actual todavía no está publicado aquí**; los documentos describen el método de trabajo y el alcance de la auditoría. No asumir que este repositorio es una copia ejecutable del sistema.

## Punto de partida

1. [Guía para recopilar el código en Windows](docs/GUIA_WINDOWS.md).
2. [Estado real y fuentes disponibles](docs/ESTADO.md).
3. [Mapa de arquitectura por verificar](docs/ARQUITECTURA.md).
4. [Módulos y criterios de prueba](docs/MODULOS.md).
5. [Plan de migración del recetario REN](docs/MIGRACION_REN.md).
6. [Historial de modificaciones](docs/CHANGELOG.md).
7. [Decisiones técnicas](docs/DECISIONES.md).
8. [Cómo contribuir y mantener la documentación](CONTRIBUTING.md).

## Política de seguridad

**No subir** `.env`, `appsettings.*.json` que contengan secretos, claves, respaldos de base de datos, datos de clientes, logs con información personal ni credenciales. El repositorio actualmente es **público**: evaluar hacerlo privado antes de subir código empresarial o recetarios propietarios. `.gitignore` ayuda, pero **no sustituye** la revisión manual.

## Forma de trabajo

- Cada cambio funcional relevante se realiza en una rama y se envía mediante Pull Request.
- Actualizar `docs/CHANGELOG.md` y la documentación del módulo afectado.
- Registrar decisiones arquitectónicas en `docs/DECISIONES.md`.
- Probar en un entorno de desarrollo o copia antes de desplegar.
- No borrar registros históricos ni reescribir el inventario productivo sin respaldo y plan de reversión.
