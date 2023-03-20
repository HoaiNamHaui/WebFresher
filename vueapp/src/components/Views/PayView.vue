<template>
  <div class="pay-content">
    <div class="pay-master">
      <div class="pay-master-header">
        <div class="pay-header-left flex">
          <div class="pay-header-left-icon"></div>
          <div class="pay-batch">
            <div>Thực hiện hàng loạt</div>
            <div class="pay-batch-icon"></div>
          </div>
        </div>
        <div class="pay-header-right">
          <div
            class="search-box search-box-custom"
            :class="{ focus: this.focusSearch }"
          >
            <input
              @focus="this.focusSearch = true"
              @blur="this.focusSearch = false"
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
              v-debounce:1s="debounceSearch"
              class="pay-search-custom"
            />
            <div class="search-icon pay-icom-search-custom"></div>
          </div>
          <div
            class="refresh"
            style="position: relative"
            @click="refreshListpayment"
          >
            <base-tooltip message="Làm mới danh sách" />
          </div>
          <div
            class="excel"
            @click="exportToExcel"
            style="position: relative; margin-right: 20px"
          >
            <base-tooltip message="Xuất Excel" />
          </div>
          <base-small-button
            btnName="Chi tiền"
            @click="redirectPayMoney"
            style="margin-right: 12px"
          />
        </div>
      </div>
      <div class="pay-data-master">
        <table class="pay-master-data-table" cellspacing="0">
          <tr>
            <th class="w50px">
              <base-checkbox
                ref="checkAll"
                id="checkall"
                :checked="isCheckAll"
                @changeCheckbox="changeCheckbox"
              />
            </th>
            <th class="align-center w150">Ngày hạch toán</th>
            <th class="align-center w150">Ngày chứng từ</th>
            <th class="align-left w130">Số chứng từ</th>
            <th class="align-left w200">Diễn giải</th>
            <th class="align-right" style="padding-right: 8px">Số tiền</th>
            <th class="align-left w150">Mã đối tượng</th>
            <th class="align-left w200">Đối tượng</th>
            <th class="align-left w300">Địa chỉ</th>
            <th class="align-center w150">Chức năng</th>
          </tr>
          <tr v-for="(item, index) in payments" v-bind:key="index">
            <td class="w50px">
              <base-checkbox
                :checked="rowSelected.includes(item.PaymentId)"
                :id="item.PaymentId"
                @changeCheckbox="changeCheckbox"
              />
            </td>
            <td class="align-center w150">{{ formatDate(item.RefDate) }}</td>
            <td class="align-center w150">{{ formatDate(item.PostedDate) }}</td>
            <td class="w130">{{ item.RefNo }}</td>
            <td class="w200">{{ item.Reason }}</td>
            <td class="align-right w150" style="padding-right: 8px">
              {{ item.TotalAmount }}
            </td>
            <td class="w150">{{ item.ObjectCode }}</td>
            <td class="w200">{{ item.ObjectName }}</td>
            <td class="w300">{{ item.Address }}</td>
            <td class="flex w150" style="justify-content: center">
              <span
                style="cursor: pointer; color: #0075c0; font-weight: bold"
                @click="handleEditClick(item.PaymentId)"
                >Xem</span
              >
              <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td></td>
            <td class="align-center" style="font-weight: bold">Tổng</td>
            <td></td>
            <td></td>
            <td></td>
            <td
              class="align-right"
              style="padding-right: 8px; font-weight: bold"
            >
              {{ sumTotalAmount }}
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
        </table>
      </div>
      <div class="pay-master-paging">
        <div class="total-record">
          Tổng số:
          <span style="font-family: Notosans-SemiBold">{{ totalRecord }}</span>
          bản ghi
        </div>
        <div class="pagination">
          <div class="pages-option">
            {{ pageSize }} bản ghi trên 1 trang
            <div
              class="box-icon"
              @click="showPageOption"
              v-click-outside-element="close"
            >
              <button
                id="cbx-icon"
                :class="{ 'toogle-rotate': isShowFooterCbb }"
                class="cbx-icon"
              ></button>
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
      </div>
    </div>
    <div class="pay-detail">
      <div class="pay-detail-header">
        <div class="pay-detail-control">Chi tiết</div>
        <div class="pay-detail-data">
          <table cellspacing="0" class="pay-detail-table">
            <tr>
              <th class="w50px">#</th>
              <th class="w400 align-left">Diễn giải</th>
              <th class="w150 align-left">TK Nợ</th>
              <th class="w150 align-left">TK Có</th>
              <th class="w150 align-right" style="padding-right: 8px">
                Số tiền
              </th>
              <th class="w150 align-left">Đối tượng</th>
              <th class="w400 align-left">Tên đối tượng</th>
            </tr>
            <tr>
              <td class="align-center">1</td>
              <td>Chi tiền cho CÔNG TY TNHH ABCBOOK</td>
              <td>1121</td>
              <td>1111</td>
              <td class="align-right w150" style="padding-right: 8px">0</td>
              <td>NCC00017</td>
              <td>CÔNG TY TNHH ABCBOOK</td>
            </tr>
            <tr>
              <td class="align-center">1</td>
              <td>Chi tiền cho CÔNG TY TNHH ABCBOOK</td>
              <td>1121</td>
              <td>1111</td>
              <td class="align-right w150" style="padding-right: 8px">0</td>
              <td>NCC00017</td>
              <td>CÔNG TY TNHH ABCBOOK</td>
            </tr>
            <tr>
              <td class="align-center">1</td>
              <td>Chi tiền cho CÔNG TY TNHH ABCBOOK</td>
              <td>1121</td>
              <td>1111</td>
              <td class="align-right w150" style="padding-right: 8px">0</td>
              <td>NCC00017</td>
              <td>CÔNG TY TNHH ABCBOOK</td>
            </tr>
            <tr>
              <td></td>
              <td class="align-center" style="font-weight: bold">Tổng</td>
              <td></td>
              <td></td>
              <td
                class="align-right"
                style="padding-right: 8px; font-weight: bold"
              >
                0
              </td>
              <td></td>
              <td></td>
            </tr>
          </table>
        </div>
        <div class="pay-detail-paging">
          <div class="total-record">
            Tổng số:
            <span style="font-family: Notosans-SemiBold">{{
              totalRecord
            }}</span>
            bản ghi
          </div>
          <div class="pagination">
            <div class="pages-option">
              {{ pageSize }} bản ghi trên 1 trang
              <div
                class="box-icon"
                @click="showPageOptionDetail"
                v-click-outside-element="closeDetail"
              >
                <button
                  id="cbx-icon-detail"
                  :class="{ 'toogle-rotate': isShowFooterCbbDetail }"
                  class="cbx-icon"
                ></button>
              </div>
              <page-combobox
                :isShowCbb="isShowFooterCbbDetail"
                @sizeRecord="sizeRecordDetail"
              />
            </div>
            <Paginate
              v-model="pageDetail"
              :page-count="totalPageDetail"
              :page-range="2"
              :margin-pages="1"
              :click-handler="clickCallbackDetail"
              :prev-text="'Trước'"
              :next-text="'Sau'"
              :container-class="'pagination'"
              :page-class="'page-item'"
            />
          </div>
        </div>
      </div>
    </div>
    <!-- <pay-money v-if="isShowForm" @closeForm="isShowForm = false" /> -->
    <BaseLoading v-if="isLoading" />
  </div>
</template>
<script>
import BaseTooltip from "../base/BaseTooltip.vue";
import BaseSmallButton from "../base/button/BaseSmallButton.vue";
// import PayMoney from "../forms/pay/PayMoney.vue";
import BaseCheckbox from "../base/BaseCheckbox.vue";
import PageCombobox from "../base/PageCombobox.vue";
import commonJS from "@/js/base/common";
import axios from "axios";
import MISAapi from "@/js/api";
import BaseLoading from "../base/BaseLoading.vue";
// import $ from "jquery";
import Paginate from "vuejs-paginate-next";
export default {
  name: "PayView",
  components: {
    BaseTooltip,
    BaseSmallButton,
    // PayMoney,
    BaseCheckbox,
    PageCombobox,
    Paginate,
    BaseLoading,
  },
  props: ["openForm"],
  data() {
    return {
      isCheckAll: false,
      isActive: false,
      rowSelected: [],
      isLoading: false,
      isShowForm: false,
      payments: [],
      pageSize: 20,
      pageNumber: 1, // trang hiện tại
      txtSearch: "", // keyword lọc
      totalPage: 0, // tổng số trang
      totalRecord: 0, // tổng số bản ghi
      page: 1,
      sumTotalAmount: 0,
      isShowFooterCbb: false,
      focusSearch: false,
      pageSizeDetail: 20,
      pageNumberDetail: 1, // trang hiện tại
      txtSearchDetail: "", // keyword lọc
      totalPageDetail: 0, // tổng số trang
      totalRecordDetail: 0, // tổng số bản ghi
      pageDetail: 1,
      isShowFooterCbbDetail: false,
    };
  },
  watch: {
    txtSearch: async function () {
      this.page = 1;
      this.pageNumber = 1;
      await this.filterPayment();
      this.testCheckAll();
    },
  },
  methods: {

    handleEditClick(id){
      this.$router.push({ path: '/PayMoney', params: { id: id }})
    },
    // chuyển trang form detail
    redirectPayMoney(){
      this.$router.push('/PayMoney')
    },

    //Làm mới list payment
    refreshListpayment(){
      this.filterPayment();
      this.testCheckAll();
    },
    //delay
    debounceSearch(e) {
      this.txtSearch = e;
    },

    //format ngày
    formatDate(date) {
      return commonJS.formatDate(date);
    },

    //Chuyển trang
    async clickCallback(pageNum) {
      this.pageNumber = pageNum;
      this.txtSearch = "";
      await this.filterPayment();
      this.testCheckAll();
    },
    showPageOption() {
      // $("#cbx-icon").toggleClass("toogle-rotate");
      if (!this.isShowFooterCbb) {
        this.showPageFooterOption(true);
      } else {
        this.isShowFooterCbb = false;
      }
    },
    //đóng chọn số bản ghi
    close() {
      this.isShowFooterCbb = false;
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
      await this.filterPayment();
      this.testCheckAll();
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
    async clickCallbackDetail(pageNum) {
      this.pageNumberDetail = pageNum;
      // this.rowSelected = [];

      // this.isCheckAll = false;
      this.txtSearchDetail = "";
    },
    showPageOptionDetail() {
      // $("#cbx-icon-detail").toggleClass("toogle-rotate");
      if (!this.isShowFooterCbbDetail) {
        this.showPageFooterOptionDetail(true);
      } else {
        this.isShowFooterCbbDetail = false;
      }
    },
    //đóng chọn số bản ghi
    closeDetail() {
      this.isShowFooterCbbDetail = false;
    },
    /**
     * Đổi size page
     * Author: NHNam (1/1/2023)
     */
    async sizeRecordDetail(e) {
      this.pageSize = e;
      this.showPageOptionDetail();
      this.page = 1;
      this.pageNumber = 1;
      // this.testCheckAll();
    },
    /**
     * Hiện , ẩn chọn số bản ghi trên 1 trang
     * Author: Nguyễn Hoài Nam(1/1/2023)
     * isShowFooterCbb = true: hiển thị
     * isShowFooterCbb = false: ẩn
     */
    showPageFooterOptionDetail(isShowCbb) {
      if (isShowCbb) {
        this.isShowFooterCbbDetail = isShowCbb;
      }
    },
    //lọc phân trang payment
    async filterPayment() {
      this.sumTotalAmount = 0;
      this.isLoading = true;
      var me = this;
      var url =
        MISAapi.payment.filter +
        `pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&keyword=${this.txtSearch}`;
      await axios
        .get(url)
        .then(function (res) {
          me.totalPage = res.data.TotalPage;
          me.totalRecord = res.data.TotalRecord;
          me.payments = res.data.Data;
          me.payments.forEach((element) => {
            me.sumTotalAmount += element.TotalAmount;
          });
          me.isLoading = false;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    /**
     * Check All , uncheck CheckBox
     * Author: NHNam (10/1/2023)
     */
    changeCheckbox(active, id) {
      // Check all
      if (id === "checkall" && active) {
        this.rowSelected = this.rowSelected.concat(
          this.payments
            .map((x) => x.PaymentId)
            .filter((x) => !this.rowSelected.includes(x))
        );
      } else if (id === "checkall" && !active) {
        this.payments
          .map((x) => x.PaymentId)
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
      this.testCheckAll();
    },

    testCheckAll() {
      // Kiểm tra có check all hay không
      var count = 0;
      // Đếm số phần tử check của page
      this.payments
        .map((x) => x.PaymentId)
        .forEach((e) => {
          if (this.rowSelected.filter((x) => x == e).length > 0) count++;
        });

      // Kiểm tra để check
      if (this.payments.length > 0 && count == this.payments.length) {
        this.isCheckAll = true;
      } else if (count < this.payments.length) {
        this.isCheckAll = false;
      }
    },
  },
  created() {
    this.isShowForm = this.openForm;
    this.filterPayment();
  },
};
</script>

<style scoped>
@import url(../../css/pay/pay.css);
.pay-search-custom {
  height: 26px !important;
  border: none;
  outline: none;
}
.search-box-custom {
  height: 26px !important;
  overflow: hidden;
  font-size: 13px !important;
}
.search-box-custom::placeholder {
  font-size: 13px !important;
}
.pay-icom-search-custom {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: unset;
  padding: 0 12px;
  background-position: -987px -360px;
}
</style>
