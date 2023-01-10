<template>
    <div class="overlay-message">
        <div class="messagebox">
        <div class="messagebox-header">
            <div class="messagebox-header-title">Xóa nhân viên</div>
            <div class="messagebox-header-close" @click="closeMessage"></div>
        </div>
        <div class="messagebox-content">
            <div class="messagebox-content-describe">Bạn có chắc chắn muốn xóa nhân viên <b>{{ employeeSelected.EmployeeCode }}</b></div>
        </div>
        <div class="messagebox-footer">
            <button class="button-white" @click="closeMessage">Hủy</button>
            <button class="button" @click="deleteEmployee">Đồng ý</button>
        </div>
    </div>
    </div>
</template>
<script>
  import axios from "axios";
export default{
    name: "MessageDelete",
    data(){
        return{
        }
    },
    props: ["employeeSelected"],
    methods:{
        /**
         * Đóng form xác nhận xóa
         * Author: NHNam (7/1/2023)
         */
        closeMessage(){
            this.$emit('cancelDelete',false)
        },
        deleteEmployee(){
            var me = this;
            axios
                .delete(`https://cukcuk.manhnv.net/api/v1/Employees/${me.employeeSelected.EmployeeId}`)
                .then((data) => console.log(data))
                .catch((error) => console.log(error));
            this.$emit('cancelDelete',false)   
            this.$emit('DeleteSuccess') 
        },
    }
}
</script>
<style>
@import url(../../../css/base/messagebox/message-delete.css);
@import url(../../../css/layout/overlay.css);

</style>