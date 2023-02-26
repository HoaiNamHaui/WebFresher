<template>
    <button id="addEmployee" class="button" :class="btnClass">{{ btnName }}</button>
</template>
<script>
    export default{
        name: "AddButton",
        props:["btnName","btnClass"]
    }
</script>
<style>
@import url(../../../css/base/button/button.css);
</style>