<template>
  <div>
    <input
      class="input"
      type="text"
      @input="changeValue"
      v-model="valueFormat"
      @keydown="handleBackspace"
    />
  </div>
</template>
<script>
export default {
  name: "BaseInputMoney",
  props: ["modelValue"],
  emits: ["update:modelValue"],
  data() {
    return {
      value: null,
      valueFormat: null,
    };
  },
  watch: {
    valueFormat: function (newValue) {
      // Cập nhật giá trị value
      this.value = parseInt(newValue.replaceAll(".", "").replace("đ",""));
      this.valueFormat = this.money;
    },
    value: function (newValue) {
      this.$emit("update:modelValue", newValue);
    },
  },
  methods: {
    handleBackspace(e){
      if(e.keyCode == 8){
        var length = this.valueFormat.length;
        if(length >= 1)
        this.valueFormat = this.valueFormat.slice(0, length -2)
      }
    }
  },
  computed: {
    money: function () {
      if (this.value) {
        var money = new Intl.NumberFormat().format(this.value) + "đ";
        return money;
      }
      else{
        return "";
      }
    },
  },
  created() {
    this.value = this.modelValue;
    this.valueFormat = this.money;
  },
};
</script>

<style scoped>
@import url(../../../css/base/input/input.css);
</style>
