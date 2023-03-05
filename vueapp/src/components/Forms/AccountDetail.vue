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
          <BaseSmallInputVue
            label="Tính chất"
            :require="true"
            style="width: calc(50% - 9px)"
            class="m-l-6"
          />
        </div>
        <div>
          <label for="">Diễn giải</label> <br />
          <textarea class="text-area"></textarea>
        </div>
        <div style="display: flex">
          <base-checkbox
            @changeCheckbox="changeCheckbox"
            :checked="hasForeignCurrencyAccounting"
          />
          <div style="margin-left: 8px">Có hạch toán ngoại tệ</div>
        </div>
        <div>
          <div>
            <div style="display: flex; margin-top: 20px; align-items: center;">
              <div class="arrow-collapse"></div>
              <div class="collapse-text">Theo dõi chi tiết theo</div>
            </div>
            <div>
              <table class="collapse-content" cellspacing="0">
                <tr>
                  <td><base-checkbox/></td>
                  <td class="mw">Đối tượng</td>
                  <td><base-small-input-vue class="align-center-y" style="margin-right: 30px;"/></td>
                  <td><base-checkbox/></td>
                  <td class="mw">Tài khoản ngân hàng</td>
                  <td></td>
                </tr>
                <tr>
                  <td><base-checkbox/></td>
                  <td class="mw">Đối tượng THCP</td>
                  <td><base-small-input-vue class="align-center-y"  style="margin-right: 30px;"/></td>
                  <td><base-checkbox/></td>
                  <td class="mw">Công trình</td>
                  <td><base-small-input-vue class="align-center-y" /></td>
                </tr>
                <tr>
                  <td><base-checkbox/></td>
                  <td class="mw">Đơn đặt hàng</td>
                  <td><base-small-input-vue class="align-center-y"  style="margin-right: 30px;"/></td>
                  <td><base-checkbox/></td>
                  <td class="mw">Hợp đồng bán</td>
                  <td><base-small-input-vue class="align-center-y" /></td>
                </tr>
                <tr>
                  <td><base-checkbox/></td>
                  <td class="mw">Hợp đồng mua</td>
                  <td><base-small-input-vue class="align-center-y"  style="margin-right: 30px;"/></td>
                  <td><base-checkbox/></td>
                  <td class="mw">Khoản mục CP</td>
                  <td><base-small-input-vue class="align-center-y"  /></td>
                </tr>
                <tr>
                  <td><base-checkbox/></td>
                  <td class="mw">Đơn vị</td>
                  <td><base-small-input-vue class="align-center-y"  style="margin-right: 30px;"/></td>
                  <td><base-checkbox/></td>
                  <td class="mw">Mã thống kê</td>
                  <td><base-small-input-vue class="align-center-y" /></td>
                </tr>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div class="form-account-footer">
        <button class="button-white" id="btnCancel" @click="closeForm">
          Hủy
        </button>
        <div style="display: flex; align-items: center; margin-right: 32px;">
          <button class="button-white">Cất</button>
          <base-button  btnName="Cất và Thêm" />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseSmallInputVue from "../base/input/BaseSmallInput.vue";
import BaseCheckbox from "../base/BaseCheckbox.vue";
import BaseButton from "../base/button/BaseButton.vue";
export default {
  name: "AccountDetail",
  components: {
    BaseSmallInputVue,
    BaseCheckbox,
    BaseButton
  },
  data() {
    return {
      isFullsize: false,
      hasForeignCurrencyAccounting: false,
    };
  },
  methods: {
    changeCheckbox(active) {
      this.hasForeignCurrencyAccounting = active;
    },

    closeForm(){
      console.log(12);
      this.$emit('closeForm');
    }
  },
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
