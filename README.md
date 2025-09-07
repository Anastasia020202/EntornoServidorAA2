# 🅿️ ParkingApp2 - Sistema de Gestión de Parking

Una API REST desarrollada en .NET 8.0 para la gestión de parking con autenticación JWT y autorización por roles.

## Arquitectura del Proyecto

```
ParkingApp2/
├── Backend/
│   ├── API/                 # Capa de presentación (Controllers)
│   ├── Business/            # Lógica de negocio (Services)
│   ├── Data/               # Acceso a datos (Repositories, DbContext)
│   ├── Models/             # Entidades y DTOs
│   └── Migrations/         # Migraciones de base de datos
├── docker-compose.yml      # Orquestación de servicios
└── README.md
```sa

##  Tecnologías Utilizadas

- **.NET 8.0**
- **Entity Framework Core** con MySQL
- **JWT Authentication**
- **Docker & Docker Compose**
- **Swagger/OpenAPI**
- **MySQL**


## Instalación y Ejecución

### Opción 1: Con Docker (Recomendado)

1. **Clonar el repositorio:**
   ```bash
   git clone <repositorio>
   cd ParkingApp2
   ```

2. **Ejecutar con Docker Compose:**
   ```bash
   docker-compose up --build
   ```

3. **Acceder a la aplicación:**
   - API: http://localhost:7138
   - Swagger: http://localhost:7138/swagger
   - MySQL: localhost:3306

### Opción 2: Desarrollo Local

1. **Instalar dependencias:**
   ```bash
   dotnet restore
   ```

2. **Configurar MySQL local:**
   - Crear base de datos: `ParkingApp2Db`
   - Actualizar connection string en `appsettings.json`

3. **Ejecutar migraciones:**
   ```bash
   dotnet ef database update --project Backend/Data --startup-project Backend/API
   ```

4. **Ejecutar la aplicación:**
   ```bash
   dotnet run --project Backend/API
   ```

## Autenticación y Autorización

### Roles Disponibles
- **Admin**: Acceso completo a todos los endpoints
- **User**: Acceso limitado a sus propios recursos

## Modelo de Datos

### Entidades Principales

- **Usuario**: Gestión de usuarios y autenticación
- **Plaza**: Plazas de aparcamiento disponibles
- **Vehiculo**: Vehículos registrados por usuario
- **Reserva**: Reservas de plazas con fechas y precios

### Relaciones
- Usuario → Vehiculos (1:N)
- Usuario → Reservas (1:N)
- Vehiculo → Reservas (1:N)
- Plaza → Reservas (1:N)


### Variables de Entorno
```yaml
# MySQL
MYSQL_ROOT_PASSWORD: password
MYSQL_DATABASE: ParkingApp2Db
MYSQL_USER: parkinguser
MYSQL_PASSWORD: parkingpass




