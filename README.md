# 🅿️ ParkingApp2 - Sistema de Gestión de Parking

Una API REST desarrollada en .NET 8.0 para la gestión de parking con autenticación JWT y autorización por roles.

##  Características Principales

- ✅ **Autenticación JWT** con roles (Admin/User)
- ✅ **API RESTful** completa con CRUD operations
- ✅ **Zonas públicas y privadas** de acceso
- ✅ **Validación de datos** y lógica de negocio
- ✅ **Containerización** con Docker
- ✅ **Base de datos MySQL** con Entity Framework Core
- ✅ **Documentación Swagger** integrada
- 

##  Arquitectura del Proyecto

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
```

## Tecnologías Utilizadas

- **.NET 8.0** - Framework principal
- **Entity Framework Core** - ORM para MySQL
- **JWT Authentication** - Autenticación basada en tokens
- **Docker & Docker Compose** - Containerización
- **Swagger/OpenAPI** - Documentación de API
- **MySQL** - Base de datos relacional

##  Instalación y Ejecución

### Opción 1: Con Docker (Recomendado)

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/Anastasia020202/EntornoServidorAA2.git
   cd ParkingApp2
   ```

2. **Ejecutar con Docker Compose:**
   ```bash
   docker-compose up --build
   ```

3. **Acceder a la aplicación:**
   - **API**: http://localhost:7138
   - **Swagger**: http://localhost:7138/swagger
   - **MySQL**: localhost:8317

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

##  Autenticación y Autorización

### Roles Disponibles
- **Admin**: Acceso completo a todos los endpoints
- **User**: Acceso limitado a sus propios recursos

### Usuario por Defecto
- **Email**: admin@parking.com
- **Contraseña**: admin123
- **Rol**: Admin

### Endpoints por Zona

####  Zona Pública (Sin autenticación)
- `GET /api/plazas` - Listar todas las plazas disponibles
- `GET /api/plazas?tipo=estandar` - Filtrar plazas disponibles por tipo
- `GET /api/plazas/tipo/estandar` - Buscar plazas estándar disponibles
- `GET /api/plazas/tipo/moto` - Buscar plazas para motos disponibles
- `GET /api/plazas/tipo/discapacitados` - Buscar plazas para discapacitados disponibles
- `GET /api/plazas/tipo/electrico` - Buscar plazas eléctricas disponibles
- `GET /api/plazas/tipo/vip` - Buscar plazas VIP disponibles
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrarse

**💡 Lógica de Negocio**: Los usuarios no identificados solo ven plazas disponibles para decidir si registrarse y hacer una reserva.

####  Zona Privada (Requiere autenticación)
- `GET /api/plazas/{id}` - Obtener plaza específica (User/Admin)
- `GET /api/reservas/mis-reservas` - Mis reservas (User/Admin)
- `GET /api/vehiculos/mis-vehiculos` - Mis vehículos (User/Admin)
- `POST /api/reservas` - Crear reserva (User/Admin)
- `PUT /api/reservas/{id}` - Actualizar reserva (User/Admin)

####  Zona de Administración (Solo Admin)
- `GET /api/plazas/admin` - Ver todas las plazas con filtros completos (disponible=true/false)
- `GET /api/plazas/admin?tipo=estandar&disponible=false` - Ver plazas ocupadas por tipo
- `GET /api/usuarios` - Listar usuarios
- `POST /api/plazas` - Crear plaza
- `PUT /api/plazas/{id}` - Actualizar plaza
- `DELETE /api/plazas/{id}` - Eliminar plaza

##  Modelo de Datos

### Entidades Principales

#### Usuario
- `Id` (int) - Identificador único
- `Correo` (string) - Email del usuario
- `HashContrasena` (string) - Hash de la contraseña
- `SaltContrasena` (byte[]) - Salt para el hash
- `Rol` (string) - Rol del usuario (Admin/User)
- `FechaCreacion` (DateTime) - Fecha de registro
- `Activo` (bool) - Estado del usuario

#### Plaza
- `Id` (int) - Identificador único
- `Numero` (string) - Número de la plaza
- `Tipo` (string) - Tipo de plaza (Estándar, Moto, Discapacitados, Eléctrico, VIP)
- `Disponible` (bool) - Disponibilidad actual
- `PrecioHora` (decimal) - Precio por hora
- `FechaAlta` (DateTime) - Fecha de alta
- `Activa` (bool) - Estado administrativo de la plaza

#### Vehiculo
- `Id` (int) - Identificador único
- `Matricula` (string) - Matrícula del vehículo
- `Marca` (string) - Marca del vehículo
- `Modelo` (string) - Modelo del vehículo
- `UsuarioId` (int) - ID del propietario
- `FechaRegistro` (DateTime) - Fecha de registro
- `Activo` (bool) - Estado del vehículo

#### Reserva
- `Id` (int) - Identificador único
- `FechaInicio` (DateTime) - Fecha de inicio
- `FechaFin` (DateTime) - Fecha de fin
- `TotalAPagar` (decimal) - Total a pagar
- `Estado` (string) - Estado de la reserva
- `UsuarioId` (int) - ID del usuario
- `VehiculoId` (int) - ID del vehículo
- `PlazaId` (int) - ID de la plaza

### Relaciones
- Usuario → Vehiculos (1:N)
- Usuario → Reservas (1:N)
- Vehiculo → Reservas (1:N)
- Plaza → Reservas (1:N)



##  Endpoints Principales

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrarse

### Usuarios (Admin)
- `GET /api/usuarios` - Listar usuarios con filtros y ordenación
- `GET /api/usuarios/{id}` - Obtener usuario específico
- `PUT /api/usuarios/{id}` - Actualizar usuario
- `DELETE /api/usuarios/{id}` - Eliminar usuario

### Plazas
- `GET /api/plazas` - Listar todas las plazas disponibles - **Público**
- `GET /api/plazas?tipo=estandar` - Filtrar plazas disponibles por tipo - **Público**
- `GET /api/plazas/tipo/estandar` - Buscar plazas estándar disponibles - **Público**
- `GET /api/plazas/tipo/moto` - Buscar plazas para motos disponibles - **Público**
- `GET /api/plazas/tipo/discapacitados` - Buscar plazas para discapacitados disponibles - **Público**
- `GET /api/plazas/tipo/electrico` - Buscar plazas eléctricas disponibles - **Público**
- `GET /api/plazas/tipo/vip` - Buscar plazas VIP disponibles - **Público**
- `GET /api/plazas/{id}` - Obtener plaza específica - **Privado (User/Admin)**
- `GET /api/plazas/admin` - Ver todas las plazas con filtros completos - **Admin**
- `GET /api/plazas/admin?disponible=false` - Ver plazas ocupadas - **Admin**

### Vehículos
- `GET /api/vehiculos/mis-vehiculos` - Mis vehículos
- `POST /api/vehiculos` - Crear vehículo
- `PUT /api/vehiculos/{id}` - Actualizar vehículo
- `DELETE /api/vehiculos/{id}` - Eliminar vehículo

### Reservas
- `GET /api/reservas/mis-reservas` - Mis reservas
- `POST /api/reservas` - Crear reserva
- `PUT /api/reservas/{id}` - Actualizar reserva
- `DELETE /api/reservas/{id}` - Cancelar reserva


### Ejemplos de Uso

#### Para Usuarios No Identificados (Público)
```bash
# Ver todas las plazas disponibles (por defecto)
GET /api/plazas

# Buscar plazas para discapacitados disponibles
GET /api/plazas/tipo/discapacitados

# Buscar plazas para motos disponibles
GET /api/plazas/tipo/moto

# Buscar plazas estándar disponibles
GET /api/plazas/tipo/estandar

# Buscar plazas eléctricas disponibles
GET /api/plazas/tipo/electrico

# Buscar plazas VIP disponibles
GET /api/plazas/tipo/vip

# Filtrar por tipo específico
GET /api/plazas?tipo=estandar
```

#### Para Administradores (Solo Admin)
```bash
# Ver todas las plazas (disponibles y ocupadas)
GET /api/plazas/admin

# Ver solo plazas ocupadas
GET /api/plazas/admin?disponible=false

# Ver plazas estándar ocupadas
GET /api/plazas/admin?tipo=estandar&disponible=false

# Ver plazas VIP disponibles
GET /api/plazas/admin?tipo=vip&disponible=true

# Ver todas las plazas eléctricas
GET /api/plazas/admin?tipo=electrico
```

####  Casos de Uso Reales
```bash
# Usuario busca plaza para moto disponible
GET /api/plazas/tipo/moto?disponible=true

# Usuario con discapacidad busca plaza accesible
GET /api/plazas/tipo/discapacitados?disponible=true

# Usuario busca plaza VIP disponible
GET /api/plazas/tipo/vip?disponible=true

# Usuario busca plaza eléctrica disponible
GET /api/plazas/tipo/electrico?disponible=true
```

### Usar Swagger
1. Accede a http://localhost:7138/swagger
2. Haz clic en "Authorize" y pega tu token JWT
3. Prueba los endpoints directamente desde la interfaz

##  Testing con Postman
En la carpeta raíz encontrarás la colección `ParkingApi Collection.postman_collection.json`  
que incluye todos los endpoints organizados por carpetas (Auth, Usuarios, Plazas, Vehículos, Reservas).

### Importar la Colección
1. Abre Postman
2. Haz clic en "Import" 
3. Selecciona el archivo `ParkingApi Collection.postman_collection.json`
4. La colección se importará con todas las requests configuradas

### Configurar Variables de Entorno
1. Crea un nuevo environment en Postman
2. Agrega la variable `base_url` con valor `http://localhost:7138`
3. Selecciona este environment para usar las requests

### Flujo de Testing Recomendado
1. **Autenticación**: Usa `POST /auth/login` con admin@parking.com / admin123
2. **Copiar Token**: Guarda el token JWT del response
3. **Configurar Authorization**: En cada request, ve a "Authorization" → "Bearer Token" → pega el token


##  Docker

### Comandos Útiles
```bash
# Construir y ejecutar
docker-compose up --build

# Ejecutar en segundo plano
docker-compose up -d

# Ver logs
docker-compose logs -f

# Parar servicios
docker-compose down

# Limpiar volúmenes
docker-compose down -v
```







