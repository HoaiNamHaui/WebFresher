<template>
    <div class="checkbox-container">
        <label :for="id"><div v-on:keydown.enter="checkByEnter" :tabindex="tabindex"  @click="check" class="checkbox" :class="{ 'check-icon': checked }"></div></label><input :checked="checked" ref="checkbox" class="input-checkbox" type="checkbox" :id="id">
    </div>
</template>

<script>
    export default{
        name: "BaseCheckbox",
        data(){
            return{

            }
        },
        props: ["id","isCheckAll",'checked','tabindex'],
        methods:{
            //Check
            check(){
                this.$emit("changeCheckbox",!this.checked, this.id);
                
            },
            checkByEnter(){
                this.check();
            }
        }
    }
</script>

<style scoped>
    @import url(../../css/base/checkbox/checkbox.css);
</style>