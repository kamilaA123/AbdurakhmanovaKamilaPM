<script>
  import { RouterLink, RouterView } from 'vue-router'
  import Footer from '@/components/Footer.vue';
  export default {
    components: {
      Footer,
    },
    data() {
      return {
        isAuthenticated: false
      }
    },
    computed: {
      headerHeight() {
        return this.$route.name === 'auth' || this.$route.name === 'profile' ? '160px' : '300px';
      }
    },
    mounted() {
      this.checkAuthStatus();
    },
    methods: {
      checkAuthStatus() {
        const user = localStorage.getItem('user');
        this.isAuthenticated = !!user;
      }
    }
  }

  document.addEventListener('DOMContentLoaded', function() {
      const navButtons = document.querySelectorAll('.nav-btn');
      
      navButtons.forEach(button => {
          button.addEventListener('click', function() {
              // Убираем активный класс у всех кнопок
              navButtons.forEach(btn => btn.classList.remove('active'));
              // Добавляем активный класс к нажатой кнопке
              this.classList.add('active');
          });
      });
  });
</script>

<template>
  <section class="header-hero" :style="{ height: headerHeight }">
      <div class="container">
          <div class="header-content">
            <div class="logo">
              <h1><span class="free">Free</span><span class="choice">Choice</span></h1>
              <h2>Туры по всей России</h2>
              <p>По ценам туроператоров или ниже</p>
            </div>
            <nav class="nav">
              <RouterLink to="/" class="nav-btn active">Каталог туров</RouterLink>
              <RouterLink to="/auth" v-if="!isAuthenticated" class="nav-btn">Войти</RouterLink>
              <RouterLink to="/profile" v-if="isAuthenticated" class="nav-btn">Профиль</RouterLink>
              <RouterLink to="/order" v-if="isAuthenticated" class="nav-btn">Мои туры</RouterLink>
            </nav>
          </div>
          
          <div class="hero-content" v-if="$route.path === '/'">
              <h2>Найдите свой идеальный маршрут</h2>
          </div>
      </div>
  </section>
  <RouterView />
  <Footer/>
</template>
