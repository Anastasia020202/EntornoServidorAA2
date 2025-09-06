<template>
  <div class="register-form">
    <h2 class="text-2xl font-bold mb-6 text-center">Crear Cuenta</h2>
    
    <form @submit.prevent="handleRegister" class="space-y-4">
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
          minlength="6"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Mínimo 6 caracteres"
        />
      </div>
      
      <div>
        <label for="confirmarContrasena" class="block text-sm font-medium text-gray-700 mb-1">
          Confirmar Contraseña
        </label>
        <input
          id="confirmarContrasena"
          v-model="confirmarContrasena"
          type="password"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Repite tu contraseña"
        />
      </div>
      
      <div v-if="error" class="text-red-600 text-sm">
        {{ error }}
      </div>
      
      <div v-if="success" class="text-green-600 text-sm">
        {{ success }}
      </div>
      
      <button
        type="submit"
        :disabled="isLoading || !isFormValid"
        class="w-full bg-green-600 text-white py-2 px-4 rounded-md hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-500 disabled:opacity-50"
      >
        {{ isLoading ? 'Creando cuenta...' : 'Crear Cuenta' }}
      </button>
    </form>
    
    <div class="mt-4 text-center">
      <p class="text-sm text-gray-600">
        ¿Ya tienes cuenta?
        <router-link to="/login" class="text-blue-600 hover:underline">
          Inicia sesión aquí
        </router-link>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/auth';
import type { UsuarioCreateDto } from '@/types/api';

const router = useRouter();
const authStore = useAuthStore();

const form = reactive<UsuarioCreateDto>({
  correo: '',
  contrasena: ''
});

const confirmarContrasena = ref('');
const error = ref<string>('');
const success = ref<string>('');
const isLoading = ref(false);

const isFormValid = computed(() => {
  return form.correo && 
         form.contrasena && 
         confirmarContrasena.value && 
         form.contrasena === confirmarContrasena.value &&
         form.contrasena.length >= 6;
});

const handleRegister = async () => {
  if (!isFormValid.value) {
    error.value = 'Por favor completa todos los campos correctamente';
    return;
  }
  
  try {
    error.value = '';
    success.value = '';
    isLoading.value = true;
    
    await authStore.register(form);
    
    success.value = 'Cuenta creada exitosamente. Redirigiendo al login...';
    
    setTimeout(() => {
      router.push('/login');
    }, 2000);
  } catch (err: any) {
    error.value = err.response?.data?.message || 'Error al crear la cuenta';
  } finally {
    isLoading.value = false;
  }
};
</script>

<style scoped>
.register-form {
  max-width: 400px;
  margin: 0 auto;
  padding: 2rem;
  background: white;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
</style>

