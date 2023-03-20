<template>
    <div class="overlay-message">
        <div class="messagebox">
        <div class="messagebox-header">
            <div class="messagebox-header-title">{{ messageTitle }}</div>
            <div class="messagebox-header-close" @click="closeMessage"></div>
        </div>
        <div class="messagebox-content">
            <div class="messagebox-delete-icon"></div>
            <div class="messagebox-content-describe">Bạn có chắc chắn muốn xóa <b>{{ objectCode }}</b> ?</div>
        </div>
        <div class="messagebox-footer">
            <button class="button-white" @click="closeMessage">Hủy</button>
            <button class="button button-red" @click="confirm">Đồng ý</button>
        </div>
    </div>
    </div>
</template>
<script>
//   import axios from "axios";
//   import MISAapi from "@/js/api";
export default{
    name: "MessageDelete",
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
            this.$emit('cancelDelete',false)
        },
        /**
         * Xác nhận xóa
         */
        confirm(){
            this.$emit('confirmDelete')
        }
    }
}
</script>
<style>
@import url(../../css/base/messagebox/message-delete.css);
@import url(../../css/layout/overlay.css);

</style>