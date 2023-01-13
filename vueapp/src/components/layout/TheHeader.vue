<template>
  <div class="header">
    <div class="header-left">
      <div class="bars" v-show="isShowBar" @click="toggleMenu"></div>
      <div :class="{'header-margin-left': !isShowBar}">CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC</div>
      <!-- <div class="chevron-down"></div> -->
    </div>
    <div class="header-right">
      <div class="bell"></div>
      <img
        src="../../assets/img/default-avatar.jpg"
        alt="default avt"
        class="default-avatar"
      />
      <div class="user-name">Nguyễn Hoài Nam</div>
      <div class="chevron-down-mini"></div>
    </div>
  </div>
</template>
<script>
export default {
  name: "TheHeader",
  data(){
    return{
      isShowBar: true,
    }
  },
  watch:{
    toggle: function(value){
      this.isShowBar = value;
    }
  },
  props: ['toggle'],
  methods: {
    toggleMenu() {
      this.isShowBar = false;
      this.$emit("collapse");
    },
  },
};
</script>
<style>
@import url(../../css/layout/header.css);
</style>
