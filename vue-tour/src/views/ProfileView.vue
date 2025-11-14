<script>
import { ref } from "vue"
export default {
    data() {
        return {
            user: ref(""),
        }
    },
    mounted() {
        this.checkAuthStatus();
    },
    methods: {        
        checkAuthStatus() {
            const jsonUser = localStorage.getItem('user');
            console.log('Данные из localStorage:', jsonUser);
            
            if (jsonUser) {
                try {
                    this.user = JSON.parse(jsonUser);
                    console.log('Пользователь загружен:', this.user);
                } catch (error) {
                    console.error('Ошибка парсинга JSON:', error);
                    this.user = null;
                }
            } else {
                console.log('Пользователь не найден в localStorage');
                this.user = null;
            }
        },
        
        logout() {
            localStorage.removeItem('user');
            this.user = null;
            alert(`Пользователь вышел из системы!`);
            location.href = '/';
        }
    }
}
</script>
<template>
    <body>           
        <div class="wrapper">
            <div class="card-switch">                               
                <div class="flip-card__inner">
                    <div class="flip-card__front">
                        <div class="title">Профиль</div>
                        <form class="flip-card__form" @submit.prevent="logout">
                            <input class="flip-card__input" 
                                    name="Email" 
                                    placeholder="Почта" 
                                    v-model="user.email" 
                                    type="text" 
                                    readonly
                                    required>
                            <input class="flip-card__input" 
                                    name="password" 
                                    placeholder="Пароль" 
                                    v-model="user.password" 
                                    type="password"
                                    readonly 
                                    required>
                            <input class="flip-card__input" 
                                    name="Telephone" 
                                    placeholder="Номер телефона" 
                                    v-model="user.phoneNumber" 
                                    type="tel" 
                                    readonly
                                    required>
                            <button class="flip-card__btn">Выйти</button>
                        </form>
                    </div>
                </div>
            </div>   
        </div>
    </body>
</template>

<style scoped>

body {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 85vh;
    padding: 20px;
}

.wrapper {
    width: 100%;
    max-width: 400px;
    perspective: 1000px;
}

.card-switch {
    position: relative;
    width: 100%;
    height: 500px;
}

.flip-card__inner {
    position: relative;
    width: 100%;
    height: 100%;
    text-align: center;
    transition: transform 0.8s;
    transform-style: preserve-3d;
}

.flip-card__front {
    position: absolute;
    width: 100%;
    height: 100%;
    backface-visibility: hidden;
    -webkit-backface-visibility: hidden;
    border-radius: 15px;
    padding: 30px;
    background: white;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
    backdrop-filter: blur(8px);
}

.title {
    margin-bottom: 30px;
    font-size: 28px;
    color: #333;
    font-weight: 700;
}

.flip-card__form {
    display: flex;
    flex-direction: column;
    gap: 20px;
}

.flip-card__input {
    padding: 15px;
    border: 2px solid #ddd;
    border-radius: 8px;
    font-size: 16px;
    transition: all 0.3s ease;
}

.flip-card__input:focus {
    border-color: #7D71FF;
    outline: none;
    box-shadow: 0 0 0 2px rgba(37, 117, 252, 0.2);
}

.flip-card__btn {
    padding: 15px;
    background: #d50a0a;
    color: white;
    border: none;
    border-radius: 8px;
    font-size: 18px;
    font-weight: 600;
    cursor: pointer;
    transition: all 0.3s ease;
    margin-top: 10px;
}

.flip-card__btn:hover {
    background: #ff0000;
    transform: translateY(-2px);
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
}

.flip-card__btn:active {
    transform: translateY(0);
}

.divider {
    display: flex;
    align-items: center;
    margin: 20px 0;
    color: #777;
}

.divider::before, .divider::after {
    content: "";
    flex: 1;
    height: 1px;
    background: #ddd;
}

.divider span {
    padding: 0 15px;
}

.forgot-password {
    margin-top: 15px;
    color: #767676;
    text-decoration: none;
    font-size: 14px;
    margin-left: auto;
}

@media (max-width: 480px) {
    .flip-card__front {
        padding: 20px;
    }
    
    .title {
        font-size: 24px;
        margin-bottom: 20px;
    }
}
</style>