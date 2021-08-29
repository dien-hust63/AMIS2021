<template>
  <div class="employee-detail">
      <div class="row-input">
          <div class="row-input--left w-1/2">
              <div class="w-2/5">
            <base-input label="Mã" required="true"/>
              </div>
              <div class="w-3/5">
            <base-input label="Tên" required="true"/></div>
          </div>
          <div class="row-input--right w-1/2">
              <div class="w-2/5">
               <base-input label="Ngày sinh" type="date"/></div>
              <div class="w-3/5"></div>
          </div>
      </div>
      <div class="row-input"></div>
      <div class="row-input"></div>
      <div class="row-input"></div>
      <div class="row-input"></div>
      <div class="row-input"></div>
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";
export default {
    name:"EmployeeDetail",
    components:{
        BaseInput,
    }
}
</script>

<style scoped>
@import url("../../../css/views/employees/employeedetail.css");
</style>