<template>
  <div>
    <input
      class="input-money"
      type="text"
      v-model="valueFormat"
      @keydown="handleBackspace"
      style="text-align: right"
      :disabled="isDisable"
    />
  </div>
</template>
<script>
export default {
  name: "BaseInputMoney",
  props: ["modelValue", "isDisable"],
  emits: ["update:modelValue"],
  data() {
    return {
      value: 0,
      valueFormat: 0,
    };
  },
  watch: {
    valueFormat: function (newValue) {
      // Cập nhật giá trị value
      this.value = parseInt(newValue.replaceAll(".", ""));
      this.valueFormat = this.money;
    },
    value: function (newValue) {
      this.$emit("update:modelValue", newValue);
    },
    modelValue: function(newValue){
      this.value = newValue;
      this.valueFormat = this.money;
    }
  },
  methods: {
    // handleBackspace(e){
    //   if(e.keyCode == 8){
    //     var length = this.valueFormat.length;
    //     if(length >= 1)
    //     this.valueFormat = this.valueFormat.slice(0, length -2)
    //   }
    // }
  },
  computed: {
    money: function () {
      if (this.value) {
        var money = new Intl.NumberFormat().format(this.value);
        return money;
      } else {
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
@import url(../../../css/base/input/smallinput.css);
</style>
