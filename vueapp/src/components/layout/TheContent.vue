<template>
  <div class="content" id="content">
    <div class="content-top">
      <div class="title">Nhân viên</div>
      <base-button @click="isShowDialog = true" />
    </div>
    <div class="content-data">
      <div class="functions">
        <div class="search-box">
          <input
            v-model="txtSearch"
            type="text"
            placeholder="Tìm theo mã, tên nhân viên"
          />
          <div class="search-icon"></div>
        </div>
        <div class="refresh"></div>
        <div class="excel"></div>
      </div>
      <div class="list-employee">
        <table class="data" id="tblEmployee" cellspacing="0">
          <tr>
            <th class="col60"><input type="checkbox" name="" id="" /></th>
            <th class="col150 align-left">MÃ NHÂN VIÊN</th>
            <th class="col250 align-left">TÊN NHÂN VIÊN</th>
            <th class="col150 align-center">NGÀY SINH</th>
            <th class="col150 align-left">GIỚI TÍNH</th>
            <th class="col150 align-left">SỐ ĐIỆN THOẠI</th>
            <th class="col250 align-left">CHỨC DANH</th>
            <th class="col150 align-left">Số CMND</th>
            <th class="col250 align-left">TÊN ĐƠN VỊ</th>
            <th class="col250 align-left">SỐ TÀI KHOẢN</th>
            <th class="col250 align-left">TÊN NGÂN HÀNG</th>
            <th class="col250 align-left">CHI NHÁNH</th>
            <th class="col200">CHỨC NĂNG</th>
          </tr>
          <tr v-for="employee in employees" v-bind:key="employee.EmployeeId">
            <td><input type="checkbox" name="" id="" /></td>
            <td>{{ employee.EmployeeCode }}</td>
            <td>{{ employee.FullName }}</td>
            <td class="align-center">
              {{ formatDate(employee.DateOfBirth) }}
            </td>
            <td>{{ employee.GenderName }}</td>
            <td>{{ employee.PhoneNumber }}</td>
            <td>{{ employee.PositionName }}</td>
            <td>{{ employee.IdentityNumber }}</td>
            <td>{{ employee.DepartmentName }}</td>
            <td>{{ employee.AccountNumber }}k</td>
            <td>{{ employee.BankProvinceName }}</td>
            <td>{{ employee.BankBranchName }}</td>
            <td>
              Sửa
              <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
        </table>
        <div class="context-menu">
          <div class="context-menu-item">Nhân bản</div>
          <div class="context-menu-item">Xóa</div>
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
            :page-range="3"
            :margin-pages="2"
            :click-handler="clickCallback"
            :prev-text="'Trước'"
            :next-text="'Sau'"
            :container-class="'pagination'"
            :page-class="'page-item'"
          />
        </div>
      </footer>
    </div>
  </div>
  <employee-dialog v-if="isShowDialog" @onClose="isShowDialog = false" />
</template>
<script>
import EmployeeDialog from './EmployeeDetail.vue'
import BaseButton from "../Base/button/BaseButton.vue";
import PageCombobox from "../Base/PageCombobox.vue"
import Paginate from "vuejs-paginate-next";
import $ from "jquery";
import axios from "axios";
export default {
  name: "TheContent",

  components: {
    BaseButton, EmployeeDialog, Paginate, PageCombobox
  },
  data() {
    return {
      isShowDialog: false,
      isShowFooterCbb: false,
      employees: null,
      pageSize: 10,
      pageNumber: 1,
      txtSearch: "",
      totalPage: 0,
      totalRecord: 0,
      page: 1,
      newEmployeeCode: null,
      isSuccess: true,
      btnMenuContext: null
    };
  },
  watch: {
    //theo dõi ô search để tìm kiếm
    txtSearch: function(){
      this.filterEmployee();
    }
  },
  methods: {
    /**
     * Tìm kiếm, phân trang
     * Author: NHNam (1/1/2023)
     */
    filterEmployee() {
      $("#loading").show();
      var me = this;
      var url = `https://cukcuk.manhnv.net/api/v1/Employees/filter?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&employeeFilter=${this.txtSearch}`;
      axios.get(url).then(function (res) {
        me.totalPage = res.data.TotalPage;
        me.totalRecord = res.data.TotalRecord;
        me.isSuccess = true;
        me.employees = res.data.Data;
        console.log(me.employees);
        $("#loading").hide();
      });
    },
    /**
     * Chuyển trang
     * Author: NHNam (1/1/2023)
     */
    clickCallback(pageNum) {
      this.pageNumber = pageNum;
      this.filterEmployee();
    },
    /**
     * Đổi size page
     * Author: NHNam (1/1/2023)
     */
    sizeRecord(e) {
      this.pageSize = e;
      this.showPageOption();
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
      console.log(x,y);
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
</style>
