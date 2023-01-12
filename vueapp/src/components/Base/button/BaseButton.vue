<template>
    <button id="addEmployee" class="button">{{ btnName }}</button>
</template>
<script>
    export default{
        name: "AddButton",
        props:["btnName"]
    }
</script>
<style>
@import url(../../../css/base/button/button.css);
</style>