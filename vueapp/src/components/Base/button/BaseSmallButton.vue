<template>
    <div :tabindex="tabindex">
        <button class="small-button" :class="btnClass">{{ btnName }}</button>
    </div>
    
</template>
<script>
    export default{
        name: "BaseButton",
        props:["btnName","btnClass","tabindex"],
        methods:{
        }
    }
</script>
<style>
@import url(../../../css/base/button/button.css);
</style>