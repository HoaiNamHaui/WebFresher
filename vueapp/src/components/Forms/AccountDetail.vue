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
        <div
          @click="checkChangeAndHideDialog"
          class="icon-close"
          id="btnClose"
        ></div>
      </div>
      <div class="form-header">
        <div class="form-title">Thêm tài khoản</div>
      </div>
      <div class="form-field">
        <div>
          <BaseSmallInputVue
            label="Số tài khoản"
            class="m-r-6"
            :require="true"
            style="width: calc(25% - 9px)"
          />
        </div>
        <div style="display: flex">
          <BaseSmallInputVue
            label="Tên tài khoản"
            class="m-r-6"
            :require="true"
            style="width: 50%"

          />
          <BaseSmallInputVue
            label="Tên tiếng Anh"
            class="m-l-6"
            :require="false"
            style="width: 50%"
          />
        </div>
        <div style="width: 50%; display: flex">
          <BaseSmallInputVue
            label="Tài khoản tổng hợp"
            class="m-r-6"
            :require="false"
            style="width: calc(50% - 9px)"
          />
          <!-- <BaseSmallInputVue
            label="Tính chất"
            :require="true"
            style="width: calc(50% - 9px)"
            class="m-l-6"
          /> -->
          
          <div
          style="width: calc(50% - 9px)"
          class="m-l-6">
          <label for="">Tính chất</label><br>
          <base-combobox-v-2 :data="dataType" style="margin-top: unset !important; "/>
          </div>
        </div>
        <div>
          <label for="">Diễn giải</label> <br />
          <textarea class="text-area"></textarea>
        </div>
        <div style="display: flex">
          <base-checkbox
            @changeCheckbox="changeCheckboxHasForeignCurrencyAccounting"
            :checked="account.HasForeignCurrencyAccounting"
          />
          <div style="margin-left: 8px">Có hạch toán ngoại tệ</div>
        </div>
        <div>
          <div>
            <div style="display: flex; margin-top: 20px; align-items: center;" >
              <div class="arrow-collapse" @click="toggleDetail"></div>
              <div class="collapse-text" @click="toggleDetail">Theo dõi chi tiết theo</div>
            </div>
            <div ref="collapseContent" class="transition">
              <table class="collapse-content" cellspacing="0">
                <tr>
                  <td><base-checkbox 
                    :checked="account.IsTrackObject"
                    @changeCheckbox="changeCheckboxTrackObject"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Đối tượng</td>
                  <td style="padding-right: 30px;"> 
                    <base-combobox-v-2 :data="dataObject" :isDisable="!account.IsTrackObject"/>
                  </td>
                  <td><base-checkbox
                    :checked="account.IsTrackBankAccount"
                    @changeCheckbox="changeCheckboxTrackBankAccount"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Tài khoản ngân hàng</td>
                  <td></td>
                </tr>
                <tr>
                  <td><base-checkbox
                    :checked="account.IsTrackJob"
                    @changeCheckbox="changeCheckboxTrackJob"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Đối tượng THCP</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackJob"/>
                  </td>
                  <td><base-checkbox
                    :checked="account.IsTrackProjectWork"
                    @changeCheckbox="changeCheckboxTrackProjectWork"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Công trình</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackProjectWork"/>
                  </td>
                </tr>
                <tr>
                  <td><base-checkbox
                    :checked="account.IsTrackOrder"
                    @changeCheckbox="changeCheckboxTrackOrder"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Đơn đặt hàng</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackOrder"/>
                  </td>
                  <td><base-checkbox
                    :checked="account.IsTrackSaleContract"
                    @changeCheckbox="changeCheckboxTrackSaleContract"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Hợp đồng bán</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackSaleContract"/>
                  </td>
                </tr>
                <tr>
                  <td><base-checkbox
                    :checked="account.IsTrackPurchaseContract"
                    @changeCheckbox="changeCheckboxTrackPurchaseContract"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Hợp đồng mua</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackPurchaseContract"/>
                  </td>
                  <td><base-checkbox
                    :checked="account.IsTrackExpenseItem"
                    @changeCheckbox="changeCheckboxTrackExpenseItem"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Khoản mục CP</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackExpenseItem"/>
                  </td>
                </tr>
                <tr>
                  <td><base-checkbox
                    :checked="account.IsTrackOrganizationUnit"
                    @changeCheckbox="changeCheckboxTrackOrganizationUnit"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Đơn vị</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackOrganizationUnit"/>
                  </td>
                  <td><base-checkbox
                    :checked="account.IsTrackItem"
                    @changeCheckbox="changeCheckboxTrackItem"
                    /></td>
                  <td class="mw" style="padding-left: 6px;">Mã thống kê</td>
                  <td style="padding-right: 30px;">
                    <base-combobox-v-2 :data="dataOther" :isDisable="!account.IsTrackItem"/>
                  </td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div class="form-account-footer">
        <button class="small-button-white" id="btnCancel" @click="closeForm">
          Hủy
        </button>
        <div style="display: flex; align-items: center; margin-right: 32px;">
          <button class="small-button-white">Cất</button>
          <base-small-button  btnName="Cất và Thêm" />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseSmallInputVue from "../base/input/BaseSmallInput.vue";
import BaseCheckbox from "../base/BaseCheckbox.vue";
import BaseSmallButton from "../base/button/BaseSmallButton.vue";
import BaseComboboxV2 from "../base/BaseComboboxV2.vue";
export default {
  name: "AccountDetail",
  components: {
    BaseSmallInputVue,
    BaseCheckbox,
    BaseSmallButton,
    BaseComboboxV2
  },
  data() {
    return {
      isFullsize: false,
      account:{
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
      },
      dataObject: ['Nhà cung cấp', 'Khách hàng', 'Nhân viên'],
      dataOther: ['Chỉ cảnh báo','Bắt buộc nhập'],
      dataType: ['Dư nợ','Dư có', 'Lưỡng tính', 'Không có số dư']
      
    };
  },
  methods: {
    //check
    changeCheckboxHasForeignCurrencyAccounting(active) {
      this.account.HasForeignCurrencyAccounting = active;
    },
    //check
    changeCheckboxTrackObject(active) {
      this.account.IsTrackObject = active;
    },
    //check
    changeCheckboxTrackJob(active) {
      this.account.IsTrackJob = active;
    },
    //check
    changeCheckboxTrackOrder(active) {
      this.account.IsTrackOrder = active;
    },
    //check
    changeCheckboxTrackPurchaseContract(active) {
      this.account.IsTrackPurchaseContract = active;
    },
    //check
    changeCheckboxTrackOrganizationUnit(active) {
      this.account.IsTrackOrganizationUnit = active;
    },
    //check
    changeCheckboxTrackBankAccount(active) {
      this.account.IsTrackBankAccount = active;
    },
    //check
    changeCheckboxTrackProjectWork(active) {
      this.account.IsTrackProjectWork = active;
    },
    //check
    changeCheckboxTrackSaleContract(active) {
      this.account.IsTrackSaleContract = active;
    },
    //check
    changeCheckboxTrackExpenseItem(active) {
      this.account.IsTrackExpenseItem = active;
    },
    //check
    changeCheckboxTrackItem(active) {
      this.account.IsTrackItem = active;
    },
    //đóng form
    closeForm(){
      console.log(12);
      this.$emit('closeForm');
    },
    //mở rộng thu gọn chi tiết
    toggleDetail(){
      // console.log(this.$refs.collapseContent.classList.toggle);
      this.$refs.collapseContent.classList.toggle('hide-content');
    }

  },
  mounted(){
    
  }
};
</script>
<style scoped>
@import url(../../css/cash/accountdetail.css);

.align-center-y{
  display: flex;
  align-items: center;
}
.mw{
  min-width: 160px;
}
</style>
