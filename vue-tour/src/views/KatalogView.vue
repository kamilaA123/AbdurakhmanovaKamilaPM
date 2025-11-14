<template>
    <!-- Поисковая форма -->
    <section class="search-section" style="background: rgb(243, 240, 240);">
        <div class="container">
            <div class="hero-content">
              <div class="search-form">
                  <div class="input-group">
                      <label>Откуда</label>
                      <input type="text" 
                            placeholder="Город"
                            v-model="filters.fromCity">
                  </div>
                  <div class="input-group">
                      <label>Куда</label>
                      <input type="text" 
                            placeholder="Город"
                            v-model="filters.whereCity">
                  </div>
                  <div class="input-group">
                      <label>Дата</label>
                      <input type="date" 
                            v-model="filters.date"
                            :min="minDate">
                  </div>
                  <div class="input-group">
                      <label>Количество ночей</label>
                      <input type="number" 
                            v-model="filters.nights" 
                            min="2" 
                            max="21">
                  </div>
                  
                  <button class="search-btn" @click="applyFilters">Найти туры</button>
                  <button class="search-btn" @click="resetFilters">Сбросить</button>
              </div>
            </div>
        </div>
    </section>

    <!-- Популярные направления -->
    <section class="popular-routes">
        <div class="container">
            <h3>Доступные туры для бронирования</h3>
            <div class="routes-grid">
                <div v-for="tour in filteredTours" :key="tour.idTour">             
                    <TourItem 
                        :idTour="tour.idTour"
                        :title="tour.title" 
                        :fromCity="tour.fromCity" 
                        :whereCity="tour.whereCity" 
                        :idTourType="tour.idTourType" 
                        :description="tour.description" 
                        :dataStart="tour.dataStart" 
                        :dataEnd="tour.dataEnd" 
                        :price="tour.price" 
                        :countPeople="tour.countPeople" 
                        :image="tour.image" 
                        :typeTourStr="getTourTypeName(tour.idTourType)"
                        :orderList="orders" 
                        :isReserTour="false"
                    />
                </div>
            </div>
            <div v-if="filteredTours.length === 0" class="no-results">
                <p>По вашему запросу туров не найдено</p>
            </div>
        </div>
    </section>
</template>

<script>
import TourItem from '@/components/TourItem.vue';

export default {
  components: {
    TourItem,
  },
  data() {
    return {
      orders: [],
      tours: [],
      typeTours: [],
      filteredTours: [],
      filters: {
        fromCity: '',
        whereCity: '',
        date: '',
        nights: 2
      }
    }
  },
  computed: {
    minDate() {
      return new Date().toISOString().split('T')[0];
    }
  },
  async mounted() {
    await this.fetchAllTypeTours();
    await this.fetchAllTours();
    await this.fetchAllOrders();
    this.filteredTours = this.tours;
  },
  methods: {
    async fetchAllTours() {
      try {
        const response = await fetch("http://localhost:5125/api/Tour/GetAllTours", {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
          }
        });

        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(errorText);
        }

        const toursData = await response.json();
        this.tours = toursData;
        console.log(toursData)
      } catch (error) {
        console.error('Ошибка загрузки туров:', error);
        alert(`Ошибка: ${error.message}`);
      }
    },

    async fetchAllTypeTours() {
      try {
        const response = await fetch("http://localhost:5125/api/TypeTour/GetTypeTours", {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
          }
        });

        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(errorText);
        }

        const typeToursData = await response.json();
        this.typeTours = typeToursData;
        console.log(typeToursData)
      } catch (error) {
        console.error('Ошибка загрузки типов туров:', error);
        alert(`Ошибка: ${error.message}`);
      }
    },

    async fetchAllOrders() {
      try {
          const response = await fetch("http://localhost:5125/api/Order/GetAllOrder", {
          method: 'GET',
          headers: {
              'Content-Type': 'application/json; charset=utf-8',
          }
          });

          if (!response.ok) {
          const errorText = await response.text();
          throw new Error(errorText);
          }

          const ordersData = await response.json();
          this.orders = ordersData;
          console.log(ordersData)
      } catch (error) {
          console.error('Ошибка загрузки заказов:', error);
          alert(`Ошибка: ${error.message}`);
      }
    },

    getTourTypeName(idTourType) {
      const typeTour = this.typeTours.find(type => type.idTourTypes === idTourType);
      return typeTour ? typeTour.name : 'Тип не найден';
    },

    applyFilters() {
      this.filteredTours = this.tours.filter(tour => {
        // Фильтр по городу отправления
        if (this.filters.fromCity && 
            !tour.fromCity.toLowerCase().includes(this.filters.fromCity.toLowerCase())) {
          return false;
        } 

        // Фильтр по городу назначения
        if (this.filters.whereCity && 
            !tour.whereCity.toLowerCase().includes(this.filters.whereCity.toLowerCase())) {
          return false;
        }

        // Фильтр по дате
        if (this.filters.date) {
          const tourDate = new Date(tour.dataStart).toISOString().split('T')[0];
          if (tourDate < this.filters.date) {
            return false;
          }
        }

        // Фильтр по количеству ночей (примерная логика)
        if (this.filters.nights) {
          const startDate = new Date(tour.dataStart);
          const endDate = new Date(tour.dataEnd);
          const nights = Math.ceil((endDate - startDate) / (1000 * 60 * 60 * 24));

          if (nights < this.filters.nights) {
            return false;
          }
        }

        return true;
      });
    },

    resetFilters() {
      this.filters = {
        fromCity: '',
        whereCity: '',
        date: '',
        nights: 2
      };
      this.filteredTours = this.tours;
    }
  }
}
</script>

<style scoped>

.search-section {
  padding: 40px 0;
}

.no-results {
  text-align: center;
  padding: 40px;
  font-size: 18px;
  color: #6c757d;
}

</style>