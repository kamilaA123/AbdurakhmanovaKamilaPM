<template>
    <div class="contentAll"> 

        <div class="wrapper">
            <div class="card-switch">
                <input type="checkbox" id="toggle">
                
                <div class="toggle-container">
                    <label for="toggle" class="toggle-label active">Войти</label>
                    <label for="toggle" class="toggle-label">Зарегистрироваться</label>
                </div>

                <br>
                
                <div class="flip-card__inner">
                    <div class="flip-card__front">
                        <div class="title">Вход</div>
                        <form class="flip-card__form" @submit.prevent="Auth">
                            <input class="flip-card__input" 
                                    name="Email" 
                                    placeholder="Почта" 
                                    type="text" 
                                    v-model="LoginRequest.email"
                                    required>

                            <input class="flip-card__input" 
                                    name="Password" 
                                    placeholder="Пароль" 
                                    type="password" 
                                    v-model="LoginRequest.password"
                                    required>

                            <a href="#" class="forgot-password">Забыли пароль?</a>
                            <button type="submit" name="SignIn" class="flip-card__btn">Войти</button>
                        </form>
                    </div>
                    
                    <div class="flip-card__back">
                        <div class="title">Регистрация</div>
                        <form class="flip-card__form" @submit.prevent="Registr">
                            <input class="flip-card__input" 
                                    name="firstName" 
                                    placeholder="Имя" 
                                    type="text"
                                    v-model="user.firstName"
                                    required>
                            <input class="flip-card__input" 
                                    name="lastName" 
                                    placeholder="Фамилия" 
                                    type="text"
                                    v-model="user.lastName"
                                    required>
                            <input class="flip-card__input" 
                                    name="patronymic" 
                                    placeholder="Отчество (Необязательно)" 
                                    type="text"
                                    v-model="user.patronymic">

                            <input class="flip-card__input" 
                                    name="Email" 
                                    placeholder="Почта" 
                                    type="text"
                                    v-model="user.email"
                                    required>
                            <input class="flip-card__input" 
                                    name="phoneNumber" 
                                    placeholder="Номер телефона" 
                                    type="tel" 
                                    v-model="user.phoneNumber"
                                    required>
                            <input class="flip-card__input" 
                                    name="password" 
                                    placeholder="Пароль" 
                                    type="password" 
                                    v-model="user.password" 
                                    required>
                            <input class="flip-card__input" 
                                    name="confirm-password" 
                                    placeholder="Подтвердить пароль"
                                    type="password" 
                                    v-model="confirmPassword"
                                    required>
                                    
                            <button type="submit" name="SignUp" class="flip-card__btn">Зарегистрироваться</button>
                        </form>

                    </div>
                </div>
            </div>   
        </div>

    </div>
</template>

<script>
export default {
    data() {
        return {
            isLoginForm: true,
            confirmPassword: '',
            user: {
                id: 0,
                firstName: "",
                lastName: "",
                patronymic: "",
                email: "",
                password: '',
                phoneNumber: '',
                idRole: 1,
            },
            LoginRequest: {
                email: "",
                password: ''
            }
        }
    },
    methods: {
        async fetchAllUsers() {
            const content = {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                }
            };

            const response = await fetch("http://localhost:5125/api/User/allUsers", content);
            const responseString = await response.text();

            if (response.ok) {
                const users = await response.json();
                return users;
            } else {
                alert(`Ошибка: ${responseString}`);
                return false;
            }
        },
        displayUsers() {
            console.log('Загрузка пользователей...');
            this.fetchAllUsers()
        },
        submitHandler() {            
            console.group('Form Data')
            console.log('email:', this.user.email)
            console.log('password:', this.user.password)
            console.log('telephone:', this.user.telephone)
            console.log('confirmPassword:', this.confirmPassword)
            console.groupEnd()            
        },
      
        async Auth() {
            const content = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                },
                body: JSON.stringify(this.LoginRequest)
            };
            try {   
                const response = await fetch("http://localhost:5125/api/User/Auth", content);                

                if (response.ok) {
                    const result = await response.text();
                    alert('Авторизация успешна!');
                    localStorage.setItem('user', result);
                    location.href = '/';
                    return true;
                } else {
                    const errorText = await response.text();
                    alert(`Ошибка авторизация: ${errorText}`);
                    return false;
                }
            }
            catch(error) {
                alert('Ошибка сети: ' + error)
            }
        },

        async Registr() {
            if (this.user.password !== this.confirmPassword) {
                    alert('Пароли не совпадают!');
                    return false;
            }
            const content = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                },
                body: JSON.stringify(this.user)
            };
            try {               

                const response = await fetch("http://localhost:5125/api/User/Registration", content);                

                if (response.ok) {
                    const result = await response.text();
                    alert('Регистрация успешна!');
                    this.switchToLogin();
                    return true;
                } else {
                    const errorText = await response.text();
                    alert(`Ошибка регистрации: ${errorText}`);
                    return false;
                }
            }
            catch(error) {
                alert('Ошибка сети: ' + error)
            }            
        },
        switchToLogin() {
            this.isLoginForm = true;
            const toggle = document.getElementById('toggle');
            if (toggle) {
                toggle.checked = false;
            }
        }
    }
}
</script>

<style scoped>

.contentAll {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 120vh;
    padding: 20px;
}

.wrapper {
    padding: 60px;
    width: 100%;
    max-width: 550px;
    perspective: 500px;
}

.card-switch {
    position: relative;
    width: 100%;
    height: 500px;
}

.toggle-container {
    position: absolute;
    top: -90px;
    left: 50%;
    transform: translateX(-50%);
    display: flex;
    background: white;
    border-radius: 30px;
    padding: 5px;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
    z-index: 10;
}

.toggle-label {
    padding: 10px 20px;
    cursor: pointer;
    border-radius: 25px;
    font-weight: 600;
    transition: all 0.3s ease;
    text-align: center;
    width: 180px;
}

.toggle-label.active {
    background: #7D71FF;
    color: white;
}

#toggle {
    display: none;
}

#toggle:checked ~ .toggle-container .toggle-label:nth-child(1) {
    background: transparent;
    color: #333;
}

#toggle:checked ~ .toggle-container .toggle-label:nth-child(2) {
    background: #7D71FF;
    color: white;
}

#toggle:checked ~ .flip-card__inner .flip-card__back,
#toggle:checked ~ .card-switch {
    height: 750px;
}


.flip-card__inner {
    position: relative;
    width: 100%;
    height: 100%;
    text-align: center;
    transition: transform 0.8s;
    transform-style: preserve-3d;
}

#toggle:checked ~ .flip-card__inner {
    transform: rotateY(180deg);
}

.flip-card__front, .flip-card__back {
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

.flip-card__back {
    transform: rotateY(180deg);
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
    background: #7D71FF;
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
    background: #7D71FF;
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

.forgot-password:hover {
    color: #2575fc;
    text-decoration: underline;
}

@media (max-width: 480px) {
    .flip-card__front, .flip-card__back {
        padding: 20px;
    }
    
    .title {
        font-size: 24px;
        margin-bottom: 20px;
    }
}
</style>