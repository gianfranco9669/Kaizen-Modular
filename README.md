# Kaizen Modular (ARDESOFT)

Base del sistema en stack definitivo:

- **Frontend:** Angular
- **Backend:** ASP.NET Core Web API (C#)
- **Base de datos:** PostgreSQL + Entity Framework Core (Npgsql)

## Qué quedó operativo en este corte

- Gimnasio: socios, planes, membresías y control de acceso básico.
- Administración: consulta de impactos comerciales generados desde gimnasio.
- Integración inicial: al crear membresía se registra impacto comercial administrativo dentro de transacción.
- Frontend modular: navegación por dominios (Gimnasio y Administración).

## Qué falta todavía

- Autenticación, autorización por roles y permisos finos.
- Auditoría persistente completa y trazabilidad avanzada.
- Módulo Gastronomía operativo.
- Test automáticos (unitarios/integración/e2e).

## Comandos para levantar local

### Backend

```bash
cd backend/src/Kaizen.Api
dotnet restore
# definir cadena local de forma segura
dotnet user-secrets set "ConnectionStrings:KaizenDb" "Host=localhost;Port=5432;Database=kaizen_modular_dev;Username=kaizen_app;Password=TU_PASSWORD"
dotnet ef database update --project ../Kaizen.Infraestructura --startup-project .
dotnet run
```

### Frontend

```bash
cd frontend
npm install
npm run start
```

### Builds Angular

```bash
# Build desarrollo
npm run build

# Build producción (usa environment.prod.ts)
npm run build:prod
```
