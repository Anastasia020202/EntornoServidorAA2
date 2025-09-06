<template>
  <div class="min-h-screen bg-gray-100 flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <div>
        <h1 class="text-center text-3xl font-extrabold text-gray-900 mb-2">
          🅿️ ParkingApp2
        </h1>
        <p class="text-center text-sm text-gray-600">
          Sistema de Gestión de Aparcamiento
        </p>
      </div>
      
      <RegisterForm />
    </div>
  </div>
</template>

<script setup lang="ts">
import RegisterForm from '@/components/auth/RegisterForm.vue';
</script>
