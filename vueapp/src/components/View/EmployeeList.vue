<template>
  <div>
    <div class="content-top">
      <div class="title">Nhân viên</div>
      <base-button @click="ShowDialog" btnName="Thêm nhân viên" />
    </div>
    <div class="content-data">
      <div class="functions">
        <div
          class="functions-left"
          :class="{ 'functions-left-enable': enableBatch }"
          @click="showBatchOption"
        >
          Thực hiện hàng loạt
          <div
            class="functions-left-option"
            v-show="isShowBatchOption"
            v-click-outside="hideOption"
            @click="deleteEmployeeMuliple"
          >
            Xóa hàng loạt
          </div>
        </div>
        <div class="functions-right">
          <div class="search-box">
            <input
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
              v-debounce:1s="debounceSearch"
            />
            <!-- <input
            type="text"
            placeholder="Tìm theo mã, tên nhân viên"
            @keyup="debounceSearch"
          /> -->
            <div class="search-icon"></div>
          </div>
          <div class="refresh" @click="filterEmployee"></div>
          <div class="excel" @click="exportToExcel"></div>
        </div>
      </div>
      <div class="list-employee">
        <table class="data" id="tblEmployee" cellspacing="0">
          <tr>
            <th class="col60">
              <base-check-box
                ref="checkAll"
                id="checkall"
                :checked="isCheckAll"
                @changeCheckbox="changeCheckbox"
              />
            </th>
            <th class="col150 align-left">MÃ NHÂN VIÊN</th>
            <th class="col300 align-left">TÊN NHÂN VIÊN</th>
            <th class="col150 align-center">NGÀY SINH</th>
            <th class="col180 align-left">GIỚI TÍNH</th>
            <th class="col150 align-left">SỐ ĐIỆN THOẠI</th>
            <th class="col350 align-left">CHỨC DANH</th>
            <th class="col150 align-left">Số CMND</th>
            <th class="col350 align-left">TÊN ĐƠN VỊ</th>
            <th class="col250 align-left">SỐ TÀI KHOẢN</th>
            <th class="col250 align-left">TÊN NGÂN HÀNG</th>
            <th class="col350 align-left">CHI NHÁNH</th>
            <th class="col150">CHỨC NĂNG</th>
          </tr>
          <tr
            v-for="employee in employees"
            v-bind:key="employee.EmployeeId"
            @click="employeeSelected = employee"
            :class="{ active: rowSelected.includes(employee.EmployeeId) }"
          >
            <td class="col60">
              <base-check-box
                :checked="rowSelected.includes(employee.EmployeeId)"
                :id="employee.EmployeeId"
                @changeCheckbox="changeCheckbox"
              />
            </td>
            <td @dblclick="handleEditClick">{{ employee.EmployeeCode }}</td>
            <td @dblclick="handleEditClick">{{ employee.FullName }}</td>
            <td class="align-center" @dblclick="handleEditClick">
              {{ formatDate(employee.DateOfBirth) }}
            </td>
            <td @dblclick="handleEditClick">{{ employee.GenderName }}</td>
            <td @dblclick="handleEditClick">{{ employee.PhoneNumber }}</td>
            <td @dblclick="handleEditClick">{{ employee.PositionName }}</td>
            <td @dblclick="handleEditClick">{{ employee.IdentityNumber }}</td>
            <td @dblclick="handleEditClick">{{ employee.DepartmentName }}</td>
            <td @dblclick="handleEditClick">{{ employee.AccountNumber }}k</td>
            <td @dblclick="handleEditClick">{{ employee.BankProvinceName }}</td>
            <td @dblclick="handleEditClick">{{ employee.BankBranchName }}</td>
            <td>
              <span style="cursor: pointer" @click="handleEditClick">Sửa</span>
              <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
        </table>
        <div class="context-menu">
          <div class="context-menu-item">Nhân bản</div>
          <div class="context-menu-item" @click="deleteEmployee">Xóa</div>
          <div class="context-menu-item">Ngừng sử dụng</div>
        </div>
        <div id="loading"></div>
      </div>
      <footer>
        <div class="total-record">
          Tổng số:
          <span style="font-family: Notosans-SemiBold">{{ totalRecord }}</span>
          bản ghi
        </div>
        <div class="pagination">
          <div class="pages-option">
            {{ pageSize }} bản ghi trên 1 trang
            <div class="box-icon" @click="showPageOption">
              <button id="cbx-icon" class="cbx-icon"></button>
            </div>
            <page-combobox
              :isShowCbb="isShowFooterCbb"
              @sizeRecord="sizeRecord"
            />
          </div>
          <Paginate
            v-model="page"
            :page-count="totalPage"
            :page-range="2"
            :margin-pages="1"
            :click-handler="clickCallback"
            :prev-text="'Trước'"
            :next-text="'Sau'"
            :container-class="'pagination'"
            :page-class="'page-item'"
          />
        </div>
      </footer>
    </div>
    <employee-dialog
      v-if="isShowDialog"
      @onClose="isShowDialog = !isShowDialog"
      :employeeIdSelected="employeeIdSelected"
      @sendMessage="sendMessage"
      @confirmClose="confirmClose"
      :acceptSave="isAcceptSave"
      @showToast="showToast"
    />
    <base-message-delete
      v-if="confirmDelete"
      :employeeSelected="employeeSelected"
      :rowSelected = "rowSelected"
      @cancelDelete="cancelDelete"
      @DeleteSuccess="DeleteSuccess"
    />
    <message-delete-multiple
      v-if="confirmDeleteMultiple"
      :rowSelected = "rowSelected"
      @cancelDeleteMultiple="cancelDeleteMultiple"
      @DeleteMultipleSuccess="DeleteMultipleSuccess"
    />
    <base-message-error
      :error="errForm"
      v-if="isShowMessageError"
      @close="isShowMessageError = false"
    />
    <base-toast
      v-if="isShowToast"
      @hideToast="isShowToast = false"
      :message="message"
    />
    <base-message-change
      v-if="isShowChangeMessage"
      @closeMessageChange="isShowChangeMessage = !isShowChangeMessage"
      @hideDialogAndMessage="hideDialogAndMessage"
    />
  </div>
</template>
<script>
import ClickOutside from "vue-click-outside";
import { vue3Debounce } from "vue-debounce";
import MISAResource from "../../js/base/resource";
import MISAapi from "@/js/api";
import MISAEnum from "../../js/base/enum";
import BaseMessageChange from "../Base/message/BaseMessageChange.vue";
import BaseMessageError from "../Base/message/BaseMessageError.vue";
import BaseMessageDelete from "../Base/message/BaseMessageDelete.vue";
import MessageDeleteMultiple from "../Base/message/MessageDeleteMultiple.vue";
import EmployeeDialog from "./EmployeeDetail.vue";
import BaseToast from "../Base/BaseToast.vue";
import BaseButton from "../Base/button/BaseButton.vue";
import PageCombobox from "../Base/PageCombobox.vue";
import BaseCheckBox from "../Base/BaseCheckbox.vue";
import Paginate from "vuejs-paginate-next";
import $ from "jquery";
import axios from "axios";
export default {
  name: "TheContent",

  components: {
    BaseButton,
    EmployeeDialog,
    Paginate,
    PageCombobox,
    BaseCheckBox,
    BaseMessageDelete,
    MessageDeleteMultiple,
    BaseMessageError,
    BaseToast,
    BaseMessageChange,
  },
  data() {
    return {
      directives: {
        debounce: vue3Debounce({ lock: true }),
      },
      isShowChangeMessage: false, // ẩn hiện thông báo thay đổi dữ liệu
      isShowToast: false, // ẩn hiện toast message
      isShowMessageError: false, // ẩn hiện thông báo lỗi
      errForm: "", // message validate
      employeeSelected: {}, // employee được chọn
      isShowDialog: false, // ẩn hiện form thêm mới
      isShowFooterCbb: false, // chiện chọn số bản ghi
      employees: null, // danh sách nhân viên
      pageSize: 10, // số lượng bản ghi trên 1 trang
      pageNumber: 1, // trang hiện tại
      txtSearch: "", // keyword lọc
      totalPage: 0, // tổng số trang
      totalRecord: 0, // tổng số bản ghi
      page: 1,
      newEmployeeCode: null, //mã nhân viên mới
      isSuccess: true, // thành công
      btnMenuContext: null, // menu context
      isActive: false, //checkbox
      confirmDelete: false, // xác nhận xóa
      confirmDeleteMultiple: false, // xác nhận xóa hàng loạt
      message: "", // thông báo
      employeeIdSelected: null, //id của employee đc chọn
      isAcceptSave: false, // xác nhận lưu
      rowSelected: [], // danh sách employee được chọn
      isCheckAll: false, // check all
      debounce: null, // debounce
      enableBatch: false, // cho phép thực hiện hàng loạt
      isShowBatchOption: false, //hiện option hàng loạt
    };
  },
  watch: {
    // theo dõi keyword lọc
    txtSearch: function () {
      this.filterEmployee();
    },
    rowSelected: {
      handler: function () {
        // Nếu số dòng đã chọn lớn hơn  1
        if (this.rowSelected.length > 1) {
          this.enableBatch = true;
        } else {
          this.enableBatch = false;
          this.isShowBatchOption = false;
        }
      },
      deep: true,
    },
  },
  methods: {

    /**
     * Call api xuất file excel
     * Author: NHNam(12/2/2023)
     */
    exportToExcel() {
      $("#loading").show();
      axios({
        url: MISAapi.employee.export, // endpoint của API
        method: "GET",
        responseType: "blob", // định dạng dữ liệu trả về
      }).then((response) => {
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", "Employee.xlsx"); // tên file được download
        document.body.appendChild(link);
        link.click();
        $("#loading").hide();
      });
    },

    /**
     * show option hàng loạt
     * Author: NHNam(14/1/2023)
     */
    showBatchOption() {
      if (this.enableBatch) {
        this.isShowBatchOption = !this.isShowBatchOption;
      }
    },
    /**
     * ẩn option hàng loạt
     * Author: NHNam(14/1/2023)
     */
    hideOption() {
      this.isShowBatchOption = false;
      alert("hello");
    },
    /**
     * Trì hoãn 1s trước khi gắn txtSearch = value của input search
     * Author: NHNam (14/1/2023)
     */
    // debounceSearch(event) {
    //   clearTimeout(this.debounce);
    //   this.txtSearch = null;
    //   this.debounce = setTimeout(() => {
    //     this.txtSearch = event.target.value;
    //   }, 1000);
    // },
    debounceSearch(event) {
      this.txtSearch = event;
    },
    /**
     * Check All , uncheck CheckBox
     * Author: NHNam (10/1/2023)
     */
    changeCheckbox(active, id) {
      // Check all
      if (id === "checkall" && active) {
        this.rowSelected = this.employees.map((item) => item.EmployeeId);
      } else if (id === "checkall" && !active) {
        this.rowSelected = [];
      } else {
        if (active) {
          this.rowSelected.push(id);
        } else {
          this.rowSelected = this.rowSelected.filter((x) => x != id);
        }
      }
      // So sánh độ dài số dòng được chọn bằng số lượng bản ghi 1 trang thì check all
      if (this.rowSelected.length === this.employees.length) {
        this.isCheckAll = true;
      } else {
        this.isCheckAll = false;
      }
    },
    /**
     * Xác nhận đóng hoặc cất dữ liệu
     * Author: NHNam (10/1/2023)
     */
    confirmClose() {
      this.isShowChangeMessage = true;
    },
    /**
     * Đóng tất cả thông báo, dialog
     * Author: NHNam (10/1/2023)
     */
    hideDialogAndMessage() {
      this.isShowChangeMessage = false;
      this.isShowDialog = false;
      this.isAcceptSave = false;
    },
    /**
     * Đồng ý lưu
     * Author: NHNam (10/1/2023)
     */
    acceptSave() {
      this.isAcceptSave = true;
    },

    /**
     * Hiển thị toast message theo mode thêm hoặc sửa
     * Author: NHNam (9/1/2023)
     */
    async showToast(mess, data, formMode) {
      this.message = mess;
      this.isShowToast = true;
      if (formMode == MISAEnum.FormMode.Add) {
        this.employees.unshift(data);
      } else {
        await this.filterEmployee();
      }

      var me = this;
      setTimeout(function () {
        me.isShowToast = false;
      }, 7000);
    },
    /**
     * Lấy thông báo validate
     * Author: NHNam (7/1/2023)
     */
    sendMessage(e) {
      this.errForm = e;
      this.isShowMessageError = true;
    },
    /**
     * Hiện form chi tiết
     * Author: NHNam (4/1/2023)
     */
    ShowDialog() {
      this.employeeIdSelected = null;
      this.isShowDialog = true;
    },
    /**
     * Click sửa , gửi id cho form chi tiết
     * Author: NHNam (7/1/2023)
     */
    handleEditClick() {
      this.employeeIdSelected = this.employeeSelected.EmployeeId;
      this.isShowDialog = true;
    },
    /**
     * Đóng thông xác nhận xóa
     * Author: NHNam (7/1/2023)
     */
    cancelDelete(e) {
      this.confirmDelete = e;
    },

    /**
     * Đóng thông xác nhận xóa
     * Author: NHNam (7/1/2023)
     */
    cancelDeleteMultiple(e) {
      this.confirmDeleteMultiple = e;
    },
    /**
     * Xóa thành công hiện toast message
     * Author: NHNam (9/1/2023)
     */
    DeleteSuccess() {
      this.isShowToast = true;
      this.employees = this.employees.filter(
        (item) => item != this.employeeSelected
      );
      var me = this;
      setTimeout(function () {
        me.isShowToast = false;
      }, 4000);
    },

    /**
     * Xóa hàng loạt thành công hiện toast message
     * Author: NHNam (9/1/2023)
     */
    async DeleteMultipleSuccess() {
      this.isShowToast = true;
      var me = this;
      this.rowSelected = [];
      await me.filterEmployee();
      setTimeout(function () {
        me.isShowToast = false;
      }, 4000);
      
    },

    /**
     * hiện thông báo xác nhận xóa
     * Author: NHNam (7/1/2023)
     */
    deleteEmployee() {
      this.message = MISAResource.vi.delete;
      this.confirmDelete = true;
    },
    /**
     * hiện thông báo xác nhận xóa
     * Author: NHNam (7/1/2023)
     */

    deleteEmployeeMuliple() {
      this.message = MISAResource.vi.delete;
      this.confirmDeleteMultiple = true;
    },
    /**
     * Tìm kiếm, phân trang
     * Author: NHNam (1/1/2023)
     */
    filterEmployee() {
      $("#loading").show();
      var me = this;
      var url =
        MISAapi.employee.filter +
        `pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&keyword=${this.txtSearch}`;
      axios
        .get(url)
        .then(function (res) {
          me.totalPage = res.data.TotalPage;
          me.totalRecord = res.data.TotalRecord;
          me.isSuccess = true;
          me.employees = res.data.Data;
          $("#loading").hide();
        })
        .catch(function (res) {
          console.log(res);
        });
    },
    /**
     * Chuyển trang
     * Author: NHNam (1/1/2023)
     */
    clickCallback(pageNum) {
      this.pageNumber = pageNum;
      this.rowSelected = [];
      this.isCheckAll = false;
      (this.txtSearch = ""), this.filterEmployee();
    },
    /**
     * Đổi size page
     * Author: NHNam (1/1/2023)
     */
    sizeRecord(e) {
      this.pageSize = e;
      this.showPageOption();
      this.page = 1;
      this.pageNumber = 1;
      this.filterEmployee();
    },
    /**
     * Hiện , ẩn chọn số bản ghi trên 1 trang
     * Author: Nguyễn Hoài Nam(1/1/2023)
     * isShowCbb = true: hiển thị
     * isShowCbb = false: ẩn
     */
    showPageOption() {
      $("#cbx-icon").toggleClass("toogle-rotate");
      if (!this.isShowFooterCbb) {
        this.showPageFooterOption(true);
      } else {
        this.isShowFooterCbb = false;
      }
    },
    /**
     * Hiện , ẩn chọn số bản ghi trên 1 trang
     * Author: Nguyễn Hoài Nam(1/1/2023)
     * isShowFooterCbb = true: hiển thị
     * isShowFooterCbb = false: ẩn
     */
    showPageFooterOption(isShowCbb) {
      if (isShowCbb) {
        this.isShowFooterCbb = isShowCbb;
      }
    },
    /**
     * Click hiện menucontext theo vị trí click
     * Author: NHNam (1/1/2023)
     */
    toogleMenu(e) {
      this.btnMenuContext = e.target;
      var x = e.clientX;
      var y = e.clientY;
      var clientHeight = document.getElementById("content").clientHeight;
      if (clientHeight - y < 80) {
        y = y - 110;
      }
      $(".context-menu").css("top", y + 10 + "px");
      $(".context-menu").css("left", x - 110 + "px");
      $(".context-menu").show(100);
    },
    /**
     * Format định dạng ngày tháng
     * Author: Nguyễn Hoài Nam(15/07/2022)
     */
    formatDate(date) {
      try {
        if (date) {
          date = new Date(date);
          let newDate = date.getDate();
          let month = date.getMonth() + 1;
          let year = date.getFullYear();
          newDate = newDate < 10 ? `0${newDate}` : newDate;
          month = month < 10 ? `0${month}` : month;
          return `${newDate}/${month}/${year}`;
        }
      } catch (error) {
        console.log(error);
        return "";
      }
    },
  },
  created() {
    //Lấy danh sách nhân viên, tìm kiếm
    this.filterEmployee();
  },

  mounted() {
    // prevent click outside event with popupItem.
    this.popupItem = this.$el;
  },
  // do not forget this section
  directives: {
    ClickOutside,
  },
};
/**
 * Phát hiện click ngoài để ẩn menu chức năng
 * Author: Nguyễn Hoài Nam (1/1/2023)
 */
document.addEventListener("click", function (event) {
  if (event.target.closest(".down-icon")) return;
  $(".context-menu").hide();
});
</script>
<style>
@import url(../../css/layout/content.css);
@import url(../../css/layout/footer.css);
@import url(../../css/base/combobox/combobox.css);
.pagination {
  margin-left: 20px;
  margin-right: 4px;
  list-style: none;
  display: flex;
  justify-content: center;
  align-items: center;
}
.page-item {
  margin: 0 10px;
  cursor: pointer;
  /* width: 24px; */
  height: 24px;
  display: flex;
  align-items: center;
  text-align: center;
  cursor: pointer;
}
.page-item.disabled {
  /* width: 48px; */
  width: 24px;
}
.page-item.active {
  border: 1px solid #dfdfdf;
  font-weight: 700;
  text-align: center;
}
.page-item.active a {
  width: 24px;
  text-align: center;
}
.page-item.disabled {
  cursor: default !important;
  color: #9e9e9e;
}
tr.active {
  background-color: #e5f3ff;
}
tr.active td:first-child,
tr.active td:last-child {
  background-color: #e5f3ff;
}
</style>
