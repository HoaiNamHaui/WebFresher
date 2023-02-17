<template>
  <div class="overlay-message">
    <div class="msgbox">
      <div class="messagebox-header">
        <div class="messagebox-header-title">Dữ liệu thay đổi</div>
        <div class="messagebox-header-close" @click="closeMessageChange"></div>
      </div>
      <div class="msg-content">
        <div class="messagebox-change-icon"></div>
        <div class="messagebox-content-describe">
          Dữ liệu đã thay đổi, bạn có muốn cất không?
        </div>
      </div>
      <div class="msg-change-footer">
        <div class="msg-footer-left">
          <button class="button-white" @click="closeMessageChange">Hủy</button>
        </div>
        <div class="msg-footer-right">
          <button class="button-white" @click="hideDialogAndMessage">
            Không
          </button>
          <button class="button" @click="acceptSave">Cất</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
// import axios from "axios";
export default {
  name: "MessageChange",
  data() {
    return {

    }
    },
  props: [""],
  methods: {
    closeMessageChange(){
        this.$emit("closeMessageChange")
    },
    /**
     * Đóng cả dialog và message thay đổi dữ liệu
     */
    hideDialogAndMessage(){
        this.$emit("hideDialogAndMessage")
    },
    /**
     * Dữ liệu thay đổi đồng ý lưu dữ liệu
     * Author: NHNam (10/1/2023)
     */
    acceptSave(){
        this.$emit("acceptSave");
        this.closeMessageChange();
    },
  },
};
</script>
<style>
@import url(../../../css/base/messagebox/message-change.css);
@import url(../../../css/layout/overlay.css);
</style>
