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
          <div style="color: #0075c0;">Mở rộng</div>
          <div
            class="refresh"
            @click="filterEmployee"
            style="position: relative"
          >
            <base-tooltip message="Làm mới danh sách" />
          </div>
          <base-button @click="showDialog" btnName="Thêm" />
        </div>
      </div>
      <div class="list-account">
        <table class="data-account" id="tblAccount" cellspacing="0">
          <tr>
            <th class="w10 align-left">Số tài khoản</th>
            <th class="w15 align-left">Tên tài khoản</th>
            <th class="w10 align-left">Tính chất</th>
            <th class="w15 align-left">Tên tiếng anh</th>
            <th class="w30 align-left">Diễn giải</th>
            <th class="w10 align-left">Trạng thái</th>
            <th class="w10 align-center">Chức năng</th>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
          <tr>
            <td>111</td>
            <td>Tiền mặt</td>
            <td>Dư nợ</td>
            <td>Cash in hand</td>
            <td></td>
            <td>Đang sử dụng</td>
            <td>
                <span style="cursor: pointer" @click="handleEditClick(employee)"
                >Sửa</span>
                <div class="down-icon" @click="toogleMenu"></div>
            </td>
          </tr>
        </table>
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
            <div class="box-icon" @click="showPageOption">
              <button id="cbx-icon" class="cbx-icon"></button>
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
    @closeForm = "closeForm"
    />
  </div>
</template>
<script>
import BaseButton from "../base/button/BaseButton.vue";
import BaseTooltip from "../base/BaseTooltip.vue";
import PageCombobox from "../base/PageCombobox.vue";
import Paginate from "vuejs-paginate-next";
import AccountDetail from "../forms/AccountDetail.vue";
import $ from "jquery";
export default {
  name: "DepositView",
  components: {
    BaseButton,
    BaseTooltip,
    PageCombobox,
    Paginate,
    AccountDetail
  },
  data(){
    return{
      pageSize: 20,
      pageNumber: 1, // trang hiện tại
      txtSearch: "", // keyword lọc
      totalPage: 0, // tổng số trang
      totalRecord: 0, // tổng số bản ghi
      page: 1,
      isShowDialog: false,
      isShowFooterCbb: false,
    }
  },

  methods:{
    showDialog(){
      this.isShowDialog = true;
    },
    closeForm(){
      this.isShowDialog = false;
    },
    async clickCallback(pageNum) {
      this.pageNumber = pageNum;
      // this.rowSelected = [];

      // this.isCheckAll = false;
      this.txtSearch = "";
    },
    showPageOption() {
      $("#cbx-icon").toggleClass("toogle-rotate");
      if (!this.isShowFooterCbb) {
        this.showPageFooterOption(true);
      } else {
        this.isShowFooterCbb = false;
      }
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
  }
};
</script>
<style>

@import url(../../css/layout/content.css);
@import url(../../css/layout/footer.css);
@import url(../../css/base/combobox/combobox.css);

.data-account td{
  border-right: 1px solid #c7c7c7;
    border-bottom: 1px solid #c7c7c7;
}
.data-account th{
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

</style>
