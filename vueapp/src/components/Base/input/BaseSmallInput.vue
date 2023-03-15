<template>
    <div class="m-b-8">
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
      />
      <br />
    </div>
  </template>
  <script>

  export default {
    name: "BaseInput",
    props: {
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
      },
      //Lỗi hiển thị border lỗi
      error: function () {
        if (this.error != "") {
          this.isError = true;
        } else {
          this.isError = false;
        }
      },
      shouldFocus: function(){
        this.$nextTick(() => {
          this.$refs.myInput.focus();
        });
      }
    },
  
  };
  </script>
  <style scoped>
  @import url(../../../css/base/input/smallinput.css);
  .hide {
    display: none;
  }
  </style>
  