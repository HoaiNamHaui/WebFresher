<template>
    <div class="toast">
        <div class="toast-left"><div class="toast-icon"></div>
        <div class="toast-message"><span>Thành công! </span> {{ message }}</div></div>
        <div class="toast-right">
            <div class="undo">Hoàn tác</div>
            <div class="close-icon" @click="hideToast"></div>
        </div>
    </div>    
</template>
<script>
    export default{
        name: "BaseToast",
        props:['message'],
        methods:{
            hideToast(){
                this.$emit("hideToast")
            }
        }
    }
</script>
<style>
@import url(../../css/base/toast/toast.css);
</style>