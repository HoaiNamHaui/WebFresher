<template>
  <div class="container"> 
    <the-menu/>
    <div class="main">
      <the-header/>
      <employee-list/>
    </div>
  </div>
</template>

<script>
import TheMenu from './components/layout/TheMenu.vue';
import TheHeader from './components/layout/TheHeader.vue';
import EmployeeList from './components/View/EmployeeList.vue';
export default {
  name: 'App',
  components: {
    TheMenu, TheHeader, EmployeeList
  }
}
</script>

<style>
  @import url(./css/main.css);
</style>
