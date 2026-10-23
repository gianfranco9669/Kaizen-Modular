# Arquitectura del proyecto local (inspección estática 23/10/2026)

> Basado en código local recibido. Sin acceso a base en ejecución ni validación runtime.

## Backend

| Capa | Ruta local | Responsabilidad |
| --- | --- | --- |
| API | `backend/src/Kaizen.Api` | Endpoints, autenticación/autorización, cookies, CSRF, rate limiting, Rappi |
| Aplicación | `backend/src/Kaizen.Aplicacion` | Servicios de casos de uso; gastronomía, administración, gimnasio |
| Dominio | `backend/src/Kaizen.Dominio` | Entidades y contratos del negocio |
| Infraestructura | `backend/src/Kaizen.Infraestructura` | Repositorios EF Core, PostgreSQL y migraciones |

- .NET 10 con PostgreSQL, EF Core y Npgsql.
- Los servicios gastronómicos se reparten en `ServicioGastronomia.cs`, `ServicioGastronomia.OperacionPremium.cs`, `ServicioGastronomia.KdsPremium.cs` e `Integraciones.cs`.
- Los pedidos se registran de forma transaccional con controles de idempotencia; descontar stock se llama al crear pedidos y agregar rondas.
- Las cancelaciones reintegran consumos tomando movimientos originales del pedido.
- Las recetas actuales son insumos directos por producto. **No hay expansión de recetas compuestas en el descuento revisado.**
- Reposición de recetas: el repositorio elimina el detalle previo y lo reemplaza, sin versionado formal de la receta en la entidad.

## Frontend

`frontend/src/app/modulos/gastronomia`: salón, tomar pedido, cocina, ventana, caja, clientes, productos, recetas, insumos, categorías, modificadores, Rappi y KDS.

`frontend/src/app/modulos/administracion`: ventas, comprobantes, compras, gastos, proveedores, tesorería, cuentas corrientes, rentabilidad, auditoría y seguridad.

Hay código de Gimnasio y Economía; su exposición en las rutas principales depende de configuración actual por validar.

## Infraestructura

`docker-compose.production.yml` y `deploy/`: preparación para VPS con proxy HTTPS y PostgreSQL privado. Verificar red, respaldos, configuración y secretos antes de desplegar.

## Flujo actual relevante para REN

```text
Pedido confirmado (local o externo)
  -> validar productos y modificadores
  -> transacción + idempotencia
  -> obtener receta activa por producto
  -> calcular consumos directos por línea
  -> descontar stock inicializado y registrar movimientos
  -> pedido operativo / cocina / caja
```

Para la tienda web con WhatsApp debe agregarse **solicitud web separada**, que todavía no produce comanda ni descuenta stock, y una conversión confirmada e idempotente hacia el flujo anterior.
