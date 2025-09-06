import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../views/HomeView.vue'),
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/auth/LoginView.vue'),
      meta: { requiresGuest: true }
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('../views/auth/RegisterView.vue'),
      meta: { requiresGuest: true }
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('../views/dashboard/DashboardView.vue'),
      meta: { requiresAuth: true }
    },
    {
      path: '/admin',
      name: 'admin',
      component: () => import('../views/dashboard/AdminDashboard.vue'),
      meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue'),
    },
  ],
})

// Guards de navegación
router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  
  // Inicializar autenticación si no está inicializada
  if (!authStore.user && !authStore.token) {
    authStore.initializeAuth()
  }
  
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth)
  const requiresGuest = to.matched.some(record => record.meta.requiresGuest)
  const requiresAdmin = to.matched.some(record => record.meta.requiresAdmin)
  
  if (requiresAuth && !authStore.isAuthenticated) {
    // Redirigir al login si requiere autenticación
    next('/login')
  } else if (requiresGuest && authStore.isAuthenticated) {
    // Redirigir al dashboard si ya está autenticado
    next('/dashboard')
  } else if (requiresAdmin && !authStore.isAdmin) {
    // Redirigir al dashboard normal si no es admin
    next('/dashboard')
  } else {
    next()
  }
})

export default router
