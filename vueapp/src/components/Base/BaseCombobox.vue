<template>
  <div class="combobox-container" :class="{error: isError}">
    <input
      :tabindex="tabindex"
      id="id"
      type="text"
      class="combobox-input"
      v-model="textSelected"
      @input="onSearchItem"
      @keydown="inputOnKeyDown"
      :class="{'none-border': isError}"
    />
    <div class="combobox-button" @click="onShowHideData" :class="{'none-border': isError}">
      <div class="combobox-button-icon"></div>
    </div>
    <div class="combobox-data" v-show="isShowData">
      <a
        :ref="`item_${index}`"
        :class="{ 'item-selected': index == indexItemSelect }"
        class="combobox-item"
        v-for="(item, index) in entitySearch"
        :key="index"
        :value="item[propValue]"
        @click="itemOnSelect(item)"
        >{{ item[propName] }}</a
      >
    </div>
  </div>
</template>

<script>
import axios from "axios";
import MISAEnum from '../../js/base/enum'
export default {
  name: "ComboxboxInput",
  data() {
    return {
      isShowData: false,
      entities: [],
      textSelected: null,
      entitySearch: [],
      indexItemSelect: 0,
      itemSelected: null,
      isError: false
    };
  },
  watch: {
    modelValue: function () {
      if (this.modelValue) {
        this.setItemSelected();
      }
    },
    //Lỗi hiển thị border lỗi
    error: function(){
      if(this.error != ""){
        this.isError = true;
      }
      else{
        this.isError = false;
      }
    }
  },
  created() {
    /**
     * Gọi API lấy danh sách phòng ban
     * Author: NHNam (11/1/2023)
     */
    if (this.api) {
      axios
        .get(this.api)
        .then((data) => {
          this.entities = data.data;
          this.entitySearch = data.data;
          // this.setItemSelected();
        })
        .catch((res) => {
          console.log(res);
        });
    }
  },
  updated() {
    /**
     * ModelValue cập nhật bind dữ liệu input, set item
     * Author: NHNam (12/1/2023)
     */
    // if(this.modelValue){
    //   this.setItemSelected();
    // }
  },
  props: ["id", "api", "propName", "propValue", "modelValue", "tabindex", "error"],
  emits: ["update:modelValue"],
  components: {},
  methods: {
    /**
     * Ẩn hiện combobox data
     * Author: NHNam (11/1/2023)
     */
    onShowHideData() {
      this.isShowData = !this.isShowData;
    },
    /**
     * Chọn item và highlight item đc chọn, bind 2 chiều
     * Author: NHNam (11/1/2023)
     */
    itemOnSelect(entity) {
      var me = this;
      //reset lại danh sách
      this.entitySearch = this.entities;
      this.itemSelected = entity;
      // Set index của item được chọn
      this.indexItemSelect = me.findIndexSelected; // ----> Lấy tại computed  
      this.textSelected = entity[this.propName];
      this.isShowData = false;
      this.$emit("update:modelValue", entity[this.propValue]);
    },
    /**
     * Bind giá trị được select, hightlight item đc chọn
     * Author: NHNam (1/1/2023)
     */
    setItemSelected() {
      var me = this;
      // Tìm item tương ứng với cái modelValue:
      let entitySelected = this.entities.find(
        (item) => item[me.propValue] == me["modelValue"]
      );
      if (entitySelected) {
        me.textSelected = entitySelected[me.propName];
        // Tính toán lại Index của item đã được chọn
        let findIndex = this.entities.findIndex(
          (item) => item[me.propValue] == entitySelected[me.propValue]
        );
        // Set index của item được chọn
        this.indexItemSelect = findIndex;
      }
    },
    /**
     * Search phòng ban
     * Author: NHNam (11/1/2023)
     */
    onSearchItem() {
      var me = this;
      // Tìm item tương ứng với cái modelValue:
      this.entitySearch = this.entities.filter((item) =>
        item[me.propName].toLowerCase().includes(me.textSelected.toLowerCase())
      );
      this.isShowData = true;
    },
    /**
     * Bắt sự kiện keydown dùng phím để chọn value
     * Author: NHNam (14/1/2023)
     */
    inputOnKeyDown(event){
      console.log(event.keyCode);
      const keyCode = event.keyCode;
      switch (keyCode) {
        case MISAEnum.KEY_CODE.ENTER:{
          // Xác định Item đang chọn dựa vào index
          const item = this.entitySearch[this.indexItemSelect];
          this.itemOnSelect(item); 
        }
          
          break;
        case MISAEnum.KEY_CODE.ROW_UP:
          if(this.indexItemSelect > 0){
            this.indexItemSelect--;
          }
          break;
        case MISAEnum.KEY_CODE.ROW_DOWN:{
          this.isShowData = true;
          let maxLength = this.entitySearch.length;
          if(this.indexItemSelect < maxLength -1){
            this.indexItemSelect++;
          }
        }  
          break;
        default:
          break;
      }
    },
  },
  computed:{
    // tìm index value được chọn
    findIndexSelected: function(){
      var me = this;
      let findIndex = this.entitySearch.findIndex(
        (item) => item[me.propValue] == me.itemSelected[me.propValue]
      );
      return findIndex;
    }
  }
};
</script>

<style>
.combobox-container {
  margin-top: 6px;
  width: 100%;
  position: relative;
}
.combobox-input {
  width: 100%;
  height: 36px;
  outline: none;
  border: 1px solid #c2c2c2;
  border-radius: 2px;
  box-sizing: border-box;
  font-family: Notosans-Regular;
}
.error{
  border: 1px solid red;
  border-radius: 2px;
}
.none-border{
  border: none !important;
}
.combobox-button {
  width: 36px;
  height: 36px;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #fff;
  position: absolute;
  right: 0;
  top: 0;
  border-radius: 0 2px 2px 0;
  cursor: pointer;
  box-sizing: border-box;
  border-top: 1px solid #c2c2c2;
  border-right: 1px solid #c2c2c2;
  border-bottom: 1px solid #c2c2c2;
}
.combobox-button:hover {
  background-color: #e0e0e0;
}
.combobox-button-icon {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -1756px -317px;
  width: 8px;
  height: 5px;
}
.combobox-data {
  width: 100%;
  position: absolute;
  top: calc(100% + 2px);
  display: flex;
  flex-direction: column;
  z-index: 999;
  background-color: #fff;
  box-shadow: 0 0 6px #ccc;
  font-family: Notosans-Regular;
  font-size: 14px;
}
.combobox-item {
  width: 100%;
  height: 36px;
  line-height: 36px;
  padding: 0 8px;
  box-sizing: border-box;
  border-top: 1px solid #c2c2c2;
}
.combobox-item:hover {
  background-color: #ebedf0;
  color: #2ca01c;
}
.item-selected {
  background-color: #2ca01c;
  color: #fff;
}
@font-face {
  font-family: Notosans-Regular;
  src: url(../../assets/fonts/notosans-regular.2cb88a13.woff2);
}
</style>
