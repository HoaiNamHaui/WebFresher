<template>
    <div class="checkbox-container">
        <label :for="id"><div @click="check" class="checkbox" :class="{ 'check-icon': isActive }"></div></label><input ref="checkbox" class="input-checkbox" type="checkbox" :id="id">
    </div>
</template>

<script>
    export default{
        name: "BaseCheckbox",
        data(){
            return{
                isActive: false
            }
        },
        props: ["id","isCheckAll"],
        methods:{
            check(){
                if(this.isCheckAll){
                    console.log("check all = ", this.isCheckAll);
                    this.isActive =true;
                    this.$refs.checkbox = true;
                    return
                }
                if(this.isActive){
                    this.isActive = false
                    return
                } 
                this.isActive = true
            }
        }
    }
</script>

<style scoped>
    @import url(../../css/base/checkbox/checkbox.css);
</style>