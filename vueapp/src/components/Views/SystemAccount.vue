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
          <div style="color: #0075c0; cursor: pointer" @click="toogleExpanded">
            {{ isExpandAll ? "Thu gọn" : "Mở rộng" }}
          </div>
          <div
            class="refresh"
            @click="refreshListAccount"
            style="position: relative"
          >
            <base-tooltip message="Làm mới danh sách" />
          </div>
          <base-small-button @click="showDialog" btnName="Thêm">
          </base-small-button>
        </div>
      </div>
      <div class="list-account" style="position: relative">
        <base-no-data v-if="!(accounts.length > 0)" />
        <DxTreeList
          id="tasks"
          :data-source="accounts"
          :column-auto-width="true"
          :word-wrap-enabled="true"
          :sorting="false"
          :expanded-row-keys="
            !isExpandAll ? expandedRowKeys : newExpandedRowKeys
          "
          key-expr="AccountId"
          parent-id-expr="ParentId"
          @cellDblClick="handleRowDblClick"
          @cellClick="handleCellClick"
          noDataText=""
        >
          <DxScrolling mode="standard" />
          <DxPaging :enabled="false" :page-size="10" />
          <DxPager
            :show-page-size-selector="true"
            :allowed-page-sizes="allowedPageSizes"
            :show-info="true"
          />
          <DxColumn
            :width="130"
            data-field="AccountNumber"
            caption="Số tài khoản"
          />
          <!-- <DxColumn :width="0" data-field="AccountId" caption="ID" /> -->
          <DxColumn
            :width="240"
            data-field="AccountName"
            caption="Tên tài khoản"
          />
          <DxColumn :width="140" data-field="TypeName" caption="Tính chất" />
          <DxColumn
            :width="250"
            data-field="EnglishName"
            caption="Tên tiếng anh"
          />
          <DxColumn :width="300" data-field="Description" caption="Diễn giải" />
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
          <div class="context-menu-item" @click="handleDuplicate">Nhân bản</div>
          <div class="context-menu-item" @click="ShowWarningDelete">Xóa</div>
          <div
            class="context-menu-item"
            v-if="accountSelected.IsActive"
            @click="stopUsing"
          >
            Ngừng sử dụng
          </div>
          <div
            class="context-menu-item"
            v-if="!accountSelected.IsActive"
            @click="using"
          >
            Sử dụng
          </div>
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
      :id-account-selected="idAccountSelected"
      @reloadListAccount="refreshListAccount"
      @showToast="showToast"
      :isDuplicate="isDuplicate"
      @changeDuplicateMode="isDuplicate = false"
    />
    <base-loading v-if="isLoading" />
    <message-delete
      v-if="isShowMessageDelete"
      @confirmDelete="confirmDelete"
      @cancelDelete="isShowMessageDelete = false"
      messageTitle="Xóa tài khoản"
      type-object="tài khoản"
      :object-code="accountSelected.AccountNumber"
    />
    <mesage-confirm
      v-if="isShowMessageConfirm"
      @confirmMessage="confirmMessage"
      @closeMessage="closeAndUse"
      messageTitle="Thông báo"
    />
    <BaseToast
      v-if="isShowToast"
      @hideToast="isShowToast = false"
      :message="message"
    />
    <message-error v-if="isError" :error="error" @close="isError = false" />
  </div>
</template>
<script>
import MessageError from "../message/MessageError.vue";
import BaseToast from "../base/BaseToast.vue";
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
// import { DxTreeList, DxColumn } from "devextreme-vue/tree-list";
import {
  DxTreeList,
  DxColumn,
  DxPaging,
  DxPager,
  DxScrolling,
} from "devextreme-vue/tree-list";
import BaseLoading from "../base/BaseLoading.vue";
import MessageDelete from "../message/MessageDelete.vue";
import MISAResource from "@/js/base/resource";
import MISAEnum from "@/js/base/enum";
import MesageConfirm from '../message/MesageConfirm.vue';
// import $ from "jquery";
export default {
  name: "SystemAccount",
  components: {
    BaseSmallButton,
    DxPaging,
    DxPager,
    DxScrolling,
    BaseTooltip,
    PageCombobox,
    Paginate,
    AccountDetail,
    DxTreeList,
    DxColumn,
    BaseLoading,
    BaseNoData,
    MessageDelete,
    BaseToast,
    MessageError,
    MesageConfirm
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
      focusSearch: false,
      isShowDialog: false,
      isShowFooterCbb: false,
      accounts: [],
      childAccounts: [],
      accountSelected: {},
      idAccountSelected: null,
      isShowMessageDelete: false,
      isShowMessageConfirm: false,
      isError: false,
      error: "",
      isShowToast: false,
      message: "",
      isDuplicate: 0,
      isExpandAll: false,
      newExpandedRowKeys: [],
      expandedRowKeys: [],
      allowedPageSizes: [10, 20, 30, 50, 100],
    };
  },
  created() {
    this.getChildrenAccount();
    this.filterAccount();
  },
  mounted() {
    document.addEventListener("keydown", this.handleKeydown);
  },
  watch: {
    txtSearch: function () {
      this.page = 1;
      this.pageNumber = 1;
      this.getChildrenAccount();
      this.filterAccount();
    },
  },
  methods: {
    toogleExpanded() {
      try {
        this.isExpandAll = !this.isExpandAll;
        const newListAccount = this.accounts.map((item) => {
          if (item.IsParent === true) {
            return item.AccountId;
          }
        });
        this.newExpandedRowKeys = [...newListAccount];
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Ngưng sử dụng
     * Author: NHNam (203/2023)
     */
    async stopUsing() {
      // var me = this;
      // this.accountSelected.IsActive = false;
      // console.log(this.accountSelected);
      // if (this.accountSelected.ParentId == 0) {
      //   this.accountSelected.ParentId = MISAResource.vi.GUID_EMPTY;
      // }
      // axios
      //   .put(
      //     MISAapi.account.base + this.accountSelected.AccountId,
      //     this.accountSelected
      //   )
      //   .then(() => {
      //     me.refreshListAccount();
      //   })
      //   .catch((res) => {
      //     console.log(res);
      //   });
      await this.handleActiveAccount(this.accountSelected.AccountId, true, this.accountSelected.IsParent, true)
      await this.getChildrenAccount();
      this.filterAccount();
    },

    /**
     * update trạng thái sử dụng
     * @param {} isActive
     * @param {*} listId
     */
    async updateIsActiveAccount(listId, isActive) {
      var me = this;
      axios
        .put(
          `https://localhost:7116/api/v1/Accounts/UpdateIsActive?isActive=${isActive}`,
          listId
        )
        .then(() => {
          me.refreshListAccount();
        })
        .catch((res) => {
          console.log(res);
        });
    },

    async handleActiveAccount(accountId, isActive, isParent, hasActiveChild) {
      try {
        if (isParent) {
          if (hasActiveChild) {
            var accountIds = [accountId];
            await this.getChildrenAccount();
            var childs = this.childAccounts;

            childs.forEach((child) => {
              try {
                if (accountIds.includes(child.ParentId)) {
                  accountIds.push(child.AccountId);
                }
              } catch (error) {
                console.log(error);
              }
            });
            await this.updateIsActiveAccount(accountIds, !isActive);
          } else {
            await this.updateIsActiveAccount([accountId], !isActive);
          }
        }else{
          await this.updateIsActiveAccount([accountId], !isActive);
        }
      } catch (error) {
        console.log(error);
      }
    },
    async closeAndUse(){
      await this.handleActiveAccount(this.accountSelected.AccountId, false, this.accountSelected.IsParent, false)
      await this.getChildrenAccount();
      this.isShowMessageConfirm = false;
      this.filterAccount();
    },
    /**
     * Sử dụng
     * Author: NHNam (203/2023)
     */
    async using() {
      // var me = this;
      // this.accountSelected.IsActive = true;
      // console.log(this.accountSelected);
      // if (this.accountSelected.ParentId == 0) {
      //   this.accountSelected.ParentId = MISAResource.vi.GUID_EMPTY;
      // }
      // axios
      //   .put(
      //     MISAapi.account.base + this.accountSelected.AccountId,
      //     this.accountSelected
      //   )
      //   .then(() => {
      //     me.refreshListAccount();
      //   })
      //   .catch((res) => {
      //     console.log(res);
      //   });
      if(this.accountSelected.IsParent){
        this.isShowMessageConfirm = true;
      }
      else{
        await this.confirmMessage();
      }
    },

    /**
     * xử lý active tài khoản
     * Author: NHNam (20/3/2023)
     */
    async confirmMessage(){
      await this.handleActiveAccount(this.accountSelected.AccountId, false, this.accountSelected.IsParent, true)
      await this.getChildrenAccount();
      this.isShowMessageConfirm = false;
      this.filterAccount();
    },
    /**
     * nhân bản
     * Author: NHNam (203/2023)
     */
    handleDuplicate() {
      this.isDuplicate = MISAEnum.FormMode.Duplicate;
      this.handleClickEdit(this.accountSelected);
    },

    /**
     * show toast message
     * Author: NHNam (203/2023)
     */
    showToast(e) {
      this.message = e;
      this.isShowToast = true;
      var me = this;
      setTimeout(function () {
        me.isShowToast = false;
      }, 3000);
    },
    /**
     * Xác nhận xóa
     * Author: NHNam (203/2023)
     */
    confirmDelete() {
      var me = this;
      try {
        var url = MISAapi.account.base + me.accountSelected.AccountId;
        me.isLoading = true;
        me.isShowMessageDelete = false;
        axios
          .delete(url)
          .then(() => {
            me.getChildrenAccount();
            me.filterAccount();
            me.isLoading = false;
          })
          .catch((res) => {
            console.log(res);
            me.isLoading = false;
            me.error = MISAResource.vi.account.referenceMessage;
            me.isError = true;
          });
      } catch (error) {
        console.log("catch dưới");
        me.isLoading = false;
        console.log(error);
      }
    },

    /**
     * Cảnh báo xóa
     * Author: NHNam
     */
    ShowWarningDelete() {
      if (this.accountSelected.IsParent) {
        this.error = MISAResource.vi.account.parentMessage;
        this.isError = true;
      } else {
        console.log(this.accountSelected);
        this.isShowMessageDelete = true;
      }
    },

    /**
     * delay thời gian lấy giá trị input search
     * Author: NHNam (203/2023)
     */
    debounceSearch(e) {
      this.txtSearch = e;
    },
    /**
     * Row Double click
     * @param {account selected} e
     * Author: NHNam (203/2023)
     */
    handleRowDblClick(e) {
      this.handleClickEdit(e.data);
    },
    /**
     * Row click
     * @param {account selected} e
     * Author: NHNam (203/2023)
     */
    handleCellClick(e) {
      this.accountSelected = e.data;
    },

    /**
     * click nút sửa
     * Author: NHNam (203/2023)
     */
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
     * Author: NHNam (19/3/2023)
     */
    async refreshListAccount() {
      await this.getChildrenAccount();
      this.filterAccount();
    },
    /**
     * lọc phân trang theo tài khoản cha
     * Author: NHNam (203/2023)
     */
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
          if (!me.txtSearch) {
            me.totalRecord += me.childAccounts.length;
          } else {
            me.totalRecord += countRecords;
          }

          me.isLoading = false;
        })
        .catch(function (res) {
          console.log(res);
        });
    },

    /**
     * lấy tài khoản con
     * Author: NHNam (203/2023)
     */
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
      await this.getChildrenAccount();
      await this.filterAccount();
    },
    /**
     * mở menu context
     * Author: NHNam (1/1/2023)
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

    /**
     * show form detail
     * Author: NHNam (203/2023)
     */
    showDialog() {
      this.isShowDialog = true;
    },
    /**
     * đóng form detail
     * Author: NHNam (203/2023)
     */
    closeForm() {
      (this.accountSelected = {}), (this.idAccountSelected = null);
      this.isShowDialog = false;
    },
    /**
     * chuyển trang
     * Author: NHNam (203/2023)
     */
    async clickCallback(pageNum) {
      this.pageNumber = pageNum;
      await this.getChildrenAccount();
      this.filterAccount();
      this.txtSearch = "";
    },
    /**
     * mở option chọn số lượng bản ghi
     * Author: NHNam (203/2023)
     */
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
  /* height: 100%; */
  overflow: auto;
}
/* .dx-treelist-content{
  height: 100%;
  width: 100%;
  overflow: scroll;
} */
/* .dx-treelist-table tbody tr:first-child{
  position: sticky !important;
  top: 0;
} */
/* .dx-treelist-table tbody tr:first-child td:last-child{
  background-color: #e5e8ec;
  border-bottom: 1px solid #ddd;
} */
.dx-treelist-headers {
  background-color: #e5e8ec;
  color: #333333;
  display: table-cell;
  font-size: 13px;
  font-weight: 700;
  line-height: 18px;
  text-align: left;
}
.dx-treelist-rowsview {
  height: calc(484px - 28px) !important;
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
  /* position: sticky;
  right: 0; */
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
