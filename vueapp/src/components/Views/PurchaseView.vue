<template>
  <div style="padding: 20px 20px">
    <h1>Mua hàng</h1>
    <br />
    <br />
    <br />
    <br />
    <br />
    <!-- <div class="button-option-container" v-click-outside-element="close">
      <div class="button-option-select" v-show="isShowSelection">
        <div class="button-option-select-item" @click="onClickSaveAndAdd">
          Cất và Thêm
        </div>
        <div class="button-option-select-item" @click="onClickSaveAndClose">
          Cất và Đóng
        </div>
      </div>
      <button class="button-option" v-if="!toogleButton">Cất và Thêm</button>
      <button class="button-option" v-if="toogleButton">Cất và Đóng</button>
      <div class="button-line"></div>
      <button
        class="button-option-icon"
        @click="isShowSelection = !isShowSelection"
      >
        <div></div>
      </button>
    </div> -->
    <base-button-option />
  </div>
</template>
<script>
import BaseButtonOption from '../base/button/BaseButtonOption.vue';
export default {
  name: "PurchaseView",
  components: {BaseButtonOption},
  data() {
    return {
      toogleButton: true,
      isShowSelection: false,
    };
  },
  methods: {
    // close() {
    //   this.isShowSelection = false;
    // },
    // onClickSaveAndAdd() {
    //   this.toogleButton = false;
    //   this.isShowSelection = false;
    // },
    // onClickSaveAndClose() {
    //   this.toogleButton = true;
    //   this.isShowSelection = false;
    // },
  },
};
</script>
<style>
/* .button-option-container {
  display: flex;
  align-items: center;
  background-color: rgb(61, 230, 81);
  width: max-content;
  border-radius: 4px;
  position: relative;
}
.button-option {
  min-width: 100px;
  height: 28px;
  background-color: transparent;
  border: none;
  text-align: center;
  cursor: pointer;
  color: #ffff;
} */
/* .button-line {
  width: 1px;
  height: 20px;
  background-color: #ffff;
}
.button-option-icon {
  height: 28px;
  width: 28px;
  background-color: transparent;
  border: none;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}
.button-option-icon div {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -852px -365px;
  width: 8px;
  height: 5px;
}
.button-option-select {
  position: absolute;
  bottom: calc(100% + 2px);
  background-color: #fff;
  right: 0;
  border-radius: 2px;
  border: 1px solid #babec5;
}
.button-option-select-item {
  padding: 5px 10px;
  cursor: pointer;
}
.button-option-select-item:hover {
  color: rgb(61, 230, 81);
  background-color: #eceef1;
} */
</style>
