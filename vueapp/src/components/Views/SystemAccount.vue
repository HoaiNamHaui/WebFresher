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
          <div style="color: #0075c0; cursor: pointer">Mở rộng</div>
          <div
            class="refresh"
            @click="refreshListAccount"
            style="position: relative"
          >
            <base-tooltip message="Làm mới danh sách" />
          </div>
          <base-small-button @click="showDialog" btnName="Thêm" />
        </div>
      </div>
      <div class="list-account" style="position: relative;">
        <base-no-data v-if="accounts.length > 0"/>
        <DxTreeList
          id="tasks"
          :data-source="accounts"
          :column-auto-width="true"
          :word-wrap-enabled="true"
          :sorting="false"
          key-expr="AccountId"
          parent-id-expr="ParentId"
          @cellDblClick="handleRowDblClick"
          noDataText="''"
        >
          <DxColumn
            :width="130"
            data-field="AccountNumber"
            caption="Số tài khoản"
          />
          <!-- <DxColumn :width="0" data-field="AccountId" caption="ID" /> -->
          <DxColumn
            :width="250"
            data-field="AccountName"
            caption="Tên tài khoản"
          />
          <DxColumn :width="100" data-field="TypeName" caption="Tính chất" />
          <DxColumn
            :width="250"
            data-field="EnglishName"
            caption="Tên tiếng anh"
          />
          <DxColumn :width="332" data-field="Description" caption="Diễn giải" />
          <DxColumn :width="140" data-field="Active" caption="Trạng thái" />
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
          <template #functionTemplate="{ data: options }">
            <div class="flex">
              <span
                style="
                  cursor: pointer;
                  font-weight: bold;
                  color: #0075c0 !important;
                "
                @click="handleClickEdit(options.data)"
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
    <account-detail
      v-if="isShowDialog"
      @closeForm="closeForm"
      :idAccountSelected="idAccountSelected"
    />
    <base-loading v-if="isLoading"/>
  </div>
</template>
<script>
import BaseNoData from "../base/BaseNoData.vue";
import { vue3Debounce } from "vue-debounce";
import axios from "axios";
import MISAapi from "@/js/api";
import BaseSmallButton from "../base/button/BaseSmallButton.vue";
import BaseTooltip from "../base/BaseTooltip.vue";
import PageCombobox from "../base/PageCombobox.vue";
import Paginate from "vuejs-paginate-next";
import AccountDetail from "../forms/account/AccountDetail.vue";
import $ from "jquery";
import { DxTreeList, DxColumn } from "devextreme-vue/tree-list";
import BaseLoading from '../base/BaseLoading.vue';
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
    BaseLoading,
    BaseNoData
  },
  data() {
    return {
      directives: {
        debounce: vue3Debounce({ lock: true }),
      },
      isLoading: false,
      pageSize: 20,
      pageNumber: 1, // trang hiện tại
      txtSearch: "", // keyword lọc
      totalPage: 0, // tổng số trang
      totalRecord: 0, // tổng số bản ghi
      page: 1,
      isShowDialog: false,
      isShowFooterCbb: false,
      accounts: [],
      childAccounts: [],
      accountSelected: {},
      idAccountSelected: null,
    };
  },
  created() {
    this.getChildrenAccount();
    this.filterAccount();
  },
  mounted() {
    document.addEventListener("keydown", this.handleKeydown);
  },
  watch:{
    txtSearch: function () {
      this.page = 1;
      this.pageNumber = 1;
      this.getChildrenAccount();
      this.filterAccount();
    },
  },
  methods: {

    debounceSearch(e){
      this.txtSearch = e;
    },
    /**
     * Row Double click
     * @param {account selected} e
     */
    handleRowDblClick(e) {
      this.handleClickEdit(e.data);
    },

    // Xử lý click nút sửa
    handleClickEdit(account) {
      this.accountSelected = account;
      this.idAccountSelected = account.AccountId;
      this.isShowDialog = true;
    },
    /**
     * phím tắt mở form thêm mới
     * Author: NHNam (22/2/2023)
     */
    handleKeydown(event) {
      var me = this;
      if (event.ctrlKey && event.key == "1") {
        event.preventDefault();
        // event.stopPropagation();
        me.isShowDialog = true;
      }
    },
    /**
     * Làm mới list account
     */
    refreshListAccount() {
      this.getChildrenAccount();
      this.filterAccount();
    },
    //lọc phân trang theo tài khoản cha
    async filterAccount() {
      var me = this;
      var url =
        MISAapi.account.filter +
        `pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&keyword=${this.txtSearch}`;
      await axios
        .get(url)
        .then(async function (res) {
          me.totalPage = res.data.TotalPage;
          me.totalRecord = res.data.TotalRecord;
          me.accounts = res.data.Data;
        })
        .then(function () {
          // xử lý tài khoản không có tài khoản cha
          me.accounts.forEach((item) => (item.ParentId = "0"));

          // Lấy ra các id của tài khoản cha
          let accountIds = me.accounts.map((account) => account.AccountId);

          // Lấy số tài khoản con tương ứng với tài khoản cha

          let countRecords = 0;

          // Lấy những tài khoản con
          me.childAccounts.forEach((child) => {
            try {
              if (accountIds.includes(child.ParentId)) {
                me.accounts.push(child);
                accountIds = me.accounts.map((account) => account.AccountId);
                countRecords++;
              }
            } catch (error) {
              console.log(error);
            }
          });
          me.totalRecord += countRecords;
          me.isLoading = false;
        })
        .catch(function (res) {
          console.log(res);
        });
    },

    // lấy tài khoản con
    async getChildrenAccount() {
      this.isLoading = true;
      var me = this;
      var url = MISAapi.account.base + "GetChildrenAccount";
      await axios
        .get(url)
        .then(function (res) {
          me.childAccounts = res.data;
        })
        .catch(function (res) {
          console.log(res);
        });
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
      await this.filterAccount();
      await this.getChildrenAccount();
    },
    /**
     * mở menu context
     */
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
      this.filterAccount();
      this.getChildrenAccount();
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
