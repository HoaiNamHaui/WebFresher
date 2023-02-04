<template>
    <div class="m-b-12">
      <label for="">{{ label }} <span :class="{hide : !isRequire}" class="red" >*</span></label> <br />
      <input
        ref="txtfield"
        type="text"
        style="width: 100%;"
        class="input"
        v-model="value"
        :class="{error: isError}"
        @blur="blurInput"
        tabindex="tabindex"
      />
      <br />
    </div>
</template>
<script>
export default {
  name: "BaseInput",
  props:{
    label: String,
    require:Boolean,
    modelValue: String,
    error: String,
    tabindex: String,
    focus: Boolean
  },
  // props: ["label","require","modelValue","tabindex"],
  data(){
    return{
        value: null,
        isRequire: false,
        isError: false,
    }
  },
  methods:{
    /**
     * Blur input bỏ border lỗi
     * Author: NHNam (4/1/2023)
     */
    blurInput(){
      this.isError = false;
    },
    //focus vào input
    setFocus(){
      this.$refs.txtfield.focus();
    }
  },
  created(){
    this.value = this.modelValue;
    this.isRequire = this.require;
  },
  watch:{
    modelValue: function(newValue){
      this.value = newValue;
    },
    value: function(newValue){
      this.$emit("update:modelValue", newValue);
    },
    //Lỗi hiển thị border lỗi
    error: function(){
      if(this.error != ""){
        this.isError = true;
      }
      else{
        this.isError = false;
      }
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
