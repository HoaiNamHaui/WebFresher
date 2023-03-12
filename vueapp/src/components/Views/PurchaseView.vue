<template>
    <div style="padding: 20px 20px">
       <h1>Mua hàng</h1>
       <base-combobox-read-only :data="data" v-model="value" :propValue="value"/>
        <p>giá trị: {{ value }}</p>
    </div>
</template>
<script>
import BaseComboboxReadOnly from '../base/BaseComboboxReadOnly.vue';
export default{
    name: "PurchaseView",
    components:{
        BaseComboboxReadOnly
    },
    data(){
        return{
            data: ['Nguyễn Hoài Nam 1','Nguyễn Hoài Nam 2','Nguyễn Hoài Nam 3','Nguyễn Hoài Nam 4','Nguyễn Hoài Nam 5','Nguyễn Hoài Nam 6','Nguyễn Hoài Nam 7',],
            value: 'Nguyễn Hoài Nam 1',
        }
    }
}
</script>
<style>

</style>