<script>
export default {
    props: {
        idTour: {
            type: String,
            required: true,
        },
        title: {
            type: String,
            required: true,
        },
        fromCity: {
            type: String,
            required: true,
        },
        whereCity: {
            type: String,
            required: true,
        },
        idTourType: {
            type: String,
            required: true,
        },
        description: {
            type: String,
            required: true,
        },
        dataStart: {
            type: String,
            required: true,
        },
        dataEnd: {
            type: String,
            required: true,
        },
        price: {
            type: String,
            required: true,
        },
        countPeople: {
            type: String,
            required: true,
        },
        image: {
            type: String,
            required: true,
        },
        typeTourStr: {
            type: String,
            required: true,
        },
        orderList: {
            type: Array,
            required: true,
        },
        isReserTour: {
            type: Boolean,
            required: true,
        },
        orderId: {
            type: Number,
            required: false,
        },
        statusName: {
            type: String,
            required: false,
        },
    },
    data() {
        return {
            isAuthenticated: false,

            showConfirmation: false,
            showCancelConfirmation: false,
            showDetailsModal: false,

            ordersCountMest: 0,

            order: {
                idOrder: 0,
                userId: 0,
                tourId: this.idTour,
                bookingDatetime: new Date().toISOString(),
                totalCost: 0,
                countMest: 0,
            },
        }
    },
   computed: {
        isNoSeatsAvailable() {
            return this.order.countMest > (parseInt(this.countPeople) - this.ordersCountMest);
        },
        formattedDates() {
            const start = new Date(this.dataStart);
            const end = new Date(this.dataEnd);
            
            // Если год одинаковый - показываем год только в конце с "г."
            if (start.getFullYear() === end.getFullYear()) {
                const year = start.getFullYear();
                const options = { day: 'numeric', month: 'long' };
                const startStr = start.toLocaleDateString('ru-RU', options);
                const endStr = end.toLocaleDateString('ru-RU', options);
                return `${startStr} - ${endStr} ${year} г.`;
            } else {
                // Если годы разные - показываем оба года с "г."
                const startOptions = { day: 'numeric', month: 'long', year: 'numeric' };
                const endOptions = { day: 'numeric', month: 'long', year: 'numeric' };
                const startStr = start.toLocaleDateString('ru-RU', startOptions);
                const endStr = end.toLocaleDateString('ru-RU', endOptions);
                
                // Добавляем "г." к каждой дате
                return `${startStr} г. - ${endStr} г.`;
            }
        },
        totalPrice() {
            return this.price * this.order.countMest;
        },
        // Форматированная цена для отображения, тип 47 пробел 000
        formattedTotalPrice() {
            return this.totalPrice.toLocaleString('ru-RU');
        }

    },
    mounted() {
        this.checkAuthStatus();
        this.order.countMest = 1;
    },
    methods: {
        checkAuthStatus() {
            const userStr = localStorage.getItem('user');
            this.isAuthenticated = !!userStr;
        },
        getImageUrl(name) {
            return new URL(`./res/${name}`, import.meta.url).href;
        },
        getOrderCountMest() {        
            this.ordersCountMest = this.orderList
                .filter(order => order.tourId === this.idTour)
                .reduce((sum, order) => sum + order.countMest, 0); 
            
            console.log(this.ordersCountMest);
        },
        showTourDetails() {
            this.showDetailsModal = true;
            this.order.countMest = 1;

            this.getOrderCountMest();
        },
        closeTourDetails() {
            this.showDetailsModal = false;
        },

        increasePeople() {
            if (this.order.countMest > parseInt(this.countPeople - this.ordersCountMest)) {
                alert("К сожелению больше мест нет!");
                return;
            }   
            if(this.order.countMest > 5){
                alert("Максимум 6 человек!");
                return;
            }

            if (this.order.countMest < parseInt(this.countPeople - this.ordersCountMest)) {
                this.order.countMest++;
            }else{
                alert("К сожелению больше мест нет!");
            }
        },
        decreasePeople() {
            if (this.order.countMest > 1) {
                this.order.countMest--;
            }
        },

        reserveCar() {
            this.showConfirmation = true;
        },
        
        async confirmOrder() { 
            const user = JSON.parse(localStorage.getItem('user'));  
            this.order.userId = user.idUser;
            this.order.totalCost = this.totalPrice;
            this.order.countMest = this.order.countMest;
            const content = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                },
                body: JSON.stringify(this.order)
            };
            
            try {   
                const response = await fetch("http://localhost:5125/api/Order/AddOrder", content);                

                if (response.ok) {
                    const result = await response.text();
                    alert(result);
                    this.showConfirmation = false;
                    this.showDetailsModal = false;
                    location.href = '/';
                    return true;
                } else {
                    const errorText = await response.text();
                    alert(`Ошибка бронирования: ${errorText}`);
                    this.showConfirmation = false;
                    return false;
                }
            }
            catch(error) {
                alert('Ошибка сети: ' + error)
            }
        },
        cancelOrder() {
            alert("Бронирование не удалось!");
            this.showConfirmation = false;
        },
        async confirmCanelOrder() {
            
            const content = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                }
            };
            try {   
                const response = await fetch(`http://localhost:5125/api/Order/DeleteOrder/${this.orderId}`, content);                

                if (response.ok) {
                    const result = await response.text();
                    alert(result);
                    location.href = '/order';
                    this.showCancelConfirmation = false;
                    return true;
                } else {
                    const errorText = await response.text();
                    alert(`Ошибка отмены бронирования: ${errorText}`);
                    this.showCancelConfirmation = false;
                    return false;
                }
            }
            catch(error) {
                alert('Ошибка сети: ' + error)
            }
        },
        cancelCancelOrder() {
            alert("Отменить бронирование не удалось!");
            this.showCancelConfirmation = false;
        },
        cancelReserveCar() {
            this.showCancelConfirmation = true;
        },
    }
}
</script>

<template>
    <div class="route-card">
        <img :src="getImageUrl(image)" alt="Москва - Санкт-Петербург" class="route-image">
        <div class="route-details">
            <h3 class="route-title">{{ title }}</h3>
            <div class="route-specs">
                <div class="spec-column">
                    <span class="spec-name">Откуда:</span>
                    <span class="spec-value">{{fromCity}}</span>

                     <span class="spec-name">Куда:</span>
                    <span class="spec-value">{{whereCity}}</span>
                </div>
                <div class="spec-column">
                    <span class="spec-name">Стоимость:</span>
                    <span class="spec-value">от {{formattedTotalPrice}} ₽</span>
                </div>
                <div class="spec-column">
                    <span class="spec-name">Даты тура:</span>
                    <span class="spec-value">{{formattedDates}}</span>
                </div>
                <div class="spec-column">
                    <span class="spec-name">Тип тура:</span>
                    <span class="spec-value">{{typeTourStr}}</span>
                </div>
                <div class="spec-column" v-if="statusName">
                    <span class="spec-name">Статус заказа:</span>
                    <span class="spec-value">{{statusName}}</span>
                </div>
            </div>
            <button class="btn-details" @click="showTourDetails" v-if="!isReserTour">Подробнее</button>
            <button class="btn-details" @click="cancelReserveCar" v-if="isReserTour && statusName !== 'Отменен' 
                        && statusName !== 'Завершен' && statusName !== 'Подтвержден' ">Отменить бронь</button>
        </div>
    </div>

    <!-- Модальное окно с описанием тура -->
    <div v-if="showDetailsModal" class="details-modal">
        <div class="modal-content">
            <div class="modal-header">
                <h2>{{ title }}</h2>
                <button class="close-btn" @click="closeTourDetails">×</button>
            </div>
            
            <div class="modal-body">
                <div class="tour-image-container">
                    <img :src="getImageUrl(image)" :alt="title" class="tour-modal-image">
                </div>
                
                <div class="tour-info">
                    <div class="info-grid">
                        <div class="info-item">
                            <span class="info-label">Маршрут:</span>
                            <span class="info-value">{{ fromCity }} → {{ whereCity }}</span>
                        </div>
                        <div class="info-item">
                            <span class="info-label">Даты:</span>
                            <span class="info-value">{{ formattedDates }}</span>
                        </div>
                        <div class="info-item">
                            <span class="info-label">Тип тура:</span>
                            <span class="info-value">{{ typeTourStr }}</span>
                        </div>
                        <div class="info-item">
                            <span class="info-label">Участников:</span>
                            <span class="info-value">до {{ countPeople }} человек</span>
                        </div>
                    </div>
                    
                    <!-- Блок выбора количества людей -->
                    <div class="people-selector">
                        <h3>Выберите количество участников</h3>
                        <div class="selector-controls">
                            <button class="selector-btn" 
                                @click="decreasePeople"
                                :disabled="order.countMest <= 1"
                            >−</button>
                            
                            <span class="people-count">{{ order.countMest }} человек</span>
                            
                            <button class="selector-btn" 
                                @click="increasePeople"
                                :disabled="order.countMest >= parseInt(countPeople)"
                            >+</button>
                        </div>
                    </div>
                    
                    <!-- Блок с динамической ценой -->
                    <div class="price-calculation">
                        <div class="price-breakdown">
                            <div class="price-item">
                                <span>Цена за 1 человека:</span>
                                <span>{{ formattedTotalPrice }} ₽</span>
                            </div>
                            <div class="price-item">
                                <span>Количество:</span>
                                <span>{{ order.countMest }} ×</span>
                            </div>
                            <div class="price-divider"></div>
                            <div class="price-total">
                                <span>Итого:</span>
                                <span class="total-amount">{{ formattedTotalPrice }} ₽</span>
                            </div>
                        </div>
                    </div>
                    
                    <div class="description-section">
                        <h3>Описание тура</h3>
                        <p class="tour-description">{{ description }}</p>
                    </div>
                </div>
            </div>
            
            <div class="modal-footer">
                <button :class="isNoSeatsAvailable ? 'btn-close' : 'btn-reserve'"
                        @click="isNoSeatsAvailable ? null : reserveCar()"
                        :disabled="isNoSeatsAvailable">
                        {{ isNoSeatsAvailable ? 'Мест нет' : `Забронировать за ${formattedTotalPrice} ₽` }}
                </button>
                <button class="btn-close" @click="closeTourDetails">Закрыть</button>
            </div>
        </div>
    </div>

    <!-- Модальное окно подтверждения бронирования -->
    <div v-if="showConfirmation" class="confirmation-modal">
        <div class="modal-content">
            <h3>Подтверждение бронирования</h3>
            <div class="confirmation-details">
                <p><strong>Тур:</strong> {{ title }}</p>
                <p><strong>Участников:</strong> {{ order.countMest }} человек</p>
                <p><strong>Общая стоимость:</strong> {{ formattedTotalPrice }} ₽</p>
                <p><strong>Даты:</strong> {{ formattedDates }}</p>
            </div>
            <div class="modal-buttons">
                <button class="btn-confirm" @click="confirmOrder">Подтвердить</button>
                <button class="btn-cancel" @click="cancelOrder">Отмена</button>
            </div>
        </div>
    </div>

    <!-- Модальное окно с отменой бронирования -->
    <div v-if="showCancelConfirmation" class="confirmation-modal">
        <div class="modal-content">
            <h3>Отмена бронирования</h3>
            <p>Вы уверены, что хотите отменить бронирование <strong>{{ title }}</strong>?</p>
            <div class="modal-buttons">
                <button class="btn-confirm" @click="confirmCanelOrder">Да</button>
                <button class="btn-cancel" @click="cancelCancelOrder">Нет</button>
            </div>
        </div>
    </div>
</template>

<style scoped>
/* Стили для модального окна с описанием */
.details-modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.7);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
}

.details-modal .modal-content {
    background: white;
    border-radius: 12px;
    max-width: 600px;
    width: 90%;
    max-height: 90vh;
    overflow-y: auto;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
}

.modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 20px;
    border-bottom: 1px solid #eee;
    background: linear-gradient(135deg, #7D71FF, #5a4be3);
    color: white;
    border-radius: 12px 12px 0 0;
}

.modal-header h2 {
    margin: 0;
    font-size: 1.5em;
}

.close-btn {
    background: none;
    border: none;
    font-size: 24px;
    color: white;
    cursor: pointer;
    padding: 0;
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.close-btn:hover {
    background: rgba(255, 255, 255, 0.2);
    border-radius: 50%;
}

.modal-body {
    padding: 20px;
}

.tour-image-container {
    margin-bottom: 20px;
}

.tour-modal-image {
    width: 100%;
    height: 200px;
    object-fit: cover;
    border-radius: 8px;
}

.info-grid {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 15px;
    margin-bottom: 20px;
}

.info-item {
    display: flex;
    flex-direction: column;
}

.info-label {
    font-weight: bold;
    color: #666;
    font-size: 0.9em;
    margin-bottom: 4px;
}

.info-value {
    color: #333;
}

.people-selector {
    background: #f8f9fa;
    padding: 20px;
    border-radius: 8px;
    margin-bottom: 20px;
}

.people-selector h3 {
    margin-bottom: 15px;
    color: #333;
    text-align: center;
}

.selector-controls {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 20px;
}

.selector-btn {
    width: 40px;
    height: 40px;
    border: 2px solid #7D71FF;
    background: white;
    border-radius: 50%;
    font-size: 20px;
    font-weight: bold;
    color: #7D71FF;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
}

.selector-btn:hover:not(:disabled) {
    background: #7D71FF;
    color: white;
}

.selector-btn:disabled {
    border-color: #ccc;
    color: #ccc;
    cursor: not-allowed;
}

.people-count {
    font-size: 1.2em;
    font-weight: bold;
    color: #333;
    min-width: 120px;
    text-align: center;
}

/* Стили для расчета цены */
.price-calculation {
    background: #f8f9fa;
    padding: 20px;
    border-radius: 8px;
    margin-bottom: 20px;
}

.price-breakdown {
    max-width: 300px;
    margin: 0 auto;
}

.price-item {
    display: flex;
    justify-content: space-between;
    margin-bottom: 10px;
    color: #555;
}

.price-divider {
    border-top: 1px dashed #ccc;
    margin: 10px 0;
}

.price-total {
    display: flex;
    justify-content: space-between;
    font-size: 1.2em;
    font-weight: bold;
    color: #333;
}

.total-amount {
    color: #7D71FF;
    font-size: 1.3em;
}

.description-section h3 {
    margin-bottom: 10px;
    color: #333;
}

.tour-description {
    line-height: 1.6;
    color: #555;
    text-align: justify;
}

.modal-footer {
    padding: 20px;
    border-top: 1px solid #eee;
    display: flex;
    gap: 10px;
    justify-content: flex-end;
}

.btn-reserve {
    background: linear-gradient(135deg, #7D71FF, #5a4be3);
    color: white;
    border: none;
    padding: 12px 24px;
    border-radius: 6px;
    cursor: pointer;
    font-weight: bold;
    font-size: 1.1em;
}

.btn-reserve:hover {
    background: linear-gradient(135deg, #6a5feb, #4a3bd3);
}

.btn-close {
    background: #6c757d;
    color: white;
    border: none;
    padding: 12px 24px;
    border-radius: 6px;
    cursor: pointer;
}

.btn-close:hover {
    background: #5a6268;
}

/* Стили для окна подтверждения */
.confirmation-details {
    background: #f8f9fa;
    padding: 15px;
    border-radius: 6px;
    margin: 15px 0;
    text-align: left;
}

.confirmation-details p {
    margin: 8px 0;
}

/* Адаптивность */
@media (max-width: 768px) {
    .details-modal .modal-content {
        width: 95%;
        margin: 20px;
    }
    
    .info-grid {
        grid-template-columns: 1fr;
    }
    
    .modal-footer {
        flex-direction: column;
    }
    
    .btn-reserve, .btn-close {
        width: 100%;
    }
    
    .selector-controls {
        gap: 15px;
    }
    
    .people-count {
        min-width: 100px;
        font-size: 1.1em;
    }
}

.confirmation-modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
}

.modal-content {
    background: white;
    padding: 20px;
    border-radius: 8px;
    text-align: center;
    max-width: 400px;
    width: 90%;
}

.modal-buttons {
    margin-top: 20px;
    display: flex;
    gap: 10px;
    justify-content: center;
}

.btn-confirm {
    background-color: #4CAF50;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    cursor: pointer;
}

.btn-cancel {
    background-color: #f44336;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    cursor: pointer;
}

.btn-confirm:hover {
    background-color: #45a049;
}

.btn-cancel:hover {
    background-color: #da190b;
}
</style>