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
        </div>
      </div>
    </div>
    <div class="main">
      <div class="container">
        <div class="content">

        </div>
        <div class="content-center">
          <router-view></router-view>
        </div>
      </div>
    </div>
    
  </div>
</template>

<script>

export default {
  name: 'App',
  components: {
  }
}
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