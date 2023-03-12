<template>
  <div style="padding: 20px 20px">
        <h1>Tiền gửi</h1>
        <base-combobox :readOnly = "true" :data="name" :isDisable="test"/>
        <div @click="test = !test">click </div>
    </div>
</template>
<script>
import BaseCombobox from '../base/BaseComboboxV2.vue';
export default {
  name: "DepositView",
  components:{
    BaseCombobox
  },
  data(){
    return{
      name: ['Nguyen Hoai Nam1', 'Nguyen Hoai Nam2', 'Nguyen Hoai Nam3', 'Nguyen Hoai Nam4','Nguyen Hoai Nam1', 'Nguyen Hoai Nam2', 'Nguyen Hoai Nam3', 'Nguyen Hoai Nam4'],
      test: true,
    
    }
  },
  methods:{
  }
};
</script>
<style>
</style>
