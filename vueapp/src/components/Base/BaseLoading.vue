<template>
    <div class="container-loading">
        <div class="loading"></div>
    </div>
</template>
<script>
export default{
    name: 'BaseLoading'
}
</script>
<style scoped>
.container-loading{
    position: absolute;
    width: 100vw;
    height: 100vh;
    background-color: rgb(0, 0, 0, 0.3);
    right: 0;
    bottom: 0;
    z-index: 30;
    display: flex;
    align-items: center;
    justify-content: center;
}
.loading{
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    border: 10px solid #f3f3f3;
    border-top: 10px solid #2ca01c; 
    border-radius: 50%;
    width: 60px;
    height: 60px;
    animation: spin 1.5s linear infinite;
}
</style>