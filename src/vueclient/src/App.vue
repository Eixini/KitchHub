<template>
  <div id="app">
    <p>
      <router-link to="/">Home</router-link>
      <router-link to="/enteringingredients">Recipe Search</router-link>
    </p>

    <router-view></router-view>
  </div>
</template>

<script>

export default {
  name: 'App',
  components: {
  }
}
</script>