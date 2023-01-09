<template>
    <div class="overlay-message">
        <div class="msgbox">
        <div class="msg-content">
            <div class="messagebox-success-icon"></div>
            <div class="messagebox-content-describe">{{ message }}</div>
        </div>
        <div class="msg-footer">
            <button class="button" @click="closeMessage">Đóng</button>
        </div>
    </div>
    </div>
</template>

<script>
export default{
    name: "MessageSuccess",
    props:["message"],
    methods:{
        closeMessage(){
            this.$emit('hideMessage', false)
        }
    }
}
</script>

<style>
@import url(../../../css/base/messagebox/messagesuccess.css);
@import url(../../../css/layout/overlay.css);
</style>