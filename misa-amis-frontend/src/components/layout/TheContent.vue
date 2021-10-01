<template>
  <div class="content-area">
    <the-navbar />
    <base-tooltip />
    <div class="content-area__body">
        <router-view></router-view>
    </div>
    <customer-detail />
    <employee-detail />
    <commodity-detail />
    <commodity-group />
  </div>
</template>

<script>
import TheNavbar from '../layout/TheNavbar.vue';
import BaseTooltip from "../base/BaseTooltip.vue";
import CustomerDetail from "../../components/views/customer/CustomerDetail.vue";
import EmployeeDetail from "../../components/views/employees/EmployeeDetail.vue";
import CommodityDetail from "../../components/views/commodity/CommodityDetail.vue";
import CommodityGroup from "../../components/views/commodity/CommodityGroup.vue";
export default {
    name: "TheContent",
    components:{
        TheNavbar,
        BaseTooltip,
        CustomerDetail,
        EmployeeDetail,
        CommodityGroup,
        CommodityDetail
    }
}
</script>

<style scoped>
    @import url("../../css/layout/content.css");
</style>