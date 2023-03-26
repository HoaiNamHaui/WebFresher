<template>
  <div class="pay-form-container">
    <div class="header-pay-form">
      <div class="header-pay-form-left">
        <div class="icon-recent-log"></div>
        <div class="pay-form-title">Phiếu chi {{ payment.RefNo }}</div>
        <div class="w40">
          <BaseComboboxV2
            :data="dataPayType"
            v-model="payment.ReasonType"
            propName="text"
            propValue="value"
          />
        </div>
        <!-- <BaseComboboxV2 :data="dataPayType" v-model="dataPayTypeDefault"/> -->
      </div>
      <div class="header-pay-form-right" @click="closeForm">
        <div class="icon-close" style="margin: 6px 8px"></div>
      </div>
    </div>
    <div class="pay-form-enter flex" style="align-items: unset">
      <div class="pay-form-enter-left">
        <div class="pay-form-left-first-line flex w100">
          <div class="w45">
            <label for="">Mã đối tượng</label>
            <!-- <base-combobox-read-only style="margin-bottom: 8px" /> -->
            <BaseComboboxTable
              :listTitle="objectTilte"
              :api="objectApi"
              v-model="payment.ObjectId"
              style="margin-top: unset !important; margin-bottom: 8px"
              prop-name="ObjectCode"
              prop-value="ObjectId"
              @getObjectName="getObjectName"
            />
          </div>
          <base-small-input
            label="Tên đối tượng"
            class="custom-padding-input w55"
            v-model="payment.ObjectName"
            @getTempReson="getTempReson"
          />
        </div>
        <div class="pay-form-left-second-line flex w100">
          <base-small-input
            label="Người nhận"
            class="custom-padding-input w45"
            v-model="payment.Reciver"
          />
          <base-small-input
            label="Địa chỉ"
            class="custom-padding-input w55"
            v-model="payment.Address"
          />
        </div>
        <div class="pay-form-left-third-line flex w100">
          <base-small-input
            label="Lý do chi"
            class="w100"
            style="margin-right: 12px"
            v-model="payment.Reason"
            @checkChangeByUser="checkChangeByUser"
          />
        </div>
        <div class="pay-form-left-fourth-line flex w100">
          <div class="w40">
            <label for="">Nhân viên</label>
            <!-- <base-combobox-read-only style="margin-bottom: 8px" /> -->
            <BaseComboboxTable
              :listTitle="employeeTitle"
              :api="employeeApi"
              v-model="payment.EmployeeId"
              style="margin-top: unset !important; margin-bottom: 8px"
              prop-name="EmployeeCode"
              prop-value="EmployeeId"
            />
          </div>
          <base-small-input
            class="w20"
            label="Kèm theo"
            v-model="payment.Attachment"
            placeholder-input="Số lượng"
            text-align="right"
          />
          <div style="padding-top: 20px; padding-left: 6px">chứng từ gốc</div>
        </div>
        <div>Tham chiếu</div>
      </div>
      <div
        class="pay-form-enter-right w100 flex"
        style="align-items: unset !important"
      >
        <div class="w50">
          <div
            class="pay-form-right-first-line w60"
            style="margin-bottom: 8px; margin-left: 12px"
          >
            <label for="">Ngày hạch toán</label><br />
            <!-- <input :class="{'text-gray': true}" type="date" class="input-small w100" /> -->
            <base-datepicker
              date-name="postedDate"
              v-model="payment.PostedDate"
              @changeDate="handleDateChange"
            />
          </div>
          <div
            class="pay-form-right-first-line w60"
            style="margin-bottom: 8px; margin-left: 12px"
          >
            <label for="">Ngày Phiếu chi</label><br />
            <!-- <input :class="{'text-gray': true}" type="date" class="input-small w100" /> -->
            <base-datepicker
              dateName="refDate"
              v-model="payment.RefDate"
              @changeDate="handleRefDateChange"
            />
          </div>
          <div
            class="pay-form-right-first-line w60"
            style="margin-left: 12px; max-width: 150px"
          >
            <base-small-input
              :error="errors.refNo"
              label="Số phiếu chi"
              v-model="payment.RefNo"
            />
          </div>
        </div>
        <div style="text-align: right" class="w50">
          <div>Tổng tiền</div>
          <div style="font-size: 36px; font-weight: bold">
            {{ payment.TotalAmount }}
          </div>
        </div>
      </div>
    </div>
    <div class="pay-form-detail w100">
      <span style="font-family: Notosans-SemiBold">Hạch toán</span>
      <table class="w100 pay-data-detail" cellspacing="0">
        <tr>
          <th>#</th>
          <th class="align-left">Diễn giải</th>
          <th class="align-left">TK nợ</th>
          <th class="align-left">TK có</th>
          <th class="align-right">Số tiền</th>
          <th class="align-left">Đối tượng</th>
          <th class="align-left">Tên đối tượng</th>
          <th></th>
        </tr>
        <tr v-for="(detail, index) in paymentDetails" :key="index">
          <td>{{ index + 1 }}</td>
          <td><input type="text" v-model="detail.Description" /></td>
          <td>
            <!-- <input type="text" /> -->
            <base-combobox-table
              :api="api"
              style="margin-top: unset !important"
              prop-value="AccountId"
              prop-name="AccountNumber"
              :list-title="listTitle"
              v-model="detail.DebitAccount"
            />
          </td>
          <td>
            <!-- <input type="text" value="111" /> -->
            <base-combobox-table
              :api="api"
              style="margin-top: unset !important"
              prop-value="AccountId"
              prop-name="AccountNumber"
              :list-title="listTitle"
              v-model="detail.CreditAccount"
            />
          </td>
          <td>
            <input
              type="text"
              v-model="detail.Amount"
              style="text-align: right"
            />
          </td>
          <td><input type="text" v-model="detail.ObjectName" /></td>
          <td><input type="text" /></td>
          <td><div class="icon-delete"></div></td>
        </tr>
        <tr>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td style="text-align: right; font-size: 14px">0</td>
          <td></td>
          <td></td>
          <td></td>
        </tr>
      </table>
      <div class="pay-control-detail">
        <button
          class="button-white"
          id="btnCancel"
          style="height: 24px !important; line-height: 24px; font-size: 12px"
          @click="addLine"
        >
          Thêm dòng
        </button>
        <button
          class="button-white"
          id="btnCancel"
          style="height: 24px !important; line-height: 24px; font-size: 12px"
          @click="deleteAllLine"
        >
          Xóa hết dòng
        </button>
      </div>
      <div class="pay-form-footer">
        <button
          style="margin-left: 20px"
          class="small-button-white small-button"
          id="btnCancel"
          @click="closeForm"
        >
          Hủy
        </button>
        <div style="display: flex; align-items: center; padding-right: 20px">
          <button class="small-button-white small-button" @click="savePayment">
            Cất
          </button>
          <base-small-button btnName="Cất và Thêm" />
        </div>
      </div>
    </div>
    <message-error v-if="isError" :error="error" @close="isError = false" />
  </div>
</template>
<script>
// import BaseComboboxReadOnly from "../../base/BaseComboboxReadOnly.vue";
import BaseComboboxV2 from "../../base/BaseComboboxV2.vue";
import BaseSmallInput from "../../base/input/BaseSmallInput.vue";
import BaseSmallButton from "../../base/button/BaseSmallButton.vue";
import BaseDatepicker from "@/components/base/BaseDatepicker.vue";
import paymentData from "@/js/payment/payment";
import ObjectTitle from "@/js/object/object";
import EmployeeTitle from "@/js/employee/employee";
import MISAapi from "@/js/api";
import accountData from "@/js/account/account";
import BaseComboboxTable from "@/components/base/BaseComboboxTable.vue";
import MessageError from "@/components/message/MessageError.vue";
import MISAResource from "@/js/base/resource";
import axios from "axios";
export default {
  name: "PayForm",
  components: {
    // BaseComboboxReadOnly,
    MessageError,
    BaseSmallInput,
    BaseSmallButton,
    BaseComboboxV2,
    BaseDatepicker,
    BaseComboboxTable,
  },
  data() {
    return {
      error: "",
      isError: false,
      dataPayType: paymentData.options,
      payment: {
        RefNo: "",
        ReasonType: paymentData.options[6].value,
        PostedDate: new Date(),
        RefDate: new Date(),
        Reason: "Chi tiền cho ",
      },
      objectApi: MISAapi.object.base,
      objectTilte: ObjectTitle.listTitle,
      employeeApi:
        MISAapi.employee.filter +
        `pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`,
      employeeTitle: EmployeeTitle.listTitle,

      errors: {
        postedDate: "",
        refDate: "",
        refNo: "",
      },
      isValid: false,
      isSameDate: true,
      listTitle: accountData.listTitle,
      api: MISAapi.account.base,
      paymentDetails: [
        {
          Description: MISAResource.vi.payment.reason,
        },
      ],
      isChangeByUser: false,
      reasonTemp: "",
    };
  },
  watch: {},
  created() {
    const id = this.$route.query.id;
    if (id) {
      var url = MISAapi.payment.base + id;
      axios
        .get(url)
        .then((res) => {
          console.log(res.data);
          this.payment = res.data;
        })
        .catch((error) => {
          console.log(error);
        });
    } else {
      this.getNewRefNo();
    }
  },
  methods: {
    /**
     * Xóa hết dòng
     * Author: NHNam(22/3/2023)
     */
    deleteAllLine() {
      this.paymentDetails = [
        {
          Description: MISAResource.vi.payment.reason,
        },
      ];
    },

    /**
     * Thêm dòng
     * Author: NHNam(22/3/2023)
     */
    addLine() {
      var numberOfDetail = this.paymentDetails.length;
      this.paymentDetails = [
        ...this.paymentDetails,
        this.paymentDetails[numberOfDetail - 1],
      ];
    },

    /**
     * Lấy số phiếu chi mới
     * Author: NHNam (24/3/2023)
     */
    async getNewRefNo() {
      var url = MISAapi.payment.base + "NewRefNo";
      axios
        .get(url)
        .then((res) => {
          this.payment.RefNo = res.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Nhận lý do chi tạm thời
     * Author: NHNam (24/3/2023)
     */
    getTempReson(e) {
      this.reasonTemp = MISAResource.vi.payment.reason + e;
      if (!this.isChangeByUser) {
        this.payment.Reason = this.reasonTemp;
      }
    },
    /**
     * Lưu trạng thái người dùng đã thay đổi lý do
     * @param {} e
     */
    checkChangeByUser(e) {
      if (e !== MISAResource.vi.payment.reason) {
        this.isChangeByUser = true;
      }
    },

    /**
     * Nhận ObjectName từ combobox mã đối tượng
     * Author: NHNam (24/3/2023)
     */
    getObjectName(e) {
      this.payment.ObjectName = e;
    },

    /**
     * Xử lý thay đổi ngày chứng từ
     * Author: NHNam (24/3/2023)
     */
    handleRefDateChange() {
      var date1 = this.formatDate(this.payment.PostedDate);
      var date2 = this.formatDate(this.payment.RefDate);
      if (date1 === date2) {
        this.isSameDate = true;
      } else {
        this.isSameDate = false;
      }
    },
    /**
     * Xử lý thay đổi ngày
     * Author: NHNam (24/3/2023)
     */
    handleDateChange() {
      if (this.isSameDate) {
        this.payment.RefDate = this.payment.PostedDate;
      }
      this.handleRefDateChange();
    },
    /**
     * Lưu phiếu chi
     * Author: NHNam (23/3/2023)
     */
    savePayment() {
      this.validate();
    },

    /**
     * Validate phiếu chi
     * Author: NHNam (23/3/2023)
     */
    validate() {
      this.resetError();
      // Ngày hạch toán trống
      if (!this.payment.PostedDate || this.payment.PostedDate == undefined) {
        this.errors.postedDate = "Ngày hạch toán không được để trống";
        this.isValid = false;
      }
      // Ngày phiếu chi trống
      if (!this.payment.RefDate || this.payment.RefDate == undefined) {
        this.errors.refDate = "Ngày phiếu chi không được để trống";
        this.isValid = false;
      }
      // Ngày hạch toán nhỏ hơn
      if (this.payment.RefDate && this.payment.PostedDate) {
        var postDate = new Date(this.payment.PostedDate);
        var refDate = new Date(this.payment.RefDate);
        if (postDate.getTime() < refDate.getTime()) {
          this.errors.postedDate = "Ngày hạch toán phải lớn hơn ngày phiếu chi";
          this.isValid = false;
        }
      }

      // Trống số phiếu chi
      if (!this.payment.RefNo) {
        this.errors.refNo = "Số phiếu chi không được để trống";
        this.isValid = false;
      }

      if (
        !this.errors.postedDate &&
        !this.errors.refDate &&
        !this.errors.refNo
      ) {
        this.isValid = true;
      } else {
        this.sendErrorMessage();
      }
    },

    /**
     * HIện message lỗi validate
     * Author: NHNam (19/3/2023)
     */
    sendErrorMessage() {
      if (this.errors.postedDate) {
        this.error = this.errors.postedDate;
        this.isError = true;
      }

      if (this.errors.refDate) {
        this.error = this.errors.refDate;
        this.isError = true;
      }

      if (this.errors.refNo) {
        this.error = this.errors.refNo;
        this.isError = true;
      }
    },

    /**
     * reset các lỗi
     * Author: NHNam (19/3/2023)
     */
    resetError() {
      this.errors = {
        postedDate: "",
        refDate: "",
        refNo: "",
      };
    },

    /**
     * đóng form
     */
    closeForm() {
      // this.$emit('closeForm')
      this.$router.go(-1);
    },
    formatDate(date) {
      try {
        if (date) {
          //Tạo đối tượng date
          date = new Date(date);
          //Khai báo biến year gán year hiện tại
          let year = date.getFullYear();
          //Khai báo biến month gán month vì month bắt đầu từ 0 nên ta + thêm 1
          let month = date.getMonth() + 1;
          //Dùng toán tử 3 ngôi để thêm 0 đằng trước ngày nhỏ hơn 10
          // month < 10 ? `0${month}` : `${month}`;
          if (month < 10) {
            month = "0" + month;
          } else {
            month = "" + month;
          }
          //Khai báo biến day và gán day bằng ngày hiện tại
          let day = date.getDate();
          //Dùng toán tử 3 ngôi để thêm 0 đằng trước ngày nhỏ hơn 10
          // day < 10 ? `0${day}` : `${day}`;
          if (day < 10) {
            day = "0" + day;
          } else {
            day = "" + day;
          }
          return `${day}/${month}/${year}`;
        } else {
          ("");
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style>
@import url(../../../css/pay/payform.css);
@import url(../../../css/base/button/button.css);
</style>
