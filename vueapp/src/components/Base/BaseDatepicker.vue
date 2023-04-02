<template>
  <div class="date_wrapper" @click="handleDisable">
    <DatePicker
      style="min-width: 150px; padding: 0px; border: 0px"
      class="textfield__input"
      :enable-time-picker="false"
      v-model="date"
      :disabled-dates="disabledDate"
      @update:modelValue="changeDateValue"
      :config="options"
      locale="vi"
      now-button-label="Hôm nay"
      text-input
      :ref="dateName"
      auto-apply
      close-on-scroll
      show-now-button
      format="dd/MM/yyyy"
      :day-names="['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']"
    >
      <template #dp-input="{ value }">
        <div class="date-picker__wrapper">
          <input
            style="border: 1px solid #babec5;"
            class="input-small"
            :class="{
              textfield__input: true,
              'modal-textfield__input': true,
              'textfield--error-input': isError,
            }"
            type="text"
            placeholder="DD/MM/YYYY"
            :ref="name"
            :name="name"
            :value="value"
            autocomplete="off"
            :tabindex="tabIndex"
            @blur="blurInputValue"
            @input="onInputValue"
            :disabled="isDisable"
          />
          <p class="text-error">{{ errorMsg }}</p>
          <p v-if="!date" class="date-picker__icon">
            <i></i>
          </p>
        </div>
      </template>
    </DatePicker>
    <div
      class="error-info error-bg"
      v-if="tooltipError"
      style="top: 42px; left: -80px"
    >
      <div class="error-arrow error-bg" style="left: 50%; top: -15%"></div>
      <div class="error-text">{{ tooltipContent }}</div>
    </div>
  </div>
</template>
<script>
import commonJS from "@/js/base/common";
import DatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
export default {
  name: "MDatePicker",
  emits: ["errorInputMessage", "update:modelValue", "blurInput","changeDate"],
  components: {
    DatePicker,
  },
  data() {
    return {
      isError: false,
      date: this.modelValue,
      options: {
        format: "DD/MM/YYYY",
        useCurrent: false,
      },
    };
  },
  props: {
    isErrorInput: Boolean,
    name: {
      type: String,
    },
    errorMsg: {
      type: String,
    },
    rules: {
      type: Array,
      default: new Array(0),
    },
    tabIndex: Number,
    modelValue: [String, Date],
    isDisable: Boolean,
    dateName: String,
    tooltipError: Boolean,
    tooltipContent: String,
  },
  methods: {
    handleDisable(){
      console.log(this.$refs[this.dateName]);
      if(this.isDisable){
        console.log(this.$refs[this.dateName]);
        // this.$refs[].closeMenu();
        
        // datepicker.value.closeMenu();
      }
    },
    /**
     * Truyền sự kiện date thay đổi
     * Author : NHNam (05/1/2023)
     */
    changeDateValue(date) {
      this.$emit("update:modelValue", date, this.name);
      this.$refs[this.dateName].closeMenu();
    },

    /**
     * Xử lý khi blur input
     * Author : NHNam (05/1/2023)
     */
    blurInputValue(event) {
      if (this.isValidDate(event)) {
        const newDate = new Date(
          event.target.value.split("/").reverse().join("-")
        );
        // console.log("newDate", newDate);
        this.$emit("update:modelValue", newDate, this.name);
        this.$emit("blurInput");
      } else {
        this.date = null;
        this.$refs[this.name] = "";
        this.$emit("update:modelValue", null, this.name);
      }
    },
    /**
     * Xử lý khi nhập input
     * Author : NHNam (05/1/2023)
     */
    onInputValue(event) {
      if (this.isValidDate(event)) {
        const newDate = new Date(
          event.target.value.split("/").reverse().join("-")
        );
        this.$emit("update:modelValue", newDate, this.name);
        // console.log(newDate);
      }
      else{
        this.$emit("update:modelValue", "", this.name);
      }
    },
    /**
     * Check date hợp lệ
     * Author : Author : NHNam (05/1/2023)
     */
    isValidDate(event) {
      //   var propertyName = this.dateName;
      var value = event.target.value;
      const dateRegex =
        /^([1-9]|[0-2][0-9]|3[0-1])\/([1-9]|0[1-9]|1[0-2])\/[1-2][0-9][0-9][0-9]$/;
      return dateRegex.test(value);
    },
    /**
     * Disable ngày lớn hơn ngày hiện tại
     * Author: NHNam (10/2/2023)
     */
    disabledDate(time) {
      return time.getTime() > Date.now();
    },
    /**
     * author:NHNam(2/1/2023)
     * Focus vào ô input
     */
    onFocus() {
      this.$refs[this.name].focus();
    },
    /**
     * Hàm forcus vào input khi có lỗi
     * Author: NHNam (9/1/2023)
     */
    inputFocus() {
      try {
        this.$nextTick(function () {
          this.$refs[this.name].focus();
        });
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    date: function(){
      this.$emit("changeDate");
    },
    /**
     * Truyền modelValue thay đổi
     * Author : NHNam (05/1/2023)
     */
    modelValue: function () {
      this.date = this.modelValue;
      // console.log("date: " + this.date);
    },
    /**
     * Hàm theo dõi sự thay đổi của isErrorInput mỗi khi validate
     * Author: NHNam(2/3/2023)
     */
    isErrorInput: function () {
      this.isError = this.isErrorInput;
      // if (!this.isErrorInput){
      //   this.isError = false;
      // }
      // else {
      //   this.isError = true;
      // }
    },
  },
  computed: {
    /**
     * Format date
     * Author : NHNam (05/1/2023)
     */
    formatDate() {
      return commonJS.formatDate(this.modelValue);
    },
  },
};
</script>
<style>
    .date-picker__icon {
    top: 4px;
    right: 8px;
    position: absolute;
    background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -128px -312px;
    width: 16px;
    height: 16px;
  }
  /* .dp__today{
    border: 1px solid #50b83c !important;
  }
  .dp__active_date{
    background-color: #50b83c !important;
  } */
  .dp__input_icon_pad {
    padding-left: 0;
  }
  .dp__input {
    border: none !important;
  }
  .dp__clear_icon {
    display: none;
  }
  .dp__input:hover {
    background-color: #f6f6f6;
  }
  .dp__input_icon {
    display: none;
    left: unset;
    right: 0;
    background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -125px -310px;
    margin-right: 0;
  }
  .dp__input:hover {
    background-color: #f6f6f6;
  }
  .dp__input_icons {
    display: inline-block;
    width: 0;
    height: 1rem;
    stroke-width: 0;
    font-size: 1rem;
    line-height: 1.5rem;
    padding: 3px 12px;
    color: var(--dp-icon-color);
    box-sizing: content-box;
  }
  .dp__range_end,
  .dp__range_start,
  .dp__active_date {
    background: #50b83c;
    color: var(--dp-primary-text-color);
  }
  .dp__cell_inner {
    display: flex;
    align-items: center;
    text-align: center;
    justify-content: center;
    border-radius: 42px;
    height: 35px;
    padding: 5px;
    width: 35px;
    border: 1px solid rgba(0, 0, 0, 0);
    box-sizing: border-box;
    position: relative;
  }
  
  .dp__now_button {
      color: #50b83c;
      width: 100%;
      padding: 10px 4px;
      font-weight: bold;
      border-radius: 0;;
      font-size: 1rem;
      cursor: pointer;
      background: rgba(0,0,0,0);
      outline: none;
      border: none;
      border-top: 1px solid #ccc;
  }
  .dp__main {
    font-family: Notosans-Regular;
  }
  .textfield__input {
    font-family: Notosans-Regular;
    outline: none;
    border-radius: 4px;
    border: 1px solid var(--border-color);
    min-width: 150px; 
    width: 150px;
    height: 26px;
    position:relative;
    padding: 8px 8px  8px 12px;
  }
  .date_wrapper {
    position: relative;
  }
  .date_wrapper:hover .error-info {
    display: block;
  }
  
.error-info{
  top: -28px;
    left: -58px;
  text-align: center;
  background-color: #393a3d;
  padding: 5px 16px;
  width: max-content;
  border-radius: 4px;
  color: #ffff;
  position: absolute; 
  transform: translate(35px, 0);
  font-family: Notosans-Regular;
  display: none;
  z-index: 99;
}

.error-arrow{
  width: 7px;
  height: 7px;
  background-color: #393a3d;
  position: absolute;
  left: 50%;
  top: 88%;
  transform: translate(-50%,-50%);
  transform: rotate(45deg);
}
.input_content {
  position: relative;
}
.error-bg{
  background-color: #f34545;
  color: #fff;
}
.textfield--error-input {
  border: 1px solid #ff0000 !important;

}
@font-face {
    font-family: Notosans-Regular;
    src: url(../../assets/fonts/notosans-regular.2cb88a13.woff2);
}
</style>
