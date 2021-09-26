<template>
  <div id="app">
    <div class="layout--main">
      <the-menu />
      <the-content />
    </div>
    <base-context-menu />
    <base-toast-message />
    <base-message />
    <base-combo-dropdown />
  </div>
</template>

<script>
import TheMenu from "./components/layout/TheMenu.vue";
import TheContent from "./components/layout/TheContent.vue";
import BaseContextMenu from "./components/base/BaseContextMenu.vue";
import BaseToastMessage from "./components/base/BaseToastMessage.vue";
import BaseMessage from './components/base/BaseMessage.vue';
import BaseComboDropdown from './components/base/BaseComboDropdown.vue';
export default {
  name: "App",
  components: {
    TheMenu,
    TheContent,
    BaseContextMenu,
    BaseToastMessage,
    BaseMessage,
    BaseComboDropdown,
  },
};
</script>
  
<style>
@import url("./css/common/main.css");
</style>
