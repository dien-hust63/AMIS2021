<template>
  <div class="content-area">
    <the-navbar />
    <base-tooltip />
    <div class="content-area__body">
        <router-view></router-view>
    </div>
    <customer-detail />
  </div>
</template>

<script>
import TheNavbar from '../layout/TheNavbar.vue';
import BaseTooltip from "../base/BaseTooltip.vue";
import CustomerDetail from "../../components/views/customer/CustomerDetail.vue";
export default {
    name: "TheContent",
    components:{
        TheNavbar,
        BaseTooltip,
        CustomerDetail
    }
}
</script>

<style scoped>
    @import url("../../css/layout/content.css");
</style>