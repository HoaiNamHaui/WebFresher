<template>
    <div class="m-b-12" >
      <label for="">{{ label }} <span class="red" :class="{hide : !require}">*</span></label> <br />
      <input
        ref="txtCode"
        type="text"
        style="width: 100%;"
        class="input"
        v-model="value"
        :tabindex="tabindex"
      />
      <br />
    </div>
</template>
<script>
export default {
  name: "BaseInput",
  props: ["label","require","modelValue","tabindex"],
  data(){
    return{
        value: null,
    }
  },
  created(){
    this.value = this.modelValue;
  },
  watch:{
    modelValue: function(newValue){
      this.value = newValue;
    },
    value: function(newValue){
      this.$emit("update:modelValue", newValue);
    }
  }
};
</script>
<style scoped>
@import url(../../../css/base/input/input.css);
.hide{
    display: none;
}
</style>
