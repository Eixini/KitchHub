<template>

    <div class="container">
        <div class="navbar-logo">
            <router-link to="/">KitchHub</router-link>
        </div>

        <div class="navbar">
    
            <div class="navbar-element">
                <router-link to="/enteringingredients">Найти рецепты</router-link>
            </div>

            <div class="navbar-element" v-if="currentUser">
                <router-link to="/createrecipe">Создать рецепт</router-link>
            </div>

            <div class="navbar-element" v-if="currentUser && (currentUser.role == 2 || currentUser.role == 3)">
                <router-link to="/moder">Модерация</router-link>
            </div>

            <div class="navbar-element" v-if="currentUser && currentUser.role == 3">
                <router-link to="/admin">Администрирование</router-link>
            </div>
        
            <div class="navbar-element">
                <router-link to="/login" v-if="!currentUser"> Вход </router-link>
            </div>

            <div class="navbar-element">
                <router-link to="/register" v-if="!currentUser"> Регистрация </router-link>
            </div>
            
            <div class="navbar-element" v-if="currentUser">
                <router-link to="/profile"> {{ currentUser.email }}</router-link>
            </div>

            <div class="navbar-element" v-if="currentUser">
                <a @click.prevent="logOut"> Выйти </a>
            </div>

        </div>
    </div>
    
</template>

<script lang="js">
    export default {
        computed: {
            currentUser() {
                return this.$store.state.auth.user;
            }
        },

        methods: {
            logOut() {
                this.$store.dispatch('auth/logout');
                this.$router.push('/login');
            }
        }
    };
</script>

<style scoped>

.container {
    background-color: #35BFA2;
    display: flex;
    height: 2.5em;
    justify-content: space-between;
    padding: 0em;
    margin: 0em;
}

.navbar-logo {
    margin-left: 1em;
    margin-right: 1em;
    padding: 0.5em;

    z-index: 99;
}

.navbar-logo:hover {
    background: #86DFCB;
    font-size: 16pt;
    transition: 0.3s;
}

.navbar {
    display: inline-flex;
}

.navbar-element {
    margin-left: 0.01em;
    margin-right: 0.01em;
    padding: 0.5em;

    z-index: 99;
}

.navbar-element:hover {
    background: #86DFCB;
    font-size: 17pt;
    transition: 0.3s;
}

li {
    padding: 0em;
    margin: 0em;
    list-style-type: none;
}

a {
    /* Стили для router-link */
    color: #263238;
    text-decoration: none;
}

</style>