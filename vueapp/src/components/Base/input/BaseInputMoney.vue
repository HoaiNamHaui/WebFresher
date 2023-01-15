<template>
  <div>
    <input
      class="input"
      type="text"
      @input="changeValue"
      v-model="valueFormat"
    /> VNĐ
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
      this.value = parseInt(newValue.replaceAll(".", ""));
      this.valueFormat = this.money;
    },
    value: function (newValue) {
      this.$emit("update:modelValue", newValue);
    },
  },
  methods: {},
  computed: {
    money: function () {
      if (this.value) {
        var money = new Intl.NumberFormat().format(this.value);
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
