<template>
  <div class="cash-container">
    <ul class="menu-cash">
      <!-- <li :class="{'active-item': isProcessTab}" @click="isProcessTab = true">Quy trình</li>
      <li :class="{'active-item': isPayTab}" @click="isPayTab = true">Chi tiền</li> -->
      <!-- <router-link :class="{'active-item': isProcessTab}" @click="isProcessTab = true" to="/CashView/ProcessView">Quy trình</router-link> -->
      <!-- <router-link :class="{'active-item': isPayTab}" @click="isPayTab = true" to="/CashView/PayView">Chi tiền</router-link> -->
      <router-link to="/CashView/ProcessView">Quy trình</router-link>
      <router-link to="/CashView/PayView">Chi tiền</router-link>
    </ul>
    <!-- <process-view v-if="isProcessTab"/>
    <pay-view v-if="isPayTab"/> -->
    <router-view :openForm="isOpenForm" @openFormPayMoney="isOpenForm = true"></router-view>
  </div>
</template>
<script>
// import ProcessView from './ProcessView.vue';
// import PayView from './PayView.vue'
export default {
  name: "CashView",
  // components:{
  //   ProcessView, PayView
  // },
  data() {
    return {
      // isProcessTab: false,
      // isPayTab: false
      isOpenForm: false
    };
  },
  watch: {
    // isPayTab: function(){
    //   this.isProcessTab = !this.isPayTab;
    // },
    // isProcessTab: function(){
    //   this.isPayTab = !this.isProcessTab;
    // }
  },
};
</script>

<style scoped>
.router-link-active {
  font-weight: bold;
  border-bottom: 4px solid #2ca01c;
  background-color: #f5eecb;
  border-radius: 10px 10px 0 0;
  border-left: unset;
}
@import url(../../css/cash/cash.css);
</style>
