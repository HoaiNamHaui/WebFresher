<template>
    <div id="overlay" class="overlay"> 
        <div class="dialog">
            <div class="dialog-button">
                <div class="icon-hint"></div>
                <div @click="hideDialog" class="icon-close" id="btnClose"></div>
            </div>
            <div class="dialog-header">
                <div class="dialog-title">Thông tin nhân viên</div>
                <div class="dialog-option">
                    <input type="checkbox" name="" id=""> <label for="">Là khách hàng</label>
                    <input type="checkbox" name="" id=""> <label for="">Là nhà cung cấp</label>
                </div>
            </div>
            <div class="data-field">
                <div class="field-above flexbox">
                    <div class="field-above-left"  style="margin-right: 26px;">
                        <div class="flexbox">
                            <div class="m-b-12" style="margin-right: 8px;">
                                <label for="">Mã <span class="red">*</span></label> <br>
                                <input v-model="newEmployeeCode" type="text" class="input input150">
                            </div>
                            <div class="m-b-12">
                                <label for="">Tên <span class="red">*</span></label> <br>
                                <input type="text" class="input input230">
                            </div>
                        </div>
                        <div class="m-b-12">
                            <label for="">Đơn vị <span class="red">*</span></label> <br>
                            <input type="text" class="input input385">
                        </div>
                        <div>
                            <label for="">Chức danh</label> <br>
                            <input type="text" class="input input385">
                        </div>
                    </div>
                    <div class="field-above-right">
                        <div class="flexbox">
                            <div class="m-b-12" style="margin-right: 10px;">
                                <label for="">Ngày sinh</label> <br>
                                <input type="date" class="input input150">
                            </div>
                            <div class="m-b-12">
                                <label for="">Giới tính</label> <br>
                                <div style="line-height: 32px; margin-top: 6px;" class="flexbox">
                                    <input checked class="m-l-10 m-r-5" type="radio" name="gender"> Nam
                                    <input class="m-l-10 m-r-5" type="radio" name="gender"> Nữ
                                    <input class="m-l-10 m-r-5" type="radio" name="gender"> Khác
                                </div>
                            </div>
                        </div>
                        <div class="flexbox">
                            <div class="m-b-12 label" style="margin-right: 8px;">
                                <div class="error-info">
                                    <div class="error-text">Số chứng minh nhân dân</div>
                                    <div class="error-arrow"></div>
                                </div>
                                <label for="" class="">Số CMND</label> <br>
                                <input type="datetime" class="input input240">
                            </div>
                            <div class="m-b-12" style="margin-right: 8px;">
                                <label for="">Ngày cấp</label> <br>
                                <input type="date" class="input input150">
                            </div>
                        </div>
                        <div>
                            <label for="">Nơi cấp</label> <br>
                            <input type="datetime" class="input input395">
                        </div>
                    </div>
                </div>
                <div class="field-below">
                    <div class="m-b-12">
                        <label for="">Địa chỉ</label> <br>
                        <input class="input input800">
                    </div>
                    <div class="flexbox">
                        <div class="m-b-12" style="margin-right: 8px;">
                            <label for="">ĐT di động</label> <br>
                            <input class="input input200">
                        </div>
                        <div class="m-b-12" style="margin-right: 8px;">
                            <label for="">ĐT cố định</label> <br>
                            <input class="input input200">
                        </div>
                        <div class="m-b-12">
                            <label for="">Email</label> <br>
                            <input class="input input200">
                        </div>
                    </div>
                    <div class="flexbox">
                        <div style="margin-right: 8px;">
                            <label for="">Tài khoản ngân hàng</label> <br>
                            <input class="input input200">
                        </div>
                        <div style="margin-right: 8px;">
                            <label for="">Tên ngân hàng</label> <br>
                            <input class="input input200">
                        </div>
                        <div>
                            <label for="">Chi nhánh</label> <br>
                            <input class="input input200">
                        </div>
                    </div>
                    
                </div>
            </div>
            <div class="dialog-footer">
                <button @click="hideDialog" class="button-white" id="btnCancel">Hủy</button>
                <div>
                    <button class="button-white">Cất</button>
                    <button class="button">Cất và Thêm</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from "axios";
    export default{
        name: "EmployeeDialog",
        data(){
            return{
                newEmployeeCode: null
            }
        },
        methods:{
            hideDialog(){
                this.$emit("onClose")
            }
        },
        created(){
            axios.get('https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode').then(function (res) {
            this.newEmployeeCode = res.data;
            // console.log(res.data);            
        })}
    }
</script>
<style>
    @import url(../../css/layout/dialog.css);
</style>