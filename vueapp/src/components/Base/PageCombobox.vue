<template>
    <div class="box-option" v-show="isShowCbb">
      <div class="box-option-item selected-option" pageSize="10" @click="chosePageSize">
        10 bản ghi trên 1 trang
      </div>
      <div class="box-option-item" pageSize="20" @click="chosePageSize">
        20 bản ghi trên 1 trang
      </div>
      <div class="box-option-item" pageSize="30" @click="chosePageSize">
        30 bản ghi trên 1 trang
      </div>
      <div class="box-option-item" pageSize="50" @click="chosePageSize">
        50 bản ghi trên 1 trang
      </div>
      <div class="box-option-item" pageSize="100" @click="chosePageSize">
        100 bản ghi trên 1 trang
      </div>
    </div>
</template>
  
<script>
  import $ from "jquery";
  export default {
    components: {},
    props: ["isShowCbb"],
    methods: {
      /**
       * Chọn số bản ghi trên 1 trang
       * Author: Nguyễn Hoài Nam
       */
      chosePageSize(e) {
        this.$emit("sizeRecord", e.target.getAttribute("pageSize")); 
        $('.box-option-item.selected-option').removeClass('selected-option')
        e.target.classList.add('selected-option')
      },
    },
  }
  </script>
  
  <style scoped>
  @import url(../../css/base/combobox/combobox.css);
  </style>