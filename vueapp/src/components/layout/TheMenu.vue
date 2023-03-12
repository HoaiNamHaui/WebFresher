<template>
    <div class="menu" :class="{collapse: isCollapse}">
            <div class="logo-container">
                <div v-show="!isCollapse" class="menu-icon" ></div>
                <img v-show="!isCollapse" src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="" class="logo">
                <div v-show="isCollapse" class="bars-white" @click="expandMenu"></div>
            </div>
            <div class="menu-wrapper">
                <router-link to="/" class="menu-item select">
                    <div class="icon24 icon-dashboard"></div>
                    <div v-show="!isCollapse" class="menu-option">Tổng quan</div>
                </router-link>
                <router-link to="/CashView/ProcessView" class="menu-item">
                    <div class="icon24 icon-buy"></div>
                    <div v-show="!isCollapse" class="menu-option">Tiền mặt</div>
                </router-link>
                <router-link to="/DepositView" class="menu-item">
                    <div class="icon24 icon-cash"></div>
                    <div v-show="!isCollapse" class="menu-option">Tiền gửi</div>
                </router-link>
                <router-link to="/PurchaseView" class="menu-item">
                    <div class="icon24 icon-bank"></div>
                    <div v-show="!isCollapse" class="menu-option">Mua hàng</div>
                </router-link>
                <router-link to="/SellView" class="menu-item">
                    <div class="icon24 icon-sell"></div>
                    <div v-show="!isCollapse" class="menu-option">Bán hàng</div>
                </router-link>
                <router-link to="/BillView" class="menu-item">
                    <div class="icon24 icon-invoice"></div>
                    <div v-show="!isCollapse"  class="menu-option">Quản lý hóa đơn</div>
                </router-link>
                <router-link to="/RepositoryView" class="menu-item">
                    <div class="icon24 icon-stock"></div>
                    <div v-show="!isCollapse" class="menu-option">Kho</div>
                </router-link>
                <router-link to="/ToolView" class="menu-item">
                    <div class="icon24 icon-tool"></div>
                    <div v-show="!isCollapse" class="menu-option">Công cụ dụng cụ</div>
                </router-link>
                <router-link to="/AssetsView" class="menu-item">
                    <div class="icon24 icon-asset"></div>
                    <div v-show="!isCollapse" class="menu-option">Tài sản cố định</div>
                </router-link>
                <router-link to="/TaxView" class="menu-item">
                    <div class="icon24 icon-tax"></div>
                    <div v-show="!isCollapse" class="menu-option">Thuế</div>
                </router-link>
                <router-link to="/PriceView" class="menu-item">
                    <div class="icon24 icon-cost"></div>
                    <div v-show="!isCollapse" class="menu-option">Giá thành</div>
                </router-link>
                <router-link to="/SyntheticView" class="menu-item">
                    <div class="icon24 icon-general"></div>
                    <div v-show="!isCollapse" class="menu-option">Tổng hợp</div>
                </router-link>
                <!-- <router-link to="/BudgetView" class="menu-item">
                    <div class="icon24 icon-budget"></div>
                    <div v-show="!isCollapse" class="menu-option">Ngân sách</div>
                </router-link>
                <router-link to="/ReportView" class="menu-item">
                    <div class="icon24 icon-report"></div>
                    <div v-show="!isCollapse" class="menu-option">Báo cáo</div>
                </router-link>
                <router-link to="/AnalysisView" class="menu-item">
                    <div class="icon24 icon-finance"></div>
                    <div v-show="!isCollapse" class="menu-option">Phân tích tài chính</div>
                </router-link> -->
                <div class="" style="width: 80%; height: 0.5px; background-color: #fff; margin: 0 auto; margin-bottom: 10px;"></div>
                <router-link to="/CategoryView" class="menu-item">
                    <div class="icon24 icon-category"></div>
                    <div v-show="!isCollapse" class="menu-option">Danh mục</div>
                </router-link>
            </div>
        </div>
</template>


<script>
export default {
  name: 'TheMenu',
  data(){
    return{
        isCollapse: false
    }
  },
  components: {
    
  },
  props: ['toggle'],
  watch:{
    toggle: function(){
        if(this.toggle){
            this.isCollapse = true;

        }
    }
  },
  methods:{
    /**
     * Mở rộng Sidebar
     * Author: NHNam (7/1/2023)
     */
    expandMenu(){
        this.isCollapse = false;
        this.$emit("expandMenu");
    }
  }
  
}
</script>
<style scoped>
 @import url(../../css/layout/menu.css);
</style>