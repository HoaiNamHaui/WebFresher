<template>
    <div class="menu">
            <div class="logo-container">
                <div class="menu-icon"></div>
                <img src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="" class="logo">
            </div>
            <div class="menu-wrapper">
                <router-link to="/" class="menu-item select">
                    <div class="icon24 icon-dashboard"></div>
                    <div class="menu-option">Tổng quan</div>
                </router-link>
                <router-link to="/CashView" class="menu-item">
                    <div class="icon24 icon-buy"></div>
                    <div class="menu-option">Tiền mặt</div>
                </router-link>
                <router-link to="/DepositView" class="menu-item">
                    <div class="icon24 icon-cash"></div>
                    <div class="menu-option">Tiền gửi</div>
                </router-link>
                <router-link to="/PurchaseView" class="menu-item">
                    <div class="icon24 icon-bank"></div>
                    <div class="menu-option">Mua hàng</div>
                </router-link>
                <router-link to="/SellView" class="menu-item">
                    <div class="icon24 icon-sell"></div>
                    <div class="menu-option">Bán hàng</div>
                </router-link>
                <router-link to="/BillView" class="menu-item">
                    <div class="icon24 icon-invoice"></div>
                    <div class="menu-option">Quản lý hóa đơn</div>
                </router-link>
                <router-link to="/RepositoryView" class="menu-item">
                    <div class="icon24 icon-stock"></div>
                    <div class="menu-option">Kho</div>
                </router-link>
                <router-link to="/ToolView" class="menu-item">
                    <div class="icon24 icon-tool"></div>
                    <div class="menu-option">Công cụ dụng cụ</div>
                </router-link>
                <router-link to="/AssetsView" class="menu-item">
                    <div class="icon24 icon-asset"></div>
                    <div class="menu-option">Tài sản cố định</div>
                </router-link>
                <router-link to="/TaxView" class="menu-item">
                    <div class="icon24 icon-tax"></div>
                    <div class="menu-option">Thuế</div>
                </router-link>
                <router-link to="/PriceView" class="menu-item">
                    <div class="icon24 icon-cost"></div>
                    <div class="menu-option">Giá thành</div>
                </router-link>
                <router-link to="/SyntheticView" class="menu-item">
                    <div class="icon24 icon-general"></div>
                    <div class="menu-option">Tổng hợp</div>
                </router-link>
                <router-link to="/BudgetView" class="menu-item">
                    <div class="icon24 icon-budget"></div>
                    <div class="menu-option">Ngân sách</div>
                </router-link>
                <router-link to="/ReportView" class="menu-item">
                    <div class="icon24 icon-report"></div>
                    <div class="menu-option">Báo cáo</div>
                </router-link>
                <router-link to="/AnalysisView" class="menu-item">
                    <div class="icon24 icon-finance"></div>
                    <div class="menu-option">Phân tích tài chính</div>
                </router-link>
            </div>
        </div>
</template>


<script>
export default {
  name: 'TheMenu',
  components: {
    
  }
}
</script>
<style>
 @import url(../../css/layout/menu.css);
</style>