<template>
  <div>
    <div class="content-top">
      <div class="title">Nhân viên</div>
      <base-button @click="ShowDialog" btnName="Thêm nhân viên" />
    </div>
    <div class="content-data">
      <div class="functions">
        <div class="functions-left">
          <div v-show="enableBatch">
            <span>Đã chọn: </span> <span>{{ rowSelected.length }}</span>
            <span style="color: #f39c12; cursor: pointer"
            @click="resetRowSelected"
            >Bỏ chọn</span>
            <base-button 
            :btnClass="'button-red'" 
            btnName="Xóa"
            @click="deleteEmployeeMuliple"
            />
          </div>
        </div>
        <div class="functions-right">
          <div class="search-box" :class="{ focus: this.focusSearch }">
            <input
              @focus="this.focusSearch = true"
              @blur="this.focusSearch = false"
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
              v-debounce:1s="debounceSearch"
            />
            <div class="search-icon"></div>
          </div>
          <div
            class="refresh"
            @click="filterEmployee"
            style="position: relative"
          >
            <base-tooltip message="Làm mới danh sách" />
          </div>
          <div class="excel" @click="exportToExcel" style="position: relative">
            <base-tooltip message="Xuất Excel" />
          </div>
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
            @dblclick="handleEditClick(employee)"
            :class="{ active: rowSelected.includes(employee.EmployeeId) }"
            @click="employeeSelected = employee"
          >
            <td class="col60">
              <base-check-box
                :checked="rowSelected.includes(employee.EmployeeId)"
                :id="employee.EmployeeId"
                @changeCheckbox="changeCheckbox"
              />
            </td>
            <td>{{ employee.EmployeeCode }}</td>
            <td>{{ employee.FullName }}</td>
            <td class="align-center">
              {{ formatDate(employee.DateOfBirth) }}
            </td>
            <td>
              {{ employee.GenderName || getGenderName(employee.Gender) }}
            </td>
            <td>{{ employee.PhoneNumber }}</td>
            <td>{{ employee.PositionName }}</td>
            <td>{{ employee.IdentityNumber }}</td>
            <td>{{ employee.DepartmentName }}</td>
            <td>{{ employee.BankAccount }}</td>
            <td>{{ employee.BankName }}</td>
            <td>{{ employee.BankBranchName }}</td>
            <td>
              <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span
              >
              <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
        </table>
        <div class="context-menu">
          <div class="context-menu-item" @click="handleDuplicate(employeeSelected)">Nhân bản</div>
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
      :isDuplicate="isDuplicate"
    />
    <message-delete
      v-if="confirmDelete"
      :employeeSelected="employeeSelected"
      :rowSelected="rowSelected"
      @cancelDelete="cancelDelete"
      @DeleteSuccess="DeleteSuccess"
    />
    <message-delete-multiple
      v-if="confirmDeleteMultiple"
      :rowSelected="rowSelected"
      @cancelDeleteMultiple="cancelDeleteMultiple"
      @DeleteMultipleSuccess="DeleteMultipleSuccess"
    />
    <message-error
      :error="errForm"
      v-if="isShowMessageError"
      @close="isShowMessageError = false"
    />
    <base-toast
      v-if="isShowToast"
      @hideToast="isShowToast = false"
      :message="message"
    />
    <message-change
      v-if="isShowChangeMessage"
      @closeMessageChange="isShowChangeMessage = !isShowChangeMessage"
      @hideDialogAndMessage="hideDialogAndMessage"
      @acceptSave="acceptSave"
    />
  </div>
</template>
<script>
import ClickOutside from "vue-click-outside";
import { vue3Debounce } from "vue-debounce";
import MISAResource from "../../js/base/resource";
import MISAapi from "@/js/api";
import MISAcommon from "../../js/base/common";
import MISAEnum from "../../js/base/enum";
import MessageChange from "../../components/message/MessageChange.vue";
import MessageError from "../../components/message/MessageError.vue";
import MessageDelete from "../../components/message/MessageDelete.vue";
import MessageDeleteMultiple from "../../components/message/MessageDeleteMultiple.vue";
import EmployeeDialog from "../forms/EmployeeDetail.vue";
import BaseToast from "../base/BaseToast.vue";
import BaseButton from "../base/button/BaseButton.vue";
import PageCombobox from "../base/PageCombobox.vue";
import BaseCheckBox from "../base/BaseCheckbox.vue";
import BaseTooltip from "../base/BaseTooltip.vue";
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
    MessageDelete,
    MessageDeleteMultiple,
    MessageError,
    BaseToast,
    MessageChange,
    BaseTooltip,
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
      focusSearch: false,
      isSuccess: true, // thành công
      btnMenuContext: null, // menu context
      isActive: false, //checkbox
      confirmDelete: false, // xác nhận xóa
      confirmDeleteMultiple: false, // xác nhận xóa hàng loạt
      message: "", // thông báo
      employeeIdSelected: null, //id của employee đc chọn
      countEmployeeSelected: 0, //Đếm số nhân viên đã chọn
      isAcceptSave: false, // xác nhận lưu
      rowSelected: [], // danh sách employee được chọn
      isCheckAll: false, // check all
      debounce: null, // debounce
      enableBatch: false, // cho phép thực hiện hàng loạt
      //isShowBatchOption: false, //hiện option hàng loạt
      isDuplicate: 0,
    };
  },
  watch: {
    // theo dõi keyword lọc
    txtSearch: function () {
      this.page = 1;
      this.pageNumber = 1;
      this.filterEmployee();
    },
    rowSelected: {
      handler: function () {
        // Nếu số dòng đã chọn lớn hơn  1
        if (this.rowSelected.length > 0) {
          this.enableBatch = true;
        } else {
          this.enableBatch = false;
          // this.isShowBatchOption = false;
        }
      },
      deep: true,
    },
  },
  methods: {
    /**
     * click bỏ chọn, reset list nhân viên đã chọn
     * Author: NHNam (24/2/2023)
     */
    resetRowSelected(){
      this.rowSelected = [],
      //tính toán lại
      this.testCheckAll();
    },

    /**
     * xử lý lỗi server trả về
     * @param statuscode} errorCode
     * Author: NHNam (22/2/2023)
     */
    handleErrorCode(errorCode) {
      // return MISAcommon.handleErrorCode(errorCode)
      if (errorCode === 400) {
        this.errForm = MISAResource.vi.errorServerResponse[400];
        this.isShowMessageError = true;
      } else if (errorCode === 500) {
        this.errForm = MISAResource.vi.errorServerResponse[500];
        this.isShowMessageError = true;
      } else {
        this.errForm = MISAResource.vi.errorServerResponse.otherCode;
        this.isShowMessageError = true;
      }
    },

    /**
     * lấy tên giới tính theo mã giới tính
     * @param {mã giới tính} gender
     * Author: NHNam(12/2/2023)
     */
    getGenderName(gender) {
      return MISAcommon.getTitleGender(gender);
    },
    /**
     * Call api xuất file excel
     * Author: NHNam(12/2/2023)
     */
    exportToExcel() {
      $("#loading").show();
      axios({
        url:
          MISAapi.employee.export +
          `pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&keyword=${this.txtSearch}`, // endpoint của API
        method: "GET",
        responseType: "blob", // định dạng dữ liệu trả về
      }).then((response) => {
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", "DS_Nhan_Vien.xlsx"); // tên file được download
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
        this.rowSelected = this.rowSelected.concat(
          this.employees
            .map((x) => x.EmployeeId)
            .filter((x) => !this.rowSelected.includes(x))
        );
        this.isCheckAll = true;
        // this.rowSelected = this.employees.map((item) => item.EmployeeId);
      } else if (id === "checkall" && !active) {
        // this.rowSelected = [];
        this.employees
          .map((x) => x.EmployeeId)
          .forEach((e) => {
            this.rowSelected = this.rowSelected.filter((x) => x != e);
          });
        this.isCheckAll = false;
      } else {
        if (active) {
          this.rowSelected.push(id);
        } else {
          this.rowSelected = this.rowSelected.filter((x) => x != id);
        }
      }
      // So sánh độ dài số dòng được chọn bằng số lượng bản ghi 1 trang thì check all
      // if (this.rowSelected.length === this.employees.length) {
      //   this.isCheckAll = true;
      // } else {
      //   this.isCheckAll = false;
      // }
      this.testCheckAll();
    },

    testCheckAll() {
      // Kiểm tra coi có check all hay không
      var count = 0;
      // Đếm số phần tử check của page
      this.employees
        .map((x) => x.EmployeeId)
        .forEach((e) => {
          if (this.rowSelected.filter((x) => x == e).length > 0) count++;
        });

      // Kiểm tra để check
      if (this.employees.length > 0 && count == this.employees.length) {
        this.isCheckAll = true;
      } else if (count < this.employees.length) {
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
      }, 3000);
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
    handleEditClick(item) {
      this.employeeIdSelected = item.EmployeeId;
      this.isShowDialog = true;
    },

    /**
     * Click nhân bản
     * Author: NHNam (20/2/2023)
     */
    handleDuplicate(item) {
      this.isDuplicate = MISAEnum.FormMode.Duplicate;
      this.handleEditClick(item);
    },
    /**confirmDelete
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
      if (this.rowSelected.length > 0) {
        this.rowSelected = this.rowSelected.filter(
          (item) => item != this.employeeSelected
        );
      }

      var me = this;
      setTimeout(function () {
        me.isShowToast = false;
      }, 3000);
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
    async filterEmployee() {
      $("#loading").show();
      var me = this;
      var url =
        MISAapi.employee.filter +
        `pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&keyword=${this.txtSearch}`;
      await axios
        .get(url)
        .then(async function (res) {
          me.totalPage = res.data.TotalPage;
          me.totalRecord = res.data.TotalRecord;
          me.isSuccess = true;
          me.employees = res.data.Data;
          $("#loading").hide();
        })
        .catch(function (res) {
          console.log(res.code);
          me.handleErrorCode(res.code);
        });
    },
    /**
     * Chuyển trang
     * Author: NHNam (1/1/2023)
     */
    async clickCallback(pageNum) {
      this.pageNumber = pageNum;
      // this.rowSelected = [];

      // this.isCheckAll = false;
      this.txtSearch = "";
      await this.filterEmployee();
      this.testCheckAll();
    },
    /**
     * Đổi size page
     * Author: NHNam (1/1/2023)
     */
    async sizeRecord(e) {
      this.pageSize = e;
      this.showPageOption();
      this.page = 1;
      this.pageNumber = 1;
      await this.filterEmployee();
      this.testCheckAll();
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
    /**
     * phím tắt mở form thêm mới
     * Author: NHNam (22/2/2023)
     */
    handleKeydown(event) {
      var me = this;
      if (event.ctrlKey && event.key == "1") {
        event.preventDefault();
        // event.stopPropagation();
        me.isShowDialog = true;
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
    document.addEventListener("keydown", this.handleKeydown);
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
<style scoped>
@import url(../../css/layout/content.css);
@import url(../../css/layout/footer.css);
@import url(../../css/base/combobox/combobox.css);
@import url(../../css/base/table/table.css);
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
