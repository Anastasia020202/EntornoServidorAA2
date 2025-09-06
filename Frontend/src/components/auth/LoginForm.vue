<template>
  <div class="login-form">
    <h2 class="text-2xl font-bold mb-6 text-center">Iniciar Sesión</h2>
    
    <form @submit.prevent="handleLogin" class="space-y-4">
      <div>
        <label for="correo" class="block text-sm font-medium text-gray-700 mb-1">
          Correo Electrónico
        </label>
        <input
          id="correo"
          v-model="form.correo"
          type="email"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="usuario@parking.com"
        />
      </div>
      
      <div>
        <label for="contrasena" class="block text-sm font-medium text-gray-700 mb-1">
          Contraseña
        </label>
        <input
          id="contrasena"
          v-model="form.contrasena"
          type="password"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Tu contraseña"
        />
      </div>
      
      <div v-if="error" class="text-red-600 text-sm">
        {{ error }}
      </div>
      
      <button
        type="submit"
        :disabled="isLoading"
        class="w-full bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50"
      >
        {{ isLoading ? 'Iniciando sesión...' : 'Iniciar Sesión' }}
      </button>
    </form>
    
    <div class="mt-4 text-center">
      <p class="text-sm text-gray-600">
        ¿No tienes cuenta?
        <router-link to="/register" class="text-blue-600 hover:underline">
          Regístrate aquí
        </router-link>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/auth';
import type { UsuarioLoginDto } from '@/types/api';

const router = useRouter();
const authStore = useAuthStore();

const form = reactive<UsuarioLoginDto>({
  correo: '',
  contrasena: ''
});

const error = ref<string>('');
const isLoading = ref(false);

const handleLogin = async () => {
  try {
    error.value = '';
    isLoading.value = true;
    
    await authStore.login(form);
    
    // Redirigir según el rol
    if (authStore.isAdmin) {
      router.push('/admin/dashboard');
    } else {
      router.push('/dashboard');
    }
  } catch (err: any) {
    error.value = err.response?.data?.message || 'Error al iniciar sesión';
  } finally {
    isLoading.value = false;
  }
};
</script>

<style scoped>
.login-form {
  max-width: 400px;
  margin: 0 auto;
  padding: 2rem;
  background: white;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
</style>
