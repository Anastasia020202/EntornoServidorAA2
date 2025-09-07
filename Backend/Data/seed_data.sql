-- Script para insertar datos de ejemplo en la base de datos ParkingApp2Db

-- Insertar plazas de ejemplo
INSERT INTO Plazas (Numero, Tipo, Disponible, PrecioHora, FechaAlta, Activa) VALUES
('A-001', 'Estándar', true, 2.50, '2024-01-01 00:00:00', true),
('A-002', 'Estándar', true, 2.50, '2024-01-01 00:00:00', true),
('M-001', 'Moto', true, 1.50, '2024-01-01 00:00:00', true),
('D-001', 'Discapacitados', true, 1.00, '2024-01-01 00:00:00', true),
('E-001', 'Eléctrico', true, 3.00, '2024-01-01 00:00:00', true),
('V-001', 'VIP', true, 5.00, '2024-01-01 00:00:00', true);

-- Insertar vehículos de ejemplo
INSERT INTO Vehiculos (Matricula, Marca, Modelo, UsuarioId, FechaRegistro, Activo) VALUES
('1234-ABC', 'Toyota', 'Corolla', 2, '2024-01-20 00:00:00', true),
('5678-DEF', 'Honda', 'Civic', 2, '2024-02-01 00:00:00', true),
('9012-GHI', 'BMW', 'X3', 3, '2024-02-05 00:00:00', true),
('3456-JKL', 'Yamaha', 'MT-07', 4, '2024-02-10 00:00:00', true);

-- Insertar reservas de ejemplo
INSERT INTO Reservas (FechaInicio, FechaFin, TotalAPagar, Estado, UsuarioId, VehiculoId, PlazaId) VALUES
('2024-09-07 09:00:00', '2024-09-07 11:00:00', 5.00, 'Activa', 2, 1, 1),
('2024-09-07 14:00:00', '2024-09-07 16:30:00', 6.25, 'Activa', 3, 3, 2),
('2024-09-06 10:00:00', '2024-09-06 12:00:00', 3.00, 'Completada', 4, 4, 3);