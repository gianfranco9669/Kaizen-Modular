# Contribución y mantenimiento documental

## Rutina para cada cambio importante

1. Crear rama `feat/tema`, `fix/tema`, `data/tema` o `docs/tema`.
2. Registrar qué problema resuelve, archivos afectados y riesgos.
3. Agregar o actualizar pruebas; verificar funcionamiento de cocina, pedidos, caja, stock e integraciones si aplica.
4. Actualizar **`docs/CHANGELOG.md`** (fecha, módulo, cambio, prueba, impacto, responsable).
5. Actualizar **`docs/MODULOS.md`** o **`docs/ARQUITECTURA.md`** si cambian circuitos o esquema.
6. Anotar en **`docs/DECISIONES.md`** cambios de criterio o arquitectura.
7. Abrir Pull Request usando la plantilla, revisar y fusionar solo después de los controles.
8. En producción: respaldo, migración idempotente, verificación y forma de revertir.

## Reglas no negociables

- Ninguna clave, token, archivo de configuración real ni información de clientes puede formar parte de un commit.
- No subir exportaciones SQL de datos de producción ni archivos comprimidos con respaldos.
- Para las recetas de REN, la información validada en cocina y el menú comercial son fuentes separadas; discrepancias pendientes deben figurar como pendientes, no como cantidades inventadas.
- No borrar IDs, transacciones históricas ni SKU de integraciones al modificar el menú.
- El control automático de Pull Requests exige una actualización documental si se modifica código o infraestructura. **No redacta automáticamente la documentación:** la persona que modifica debe mantenerla.

## Nombres sugeridos de commits

- `docs: documentar circuito de stock`
- `feat(gastronomia): habilitar versiones de recetas`
- `fix(pedidos): descontar salsa una vez por pedido`
- `data(ren): preparar migración auditable de menú`
