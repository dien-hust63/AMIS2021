<template>
  <div id="app">
    <div class="layout--main">
      <the-menu />
      <the-content />
    </div>
    <base-context-menu />
  </div>
</template>

<script>
import TheMenu from "./components/layout/TheMenu.vue";
import TheContent from "./components/layout/TheContent.vue";
import BaseContextMenu from "./components/base/BaseContextMenu.vue";
export default {
  name: "App",
  components: {
    TheMenu,
    TheContent,
    BaseContextMenu
  },
};
</script>
  
<style>
@import url("./css/common/main.css");
</style>
