<template>
    <div class="overlay-message">
        <div class="messagebox">
        <div class="messagebox-header">
            <div class="messagebox-header-title">{{ messageTitle }}</div>
            <div class="messagebox-header-close" @click="closeMessage"></div>
        </div>
        <div class="messagebox-content">
            <div class="messagebox-delete-icon"></div>
            <div class="messagebox-content-describe">Bạn có muốn thiết lập "Sử dụng" cho các Tài khoản con không?</div>
        </div>
        <div class="messagebox-footer">
            <button class="button-white" @click="closeMessage">Không</button>
            <button class="button" @click="confirm">Đồng ý</button>
        </div>
    </div>
    </div>
</template>
<script>
//   import axios from "axios";
//   import MISAapi from "@/js/api";
export default{
    name: "MessageConfirm",
    data(){
        return{
        }
    },
    props: ["objectCode", "messageTitle"],
    methods:{
        /**
         * Đóng form xác nhận xóa
         * Author: NHNam (7/1/2023)
         */
        closeMessage(){
            this.$emit('closeMessage',false);
        },
        /**
         * Xác nhận xóa
         */
        confirm(){
            this.$emit('confirmMessage');
        }
    }
}
</script>
<style>
@import url(../../css/base/messagebox/message-delete.css);
@import url(../../css/layout/overlay.css);

</style>