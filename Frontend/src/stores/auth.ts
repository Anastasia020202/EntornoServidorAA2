import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import type { Usuario, UsuarioLoginDto, UsuarioCreateDto } from '@/types/api';
import { authService } from '@/services/api';

export const useAuthStore = defineStore('auth', () => {
  // Estado
  const user = ref<Usuario | null>(null);
  const token = ref<string | null>(null);
  const isLoading = ref(false);

  // Getters
  const isAuthenticated = computed(() => !!token.value);
  const isAdmin = computed(() => user.value?.rol === 'Admin');
  const isUser = computed(() => user.value?.rol === 'User');

  // Acciones
  const login = async (credentials: UsuarioLoginDto) => {
    try {
      isLoading.value = true;
      const response = await authService.login(credentials);
      
      token.value = response.token;
      user.value = response.usuario;
      
      // Guardar en localStorage
      localStorage.setItem('jwt_token', response.token);
      localStorage.setItem('user', JSON.stringify(response.usuario));
      
      return response;
    } catch (error) {
      throw error;
    } finally {
      isLoading.value = false;
    }
  };

  const register = async (userData: UsuarioCreateDto) => {
    try {
      isLoading.value = true;
      const response = await authService.register(userData);
      return response;
    } catch (error) {
      throw error;
    } finally {
      isLoading.value = false;
    }
  };

  const createAdmin = async (userData: UsuarioCreateDto) => {
    try {
      isLoading.value = true;
      const response = await authService.createAdmin(userData);
      return response;
    } catch (error) {
      throw error;
    } finally {
      isLoading.value = false;
    }
  };

  const logout = () => {
    token.value = null;
    user.value = null;
    localStorage.removeItem('jwt_token');
    localStorage.removeItem('user');
  };

  const initializeAuth = () => {
    const savedToken = localStorage.getItem('jwt_token');
    const savedUser = localStorage.getItem('user');
    
    if (savedToken && savedUser) {
      token.value = savedToken;
      user.value = JSON.parse(savedUser);
    }
  };

  return {
    // Estado
    user,
    token,
    isLoading,
    
    // Getters
    isAuthenticated,
    isAdmin,
    isUser,
    
    // Acciones
    login,
    register,
    createAdmin,
    logout,
    initializeAuth
  };
});

