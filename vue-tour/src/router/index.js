import { createRouter, createWebHistory } from 'vue-router'
import AuthRegView from '../views/AuthRegView.vue'
import KatalogView from '../views/KatalogView.vue'
import ProfileView from '../views/ProfileView.vue'
import OrdersView from '../views/OrdersView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'katalog',
      component: KatalogView,
    },
    {
      path: '/auth',
      name: 'auth',
      component: AuthRegView,
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfileView,
    },
    {
      path: '/order',
      name: 'order',
      component: OrdersView,
    },
  ],
})

export default router
