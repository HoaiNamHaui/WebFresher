<template>
    <div class="checkbox-container">
        <label :for="id"><div @click="check" class="checkbox" :class="{ 'check-icon': checked }"></div></label><input :checked="checked" ref="checkbox" class="input-checkbox" type="checkbox" :id="id">
    </div>
</template>

<script>
    export default{
        name: "BaseCheckbox",
        data(){
            return{

            }
        },
        props: ["id","isCheckAll",'checked'],
        methods:{
            check(){
                console.log(!this.checked);
                this.$emit("changeCheckbox",!this.checked, this.id);
                
            }
        }
    }
</script>

<style scoped>
    @import url(../../css/base/checkbox/checkbox.css);
</style>