<template>
  <div id="overlay" class="overlay">
    <div class="dialog">
      <div class="dialog-button">
        <!-- <div class="icon-hint"></div> -->
        <div
          @click="checkChangeAndHideDialog"
          class="icon-close"
          id="btnClose"
        ></div>
      </div>
      <div class="dialog-header">
        <div class="dialog-title">{{ titleForm }}</div>
        <div class="dialog-option">
          <input type="checkbox" name="" id="" disabled />
          <label for="">Là khách hàng</label>
          <input type="checkbox" name="" id="" disabled />
          <label for="">Là nhà cung cấp</label>
        </div>
      </div>
      <div class="data-field">
        <div class="field-above flexbox">
          <div class="field-above-left" style="margin-right: 26px">
            <div class="flexbox">
              <BaseInput
                :shouldFocus="shouldFocus"
                class="input150"
                label="Mã"
                :focus="true"
                :require="true"
                v-model="employee.EmployeeCode"
                style="margin-right: 8px"
                :error="errors.code"
                ref="empCode"
                :title="errors.code"
                @unfocus="unfocus"
              />
              <BaseInput
                class="input230"
                :error="errors.name"
                label="Tên"
                :require="true"
                v-model="employee.FullName"
                :title="errors.name"
              />
            </div>
            <div class="m-b-12" style="width: 388px">
              <label for="">Đơn vị <span class="red">*</span></label>
              <BaseCombobox
                id="cbxDepartment"
                :api="departmentApi"
                propName="DepartmentName"
                propValue="DepartmentId"
                v-model="employee.DepartmentId"
                :error="errors.dept"
                :title="errors.dept"
              />
            </div>
            <BaseInput
              class="input388"
              label="Chức danh"
              :require="false"
              v-model="employee.PositionName"
            />
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
                  :class="{ 'text-gray': !employee.DateOfBirth }"
                  :title="errors.dob"
                />
              </div>
              <div class="m-b-12">
                <label for="">Giới tính</label> <br />
                <div style="line-height: 32px; margin-top: 6px" class="flexbox">
                  <input
                    ref="checked"
                    class="m-l-5 m-r-10"
                    type="radio"
                    name="gender"
                    v-model="employee.Gender"
                    :value="0"
                  />
                  Nam
                  <input
                    v-model="employee.Gender"
                    :value="1"
                    class="m-l-30 m-r-10"
                    type="radio"
                    name="gender"
                  />
                  Nữ
                  <input
                    v-model="employee.Gender"
                    :value="2"
                    class="m-l-30 m-r-10"
                    type="radio"
                    name="gender"
                  />
                  Khác
                </div>
              </div>
            </div>
            <div class="flexbox">
              <BaseInput
                class="input240"
                style="margin-right: 8px"
                label="Số CMND"
                tooltip="true"
                tooltipContent="Số chứng minh nhân dân"
                v-model="employee.IdentityNumber"
              >
              </BaseInput>
              <div class="m-b-12">
                <label for="">Ngày cấp</label> <br />
                <input
                  ref="idtDate"
                  v-model="employee.IdentityDate"
                  type="date"
                  class="input input150"
                  :class="{ 'text-gray': !employee.IdentityDate }"
                />
              </div>
            </div>
            <BaseInput
              class="input395"
              label="Nơi cấp"
              v-model="employee.IdentityPlace"
            />
          </div>
        </div>
        <!-- <div class="line"></div> -->
        <div class="field-below">
          <BaseInput
            class="input800"
            label="Địa chỉ"
            v-model="employee.Address"
          />
          <div class="flexbox">
            <!-- <div class="m-b-12" style="margin-right: 8px">
              <label for="">ĐT di động</label> <br />
              <input
                v-model="employee.PhoneNumber"
                class="input input200"
                tabindex="0"
              />
            </div> -->
            <BaseInput
              style="margin-right: 8px"
              :error="errors.phone"
              class="input200"
              label="ĐT di động"
              tooltip="true"
              tooltipContent="Điện thoại di động"
              v-model="employee.PhoneNumber"
              :title="errors.phone"
            />
            <!-- <div class="m-b-12" style="margin-right: 8px">
              <label for="">ĐT cố định</label> <br />
              <input class="input input200" tabindex="00" />
            </div> -->
            <BaseInput
              style="margin-right: 8px"
              class="input200"
              label="ĐT cố định"
              tooltip="true"
              tooltipContent="Điện thoại cố định"
              v-model="employee.TelephoneNumber"
            />
            <!-- <div class="m-b-12">
              <label for="">Email</label> <br />
              <input
                v-model="employee.Email"
                class="input input200"
                tabindex="01"
              />
            </div> -->
            <BaseInput
              class="input200"
              label="Email"
              :error="errors.email"
              v-model="employee.Email"
              :title="errors.email"
            />
          </div>
          <div class="flexbox">
            <!-- <div style="margin-right: 8px">
              <label for="">Tài khoản ngân hàng</label> <br />
              <input class="input input200" tabindex="02" />
            </div> -->
            <BaseInput
              style="margin-right: 8px"
              class="input200"
              label="Tài khoản ngân hàng"
              v-model="employee.BankAccount"
            />
            <!-- <div style="margin-right: 8px">
              <label for="">Tên ngân hàng</label> <br />
              <input class="input input200" tabindex="03" />
            </div> -->
            <BaseInput
              style="margin-right: 8px"
              class="input200"
              label="Tên ngân hàng"
              v-model="employee.BankName"
            />
            <!-- <div>
              <label for="">Chi nhánh</label> <br />
              <input class="input input200" tabindex="04" />
            </div> -->
            <BaseInput
              class="input200"
              label="Chi nhánh"
              v-model="employee.BankBranchName"
              @keydown="handleKeyDown"
            />
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <button @click="hideDialog" class="button-white" id="btnCancel">
          Hủy
        </button>
        <div style="display: flex; align-items: center">
          <button class="button-white" @click="saveEmployee">Cất</button>
          <!-- <button class="button" @click="saveAndAdd">Cất và Thêm</button> -->
          <base-button @click="saveAndAdd" btnName="Cất và Thêm" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import MISAResource from "@/js/base/resource";
import MISAapi from "@/js/api";
import MISAEnum from "@/js/base/enum";
import MISAcommon from"@/js/base/enum";
import BaseCombobox from "../base/BaseCombobox.vue";
import BaseInput from "../base/input/BaseInput.vue";
import BaseButton from "../base/button/BaseButton.vue";
export default {
  name: "EmployeeDialog",
  components: {
    BaseCombobox,
    BaseInput,
    BaseButton,
  },
  data() {
    return {
      employee: {}, //nhân viên
      employeeTemp: {},
      errors: {
        // các trường validate
        code: "", // mã nv
        name: "", // tên nv
        phone: "", // số đt
        dob: "", // ngày sinh
        dept: "", // đơn vị
        email: "", // email
      },
      shouldFocus: true,
      isValid: false, // dữ liệu hợp lệ
      department: {}, // phòng ban
      departmentApi: MISAapi.department.filter,
      titleForm: "", // tiêu đề form
    };
  },
  props: ["employeeIdSelected", "acceptSave", "isDuplicate"],
  watch: {
    acceptSave: function () {
      this.saveEmployee();
    },
  },
  methods: {
    /**
     * Tab vòng về mã nhân viên
     * Author: NHNam (22/2/2023)
     */
     handleKeyDown(event){
      if (event.keyCode === 9) {
        this.shouldFocus = true;
      }
     },
    /**
     * Cập nhật trạng thái focus
     * Author: NHNam (21/2/2023)
     */
    unfocus(){
      this.shouldFocus = false;
    },

    /**
     * xử lý lỗi server trả về
     * @param statuscode} errorCode 
     * Author: NHNam (22/2/2023)
     */
     handleErrorCode(errorCode){
      return MISAcommon.handleErrorCode(errorCode)
    },

    /**
     * Lưu và đóng form
     * Author: NHNam (9/1/2023)
     */
    async saveEmployee() {
      try {
        this.validate();
        if (this.isValid) {
          await this.saveData();
          this.hideDialog();
        }
      } catch (error) {
        console.log(error);
      }
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
     * Validate formnhân viên
     * Author: NHNam (7/1/2023)
     */
    validate() {
      // reset errors
      this.errors = {
        code: "",
        name: "",
        phone: "",
        dob: "",
        dept: "",
        email: "",
      };
      // Bỏ trống mã nhân viên
      if (!this.employee.EmployeeCode) {
        this.errors.code = MISAResource.vi.error.emptyCode;
        // this.$emit("sendMessage", this.errors.code);
        // return false;
        this.isValid = false;
      }

      // Bỏ trống tên nhân viên
      if (!this.employee.FullName) {
        this.errors.name = MISAResource.vi.error.emptyName;
        // this.$emit("sendMessage", this.errors.name);
        // return false;
        this.isValid = false;
      } // Độ dài tên quá 255 kí tự
      else if (this.employee.FullName.length > 255) {
        this.errors.name = MISAResource.vi.error.lengthName;
        // this.$emit("sendMessage", this.errors.name);
        // return false;
        this.isValid = false;
      }

      // Bỏ trống đơn vị
      if (!this.employee.DepartmentId) {
        this.errors.dept = MISAResource.vi.error.emptyDepartment;
        // this.$emit("sendMessage", this.errors.dept);
        // return false;
        this.isValid = false;
      }

      // Dưới 18 tuổi
      if (this.employee.DateOfBirth) {
        var year = new Date(this.employee.DateOfBirth);
        var dob = year.getFullYear();
        var current = new Date();
        if (current.getFullYear() - dob < 18) {
          this.errors.dob = MISAResource.vi.error.age;
          this.$refs.dob.classList.add("error");
          // this.$emit("sendMessage", this.errors.dob);
          // return false;
          this.isValid = false;
        } else {
          this.$refs.dob.classList.remove("error");
        }
      }
      // Số điện thoại không đúng độ dài, định dạng, đầu số
      if (this.employee.PhoneNumber) {
        var phoneRegex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        if (phoneRegex.test(this.employee.PhoneNumber) == false) {
          this.errors.phone = MISAResource.vi.error.phone;
          // this.$emit("sendMessage", this.errors.phone);
          // return false;
          this.isValid = false;
        }
      }
      // Email không hợp lệ
      if (this.employee.Email) {
        var emailRegex =
          /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
        if (!this.employee.Email.match(emailRegex)) {
          this.errors.email = MISAResource.vi.error.email;
          // this.$emit("sendMessage", this.errors.email);
          // return false;
          this.isValid = false;
        }
      }
      if (
        !this.errors.code &&
        !this.errors.name &&
        !this.errors.dept &&
        !this.errors.phone &&
        !this.errors.dob &&
        !this.errors.email
      ) {
        this.isValid = true;
      } else {
        this.sendErrorMessage();
      }
    },
    /**
     * Popup hiện lỗi đầu tiên
     * Author: NHNam (7/1/2023)
     */
    sendErrorMessage() {
      if (this.errors.code != "") {
        this.$emit("sendMessage", this.errors.code);
      } else if (this.errors.name != "") {
        this.$emit("sendMessage", this.errors.name);
      } else if (this.errors.dept != "") {
        this.$emit("sendMessage", this.errors.dept);
      } else if (this.errors.dob != "") {
        this.$emit("sendMessage", this.errors.dob);
      } else if (this.errors.phone != "") {
        this.$emit("sendMessage", this.errors.phone);
      } else if (this.errors.email != "") {
        this.$emit("sendMessage", this.errors.email);
      }
    },
    /**
     * Lưu data
     * Author: NHNam (7/1/2023)
     */
    async saveData() {
      var me = this;
      // if (this.isDuplicate === MISAEnum.FormMode.Duplicate) {
      //   await this.getNewEmployeeCode();
      // }
      var newData = this.employee;
      var res;
      if (
        !this.employee.EmployeeId ||
        this.isDuplicate === MISAEnum.FormMode.Duplicate
      ) {
        res = await axios
          .post(MISAapi.employee.employeeApi, newData)
          .catch((res) => {
            this.errors.code = res.response.data.MoreInfo.ListError[0];
            this.$emit("sendMessage", this.errors.code);
            console.log(res.response.data.MoreInfo.ListError);
            // this.handleErrorCode(res.response.status);
          });
        me.$emit(
          "showToast",
          MISAResource.vi.add,
          newData,
          MISAEnum.FormMode.Add
        );
      } else {
        res = await axios
          .put(MISAapi.employee.employeeApi + this.employee.EmployeeId, newData)
          .catch((res) => {
            this.errors.code = res.response.data.MoreInfo.ListError[0];
            this.$emit("sendMessage", this.errors.code);
            // this.handleErrorCode(res.statuscode);
            console.log(res.response.data.MoreInfo.ListError);
          });
        console.log(res.data);
        me.$emit(
          "showToast",
          MISAResource.vi.update,
          newData,
          MISAEnum.FormMode.Edit
        );
      }
    },

    /**
     * Cất và thêm (lưu data và reset form)
     */
    async saveAndAdd() {
      try {
        this.validate();
        if (this.isValid) {
          await this.saveData();
          this.employee = {};
          // Cất và thêm đổi tiêu đề form
          this.titleForm = "Thêm nhân viên";
          await this.getNewEmployeeCode();
        }
      } catch (error) {
        console.log(error);
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
      // console.log(JSON.stringify(this.employee));
      // console.log(JSON.stringify(this.employeeTemp));
      if (JSON.stringify(this.employee) == JSON.stringify(this.employeeTemp)) {
        this.hideDialog();
      } else {
        this.$emit("confirmClose");
      }
    },
    /**
     * Lấy mã nhân viên mới
     * Author: NHNam (6/1/2023)
     */
    async getNewEmployeeCode() {
      try {
        var me = this;
        await axios
          .get(MISAapi.employee.newEmployeeCode)
          .then(function (res) {
            me.employee.EmployeeCode = res.data;
            // me.$refs.txtCode.focus();
          })
          .catch(function (res) {
            this.handleErrorCode(res.statuscode);
            console.log(res.response.data.MoreInfo.ListError);
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Đổi Tiêu đề form theo form mode
     * Author: NHNam (7/1/2023)
     */
    handleTitleForm() {
      if (this.employeeIdSelected == null) {
        this.titleForm = "Thêm nhân viên";
      } else {
        this.titleForm = "Thông tin nhân viên";
      }
    },
  },
  async created() {
    // Có Id thì call api lấy thông tin nhân viên, nếu không lấy mã nhân viên mới
    this.handleTitleForm();
    if (this.employeeIdSelected != null) {
      var me = this;
      await axios
        .get(MISAapi.employee.employeeApi + this.employeeIdSelected)
        .then(function (res) {
          me.employee = res.data;
          me.employee.DateOfBirth = me.fomartDate(res.data.DateOfBirth);
          me.employee.IdentityDate = me.fomartDate(res.data.IdentityDate);
          // console.log(me.employee.DepartmentId)
          // me.$refs.txtCode.focus();
        })
        .then(async function () {
          if (me.isDuplicate === MISAEnum.FormMode.Duplicate) {
            await me.getNewEmployeeCode();
          }
        })
        .then(() => {
          this.employeeTemp = Object.assign({}, this.employee);
        });
    } else {
      await this.getNewEmployeeCode();
      this.employee.Gender = 0;
      this.employeeTemp = Object.assign({}, this.employee);
    }
  },
};
</script>
<style>
@import url(../../css/layout/dialog.css);
</style>
