// Tipos para la API de ParkingApp2

export interface Usuario {
  id: number;
  correo: string;
  rol: string;
}

export interface UsuarioCreateDto {
  correo: string;
  contrasena: string;
  rol?: string;
}

export interface UsuarioLoginDto {
  correo: string;
  contrasena: string;
}

export interface LoginResponse {
  token: string;
  usuario: Usuario;
}

export interface Plaza {
  id: number;
  numero: string;
  disponible: boolean;
  precioHora: number;
}

export interface PlazaCreateDto {
  numero: string;
  disponible: boolean;
  precioHora: number;
}

export interface Vehiculo {
  id: number;
  matricula: string;
  marca: string;
  modelo: string;
  usuarioId: number;
}

export interface VehiculoCreateDto {
  matricula: string;
  marca: string;
  modelo: string;
}

export interface Reserva {
  id: number;
  fechaInicio: string;
  fechaFin: string;
  totalAPagar: number;
  estado: string;
  usuarioId: number;
  vehiculoId: number;
  plazaId: number;
}

export interface ReservaCreateDto {
  fechaInicio: string;
  fechaFin: string;
  usuarioId: number;
  vehiculoId: number;
  plazaId: number;
}

export interface ApiResponse<T> {
  data: T;
  message?: string;
  success: boolean;
}
