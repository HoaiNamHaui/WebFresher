<template>
  <div class="wrraper-combobox">
    <input
      type="text"
      readonly
      class="display-value-area"
      v-model="itemSelected"
    />
    <div class="button-show" @click="showOption">
      <div class="combobox-button-icon"></div>
    </div>
    <div class="combobox-option" v-show="isShowOption">
      <div
        v-for="(item, index) in data"
        :ref="`item_${index}`"
        :key="index"
        class="combobox-option-item"
        @click="setItemSelected(item, index)"
      >
        {{ item }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseComboboxReadOnly",
  props: ["data", "propValue"],
  watch: {},
  data() {
    return {
      isShowOption: false,
      itemSelected: null,
      indexSelected: 1,
    };
  },
  methods: {
    /**
     * Ẩn hiển option
     */
    showOption() {
      this.isShowOption = !this.isShowOption;
    },
    setItemSelected(item, index) {
      this.itemSelected = item;
      this.indexSelected = index;
      if (index) {
        this.$refs["item_" + index][0].classList.add("item-selected");
        var me = this;
        me.data.forEach((element, index) => {
          if (index != me.indexSelected) {
            me.$refs["item_" + index][0].classList.remove("item-selected");
          }
        });
      }

      this.$emit("update:modelValue", item);
      this.isShowOption = false;
    },

    // findIndex() {
    //   var me = this;
    //   me.indexSelected = me.data.findIndex((item) => item === me.propValue) + 1;
    // },
  },
  created() {
    this.itemSelected = this.propValue;
    // this.findIndex();
  },
};
</script>

<style scoped>
.wrraper-combobox {
  position: relative;
  height: 26px;
}
.display-value-area {
  height: 26px;
  width: 100%;
  border: 1px solid #babec5;
  outline: none;
  border-radius: 2px;
}
.button-show {
  position: absolute;
  height: 26px;
  width: 26px;
  right: 0;
  top: 0;
  display: flex;
  align-items: center;
  justify-content: center;
}
.button-show:hover {
  background-color: #e0e0e0;
  cursor: pointer;
}
.combobox-option-item {
  height: 26px;
  width: 100%;
  background-color: #ffff;
  line-height: 26px;
  text-indent: 8px;
  transition: 0.1s;
}
.combobox-option-item:hover {
  background-color: #2ca01c;
  color: #ffff;
}
.item-selected {
  background-color: #2ca01c;
}
.combobox-option {
  position: absolute;
  top: calc(100% + 4px);
  width: 100%;
  max-height: 130px;
  border-radius: 2px;
  overflow: scroll;
}
</style>
