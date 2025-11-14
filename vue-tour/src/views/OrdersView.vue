<template>
    <!-- Забронированные туры -->
    <section class="popular-routes">
        <div class="container">
            <h3>Ваши забронированные туры</h3>
            <div class="routes-grid">
                <div v-for="tour in userTours" :key="tour.idTour">             
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
                        :isReserTour="true"
                        :orderId="tour.orderId"
                        :statusName="tour.statusName"
                    />
                </div>
            </div>
            <div v-if="userTours.length === 0" class="no-results">
                <p>У вас нет забраннированных туров</p>
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
      currentStatuses: [],
      statuses: [],
    }
  },
  computed: {
    minDate() {
        return new Date().toISOString().split('T')[0];
    },

    userTours() {
        const user = JSON.parse(localStorage.getItem('user'));  
        const userOrders = this.orders.filter(order => order.userId === user.idUser);

        return userOrders
          .map(order => {
            const tour = this.tours.find(t => t.idTour === order.tourId);

            const currentStatus = this.currentStatuses.find(u => u.orderId === order.idOrder);

            const status = this.statuses.find(s => s.idStatus === currentStatus.statusId);
            
            return tour ? { ...tour, orderId: order.idOrder, statusName: status.nameStatus } : null;
          })
          .filter(Boolean);
    }
    },
  async mounted() {
    await this.fetchAllTypeTours();
    await this.fetchAllTours();
    await this.fetchAllOrders();
    await this.fetchAllStatus();
    await this.fetchAllCurrentStatus();
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

    async fetchAllCurrentStatus() {
      try {
        const response = await fetch("http://localhost:5125/api/CurrentStatus/GetAllCurrentStatus", {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
          }
        });

        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(errorText);
        }

        const currentStatusesData = await response.json();
        this.currentStatuses = currentStatusesData;
        console.log(currentStatusesData)
      } catch (error) {
        console.error('Ошибка загрузки текущих статусов:', error);
        alert(`Ошибка: ${error.message}`);
      }
    },

    async fetchAllStatus() {
      try {
        const response = await fetch("http://localhost:5125/api/Status/GetAllStatus", {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
          }
        });

        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(errorText);
        }

        const statusesData = await response.json();
        this.statuses = statusesData;
        console.log(statusesData)
      } catch (error) {
        console.error('Ошибка загрузки статусов:', error);
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