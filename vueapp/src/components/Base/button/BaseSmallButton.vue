<template>
    <button id="addEmployee" class="small-button" :class="btnClass">{{ btnName }}</button>
</template>
<script>
    export default{
        name: "BaseButton",
        props:["btnName","btnClass"]
    }
</script>
<style>
@import url(../../../css/base/button/button.css);
</style>