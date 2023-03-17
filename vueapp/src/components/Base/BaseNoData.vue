<template>
  <div class="no-data-container">
    <div class="no-data-image"></div>
    <div>Không có dữ liệu</div>
  </div>
</template>
<script>
export default {
  name: "BaseNoData",
  data() {
    return {};
  },
  methods: {},
};
</script>
<style>
.no-data-container{
  width: max-content;
  text-align: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 9;
}
.no-data-image {
  width: 130px;
  height: 90px;
  background-image: url(https://actappg2.misacdn.net/img/bg_report_nodata.76e50bd8.svg);
  background-repeat: no-repeat;
}
</style>
