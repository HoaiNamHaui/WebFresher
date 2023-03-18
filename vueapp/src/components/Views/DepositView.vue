<template>
  <div style="padding: 20px 20px; position: relative">
    <h1>Tiền gửi</h1>
    <base-datepicker v-model="date" style="width: 200px;"/>
    <div>DATE: {{ date }}</div>
  </div>
</template>
<script>
import BaseDatepicker from '../base/BaseDatepicker.vue';
export default {
  name: "DepositView",
  components: {BaseDatepicker},
  data() {
    return {
      date: null
    };
  },
  methods: {},
};
</script>
<style>
</style>
