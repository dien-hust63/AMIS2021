<template>
  <div class="content-area">
    <the-navbar />
    <base-tooltip />
    <div class="content-area__body">
        <router-view></router-view>
    </div>
  </div>
</template>

<script>
import TheNavbar from '../layout/TheNavbar.vue';
import BaseTooltip from "../base/BaseTooltip.vue";
export default {
    name: "TheContent",
    components:{
        TheNavbar,
        BaseTooltip,
    }
}
</script>

<style scoped>
    @import url("../../css/layout/content.css");
</style>