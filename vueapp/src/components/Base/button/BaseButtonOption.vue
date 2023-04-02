<template>
    <div class="button-option-container" v-click-outside-element="close">
      <div class="button-option-select" v-show="isShowSelection">
        <div class="button-option-select-item" @click="onClickSaveAndAdd">
          Cất và Thêm
        </div>
        <div class="button-option-select-item" @click="onClickSaveAndClose">
          Cất và Đóng
        </div>
      </div>
      <button class="button-option" @click="saveAndAdd" v-if="!toogleButton">Cất và Thêm</button>
      <button class="button-option" @click="saveAndClose" v-if="toogleButton">Cất và Đóng</button>
      <div class="button-line"></div>
      <button
        class="button-option-icon"
        @click="isShowSelection = !isShowSelection"
      >
        <div></div>
      </button>
    </div>
</template>
<script>
export default{
    name: "BaseButtonOption",
    data() {
    return {
      toogleButton: true,
      isShowSelection: false,
    };
  },
  methods: {
    /**
     * Đóng chọn option
     * Author: NHNam (20/1/2023)
     */
    close() {
      this.isShowSelection = false;
    },
    /**
     * xử lí khi click vào cất và thêm
     * Author: NHNam (20/1/2023)
     */
    onClickSaveAndAdd() {
      localStorage.setItem("buttonMode", false);
      this.toogleButton = false;
      this.isShowSelection = false;
      this.saveAndAdd();
    },
    /**
     * xử lí khi click vào cất và đóng
     * Author: NHNam (20/1/2023)
     */
    onClickSaveAndClose() {
      localStorage.setItem("buttonMode", true);
      this.toogleButton = true;
      this.isShowSelection = false;
      this.saveAndClose();
    },
    /**
     * Cất và thêm
     * Author: NHNam (20/1/2023)
     */
    saveAndAdd(){
      this.$emit("saveAndAdd");
    },
    /**
     * Cất và đóng
     * Author: NHNam (20/1/2023)
     */
    saveAndClose(){
      this.$emit("saveAndClose");
    },
  },
  created(){
    this.toogleButton = localStorage.getItem("buttonMode");
  }
}
</script>
<style>
.button-option-container {
  display: flex;
  align-items: center;
  background-color: #2ca01c;
  width: max-content;
  border-radius: 4px;
  position: relative;
}
.button-option {
  min-width: 100px;
  height: 28px;
  background-color: transparent;
  border: none;
  /* padding: 0 10px; */
  text-align: center;
  cursor: pointer;
  color: #ffff;
}
.button-line {
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
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -852px -365px;
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
  color: #2ca01c;
  background-color: #eceef1;
}
</style>