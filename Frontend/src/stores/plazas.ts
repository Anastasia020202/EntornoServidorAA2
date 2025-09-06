import { defineStore } from 'pinia';
import { ref } from 'vue';
import type { Plaza, PlazaCreateDto } from '@/types/api';
import { plazaService } from '@/services/api';

export const usePlazasStore = defineStore('plazas', () => {
  // Estado
  const plazas = ref<Plaza[]>([]);
  const isLoading = ref(false);
  const error = ref<string | null>(null);

  // Acciones
  const fetchPlazas = async () => {
    try {
      isLoading.value = true;
      error.value = null;
      plazas.value = await plazaService.getAll();
    } catch (err) {
      error.value = 'Error al cargar las plazas';
      console.error('Error fetching plazas:', err);
    } finally {
      isLoading.value = false;
    }
  };

  const getPlazaById = async (id: number) => {
    try {
      isLoading.value = true;
      error.value = null;
      return await plazaService.getById(id);
    } catch (err) {
      error.value = 'Error al cargar la plaza';
      console.error('Error fetching plaza:', err);
      throw err;
    } finally {
      isLoading.value = false;
    }
  };

  const createPlaza = async (plazaData: PlazaCreateDto) => {
    try {
      isLoading.value = true;
      error.value = null;
      const newPlaza = await plazaService.create(plazaData);
      plazas.value.push(newPlaza);
      return newPlaza;
    } catch (err) {
      error.value = 'Error al crear la plaza';
      console.error('Error creating plaza:', err);
      throw err;
    } finally {
      isLoading.value = false;
    }
  };

  const updatePlaza = async (id: number, plazaData: Partial<PlazaCreateDto>) => {
    try {
      isLoading.value = true;
      error.value = null;
      const updatedPlaza = await plazaService.update(id, plazaData);
      const index = plazas.value.findIndex(p => p.id === id);
      if (index !== -1) {
        plazas.value[index] = updatedPlaza;
      }
      return updatedPlaza;
    } catch (err) {
      error.value = 'Error al actualizar la plaza';
      console.error('Error updating plaza:', err);
      throw err;
    } finally {
      isLoading.value = false;
    }
  };

  const deletePlaza = async (id: number) => {
    try {
      isLoading.value = true;
      error.value = null;
      await plazaService.delete(id);
      plazas.value = plazas.value.filter(p => p.id !== id);
    } catch (err) {
      error.value = 'Error al eliminar la plaza';
      console.error('Error deleting plaza:', err);
      throw err;
    } finally {
      isLoading.value = false;
    }
  };

  const getPlazasDisponibles = () => {
    return plazas.value.filter(plaza => plaza.disponible);
  };

  const getPlazasOcupadas = () => {
    return plazas.value.filter(plaza => !plaza.disponible);
  };

  return {
    // Estado
    plazas,
    isLoading,
    error,
    
    // Acciones
    fetchPlazas,
    getPlazaById,
    createPlaza,
    updatePlaza,
    deletePlaza,
    getPlazasDisponibles,
    getPlazasOcupadas
  };
});
