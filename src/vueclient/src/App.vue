<template>
  <EnteringIngredients></EnteringIngredients>
</template>

<script>
import EnteringIngredients from './components/EnteringIngredients.vue'

export default {
  name: 'App',
  components: {
    EnteringIngredients
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
