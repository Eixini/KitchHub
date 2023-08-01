<template>
  <div id="app">
    <div class="header">
        <NavBar/>
    </div>
    <div class="main">
        <div class="content-center">
          <router-view></router-view>
        </div>
    </div>
    
  </div>
</template>

<script lang="js">

import NavBar from './components/partial/NavBar.vue'

export default {
  name: 'App',
  components: {
    NavBar
  },
};
</script>

<style>

  html {
    padding: 0em;
    margin: 0em;
  }

  body {
    padding: 0em;
    margin: 0em;
    font-family: 'Ubuntu';
    font-size: 14pt;
    background-color: #263238;
  }

  #app {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }

  .header {
    height: 2.5em;
  }

  .content-center {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-left: 3em;
    padding-right: 3em;
  }

  .main {
    padding: 1%;
    color: white;
  }

  .main a {
    color:#35BFA2
  }

</style>