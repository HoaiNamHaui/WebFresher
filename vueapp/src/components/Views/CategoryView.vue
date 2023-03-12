<template>
  <div>
    <div class="content-top">
      <div class="title">Danh mục</div>
    </div>
    <div class="content-data">
        <div class="content-data-item">
            <span class="title-list">Tài khoản</span>
            <ul>
                <li><router-link to="/SystemAccount">Hệ thống tài khoản</router-link></li>
                <li style="cursor: pointer;">Tài khoản kết chuyển</li>
                <li>Tài khoản ngầm định</li>
            </ul>
        </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "CategoryView",
};
</script>

<style>
@import url(../../css/category/category.css);
</style>
