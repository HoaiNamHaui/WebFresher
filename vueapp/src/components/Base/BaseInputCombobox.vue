<template>
  <div class="cbx-container">
    <input
      v-model="department.DepartmentName"
      v-bind:key="department.DepartmentId"
      @focus="focusInput"
      @blur="blurInput"
      class="cbx-input"
      type="text"
    />
    <div class="input-cbx-icon" @click="showOption = !showOption"></div>
    <div v-show="showOption" class="option-container">
      <div
        v-for="department in departments"
        v-bind:key="department.DepartmentId"
        class="option"
        @click="selectDepartment(department)"
      >
        {{ department.DepartmentName }}
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
export default {
  name: "InputCombobox",
  data() {
    return {
      department: {},
      showOption: false,
      departments: [],
      valid: false,
    };
  },
  props: ["id", "name"],
  watch: {
    id: function () {
      var me = this;
      if (!me.id) {
        me.selectFirst();
      } else {
        var url = `https://cukcuk.manhnv.net/api/v1/Departments/${this.id}`;
        axios.get(url).then(function (res) {
          me.department = res.data;
        });
      }
    },
  },
  methods: {
    /**
     * Chọn phòng ban hiện lên input, ẩn option khi chọn phòng ban
     * Author: NHNam (5/1/2023)
     */
    selectDepartment(department) {
      this.department = department;
      this.showOption = false;
      this.$emit("deparment", department);
    },

    /**
     * Đổi màu border khi focus vào phòng ban
     * Author: NHNam (5/1/2023)
     */
    focusInput(e) {
      e.target.offsetParent.classList.add("input-focus");
    },
    /**
     * Đổi màu border khi focus vào phòng ban
     * Author: NHNam (5/1/2023)
     */
    blurInput(e) {
      e.target.offsetParent.classList.remove("input-focus");
    },
    /**
     * combobox mặc định chọn cái đầu tiên
     * Author: NHNam (5/1/2023)
     */
    selectFirst() {
      var me = this;
      var url = "https://cukcuk.manhnv.net/api/v1/Departments";
      axios.get(url).then(function (res) {
        me.departments = res.data;
        me.department = me.departments[0];
        me.$emit("deparment", me.department);
      });
    },
  },
  created() {
    this.selectFirst();
  },
};
</script>
<style scoped>
@import url(../../css/base/input/inputcombobox.css);
</style>
