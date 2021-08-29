<template>
  <div class="employee-content">
    <div class="header-list">
      <div class="header-list__title">Nhân viên</div>
      <div class="ms-button ms-button-primary add-button">
        Thêm mới nhân viên
      </div>
    </div>
    <div class="employee-content-body">
      <div class="toolbar">
        <div class="toolbar__search">
          <div class="ms-input">
            <label for="">Test</label>
            <div class="ms-input">
              <input
                type="text"
                class="ms-input__input .ms-input__icon--after"
                placeholder="Tìm theo mã, tên nhân viên"
              />
              <div class="ms-input__icon mi mi-16 mi-search"></div>
            </div>
          </div>
        </div>
        <div class="mi mi-24 mi-refresh toolbar-refresh"></div>
        <div class="mi mi-24 mi-excel toolbar-excel"></div>
      </div>
      <div class="employee-table">
        <base-table
          :tableHeaders="tableEmployeeHeaders"
          :urlAPI="employeeApiFilter"
        />
      </div>
      <div class="pagination"></div>
    </div>
    <base-popup />
  </div>
</template>

<script>
import {
  apiList,
  tableEmployeeHeaders,
} from "../../../js/resources/resourcevn.js";
import BaseTable from "../../base/BaseTable.vue";
import BasePopup from "../../base/BasePopup.vue";
export default {
  name: "EmployeeContent",
  components: {
    BaseTable,
    BasePopup,
  },
  data() {
    return {
      tableEmployeeHeaders: tableEmployeeHeaders,
    };
  },
  computed: {
    employeeApiFilter: function () {
      let api = apiList.employeeApi;
      api += `Filter?employeeFilter=`;
      api += `&pageIndex=1&pageSize=10`;
      return api;
    },
  },
};
</script>

<style scoped>
@import url("../../../css/views/employees/employeecontent.css");
</style>