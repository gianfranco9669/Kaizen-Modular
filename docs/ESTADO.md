# Estado verificado del proyecto — 23/10/2026

**Fuente:** inspección estática de los ZIP de la conversación: `Kaizen Modular(5).zip` (carpeta completa) y `REN_Codigo_Actual_20261023_092128.zip` (recolector). **No se modificó producción ni se leyó la PostgreSQL activa.**

## Código local confirmado

- .NET 10 / ASP.NET Core, Entity Framework Core y PostgreSQL.
- Angular 22.1 y TypeScript; el README del proyecto pide Node 24.15+.
- Separación API, Aplicación, Dominio e Infraestructura; despliegue previsto con Docker/Caddy.
- Módulos implementados en código: Gastronomía, Administración, Economía, Gimnasio, Seguridad y Rappi.
- Control de pedidos por idempotencia y transacciones, cocina, caja, KDS, mesas, inventario y recetas.
- Hay scripts de pruebas y respaldo que no fueron ejecutados en esta auditoría.

## Estado del repositorio GitHub

Este repositorio contiene **documentación y herramientas iniciales**, pero **todavía no la copia del código productivo**. El repo es público. Antes de subir el código de la empresa hay que ponerlo privado, sanear credenciales y excluir node_modules, bin/obj, parches, copias y backups.

## Riesgos principales del recetario

1. Motor de recetas basado en lista plana de insumos; los combinados requieren componentes proporcionales.
2. Guardar receta existente reemplaza sus items sin un historial de versiones.
3. Parte de las recetas tienen gramajes incompletos.
4. Acompañamientos/salsas deben descontarse por pedido, no por cada roll.
5. Arroz preparado necesita elaboración y rendimiento por lote.
6. El consumo omite insumos sin stock inicializado y permite negativos en inicializados.

## Faltante para validación final

- Estructura y estado actual de PostgreSQL (consulta de **solo lectura**).
- Build y tests con .NET 10, Node 24 y PostgreSQL descartable.
- Validación operativa de pantallas y flujo de pedidos/caja/stock.
- Comprobación de secretos y externalización de configuraciones.
- Cierre de gramajes faltantes con cocina.

**Fuente histórica secundaria:** exportación de `kaizen_modular_dev` del 12/08/2026; no equivale al estado actual.
