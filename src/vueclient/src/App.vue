<template>
  <div id="app">
    <div>
      <router-link to="/">Home</router-link>
      <router-link to="/enteringingredients">Recipe Search</router-link>
    </div>
    <div class="content">
      <router-view></router-view>
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
    width: 100%;
  }

  .content{
    max-width: 800px;
    margin: 0 auto;
  }
</style>