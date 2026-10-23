# Publicar Kaizen Modular sin exponer datos

## Antes de subir código

El repositorio conectado es **público** en la fecha de revisión. Conviene convertirlo en **privado** mediante GitHub → Settings → General → Danger Zone → Change repository visibility.

No subir `Kaizen Modular(5).zip` ni `REN_Codigo_Actual_...zip` tal cual. Pueden incluir archivos fuente no auditados, scripts antiguos, configuraciones y respaldos. Tampoco subir dumps PostgreSQL ni información de clientes.

## Exclusiones obligatorias

- `frontend/node_modules`, `frontend/dist`, `frontend/.angular`, `backend/**/bin`, `backend/**/obj`, `.vs`, `.git`.
- `parches backup`, `parches-backup`, `backups`, `logs`, carpetas de artefactos y restauraciones.
- Archivos `.env`, claves, certificados, credenciales, exports, dumps y appsettings con valores secretos.
- Archivos fuente con secretos incrustados (requieren escaneo, no alcanza con .gitignore).

## Secuencia recomendada

1. Cambiar la visibilidad a privado.
2. Hacer una copia **limpia** del código local y revisar con detector de secretos.
3. Publicar sólo el código activo, migraciones y scripts seguros.
4. Mantener configuración pública únicamente como `*.example` con placeholders.
5. Configurar PRs, CI, revisión documental y estrategia de backups.
6. Subir cambios de datos **mediante migraciones idempotentes**, nunca con dumps de clientes.

La revisión estática no prueba que no existan secretos; no debe confundirse con un escaneo integral de seguridad.
