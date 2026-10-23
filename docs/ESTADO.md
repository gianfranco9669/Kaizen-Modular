# Estado del proyecto y fuentes

Fecha de apertura de esta documentación: **23/10/2026**.

## Confirmado

- Hay un sistema Kaizen Modular utilizado para gastronomía, administración y otros módulos.
- El código que se ejecuta en la instalación vigente **todavía no fue cotejado** con este repositorio.
- Existe una exportación de estructura y datos de prueba/desarrollo de agosto de 2026, **histórica, no representativa de producción actual**.
- Se cuenta con un menú comercial REN y un relevamiento presencial de recetas, con pendientes que impiden dar por completos todos los consumos.

## Por verificar cuando llegue el código actual

- Backend y frontend exactos, versiones, ubicación y método de despliegue.
- Base/s de datos, migraciones y tablas realmente utilizadas.
- Circuitos de pedidos, cancelaciones, stock, cocina, caja e integración externa.
- Mecanismo de descuentos, modificadores, recetas activas e historización.
- Tests, configuración, respaldos y logs.

## Objetivo inmediato

1. Obtener copia sanitizada del código vigente y exportación estructural de solo lectura.
2. Confrontar base actual con manifest del recetario nuevo.
3. Diseñar migración con respaldo, pruebas e idempotencia.
4. Incorporar al repositorio el código auditado, sin credenciales ni información privada.
5. Mantener documentación y CHANGELOG en cada modificación.

**No publicar recetas completas ni datos de clientes en este repositorio público sin autorización.**
