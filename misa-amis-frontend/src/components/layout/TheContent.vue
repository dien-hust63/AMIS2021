<template>
  <div class="content-area">
    <!-- <inward-detail /> -->
    <the-navbar />
    <div class="content-area__body">
        <router-view></router-view>
    </div>
  </div>
</template>

<script>
import TheNavbar from '../layout/TheNavbar.vue';
// import InwardDetail from "../../components/views/warehouse/InwardDetail.vue";
export default {
    name: "TheContent",
    components:{
        TheNavbar,
        // InwardDetail,
    }
}
</script>

<style scoped>
    @import url("../../css/layout/content.css");
</style>