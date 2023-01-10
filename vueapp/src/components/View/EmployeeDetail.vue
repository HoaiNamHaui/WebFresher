<template>
  <div id="overlay" class="overlay">
    <div class="dialog">
      <div class="dialog-button">
        <div class="icon-hint"></div>
        <div
          @click="checkChangeAndHideDialog"
          class="icon-close"
          id="btnClose"
        ></div>
      </div>
      <div class="dialog-header">
        <div class="dialog-title">{{ titleForm }}</div>
        <div class="dialog-option">
          <input type="checkbox" name="" id="" />
          <label for="">Là khách hàng</label>
          <input type="checkbox" name="" id="" />
          <label for="">Là nhà cung cấp</label>
        </div>
      </div>
      <div class="data-field">
        <div class="field-above flexbox">
          <div class="field-above-left" style="margin-right: 26px">
            <div class="flexbox">
              <div class="m-b-12" style="margin-right: 8px">
                <label for="">Mã <span class="red">*</span></label> <br />
                <input
                  ref="txtCode"
                  v-model="employee.EmployeeCode"
                  type="text"
                  class="input input150"
                  tabindex="1"
                />
                <br />
              </div>
              <div class="m-b-12">
                <label for="">Tên <span class="red">*</span></label> <br />
                <input
                  ref="txtName"
                  v-model="employee.FullName"
                  type="text"
                  class="input input230"
                  tabindex="2"
                />
              </div>
            </div>
            <div class="m-b-12">
              <label for="">Đơn vị <span class="red">*</span></label>
              <!-- <input type="text" class="input input385"> -->
              <base-input-combobox
                :id="employee.DepartmentId"
                :name="employee.DepartmentName"
                @deparment="deparmentSelect"
              />
            </div>
            <div>
              <label for="">Chức danh</label> <br />
              <input
                v-model="employee.PositionName"
                type="text"
                class="input input385"
                tabindex="6"
              />
            </div>
          </div>
          <div class="field-above-right">
            <div class="flexbox">
              <div class="m-b-12" style="margin-right: 10px">
                <label for="">Ngày sinh</label> <br />
                <input
                  ref="dob"
                  v-model="employee.DateOfBirth"
                  type="date"
                  class="input input150"
                  tabindex="3"
                />
              </div>
              <div class="m-b-12">
                <label for="">Giới tính</label> <br />
                <div style="line-height: 32px; margin-top: 6px" class="flexbox">
                  <input
                    ref="checked"
                    class="m-l-10 m-r-5"
                    type="radio"
                    name="gender"
                    v-model="employee.Gender"
                    value="0"
                  />
                  Nam
                  <input
                    v-model="employee.Gender"
                    value="1"
                    class="m-l-10 m-r-5"
                    type="radio"
                    name="gender"
                  />
                  Nữ
                  <input
                    v-model="employee.Gender"
                    value="2"
                    class="m-l-10 m-r-5"
                    type="radio"
                    name="gender"
                  />
                  Khác
                </div>
              </div>
            </div>
            <div class="flexbox">
              <div class="m-b-12 label" style="margin-right: 8px">
                <div class="error-info">
                  <div class="error-text">Số chứng minh nhân dân</div>
                  <div class="error-arrow"></div>
                </div>
                <label for="" class="">Số CMND</label> <br />
                <input
                  v-model="employee.IdentityNumber"
                  type="text"
                  class="input input240"
                  tabindex="4"
                />
              </div>
              <div class="m-b-12" style="margin-right: 8px">
                <label for="">Ngày cấp</label> <br />
                <input
                  v-model="employee.IdentityDate"
                  type="date"
                  class="input input150"
                  tabindex="5"
                />
              </div>
            </div>
            <div>
              <label for="">Nơi cấp</label> <br />
              <input
                v-model="employee.IdentityPlace"
                type="text"
                class="input input395"
                tabindex="7"
              />
            </div>
          </div>
        </div>
        <!-- <div class="line"></div> -->
        <div class="field-below">
          <div class="m-b-12">
            <label for="">Địa chỉ</label> <br />
            <input
              v-model="employee.Address"
              class="input input800"
              tabindex="8"
            />
          </div>
          <div class="flexbox">
            <div class="m-b-12" style="margin-right: 8px">
              <label for="">ĐT di động</label> <br />
              <input
                v-model="employee.PhoneNumber"
                class="input input200"
                tabindex="9"
              />
            </div>
            <div class="m-b-12" style="margin-right: 8px">
              <label for="">ĐT cố định</label> <br />
              <input class="input input200" tabindex="10" />
            </div>
            <div class="m-b-12">
              <label for="">Email</label> <br />
              <input
                v-model="employee.Email"
                class="input input200"
                tabindex="11"
              />
            </div>
          </div>
          <div class="flexbox">
            <div style="margin-right: 8px">
              <label for="">Tài khoản ngân hàng</label> <br />
              <input class="input input200" tabindex="12" />
            </div>
            <div style="margin-right: 8px">
              <label for="">Tên ngân hàng</label> <br />
              <input class="input input200" tabindex="13" />
            </div>
            <div>
              <label for="">Chi nhánh</label> <br />
              <input class="input input200" tabindex="14" />
            </div>
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <button @click="hideDialog" class="button-white" id="btnCancel">
          Hủy
        </button>
        <div>
          <button class="button-white" @click="saveEmployee">Cất</button>
          <button class="button" @click="saveAndAdd">Cất và Thêm</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import MISAResource from "@/js/base/resource";
import BaseInputCombobox from "../Base/BaseInputCombobox.vue";
export default {
  name: "EmployeeDialog",
  components: {
    BaseInputCombobox,
  },
  data() {
    return {
      employee: {},
      errors: {
        code: "",
        name: "",
        phone: "",
        dob: "",
      },
      department: {},
    };
  },
  props: ["employeeIdSelected", "acceptSave"],
  watch: {
    acceptSave: function () {
      this.saveEmployee();
    },
  },
  methods: {
    /**
     * Lưu và đóng form
     * Author: NHNam (9/1/2023)
     */
    async saveEmployee() {
      try {
        // this.validate();
        if (this.validate()) {
          await this.saveData();
          this.hideDialog();
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Chọn phòng ban, lấy id , tên phòng cho employee
     * Author: NHNam (8/1/2023)
     */
    deparmentSelect(e) {
      this.department = e;
      this.employee.DepartmentName = this.department.DepartmentName;
      this.employee.DepartmentId = this.department.DepartmentId;
    },
    /**
     * Định dạng lại ngày để bind vào input
     * Author: NHNam (7/1/2023)
     */
    fomartDate(date) {
      try {
        if (date) {
          date = new Date(date);
          let newDate = date.getDate();
          let month = date.getMonth() + 1;
          let year = date.getFullYear();
          newDate = newDate < 10 ? `0${newDate}` : newDate;
          month = month < 10 ? `0${month}` : month;
          return `${year}-${month}-${newDate}`;
        }
      } catch (error) {
        console.log(error);
        return "";
      }
    },
    /**
     * Validate form
     * Author: NHNam (7/1/2023)
     */
    validate() {
      // Bỏ trống mã nhân viên
      if (!this.employee.EmployeeCode) {
        this.errors.code = "Mã nhân viên không được bỏ trống";
        this.$refs.txtCode.classList.add("error");
        this.$emit("sendMessage", this.errors.code);
        return false;
      } else {
        this.$refs.txtCode.classList.remove("error");
      }
      // Bỏ trống tên nhân viên
      if (!this.employee.FullName) {
        this.errors.name = "Tên nhân viên không được bỏ trống";
        this.$refs.txtName.classList.add("error");
        this.$emit("sendMessage", this.errors.name);
        return false;
      } // Độ dài tên quá 255 kí tự
      else if (this.employee.FullName.length > 255) {
        this.errors.name = "Tên nhân viên dưới 255 kí tự";
        this.$refs.txtName.classList.add("error");
        this.$emit("sendMessage", this.errors.name);
        return false;
      } else {
        this.$refs.txtName.classList.remove("error");
      }
      // Dưới 18 tuổi
      if (this.employee.DateOfBirth) {
        var year = new Date(this.employee.DateOfBirth);
        var dob = year.getFullYear();
        var current = new Date();
        if (current.getFullYear() - dob < 18) {
          this.errors.dob = "Tuổi nhân viên phải trên 18";
          this.$refs.dob.classList.add("error");
          this.$emit("sendMessage", this.errors.dob);
          return false;
        } else {
          this.$refs.dob.classList.remove("error");
        }
      }

      return true;
    },
    /**
     * Lưu data
     * Author: NHNam (7/1/2023)
     */
    async saveData() {
      var me = this;
      var newData = this.employee;
      var res;
      if (!this.employee.EmployeeId) {
        res = await axios.post(
          "https://cukcuk.manhnv.net/api/v1/Employees",
          newData
        );
        console.log(res.data);
        console.log("test1");
        me.$emit("showToast", MISAResource.vi.add);
        console.log("test1");
      } else {
        res = await axios.put(
          `https://cukcuk.manhnv.net/api/v1/Employees/${this.employee.EmployeeId}`,
          newData
        );
        console.log(res.data);
        console.log("test2");
        me.$emit("showToast", MISAResource.vi.update);
        console.log("test2");
      }
    },
    /**
     * Cất và thêm (lưu data và reset form)
     */
    saveAndAdd() {
      if (this.validate()) {
        this.saveData();
        this.employee = {};
        this.getNewEmployeeCode();
      }
    },
    /**
     * Đóng form
     * Author: NHNam (2/1/2023)
     */
    hideDialog() {
      this.$emit("onClose");
    },
    /**
     * dữ liệu thay đổi, xác nhận có muốn cất dữ liệu hay không
     */
    checkChangeAndHideDialog() {
      this.$emit("confirmClose");
    },
    /**
     * Lấy mã nhân viên mới
     * Author: NHNam (6/1/2023)
     */
    getNewEmployeeCode() {
      var me = this;
      axios
        .get("https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode")
        .then(function (res) {
          me.employee.EmployeeCode = res.data;
          me.$refs.txtCode.focus();
        });
    },
  },
  computed: {
    /**
     * Đổi Tiêu đề form theo form mode
     * Author: NHNam (7/1/2023)
     */
    titleForm() {
      return this.employeeIdSelected === null
        ? "Thêm nhân viên"
        : "Thông tin nhân viên";
    },
  },
  created() {
    // Có Id thì call api lấy thông tin nhân viên, nếu không lấy mã nhân viên mới
    if (this.employeeIdSelected != null) {
      var me = this;
      axios
        .get(
          `https://cukcuk.manhnv.net/api/v1/Employees/${this.employeeIdSelected}`
        )
        .then(function (res) {
          me.employee = res.data;
          me.employee.DateOfBirth = me.fomartDate(res.data.DateOfBirth);
          me.employee.IdentityDate = me.fomartDate(res.data.IdentityDate);
          me.$refs.txtCode.focus();
        });
    } else {
      this.getNewEmployeeCode();
      this.employee.Gender = 0;
    }
  },
};
</script>
<style>
@import url(../../css/layout/dialog.css);
</style>
