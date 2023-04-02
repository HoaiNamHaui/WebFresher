<template>
  <div class="m-b-8 base-input-container" style="position: relative">
    <label for="" class="label"
      >{{ label }}
      <span :class="{ hide: !isRequire }" class="red">*</span>
      <div class="error-info" v-if="tooltip">
        <div class="error-text">{{ tooltipContent }}</div>
        <div class="error-arrow"></div>
      </div>
    </label>
    <br />
    <input
      ref="myInput"
      type="text"
      style="width: 100%"
      class="input-small"
      v-model="value"
      :class="{ error: isError, focus: isFocus }"
      @blur="blurInput"
      :tabindex="tabindex"
      :placeholder="placeholderInput"
      :style="{ textAlign: textAlign }"
      :disabled="isDisable"
    />
    <br />
    <div class="input-tooltip-error" v-if="isError">{{ error }}</div>
  </div>
</template>
<script>
export default {
  name: "BaseInput",
  props: {
    isDisable: Boolean,
    label: String,
    require: Boolean,
    modelValue: String,
    error: String,
    tabindex: Number,
    focus: Boolean,
    tooltip: String,
    tooltipContent: String,
    isFocus: Boolean,
    shouldFocus: {
      type: Boolean,
      default: false,
    },
    textAlign: String,
    placeholderInput: String,
  },
  // props: ["label","require","modelValue","tabindex"],
  data() {
    return {
      value: null,
      isRequire: false,
      isError: false,
    };
  },
  methods: {
    /**
     * gửi trạng thái focus
     * Author: NHNam (4/1/2023)
     */
    blurInput() {
      this.$emit("unfocus", false);
      this.$emit("checkChangeByUser",this.value)
    },
  },
  created() {
    this.value = this.modelValue;
    this.isRequire = this.require;
    if (this.shouldFocus) {
      this.$nextTick(() => {
        this.$refs.myInput.focus();
      });
    }
  },
  watch: {
    modelValue: function (newValue) {
      this.value = newValue;
    },
    value: function (newValue) {
      this.$emit("update:modelValue", newValue);
      this.$emit("getTempReson", newValue)
    },
    //Lỗi hiển thị border lỗi
    error: function () {
      if (this.error != "") {
        this.isError = true;
      } else {
        this.isError = false;
      }
    },
    shouldFocus: function () {
      this.$nextTick(() => {
        this.$refs.myInput.focus();
      });
    },
  },
};
</script>
<style scoped>
@import url(../../../css/base/input/smallinput.css);
.hide {
  display: none;
}
:placeholder-shown {
  font-style: italic;
}
</style>
