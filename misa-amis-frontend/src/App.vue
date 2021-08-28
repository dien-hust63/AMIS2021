<template>
  <div id="app">
    <div class="layout--main">
      <the-menu />
      <the-content />
    </div>
  </div>
</template>

<script>
import TheMenu from "./components/layout/TheMenu.vue";
import TheContent from "./components/layout/TheContent.vue";
export default {
  name: "App",
  components: {
    TheMenu,
    TheContent,
  },
};
</script>
  
<style>
@import url("./css/common/main.css");
</style>
