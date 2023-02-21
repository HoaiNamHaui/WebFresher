<template>
    <div class="tooltip-container">
        {{ message }}
    </div>
</template>

<script>
    export default{
        name: "BaseTooltip",
        props:['message'],
    }
</script>

<style>
@import url(../../css/base/tooltip.css);
</style>