<template>
  <div style="padding: 20px 20px">
    <h1>Tiền mặt</h1>
    <!-- <h2 for="">Lương:</h2>
    <br>
    <base-input-money
    
    v-model="employee.salary"
    />
    <br>
    <h2>Binding 2 chiều: {{ employee.salary|| 0 }}</h2> -->
  </div>
</template>
<script>
// import BaseInputMoney from '../Base/input/BaseInputMoney.vue';
export default {
  name: "CashView",
  components:{
    // BaseInputMoney
  },
  data() {
    return {
      employee: {
        salary: 0,
      },
    };
  },
  watch:{
    employee: function(newValue){
        this.employee.salary = newValue;
    }
  }
};
</script>

<style scoped>
.cash-container{
    margin-left: 20px;
}
</style>
