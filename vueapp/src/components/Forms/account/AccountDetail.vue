<template>
  <div class="overlay">
    <div class="account-form" :class="{ expand: isFullsize }">
      <div class="resize" @click="isFullsize = !isFullsize">
        <div
          :class="{ 'change-position-icon': isFullsize }"
          class="btn-resize"
        ></div>
      </div>
      <div class="form-button">
        <div @click="closeForm" class="icon-close" id="btnClose">
          <base-tooltip message="Đóng"/>
        </div>
      </div>
      <div class="form-header">
        <div class="form-title">{{ titleForm }}</div>
      </div>
      <div class="form-field">
        <div>
          <BaseSmallInputVue
            label="Số tài khoản"
            class="m-r-6"
            :require="true"
            style="width: calc(25% - 9px)"
            :tabindex="1"
            :isFocus="true"
            v-model="account.AccountNumber"
          />
        </div>
        <div style="display: flex">
          <BaseSmallInputVue
            label="Tên tài khoản"
            class="m-r-6"
            :require="true"
            style="width: 50%"
            :tabindex="2"
            v-model="account.AccountName"
          />
          <BaseSmallInputVue
            label="Tên tiếng Anh"
            class="m-l-6"
            :require="false"
            style="width: 50%"
            :tabindex="3"
            v-model="account.EnglishName"
          />
        </div>
        <div style="width: 50%; display: flex">
          <!-- <BaseSmallInputVue
            label="Tài khoản tổng hợp"
            class="m-r-6"
            :require="false"
            style="width: calc(50% - 9px)"
            :tabindex="4"
            v-model="account.ParentId"
          /> -->
          <div style="width: calc(50% - 9px)">
            <label for="">Tài khoản tổng hợp </label><br />
            <base-combobox-v-2
              :api="api"
              style="margin-top: unset !important"
              :tabindex="4"
              prop-value="AccountId"
              prop-name="AccountNumber"
              v-model="account.ParentId"
              @changeGrade="account.Grade = $event ? $event + 1 : account.Grade"
              @getParentAccountNumber="parentNumber = $event"
              @removeParentId="removeParentId"
            />
          </div>

          <div style="width: calc(50% - 9px)" class="m-l-6">
            <label for="">Tính chất </label><span style="color: red">*</span
            ><br />
            <base-combobox-v-2
              :data="dataType"
              style="margin-top: unset !important"
              :tabindex="5"
              v-model="account.Type"
              prop-name="text"
              prop-value="value"
            />
          </div>
        </div>
        <div>
          <label for="">Diễn giải</label> <br />
          <textarea
            tabindex="6"
            class="text-area"
            style="resize: none"
            v-model="account.Description"
          ></textarea>
        </div>
        <div style="display: flex">
          <base-checkbox
            @changeCheckbox="changeCheckboxHasForeignCurrencyAccounting"
            :checked="account.HasForeignCurrencyAccounting"
            :tabindex="7"
          />
          <div style="margin-left: 8px">Có hạch toán ngoại tệ</div>
        </div>
        <div>
          <div>
            <div style="display: flex; margin-top: 20px; align-items: center">
              <div class="arrow-collapse" @click="toggleDetail"></div>
              <div class="collapse-text" @click="toggleDetail">
                Theo dõi chi tiết theo
              </div>
            </div>
            <div ref="collapseContent" class="transition">
              <table class="collapse-content" cellspacing="0">
                <tr>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackObject"
                      @changeCheckbox="changeCheckboxTrackObject"
                      :tabindex="8"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Đối tượng</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :tabindex="9"
                      :data="dataObject"
                      :isDisable="!account.IsTrackObject"
                      v-model="account.Object"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackBankAccount"
                      @changeCheckbox="changeCheckboxTrackBankAccount"
                      :tabindex="10"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">
                    Tài khoản ngân hàng
                  </td>
                  <td></td>
                </tr>
                <tr>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackJob"
                      @changeCheckbox="changeCheckboxTrackJob"
                      :tabindex="10"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Đối tượng THCP</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackJob"
                      :tabindex="11"
                      v-model="account.Job"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackProjectWork"
                      @changeCheckbox="changeCheckboxTrackProjectWork"
                      :tabindex="12"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Công trình</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackProjectWork"
                      :tabindex="13"
                      v-model="account.ProjectWork"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                </tr>
                <tr>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackOrder"
                      @changeCheckbox="changeCheckboxTrackOrder"
                      :tabindex="14"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Đơn đặt hàng</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackOrder"
                      :tabindex="15"
                      v-model="account.Order"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackSaleContract"
                      @changeCheckbox="changeCheckboxTrackSaleContract"
                      :tabindex="16"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Hợp đồng bán</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackSaleContract"
                      :tabindex="17"
                      v-model="account.SaleContract"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                </tr>
                <tr>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackPurchaseContract"
                      @changeCheckbox="changeCheckboxTrackPurchaseContract"
                      :tabindex="18"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Hợp đồng mua</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackPurchaseContract"
                      :tabindex="19"
                      v-model="account.PurchaseContract"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackExpenseItem"
                      @changeCheckbox="changeCheckboxTrackExpenseItem"
                      :tabindex="20"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Khoản mục CP</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackExpenseItem"
                      :tabindex="21"
                      v-model="account.ExpenseItem"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                </tr>
                <tr>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackOrganizationUnit"
                      @changeCheckbox="changeCheckboxTrackOrganizationUnit"
                      :tabindex="22"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Đơn vị</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackOrganizationUnit"
                      :tabindex="23"
                      v-model="account.Unit"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                  <td>
                    <base-checkbox
                      :checked="account.IsTrackItem"
                      @changeCheckbox="changeCheckboxTrackItem"
                      :tabindex="24"
                    />
                  </td>
                  <td class="mw" style="padding-left: 6px">Mã thống kê</td>
                  <td style="padding-right: 30px">
                    <base-combobox-v-2
                      :data="dataOther"
                      :isDisable="!account.IsTrackItem"
                      :tabindex="25"
                      v-model="account.Item"
                      prop-name="text"
                      prop-value="value"
                    />
                  </td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div class="form-account-footer">
        <button
          class="small-button-white"
          id="btnCancel"
          @click="closeForm"
          :tabindex="26"
        >
          Hủy
        </button>
        <div style="display: flex; align-items: center; margin-right: 32px">
          <button
            class="small-button-white"
            :tabindex="27"
            @click="saveAndClose"
          >
            Cất
          </button>
          <base-small-button
            :tabindex="28"
            btnName="Cất và Thêm"
            @keydown.tab="focusFirstInput"
            @click="saveAndAdd"
          />
        </div>
      </div>
    </div>
    <base-loading v-if="isLoading" />
    <message-error v-if="isError" :error="error" @close="isError = false" />
  </div>
</template>
<script>
import BaseSmallInputVue from "../../base/input/BaseSmallInput.vue";
import MessageError from "@/components/message/MessageError.vue";
import BaseCheckbox from "../../base/BaseCheckbox.vue";
import BaseSmallButton from "../../base/button/BaseSmallButton.vue";
import BaseComboboxV2 from "../../base/BaseComboboxV2.vue";
import accountData from "@/js/account/account";
import MISAapi from "@/js/api";
import axios from "axios";
import MISAEnum from "@/js/base/enum";
import MISAResource from "@/js/base/resource";
import BaseLoading from "@/components/base/BaseLoading.vue";
import BaseTooltip from "@/components/base/BaseTooltip.vue";
export default {
  name: "AccountDetail",
  components: {
    BaseSmallInputVue,
    BaseCheckbox,
    BaseSmallButton,
    BaseComboboxV2,
    MessageError,
    BaseLoading,
    BaseTooltip
  },
  data() {
    return {
      titleForm: MISAResource.vi.formAccountMode.Add,
      isFullsize: false,
      isLoading: false,
      account: {
        parentNumber: "",
        ParentId: MISAResource.vi.GUID_EMPTY,
        HasForeignCurrencyAccounting: false,
        IsTrackObject: false,
        IsTrackJob: false,
        IsTrackOrder: false,
        IsTrackPurchaseContract: false,
        IsTrackOrganizationUnit: false,
        IsTrackBankAccount: false,
        IsTrackProjectWork: false,
        IsTrackSaleContract: false,
        IsTrackExpenseItem: false,
        IsTrackItem: false,
        IsActive: true,
        Type: 0,
        Object: 1,
        Job: 0,
        Order: 0,
        PurchaseContract: 0,
        Unit: 0,
        BankAccount: 0,
        ProjectWork: 0,
        SaleContract: 0,
        ExpenseItem: 0,
        Item: 0,
        Grade: 1,
      },
      api: MISAapi.account.base,
      dataObject: accountData.dataObject,
      dataOther: accountData.dataOther,
      dataType: accountData.dataType,
      errors: {
        accountNumber: "",
        accountName: "",
        type: "",
      },
      isValid: false,
      isError: false,
      error: "",
    };
  },
  props: ["idAccountSelected", "isDuplicate"],

  async created() {
    if (this.idAccountSelected != null) {
      var me = this;
      await axios
        .get(MISAapi.account.base + me.idAccountSelected)
        .then(function (res) {
          me.account = res.data;
        });
    }
    this.handleTitleForm();
  },

  methods: {
    // không chọn tài khoản tổng hợp
    removeParentId() {
      this.account.ParentId = MISAResource.vi.GUID_EMPTY;
      this.parentNumber = "";
      this.account.Grade = 1;
    },

    /**
     * Cất và thêm
     */
    async saveAndClose() {
      try {
        this.validate();
        if (this.isValid) {
          await this.saveAccount();
          this.closeForm();
        }
      } catch (error) {
        console.log(error);
      }
    },

    async saveAccount() {
      var me = this;
      var newData = this.account;
      this.isLoading = true;
      if (
        !this.account.AccountId ||
        this.isDuplicate === MISAEnum.FormMode.Duplicate
      ) {
        await axios
          .post(MISAapi.account.base, newData)
          .then(() => {
            this.isLoading = false;
            me.$emit("reloadListAccount");
            me.$emit("showToast", MISAResource.vi.add);
          })
          .catch((res) => {
            console.log(res);
          });
      } else {
        await axios
          .put(MISAapi.account.base + this.account.AccountId, newData)
          .then(() => {
            this.isLoading = false;
            // me.$emit("reloadListAccount");
            // me.$emit("showToast", MISAResource.vi.update);
          })
          .catch((res) => {
            console.log(res);
          });
        me.$emit("reloadListAccount");
        me.$emit("showToast", MISAResource.vi.update);
      }
      this.$emit("changeDuplicateMode");
    },
    //Xử lý phím tắt
    handleKeydown(event) {
      var me = this;
      // Đóng form
      if (event.keyCode === 27) {
        event.preventDefault();
        // event.stopPropagation();
        //me.checkChangeAndHideDialog();
        me.closeForm();
      }
      // Lưu data và đóng
      if (event.ctrlKey && event.key == "s") {
        event.preventDefault();
        this.saveAndClose();
      }

      // cất và thêm
      if (event.shiftKey && event.ctrlKey && event.key == "S") {
        event.preventDefault();
        this.saveAndAdd();
      }
    },

    /**
     * Validate
     */
    validate() {
      this.resetError();

      // Bỏ trống số tài khoản
      if (!this.account.AccountNumber) {
        this.errors.accountNumber =
          MISAResource.vi.errorAccount.accountNumberEmpty;
        this.isValid = false;
      }

      // Tài khoản chi tiết không bắt đầu bằng tài khoản cha
      else if (this.parentNumber) {
        var regex = new RegExp("^" + this.parentNumber + "*");
        if (!regex.test(this.account.AccountNumber)) {
          this.errors.accountNumber =
            MISAResource.vi.errorAccount.childrenAccount;
          this.isValid = false;
        }
      }

      // Bỏ trống tên tài khoản
      if (!this.account.AccountName) {
        this.errors.accountName = MISAResource.vi.errorAccount.accountNameEmpty;
        this.isValid = false;
      }

      // Bỏ trống tính chất
      if (!this.account.Type == null) {
        this.errors.type = MISAResource.vi.errorAccount.typeEmpty;
        this.isValid = false;
      }

      if (
        !this.errors.accountName &&
        !this.errors.accountNumber &&
        !this.errors.type
      ) {
        this.isValid = true;
      } else {
        this.sendErrorMessage();
      }
    },

    /**
     * HIện message lỗi validate
     */
    sendErrorMessage() {
      if (this.errors.accountNumber) {
        this.error = this.errors.accountNumber;
        this.isError = true;
      }

      if (this.errors.accountName) {
        this.error = this.errors.accountName;
        this.isError = true;
      }

      if (this.errors.type) {
        this.error = this.errors.type;
        this.isError = true;
      }
    },

    /**
     * reset các lỗi
     * Author: NHNam
     */
    resetError() {
      this.errors = {
        accountNumber: "",
        accountName: "",
        type: "",
      };
    },

    // reset thông tin account
    resetAccount() {
      this.account = {
        parentNumber: "",
        ParentId: MISAResource.vi.GUID_EMPTY,
        HasForeignCurrencyAccounting: false,
        IsTrackObject: false,
        IsTrackJob: false,
        IsTrackOrder: false,
        IsTrackPurchaseContract: false,
        IsTrackOrganizationUnit: false,
        IsTrackBankAccount: false,
        IsTrackProjectWork: false,
        IsTrackSaleContract: false,
        IsTrackExpenseItem: false,
        IsTrackItem: false,
        IsActive: true,
        Type: 0,
        Object: 1,
        Job: 0,
        Order: 0,
        PurchaseContract: 0,
        Unit: 0,
        BankAccount: 0,
        ProjectWork: 0,
        SaleContract: 0,
        ExpenseItem: 0,
        Item: 0,
        Grade: 1,
      };
    },

    /**
     * Cất và thêm
     */
    async saveAndAdd() {
      try {
        this.validate();
        if (this.isValid) {
          await this.saveAccount();
          this.resetAccount();
          this.handleTitleForm();
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Đổi tiêu đề form theo form mode
     */
    handleTitleForm() {
      if (this.account.AccountId == null) {
        this.titleForm = MISAResource.vi.formAccountMode.Add;
      } else {
        this.titleForm = MISAResource.vi.formAccountMode.Edit;
      }
      if (this.isDuplicate) {
        this.titleForm = MISAResource.vi.formAccountMode.Duplicate;
      }
    },

    //check có hạch toán ngoại tệ
    changeCheckboxHasForeignCurrencyAccounting(active) {
      this.account.HasForeignCurrencyAccounting = active;
    },
    //check theo dõi theo đối tượng
    changeCheckboxTrackObject(active) {
      this.account.IsTrackObject = active;
      if (this.account.IsTrackObject) {
        this.account.Object = MISAEnum.OBJECT.CUSTOMER;
      }
    },
    //check theo dõi theo tổng hợp chi phí
    changeCheckboxTrackJob(active) {
      this.account.IsTrackJob = active;
      if (this.account.IsTrackJob) {
        this.account.Job = MISAEnum.FOLLOW_DETAIL.ONLY_WARNING;
      }
    },
    //check theo dõi theo đơn hàng
    changeCheckboxTrackOrder(active) {
      this.account.IsTrackOrder = active;
      if (this.account.IsTrackOrder) {
        this.account.Order = MISAEnum.FOLLOW_DETAIL.ONLY_WARNING;
      }
    },
    //check theo dõi theo hợp đồng mua
    changeCheckboxTrackPurchaseContract(active) {
      this.account.IsTrackPurchaseContract = active;
      if (this.account.IsTrackPurchaseContract) {
        this.account.PurchaseContract = MISAEnum.FOLLOW_DETAIL.REQUIRE;
      }
    },
    //check theo dõi theo tổ chức đơn vị
    changeCheckboxTrackOrganizationUnit(active) {
      this.account.IsTrackOrganizationUnit = active;
      if (this.account.IsTrackOrganizationUnit) {
        this.account.Unit = MISAEnum.FOLLOW_DETAIL.REQUIRE;
      }
    },
    //check theo dõi theo tài khoản ngân hàng
    changeCheckboxTrackBankAccount(active) {
      this.account.IsTrackBankAccount = active;
      if (this.account.IsTrackBankAccount) {
        this.account.BankAccount = MISAEnum.FOLLOW_DETAIL.REQUIRE;
      }
    },
    //check theo dõi theo công trình
    changeCheckboxTrackProjectWork(active) {
      this.account.IsTrackProjectWork = active;
      if (this.account.IsTrackProjectWork) {
        this.account.ProjectWork = MISAEnum.FOLLOW_DETAIL.REQUIRE;
      }
    },
    //check theo dõi theo hợp đồng mua
    changeCheckboxTrackSaleContract(active) {
      this.account.IsTrackSaleContract = active;
      if (this.account.IsTrackSaleContract) {
        this.account.SaleContract = MISAEnum.FOLLOW_DETAIL.REQUIRE;
      }
    },
    //check theo dõi theo khoản mục cp
    changeCheckboxTrackExpenseItem(active) {
      this.account.IsTrackExpenseItem = active;
      if (this.account.IsTrackExpenseItem) {
        this.account.ExpenseItem = MISAEnum.FOLLOW_DETAIL.REQUIRE;
      }
    },
    //check theo dõi theo mã thống kê
    changeCheckboxTrackItem(active) {
      console.log(active);
      this.account.IsTrackItem = active;
      if (this.account.IsTrackItem) {
        this.account.Item = MISAEnum.FOLLOW_DETAIL.REQUIRE;
      }
    },
    //đóng form
    closeForm() {
      this.$emit("closeForm");
    },
    //mở rộng thu gọn chi tiết
    toggleDetail() {
      this.$refs.collapseContent.classList.toggle("hide-content");
    },

    //focus input đầu tiên
    focusFirstInput(e) {
      e.preventDefault();
      var firstInput = document.getElementsByClassName("focus");
      firstInput[0].focus();
    },
  },
  mounted() {
    document.addEventListener("keydown", this.handleKeydown);
    var firstInput = document.getElementsByClassName("focus");
    firstInput[0].focus();
  },
};
</script>
<style scoped>
@import url(../../../css/cash/accountdetail.css);

.align-center-y {
  display: flex;
  align-items: center;
}
.mw {
  min-width: 160px;
}
</style>
