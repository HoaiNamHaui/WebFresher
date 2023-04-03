<template>
  <div class="pay-content">
    <div class="pay-master">
      <div class="pay-master-header">
        <div class="pay-header-left flex">
          <div class="pay-header-left-icon"></div>
          <div
            class="pay-batch"
            :class="{ 'pay-batch-active': isActiveBatch }"
            v-click-outside-element="closeBatch"
            @click="openBatch"
          >
            <div>Thực hiện hàng loạt</div>
            <div class="pay-batch-icon"></div>
            <div
              class="pay-batch-option"
              v-if="isActiveBatch"
              v-show="isOpenbatch"
              @click="deleteMuliple"
            >
              Xóa
            </div>
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
              placeholder="Tìm kiếm"
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
          <tr
            v-for="(item, index) in payments"
            v-bind:key="index"
            @click="getDetails(item)"
          >
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
              {{ formatMoney(item.TotalAmount) }}
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
              {{ formatMoney(sumTotalAmount) }}
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
            <tr v-for="(item, index) in paymentDetails" :key="index">
              <td class="align-center">{{ index + 1 }}</td>
              <td>{{ item.Description }}</td>
              <td>{{ item.DebitAccountNumber }}</td>
              <td>{{ item.CreditAccountNumber }}</td>
              <td class="align-right w150" style="padding-right: 8px">{{ formatMoney(item.Amount) }}</td>
              <td>{{ item.ObjectCode }}</td>
              <td>{{ item.ObjectName }}</td>
            </tr>
            <!-- <tr>
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
            </tr> -->
            <tr>
              <td></td>
              <td class="align-center" style="font-weight: bold">Tổng</td>
              <td></td>
              <td></td>
              <td
                class="align-right"
                style="padding-right: 8px; font-weight: bold"
              >
                {{ formatMoney(TotalAmountDetail) }}
              </td>
              <td></td>
              <td></td>
            </tr>
          </table>
        </div>
        <div class="context-menu" ref="menuContext">
          <div class="context-menu-item" @click="handleDuplicate">Nhân bản</div>
          <div class="context-menu-item" @click="ShowWarningDelete">Xóa</div>
        </div>
        <div class="pay-detail-paging">
          <div class="total-record">
            Tổng số:
            <span style="font-family: Notosans-SemiBold">{{
              totalRecordDetail
            }}</span>
            bản ghi
          </div>
          <div class="pagination">
            <div class="pages-option">
              {{ pageSizeDetail }} bản ghi trên 1 trang
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
    <MessageDelete
      v-if="isShowMessageDelete"
      @confirmDelete="confirmDelete"
      @cancelDelete="isShowMessageDelete = false"
      messageTitle="Xóa chứng từ"
      type-object="chứng từ"
      :object-code="paymentSelected.RefNo"
    />
    <message-delete-multiple
      v-if="confirmDeleteMultiple"
      :rowSelected="rowSelected"
      @cancelDeleteMultiple="confirmDeleteMultiple = false"
      @handleDeleteMultiple="handleDeleteMultiple"
    />
  </div>
</template>
<script>
import BaseTooltip from "../base/BaseTooltip.vue";
import BaseSmallButton from "../base/button/BaseSmallButton.vue";
import MessageDelete from "../message/MessageDelete.vue";
import MessageDeleteMultiple from "../../components/message/MessageDeleteMultiple.vue";
// import PayMoney from "../forms/pay/PayMoney.vue";
import BaseCheckbox from "../base/BaseCheckbox.vue";
import PageCombobox from "../base/PageCombobox.vue";
import commonJS from "@/js/base/common";
import axios from "axios";
import $ from "jquery";
import MISAapi from "@/js/api";
import BaseLoading from "../base/BaseLoading.vue";
// import $ from "jquery";
import Paginate from "vuejs-paginate-next";
import MISAEnum from '@/js/base/enum';
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
    MessageDelete,
    MessageDeleteMultiple,
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
      paymentSelected: {},
      isShowMessageDelete: false,
      confirmDeleteMultiple: false,
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
      isActiveBatch: false,
      isOpenbatch: false,
      paymentDetails: [],
      TotalAmountDetail: 0
    };
  },
  watch: {
    txtSearch: async function () {
      this.page = 1;
      this.pageNumber = 1;
      await this.filterPayment();
      this.testCheckAll();
    },
    rowSelected: {
      handler: function () {
        if (this.rowSelected.length > 1) {
          this.isActiveBatch = true;
        } else {
          this.isActiveBatch = false;
        }
      },
      deep: true,
    },
  },
  methods: {
    /**
     * Lấy payment detail chi chọn dòng
     * Author: NHNam (27/3/2023)
     */
    getDetails(item) {
      var me = this;
      this.paymentSelected = item;
      axios
        .get(MISAapi.paymentDetail.base + "GetByPaymentIdAndPaging?ParentId=" + item.PaymentId + `&pageSize=${this.pageSizeDetail}&pageNumber=${this.pageNumberDetail}`)
        .then((res) => {
          console.log(res);
          me.totalPageDetail = res.data.TotalPage;
          me.totalRecordDetail = res.data.TotalRecord;
          me.paymentDetails = res.data.Data;
          this.TotalAmountDetail = 0;
          this.paymentDetails.forEach(element => {
            this.TotalAmountDetail += element.Amount;
          });
        });
    },

    /**
     * gọi api xóa hàng loạt
     * Author: NHNam (7/1/2023)
     */
    async handleDeleteMultiple() {
      var data = this.rowSelected;
      try {
        await axios.delete(MISAapi.payment.deleteMultiple, { data });
        this.confirmDeleteMultiple = false;
        this.filterPayment();
        this.rowSelected = [];
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * hiện thông báo xác nhận xóa
     * Author: NHNam (7/1/2023)
     */
    deleteMuliple() {
      this.confirmDeleteMultiple = true;
    },

    /**
     * Cảnh báo xóa
     * Author: NHNam
     */
    ShowWarningDelete() {
      this.isShowMessageDelete = true;
    },
    
    /**
     * Xác nhận xóa
     * Author: NHNam (203/2023)
     */
    confirmDelete() {
      var me = this;
      try {
        var url = MISAapi.payment.base + me.paymentSelected.PaymentId;
        me.isLoading = true;
        me.isShowMessageDelete = false;
        axios
          .delete(url)
          .then(() => {
            me.filterPayment();
            me.isLoading = false;
          })
          .catch((res) => {
            me.isLoading = false;
            console.log(res);
          });
      } catch (error) {
        me.isLoading = false;
        console.log(error);
      }
    },

    /**
     * Call api xuất file excel
     * Author: NHNam(12/2/2023)
     */
    exportToExcel() {
      this.isLoading = true;
      axios({
        url:
          MISAapi.payment.base +
          "ExportExcel?" +
          `pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&keyword=${this.txtSearch}`, // endpoint của API
        method: "GET",
        responseType: "blob", // định dạng dữ liệu trả về
      }).then((response) => {
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", "DS_Phieu_Chi.xlsx"); // tên file được download
        document.body.appendChild(link);
        link.click();
        this.isLoading = false;
      });
    },

    /**
     * Mở chọn thực hiện hàng loại
     * Author: NHNam (20/3/2023)
     */
    openBatch() {
      if (this.isActiveBatch) {
        this.isOpenbatch = !this.isOpenbatch;
      }
    },
    /**
     * Đóng chọn thực hiện hàng loại
     * Author: NHNam (20/3/2023)
     */
    closeBatch() {
      this.isOpenbatch = false;
    },
    /**
     * click sửa
     * Author: NHNam (20/3/2023)
     */
    handleEditClick(id) {
      this.$router.push({ path: "/PayMoney", query: { id: id } });
    },
    /**
     * Nhân bản
     * Author: NHNam
     */
     handleDuplicate(){
      var me = this;
      this.$router.push({ path: "/PayMoney", query: { id: me.paymentSelected.PaymentId, formMode: MISAEnum.FormMode.Duplicate } });
    },
    /**
     * chuyển trang form detail
     * Author: NHNam (20/3/2023)
     */
    redirectPayMoney() {
      this.$router.push("/PayMoney");
    },

    /**
     * làm mới danh sách phiếu chi
     * Author: NHNam (20/3/2023)
     */
    refreshListpayment() {
      this.filterPayment();
      this.testCheckAll();
    },
    /**
     * debounce lấy giá trị input search
     * Author: NHNam (20/3/2023)
     */
    debounceSearch(e) {
      this.txtSearch = e;
    },

    /**
     * format ngày
     * Author: NHNam (20/3/2023)
     */
    formatDate(date) {
      return commonJS.formatDate(date);
    },

    /**
     * chuyển trang
     * Author: NHNam (20/3/2023)
     */
    async clickCallback(pageNum) {
      this.pageNumber = pageNum;
      this.txtSearch = "";
      await this.filterPayment();
      this.testCheckAll();
    },
    /**
     * Mở chọn số bản ghi
     * Author: NHNam (20/3/2023)
     */
    showPageOption() {
      // $("#cbx-icon").toggleClass("toogle-rotate");
      if (!this.isShowFooterCbb) {
        this.showPageFooterOption(true);
      } else {
        this.isShowFooterCbb = false;
      }
    },
    /**
     * đóng chọn số bản ghi
     * Author: NHNam (20/3/2023)
     */
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
    /**
     * chuyển trang detail
     * Author: NHNam (20/3/2023)
     */
    async clickCallbackDetail(pageNum) {
      this.pageNumberDetail = pageNum;
      // this.rowSelected = [];

      // this.isCheckAll = false;
      this.txtSearchDetail = "";
    },
    /**
     * mở chọn số bản ghi detail
     * Author: NHNam (20/3/2023)
     */
    showPageOptionDetail() {
      // $("#cbx-icon-detail").toggleClass("toogle-rotate");
      if (!this.isShowFooterCbbDetail) {
        this.showPageFooterOptionDetail(true);
      } else {
        this.isShowFooterCbbDetail = false;
      }
    },
    /**
     * đóng chọn số bản ghi
     * Author: NHNam (20/3/2023)
     */
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
    /**
     * lọc phân trang payment
     * Author: NHNam (20/3/2023)
     */
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
          me.sumTotalAmount = me.sumTotalAmount.toFixed(4);
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

    /**
     * kiểm tra check all hay không
     * Author: NHNam (20/3/2023)
     */
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
    /**
     * Toggle context menu
     * Author: NHNam (27/3/2023)
     */
    toogleMenu(e) {
      if (this.$refs.menuContext.style.display === "block") {
        $(".context-menu").hide(100);
      } else {
        this.$refs.menuContext.style.display;
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
      }
    },
    /**
     * format tiền
     * @param {số tiền} number
     * Author: NHNam (22/3/2023)
     */
     formatMoney(number) {
      if (number) {
        return new Intl.NumberFormat().format(number);
      }
      return 0;
    },

    /**
     * phím tắt mở form thêm mới
     * Author: NHNam (22/2/2023)
     */
     handleKeydown(event) {
      var me = this;
      if (event.ctrlKey && event.key == "1") {
        event.preventDefault();
        me.redirectPayMoney();
      }
    },
  },
  async created() {
    this.isShowForm = this.openForm;
    await this.filterPayment();
    if(this.payments[0].PaymentId){
      this.getDetails(this.payments[0]);
    }
  },
  mounted() {
    document.addEventListener("keydown", this.handleKeydown);
  },
};
</script>

<style scoped>
@import url(../../css/pay/pay.css);
@import url(../../css/base/contextmenu.css);
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
