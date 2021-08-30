<template>
  <div class="employee-content">
    <div class="header-list">
      <div class="header-list__title">Nhân viên</div>
      <div
        class="ms-button ms-button-primary add-button"
        @click="addNewEmployee"
      >
        Thêm mới nhân viên
      </div>
    </div>
    <div class="employee-content-body">
      <div class="toolbar">
        <div class="toolbar__search">
          <base-input
            placeholder="Tìm theo mã, tên nhân viên"
            class="input-search"
          />
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
      <div class="employee-content-pagination">
        <base-pagination />
      </div>
    </div>
    <base-popup
      :popupData="employeeData"
      :isShowPopup="isShowPopup"
      @closePopup ="closePopup"
      :mode ="mode"
    />
  </div>
</template>

<script>
import {
  apiList,
  tableEmployeeHeaders,
} from "../../../js/resources/resourcevn.js";
import BaseTable from "../../base/BaseTable.vue";
import BasePopup from "../../base/BasePopup.vue";
import BaseInput from "../../base/BaseInput.vue";
import BasePagination from "../../base/BasePagination.vue";
import EmployeeApi from "../../../js/component/employeeapi.js";
export default {
  name: "EmployeeContent",
  components: {
    BaseTable,
    BasePopup,
    BaseInput,
    BasePagination,
  },
  data() {
    return {
      tableEmployeeHeaders: tableEmployeeHeaders,
      isShowPopup: false,
      employeeData: {},
      mode: -1,
    };
  },
  methods: {
    /**
     * Thêm mới nhân viên
     * CreatedBy: nvdien(30/8/2021)
     */
    addNewEmployee() {
      //lấy mã nhân viên mới
      EmployeeApi.getNewEmployeeCode()
        .then((response) => {
          //set mode = 0: thêm mới
          this.mode = 0;
          //hiển thị form thêm mới nhân viên
          this.isShowPopup = true;
          //truyền mã nhân viên mới lên popup
          this.employeeData = {
            EmployeeCode: response.data
          };
        })
        .catch((response) => {
          console.log(response);
          this.isShowPopup = true;
        });
    },

    /**
     * Đóng popup
     */
    closePopup(){
      this.isShowPopup  = false;
    }
  },
  computed: {
    employeeApiFilter: function () {
      let api = apiList.employeeApi;
      api += `Filter?employeeFilter=`;
      api += `&pageIndex=1&pageSize=100`;
      return api;
    },
  },
};
</script>

<style scoped>
@import url("../../../css/views/employees/employeecontent.css");
</style>