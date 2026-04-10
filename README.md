# Kaizen Modular (ARDESOFT)

Base actual del proyecto:
- Frontend: Angular
- Backend: ASP.NET Core Web API (C#)
- Base de datos: PostgreSQL + EF Core

## Configuración de base de datos (cierre técnico)

`Program.cs` resuelve la conexión en este orden:
1. Variable de entorno `KAIZEN_DB_CONNECTION`
2. `ConnectionStrings:KaizenDb` en configuración (`appsettings*.json` / user-secrets)

### Variable real a definir

- **Nombre:** `KAIZEN_DB_CONNECTION`
- **Ejemplo:**
  `Host=localhost;Port=5432;Database=kaizen_modular_dev;Username=kaizen_app;Password=tu_password`

## Arranque mínimo

### Backend

```bash
cd backend/src/Kaizen.Api
dotnet restore
# opcional recomendado para no hardcodear credenciales
dotnet user-secrets set "ConnectionStrings:KaizenDb" "Host=localhost;Port=5432;Database=kaizen_modular_dev;Username=kaizen_app;Password=tu_password"
dotnet ef database update --project ../Kaizen.Infraestructura --startup-project .
dotnet run
```

### Frontend

```bash
cd frontend
npm install
npm run start
```
