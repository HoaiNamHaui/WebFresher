<template>
    <div class="overlay-message">
        <div class="msgbox">
            <div class="msg-content">
                <div class="messagebox-validate-icon"></div>
                <div class="messagebox-content-describe">{{ error }}</div>
            </div>
            <div class="msg-footer">
                <button class="button" @click="close">Đóng</button>
            </div>
        </div>
    </div>
</template>
<script>
    export default{
        name: "MessageError",
        data(){
            return{
            
            }
        },
        props: ['error'],
        methods:{
            /**
             * Đóng message
             * Author: NHNam (5/1/2023)
             */
            close(){
                this.$emit('close');
            }
        }
    }

</script>
<style>
@import url(../../../css/base/messagebox/messagesuccess.css);
.messagebox-validate-icon {
	background: url('../../../assets/img/Sprites.64af8f61.svg') no-repeat -32px -959px;
	width: 36px;
	height: 36px;
    margin-right: 24px;
}
</style>