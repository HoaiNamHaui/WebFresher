<template>
  <div style="padding: 20px 20px; position: relative">
    <h1>Tiền gửi</h1>
    <base-combobox-table 
    api="https://localhost:7116/api/v1/Accounts" 
    propName="AccountNumber" 
    propValue="AccountId"
    :listTitle="listTitle"
    />
  </div>
</template>
<script>
import BaseComboboxTable from "../base/BaseComboboxTable.vue";
export default {
  name: "DepositView",
  components: {
    BaseComboboxTable,
  },
  data() {
    return {
      listTitle: [
        {
          title: "Số tài khoản",
          attribute: "AccountNumber"
        },
        {
          title: "Tên tài khoản",
          attribute: "AccountName"
        }
      ]
    };
  },
  methods: {},
};
</script>
<style></style>
