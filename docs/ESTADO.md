# Estado verificado del proyecto — 23/10/2026

**Fuentes:** inspección estática del código local recibido, precheck local de Git y pruebas de respaldo/restauración de PostgreSQL. **Sin cambios en producción.**

## Código y ramas

- .NET 10 / ASP.NET Core, EF Core y PostgreSQL; frontend Angular 22.
- Arquitectura API, Aplicación, Dominio e Infraestructura.
- El código de la computadora está en la rama local `gastronomia-real`, commit `c14ccad263cb`, y tiene trabajo no confirmado en Git (modificaciones y archivos nuevos).
- El remoto `main` documentado **no contiene todavía la copia completa del sistema**, por lo que no debe emplearse para sobrescribir la carpeta local ni hacerse `git reset --hard`.
- Este repositorio sigue siendo **público**; ningún ZIP empresarial ni backup se publicará hasta privatizar y sanear.

## Respaldo y restauración de la base local

- El 23/10/2026 se generó un respaldo PostgreSQL custom de `kaizen_modular_dev`: `REN_KAIZEN_20261023_111109.backup`.
- Huella SHA-256 local constatada: `fd84dbb216e133047069d8cbcdcfe07ff5967b1756be8cbd6d7ef167e6ba0d81`.
- El 23/10/2026 a las 11:21 hora Argentina, el usuario ejecutó un restore **en una base descartable**: resultado `RESTORE OK EN BASE DESCARTABLE`.
- El restaurador informó 51 tablas y 19 migraciones. La base original **no fue restaurada ni modificada durante la prueba**.
- El reporte de validación es comprobación del backup **local**. No certifica que la base local sea idéntica a un eventual servidor de producción ni que los datos capturados sigan siendo actuales después de las 11:11.

## Estado de migración REN

- Motor de recetas plano, sin componentes proporcionales para combinados; reemplazo de líneas de receta en guardado; riesgo de stock si falta inicialización.
- Recetario de trabajo: 27 productos individuales + 7 combinados; gramajes y precios pendientes documentados en `MIGRACION_REN.md`.
- **Siguiente control:** auditoría SQL de solo lectura de los productos, insumos, recetas, stock y modificadores actuales para conciliar IDs, SKU y precios.
- **Bloqueado:** despliegue de carta, cambio de recetas y actualizaciones de Git local. La auditoría todavía no se ejecutó.

## Falta para liberar producción

1. Verificar cuál es realmente el servicio y la base utilizados por producción.
2. Conciliar el Git local sin perder archivos ni commits; realizar baseline saneado en repositorio privado.
3. Revisar informe SQL de solo lectura y completar cantidades pendientes con cocina.
4. Desarrollar cambios mínimos y pruebas funcionales, históricas, migraciones y rollback.
5. Ejecutar despliegue controlado y revisar logs/servicio.
