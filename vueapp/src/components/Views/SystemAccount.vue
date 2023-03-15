<template>
  <!-- <div style="padding: 20px 20px">
          <h1>Tiền gửi</h1>
      </div> -->
  <div>
    <div class="content-top">
      <div class="title">Hệ thống tài khoản</div>
      <!-- <base-button @click="ShowDialog" btnName="Thêm" /> -->
    </div>
    <div class="content-data">
      <div class="functions">
        <div class="functions-left">
          <div class="search-box" :class="{ focus: this.focusSearch }">
            <input
              @focus="this.focusSearch = true"
              @blur="this.focusSearch = false"
              type="text"
              placeholder="Tìm theo số, tên tài khoản"
              v-debounce:1s="debounceSearch"
            />
            <div class="search-icon"></div>
          </div>
        </div>
        <div class="functions-right">
          <div style="color: #0075c0; cursor: pointer;">Mở rộng</div>
          <div
            class="refresh"
            @click="filterEmployee"
            style="position: relative"
          >
            <base-tooltip message="Làm mới danh sách" />
          </div>
          <base-small-button @click="showDialog" btnName="Thêm" />
        </div>
      </div>
      <div class="list-account">
        <DxTreeList
          id="tasks"
          :data-source="fakeData"
          :column-auto-width="true"
          :word-wrap-enabled="true"
          :sorting="false"
          key-expr="AccountId"
          parent-id-expr="ParentAccountId"
        >
          <DxColumn
            :width="130"
            data-field="BankNumber"
            caption="Số tài khoản"
          />
          <DxColumn :width="0" data-field="AccountId" caption="ID" />
          <DxColumn
            :width="250"
            data-field="BankName"
            caption="Tên tài khoản"
          />
          <DxColumn :width="100" data-field="Nature" caption="Tính chất" />
          <DxColumn
            :width="250"
            data-field="EnglishName"
            caption="Tên tiếng anh"
          />
          <DxColumn :width="332" data-field="Explain" caption="Diễn giải" />
          <DxColumn :width="140" data-field="IsActive" caption="Trạng thái" />
          <DxColumn
            :width="0"
            data-field="IsParent"
            caption="cha"
            style="display: none"
          />
          <DxColumn
            :width="124"
            caption="Chức năng"
            cell-template="functionTemplate"
          />
          <template #functionTemplate="">
            <div class="flex">
              <span
                style="
                  cursor: pointer;
                  font-weight: bold;
                  color: #0075c0 !important;
                "
                @click="handleEditClick(employee)"
                >Sửa</span
              >
              <div class="down-icon" @click="toogleMenu"></div>
            </div>
          </template>
        </DxTreeList>
        <div class="context-menu" ref="menuContext">
          <div class="context-menu-item">Nhân bản</div>
          <div class="context-menu-item">Xóa</div>
          <div class="context-menu-item">Ngừng sử dụng</div>
        </div>
      </div>
      <footer>
        <div class="total-record">
          Tổng số:
          <span style="font-family: Notosans-SemiBold">{{ totalRecord }}</span>
          bản ghi
        </div>
        <div class="pagination">
          <div class="pages-option">
            {{ pageSize }} bản ghi trên 1 trang
            <div
              class="box-icon"
              @click="showPageOption"
              v-click-outside-element="close"
            >
              <button
                id="cbx-icon"
                :class="{ 'toogle-rotate': isShowFooterCbb }"
                class="cbx-icon"
              ></button>
            </div>
            <page-combobox
              :isShowCbb="isShowFooterCbb"
              @sizeRecord="sizeRecord"
            />
          </div>
          <Paginate
            v-model="page"
            :page-count="totalPage"
            :page-range="2"
            :margin-pages="1"
            :click-handler="clickCallback"
            :prev-text="'Trước'"
            :next-text="'Sau'"
            :container-class="'pagination'"
            :page-class="'page-item'"
          />
        </div>
      </footer>
    </div>
    <account-detail v-if="isShowDialog" @closeForm="closeForm" />
  </div>
</template>
<script>
import BaseSmallButton from "../base/button/BaseSmallButton.vue";
import BaseTooltip from "../base/BaseTooltip.vue";
import PageCombobox from "../base/PageCombobox.vue";
import Paginate from "vuejs-paginate-next";
import AccountDetail from "../forms/account/AccountDetail.vue";
import $ from "jquery";
import { DxTreeList, DxColumn } from "devextreme-vue/tree-list";
// import $ from "jquery";
export default {
  name: "SystemAccount",
  components: {
    BaseSmallButton,
    BaseTooltip,
    PageCombobox,
    Paginate,
    AccountDetail,
    DxTreeList,
    DxColumn,
  },
  data() {
    return {
      pageSize: 20,
      pageNumber: 1, // trang hiện tại
      txtSearch: "", // keyword lọc
      totalPage: 0, // tổng số trang
      totalRecord: 0, // tổng số bản ghi
      page: 1,
      isShowDialog: false,
      isShowFooterCbb: false,
      fakeData: [
        {
          AccountId: "1",
          ParentAccountId: 0,
          IsParent: true,
          BankNumber: "111",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Ngừng sử dụng",
        },
        {
          AccountId: 2,
          ParentAccountId: "1",
          IsParent: true,
          BankNumber: "1112",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 3,
          ParentAccountId: 2,
          IsParent: true,
          BankNumber: "11123",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 4,
          ParentAccountId: 0,
          IsParent: true,
          BankNumber: "114",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 5,
          ParentAccountId: 4,
          IsParent: false,
          BankNumber: "1145",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 6,
          ParentAccountId: 2,
          IsParent: false,
          BankNumber: "11126",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 7,
          ParentAccountId: 0,
          IsParent: false,
          BankNumber: "117",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 8,
          ParentAccountId: 3,
          IsParent: false,
          BankNumber: "111238",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 9,
          ParentAccountId: 0,
          IsParent: true,
          BankNumber: "119",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 10,
          ParentAccountId: 9,
          IsParent: false,
          BankNumber: "1191",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 11,
          ParentAccountId: 0,
          IsParent: false,
          BankNumber: "111",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 12,
          ParentAccountId: 0,
          IsParent: false,
          BankNumber: "101",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 13,
          ParentAccountId: 0,
          IsParent: false,
          BankNumber: "102",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 14,
          ParentAccountId: 0,
          IsParent: false,
          BankNumber: "103",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 15,
          ParentAccountId: 0,
          IsParent: false,
          BankNumber: "104",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
        {
          AccountId: 16,
          ParentAccountId: 0,
          IsParent: false,
          BankNumber: "105",
          BankName: "Tiền mặt Việt Nam",
          Nature: "Dư có",
          EnglishName: "Cash in hand",
          Explain: "diễn giải",
          IsActive: "Đang sử dụng",
        },
      ],
    };
  },

  methods: {
    toogleMenu(e) {
      if (this.$refs.menuContext.style.display === "block") {
        $(".context-menu").hide(100);
      } else {
        this.$refs.menuContext.style.display;
        this.btnMenuContext = e.target;
        var x = e.clientX;
        var y = e.clientY;
        var clientHeight = document.getElementById("content").clientHeight;
        if (clientHeight - y < 80) {
          y = y - 110;
        }
        $(".context-menu").css("top", y + 10 + "px");
        $(".context-menu").css("left", x - 110 + "px");
        $(".context-menu").show(100);
      }
    },

    //hiện dialog
    showDialog() {
      this.isShowDialog = true;
    },
    // đóng form
    closeForm() {
      this.isShowDialog = false;
    },
    //click chuyển trang
    async clickCallback(pageNum) {
      this.pageNumber = pageNum;
      // this.rowSelected = [];

      // this.isCheckAll = false;
      this.txtSearch = "";
    },
    // mở option chọn số lượng bản ghi
    showPageOption() {
      // $("#cbx-icon").toggleClass("toogle-rotate");
      if (!this.isShowFooterCbb) {
        this.showPageFooterOption(true);
      } else {
        this.isShowFooterCbb = false;
      }
    },
    //đóng chọn số bản ghi
    close() {
      this.isShowFooterCbb = false;
    },
    /**
     * Đổi size page
     * Author: NHNam (1/1/2023)
     */
    async sizeRecord(e) {
      this.pageSize = e;
      this.showPageOption();
      this.page = 1;
      this.pageNumber = 1;
      // await this.filterEmployee();
      // this.testCheckAll();
    },
    /**
     * Hiện , ẩn chọn số bản ghi trên 1 trang
     * Author: Nguyễn Hoài Nam(1/1/2023)
     * isShowFooterCbb = true: hiển thị
     * isShowFooterCbb = false: ẩn
     */
    showPageFooterOption(isShowCbb) {
      if (isShowCbb) {
        this.isShowFooterCbb = isShowCbb;
      }
    },
  },
};
</script>
<style>
@import url(../../css/layout/content.css);
@import url(../../css/layout/footer.css);
@import url(../../css/base/combobox/combobox.css);
@import url(../../css/base/contextmenu.css);
.data-account td {
  border-right: 1px solid #c7c7c7;
  border-bottom: 1px solid #c7c7c7;
}
.data-account th {
  border-right: 1px solid #c7c7c7;
  border-bottom: 1px solid #c7c7c7;
}

.pagination {
  margin-left: 20px;
  margin-right: 4px;
  list-style: none;
  display: flex;
  justify-content: center;
  align-items: center;
}
.page-item {
  margin: 0 10px;
  cursor: pointer;
  /* width: 24px; */
  height: 24px;
  display: flex;
  align-items: center;
  text-align: center;
  cursor: pointer;
}
.page-item.disabled {
  /* width: 48px; */
  width: 24px;
}
.page-item.active {
  border: 1px solid #dfdfdf;
  font-weight: 700;
  text-align: center;
}
.page-item.active a {
  width: 24px;
  text-align: center;
}
.page-item.disabled {
  cursor: default !important;
  color: #9e9e9e;
}
tr.active {
  background-color: #e5f3ff;
}
tr.active td:first-child,
tr.active td:last-child {
  background-color: #e5f3ff;
}

/* __________________________________- */
.dx-treelist-table {
  width: 100%;
  font-family: Notosans-Regular;
}

.dx-treelist-headers {
  background-color: #e5e8ec;
  color: #333333;
  display: table-cell;
  font-size: 13px;
  font-weight: 700;
  line-height: 18px;
  text-align: left;
}

.dx-treelist-rowsview .dx-row {
  border-bottom: 1px solid #e0e0e0;
}

.dx-treelist .dx-row > td {
  padding: 6px 10px;
}

.dx-treelist .dx-column-lines > td:last-child {
  display: flex;
  justify-content: center;
  align-items: center;
}

.dx-treelist-rowsview .dx-treelist-collapsed span::before {
  content: "\f196";
  font-family: FontAwesome;
}

.dx-treelist-rowsview .dx-treelist-expanded span::before {
  content: "\f147";
  font-family: FontAwesome;
}

.dx-row:has(
    .dx-treelist-readonly .dx-treelist-checkbox-size input[value="true"]
  ) {
  font-weight: 700;
}

.dx-widget.dx-state-readonly.dx-checkbox {
  position: absolute;
  visibility: hidden;
}

.dx-treelist .dx-column-lines > td {
  border-left: unset;
}

.dx-treelist-text-content {
  padding-left: 6px;
}

.dx-text-content-alignment-left {
  padding-left: unset;
}

@font-face {
  font-family: Notosans-Regular;
  src: url(../../assets/fonts/notosans-regular.2cb88a13.woff2);
}
</style>
