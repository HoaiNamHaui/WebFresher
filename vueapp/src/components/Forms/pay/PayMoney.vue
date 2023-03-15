<template>
  <div class="pay-form-container">
    <div class="header-pay-form">
      <div class="header-pay-form-left">
        <div class="icon-recent-log"></div>
        <div class="pay-form-title">Phiếu chi PC2205002</div>
        <div class="w40">
          <BaseComboboxV2 :data="dataPayType" v-model="dataPayTypeDefault"/>
        </div>
        <!-- <BaseComboboxV2 :data="dataPayType" v-model="dataPayTypeDefault"/> -->
      </div>
      <div class="header-pay-form-right" @click="closeForm">
        <div class="icon-close" style="margin: 6px 8px" ></div>
      </div>
    </div>
    <div class="pay-form-enter flex" style="align-items: unset">
      <div class="pay-form-enter-left">
        <div class="pay-form-left-first-line flex w100">
          <div class="w45">
            <label for="">Mã đối tượng</label>
            <base-combobox-read-only style="margin-bottom: 8px" />
          </div>
          <base-small-input
            label="Tên đối tượng"
            class="custom-padding-input w55"
          />
        </div>
        <div class="pay-form-left-second-line flex w100">
          <base-small-input
            label="Người nhận"
            class="custom-padding-input w45"
          />
          <base-small-input label="Địa chỉ" class="custom-padding-input w55" />
        </div>
        <div class="pay-form-left-third-line flex w100">
          <base-small-input
            label="Lý do chi"
            class="w100"
            style="margin-right: 12px"
          />
        </div>
        <div class="pay-form-left-fourth-line flex w100">
          <div class="w40">
            <label for="">Nhân viên</label>
            <base-combobox-read-only style="margin-bottom: 8px" />
          </div>
          <base-small-input class="w20" label="Kèm theo" />
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
            <input :class="{'text-gray': true}" type="date" class="input-small w100" />
          </div>
          <div
            class="pay-form-right-first-line w60"
            style="margin-bottom: 8px; margin-left: 12px"
          >
            <label for="">Ngày Phiếu chi</label><br />
            <input :class="{'text-gray': true}" type="date" class="input-small w100" />
          </div>
          <div class="pay-form-right-first-line w60" style="margin-left: 12px">
            <base-small-input label="Số phiếu chi" />
          </div>
        </div>
        <div style="text-align: right" class="w50">
          <div>Tổng tiền</div>
          <div style="font-size: 36px; font-weight: bold">1.200.000</div>
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
        <tr>
          <td>1</td>
          <td><input type="text" value="Chi tiền cho" /></td>
          <td><input type="text" /></td>
          <td><input type="text" value="111" /></td>
          <td><input type="text" value="0" style="text-align: right" /></td>
          <td><input type="text" /></td>
          <td><input type="text" /></td>
          <td><div class="icon-delete"></div></td>
        </tr>
        <tr>
          <td>2</td>
          <td><input type="text" value="Chi tiền cho" /></td>
          <td><input type="text" /></td>
          <td><input type="text" /></td>
          <td><input type="text" style="text-align: right" /></td>
          <td><input type="text" /></td>
          <td><input type="text" /></td>
          <td><div class="icon-delete"></div></td>
        </tr>
      </table>
      <div class="pay-control-detail">
        <button
          class="button-white"
          id="btnCancel"
          style="height: 24px !important; line-height: 24px; font-size: 12px"
        >
          Thêm dòng
        </button>
        <button
          class="button-white"
          id="btnCancel"
          style="height: 24px !important; line-height: 24px; font-size: 12px"
        >
          Xóa hết dòng
        </button>
      </div>
      <div class="pay-form-footer">
        <button style="margin-left: 20px;"  class="small-button-white small-button" id="btnCancel" @click="closeForm">
          Hủy
        </button>
        <div style="display: flex; align-items: center; padding-right: 20px;">
          <button class="small-button-white small-button">Cất</button>
          <base-small-button  btnName="Cất và Thêm" />
        </div>
    </div>
    </div>
  </div>
</template>
<script>
import BaseComboboxReadOnly from "../../base/BaseComboboxReadOnly.vue";
import BaseComboboxV2 from "../../base/BaseComboboxV2.vue";
import BaseSmallInput from "../../base/input/BaseSmallInput.vue";
import BaseSmallButton from "../../base/button/BaseSmallButton.vue";
export default {
  name: "PayForm",
  components: {
    BaseComboboxReadOnly,
    BaseSmallInput,
    BaseSmallButton,
    BaseComboboxV2
  },
  data(){
    return{
      dataPayType: ['1. Trả tiền nhà cung cấp','2. Tạm ứng cho nhân viên', '3. Chi mua ngoài có hóa đơn',
      "4. Trả lương cho nhân viên","5. Chuyển tiền cho chi nhánh khác", "6. Gửi tiền vào ngân hàng","7. Chi khác"
    ],
    dataPayTypeDefault : "7. Chi khác"
    }
  },
  methods:{
    /**
     * đóng form
     */
    closeForm(){
      this.$emit('closeForm')
    }
  }
};
</script>
<style>
@import url(../../../css/pay/payform.css);
@import url(../../../css/base/button/button.css);
</style>
