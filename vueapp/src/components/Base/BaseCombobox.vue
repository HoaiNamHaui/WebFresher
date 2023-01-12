<template>
  <div class="combobox-container">
    <input id="id" type="text" 
    class="combobox-input" 
    :value="textSelected"
    />
    <div class="combobox-button" 
    @click="onShowHideData"
    >
        <div class="combobox-button-icon"></div>
    </div>
    <div class="combobox-data" v-show="isShowData">
      <a class="combobox-item" 
      v-for="(item,index) in entities" 
      :key="index"
      :value="item[propValue]"
      @click="itemOnSelect(item)"
      >{{ item[propName] }}</a>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: "ComboxboxInput",
  data(){
    return{
        isShowData: false,
        entities: [],
        textSelected: null,
    }
  },
  created(){
     if(this.api){
        axios.get(this.api).then((data) => {
            this.entities = data.data;
            this.setItemSelected();
        }).
        catch(res => {console.log(res);})
     }
  },
  props: ['id','api','propName','propValue','modelValue'],
  components: {},
  methods:{ 
    onShowHideData(){
        this.isShowData = !this.isShowData;
    },
    itemOnSelect(item){
        this.textSelected = item[this.propName];
        this.isShowData = false;
    },
    setItemSelected(){
        var me = this;
        // Tìm item tương ứng với cái modelValue:
        let entitySelected = this.entities.find(item => item[me.propValue] == me.modelValue);
        if(entitySelected){
            me.textSelected = entitySelected[me.propName];
        }
    }
  }
};
</script>

<style>
.combobox-container {
  width: 300px;
  position: relative;
}
.combobox-input {
  width: 100%;
  height: 36px;
  outline: none;
  border: 1px solid #c2c2c2;
  border-radius: 2px;
  box-sizing: border-box;
}
.combobox-button{
    width: 36px;
    height: 36px;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: antiquewhite;
    position: absolute;
    right: 0;
    top: 0;
    border-radius:0 2px 2px 0;
    cursor: pointer;
}
.combobox-button-icon{
    background: url('../../../assets/img/Sprites.64af8f61.svg') no-repeat -1756px -317px;
	width: 8px;
	height: 5px;

}
.combobox-data{
    width: 100%;
    position: absolute;
    top: calc(100% + 2px);
    display: flex;
    flex-direction: column;
    z-index: 999;
    background-color: #fff;
    box-shadow: 0 0 6px #ccc;
    
}
.combobox-item{
    width: 100%;
    height: 36px;
    line-height: 36px;  
    padding: 0 16px;
    box-sizing: border-box;
    border-top: 1px solid #c2c2c2;
}
.combobox-item:hover{
    background-color: #2ecc71; 
    color: #fff;
}


</style>
