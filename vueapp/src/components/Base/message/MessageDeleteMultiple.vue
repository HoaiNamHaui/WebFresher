<template>
  <div class="overlay-message">
    <div class="messagebox">
      <div class="messagebox-header">
        <div class="messagebox-header-title">Xóa hàng loạt</div>
        <div class="messagebox-header-close" @click="closeMessage"></div>
      </div>
      <div class="messagebox-content">
        <div class="messagebox-delete-icon"></div>
        <div class="messagebox-content-describe">
          Bạn có chắc chắn xóa các bản ghi đã chọn?
        </div>
      </div>
      <div class="messagebox-footer">
        <button class="button-white" @click="closeMessage">Hủy</button>
        <button class="button button-red" @click="deleteEmployeeMultiple">Đồng ý</button>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import MISAapi from "@/js/api";
export default {
  name: "MessageDelete",
  data() {
    return {};
  },
  props: ["rowSelected"],
  methods: {
    /**
     * Đóng form xác nhận xóa
     * Author: NHNam (7/1/2023)
     */
    closeMessage() {
      this.$emit("cancelDeleteMultiple", false);
    },
    /**
     * gọi api xóa nhân viên
     * Author: NHNam (5/1/2023)
     */
    async deleteEmployeeMultiple() {
      var me = this;
      var data = me.rowSelected;
      try {
        await axios.delete(MISAapi.employee.deleteMultiple, { data });
      } catch (error) {
        console.error(error);
      }
      this.$emit("cancelDeleteMultiple", false);
      this.$emit("DeleteMultipleSuccess");
    },
  },
};
</script>
<style>
@import url(../../../css/base/messagebox/message-delete.css);
@import url(../../../css/layout/overlay.css);
</style>
