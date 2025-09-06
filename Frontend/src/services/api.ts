import axios from 'axios';
import type { 
  Usuario, 
  UsuarioCreateDto, 
  UsuarioLoginDto, 
  LoginResponse,
  Plaza,
  PlazaCreateDto,
  Vehiculo,
  VehiculoCreateDto,
  Reserva,
  ReservaCreateDto
} from '@/types/api';

// Configuración base de Axios
const api = axios.create({
  baseURL: 'http://localhost:7138/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para añadir token JWT automáticamente
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('jwt_token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Interceptor para manejar errores de autenticación
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('jwt_token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// Servicios de autenticación
export const authService = {
  async login(credentials: UsuarioLoginDto): Promise<LoginResponse> {
    const response = await api.post('/auth/login', credentials);
    return response.data;
  },

  async register(userData: UsuarioCreateDto): Promise<Usuario> {
    const response = await api.post('/auth/register', userData);
    return response.data;
  },

  async createAdmin(userData: UsuarioCreateDto): Promise<Usuario> {
    const response = await api.post('/auth/create-admin', userData);
    return response.data;
  }
};

// Servicios de usuarios
export const usuarioService = {
  async getAll(): Promise<Usuario[]> {
    const response = await api.get('/usuarios');
    return response.data;
  },

  async getById(id: number): Promise<Usuario> {
    const response = await api.get(`/usuarios/${id}`);
    return response.data;
  },

  async create(userData: UsuarioCreateDto): Promise<Usuario> {
    const response = await api.post('/usuarios', userData);
    return response.data;
  },

  async update(id: number, userData: Partial<UsuarioCreateDto>): Promise<Usuario> {
    const response = await api.put(`/usuarios/${id}`, userData);
    return response.data;
  },

  async delete(id: number): Promise<void> {
    await api.delete(`/usuarios/${id}`);
  }
};

// Servicios de plazas
export const plazaService = {
  async getAll(): Promise<Plaza[]> {
    const response = await api.get('/plazas');
    return response.data;
  },

  async getById(id: number): Promise<Plaza> {
    const response = await api.get(`/plazas/${id}`);
    return response.data;
  },

  async create(plazaData: PlazaCreateDto): Promise<Plaza> {
    const response = await api.post('/plazas', plazaData);
    return response.data;
  },

  async update(id: number, plazaData: Partial<PlazaCreateDto>): Promise<Plaza> {
    const response = await api.put(`/plazas/${id}`, plazaData);
    return response.data;
  },

  async delete(id: number): Promise<void> {
    await api.delete(`/plazas/${id}`);
  }
};

// Servicios de vehículos
export const vehiculoService = {
  async getAll(): Promise<Vehiculo[]> {
    const response = await api.get('/vehiculos');
    return response.data;
  },

  async getByUsuario(usuarioId: number): Promise<Vehiculo[]> {
    const response = await api.get(`/vehiculos/usuario/${usuarioId}`);
    return response.data;
  },

  async getByMatricula(matricula: string): Promise<Vehiculo> {
    const response = await api.get(`/vehiculos/matricula/${matricula}`);
    return response.data;
  },

  async create(vehiculoData: VehiculoCreateDto): Promise<Vehiculo> {
    const response = await api.post('/vehiculos', vehiculoData);
    return response.data;
  },

  async update(id: number, vehiculoData: Partial<VehiculoCreateDto>): Promise<Vehiculo> {
    const response = await api.put(`/vehiculos/${id}`, vehiculoData);
    return response.data;
  },

  async delete(id: number): Promise<void> {
    await api.delete(`/vehiculos/${id}`);
  }
};

// Servicios de reservas
export const reservaService = {
  async getAll(): Promise<Reserva[]> {
    const response = await api.get('/reservas');
    return response.data;
  },

  async getByUsuario(usuarioId: number): Promise<Reserva[]> {
    const response = await api.get(`/reservas/usuario/${usuarioId}`);
    return response.data;
  },

  async getByVehiculo(vehiculoId: number): Promise<Reserva[]> {
    const response = await api.get(`/reservas/vehiculo/${vehiculoId}`);
    return response.data;
  },

  async getByPlaza(plazaId: number): Promise<Reserva[]> {
    const response = await api.get(`/reservas/plaza/${plazaId}`);
    return response.data;
  },

  async create(reservaData: ReservaCreateDto): Promise<Reserva> {
    const response = await api.post('/reservas', reservaData);
    return response.data;
  },

  async update(id: number, reservaData: Partial<ReservaCreateDto>): Promise<Reserva> {
    const response = await api.put(`/reservas/${id}`, reservaData);
    return response.data;
  },

  async delete(id: number): Promise<void> {
    await api.delete(`/reservas/${id}`);
  }
};

export default api;

