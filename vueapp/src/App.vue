<template>
  <div class="container"> 
    <the-menu
    :toggle="collapse"
    @expandMenu="expandMenu"
    />
    <div class="main" :class="{'responsive-main': collapse}">
      <the-header
        @collapse="collapseMenu"
        :toggle="!collapse"
      />
      <the-content>
        <router-view></router-view>
      </the-content>
    </div>
  </div>
</template>

<script>
import TheMenu from './components/layout/TheMenu.vue';
import TheHeader from './components/layout/TheHeader.vue';
import TheContent from './components/layout/TheContent.vue';

export default {
  name: 'App',
  data(){
    return{
      collapse: false
    }
  },
  components: {
    TheMenu, TheHeader, TheContent
  },
  methods:{
    /**
     * Thu gọn sibar
     * Author: NHNam (7/1/2023)
     */
    collapseMenu(){
      this.collapse = true;
    },
    /**
     * mở rộng sibar
     * Author: NHNam (7/1/2023)
     */
    expandMenu(){
      this.collapse = false;
    }
  }
}
</script>

<style>
  @import url(./css/main.css);
</style>
