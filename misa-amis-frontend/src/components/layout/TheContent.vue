<template>
  <div class="content-area">
    <the-navbar />
    <div class="content-area__body">
        <employee-content />
    </div>
  </div>
</template>

<script>
import TheNavbar from '../layout/TheNavbar.vue';
import EmployeeContent from '../views/employees/EmployeeContent.vue';
export default {
    name: "TheContent",
    components:{
        TheNavbar,
        EmployeeContent
    }
}
</script>

<style scoped>
    @import url("../../css/layout/content.css");
</style>