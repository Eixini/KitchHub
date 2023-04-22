<template>
  <div id="app">
    <div class="header">
      <div class="container">
        <div class="nav">
          KitchHub
        </div>
        <div class="navbar">

          <div>
            <router-link to="/">Home</router-link>
          </div>

          <div>
            <router-link to="/enteringingredients">Recipe Search</router-link>
          </div>

          <div v-if="currentUser">
            <router-link to="/createrecipe">Create Recipe</router-link>
          </div>

          <div v-if="currentUser && (currentUser.role == 2 || currentUser.role == 3)">
            <router-link to="/moder">Moderation</router-link>
          </div>

          <div v-if="currentUser && currentUser.role == 3">
            <router-link to="/admin">Administration</router-link>
          </div>

          <div v-if="!currentUser">
            <li>
              <router-link to="/login"> Login </router-link>
            </li>
          </div>

          <div v-if="!currentUser">
            <li>
              <router-link to="/register"> Reg </router-link>
            </li>
          </div>
          
          <div v-if="currentUser">
            <li>
              <router-link to="/profile"> {{ currentUser.email }} </router-link>
            </li>
            <li>
              <a @click.prevent="logOut"> LogOut </a>
            </li>
          </div>
        </div>
      </div>
    </div>
    <div class="main">
        <div class="content-center">
          <router-view></router-view>
        </div>
    </div>
    
  </div>
</template>

<script scoped>

export default {
  name: 'App',
  components: {
  },

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

<style>

  #app {
    margin: 0;
    box-sizing: border-box;
  }

  .container{
    max-width: 800px;
    padding: 10px;
    margin: 0 auto;
  }

  .content-center {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .header {
    background-color: black;
    margin: 0;
    padding: 0%;
  }

  .header .container {
    display: flex;
    justify-content: space-between;
  }

  .navbar {
    display: flex;
  }

  .navbar div {
    margin-right: 10px;
  }

  a {
    /* Стили для router-link */
    color: white;
    text-decoration: none;
  }

  .nav {
    color: white
  }

  .navbar div:last-child {
    margin-right: 0;
  }

</style>