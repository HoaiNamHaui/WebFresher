<template>
  <div class="cash-container">
    <ul class="menu-cash">
      <li :class="{'active-item': isProcessTab}" @click="isProcessTab = true">Quy trình</li>
      <li :class="{'active-item': isPayTab}" @click="isPayTab = true">Chi tiền</li>
    </ul>
    <process-view v-if="isProcessTab"/>
    <pay-view v-if="isPayTab"/>
  </div>
</template>
<script>
import ProcessView from './ProcessView.vue';
import PayView from './PayView.vue'
export default {
  name: "CashView",
  components:{
    ProcessView, PayView
  },
  data() {
    return {
      isProcessTab: true,
      isPayTab: false
    };
  },
  watch:{
    isPayTab: function(){
      this.isProcessTab = !this.isPayTab;
    },
    isProcessTab: function(){
      this.isPayTab = !this.isProcessTab;
    }
  }
};
</script>

<style scoped>
@import url(../../css/cash/cash.css);
</style>
