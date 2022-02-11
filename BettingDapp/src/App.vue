<template>
  <div id="app">
    <BettingDApp />
  </div>
</template>

<script>
import BettingDApp from './components/BettingDApp'
export default {
  name: 'App',
  components: {
    BettingDApp,
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
}
</style>
