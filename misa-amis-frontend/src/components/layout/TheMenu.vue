<template>
  <div class="menu-container">
    <div class="logo-container">
      <a href="" class="mi mi-24 menu-container-option"></a>
      <a href="" class="logo">
        <img
          src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg"
          alt="Logo AMIS"
          class="img-icon"
        />
      </a>
    </div>
    <div class="menu-item-list">
      <router-link
        :to="item['href']"
        class="menu-item"
        v-for="(item, index) in menuItemList"
        :key="index"
        @click.native ="chooseItem(index)"
        :class="{'menu-item-active': currentTab == index}"
      >
        <div class="menu-item__icon mi mi-24" :class="[item['menuIcon']]"></div>
        <div class="menu-item__tittle">{{ item["menuItemTitle"] }}</div>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheMenu",
  data() {
    return {
      menuItemList: this.$resourcesVN.listMenu,
      currentTab: 6,
    }
  },
  methods: {
    chooseItem(index){
      this.currentTab = index;
    }
  }
  
};
</script>

<style scoped>
@import url("../../css/layout/menu.css");
</style>