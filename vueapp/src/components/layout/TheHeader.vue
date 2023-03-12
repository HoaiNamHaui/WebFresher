<template>
  <div class="header">
    <div class="header-left">
      <div class="bars" v-show="isShowBar" @click="toggleMenu"></div>
      <div :class="{'header-margin-left': !isShowBar}">CÔNG TY TNHH SONG VIỆT - Misatest</div>
      <!-- <div class="chevron-down"></div> -->
    </div>
    <div class="header-right">
      <div class="bell" style="position: relative;">
        <BaseTooltip message="Tính năng này đang phát triển"/>
      </div>
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
import BaseTooltip from '../base/BaseTooltip.vue';
export default {
  name: "TheHeader",
  components:{
    BaseTooltip
  },
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
