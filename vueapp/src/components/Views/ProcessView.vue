<template>
  <div class="process-area">
    <div class="process-content">
      <div class="process-content-header">NGHIỆP VỤ TIỀN MẶT</div>
      <div class="process-content-image">
        <span class="process-collect-money">Thu tiền</span>
        <span class="process-inventory">Kiểm kể quỹ</span>
        <a href="/CashView/PayView" class="process-pay" @click="handleClickPayMoney">Chi tiền</a>
      </div>
      <div class="process-content-footer">
        <div class="dictionary-item">
          <div class="employee-icon"></div>
          <a href="/SystemAccount" class="system-account">Hệ thống tài khoản</a>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "ProcessView",
  methods:{
    //chuyển đến form chi tiền
    handleClickPayMoney(){
      this.$emit('openFormPayMoney');
    }
  }
};
</script>

<style></style>
