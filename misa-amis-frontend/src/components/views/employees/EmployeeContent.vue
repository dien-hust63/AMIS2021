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
            v-model="inputSearchValue"
            @keyup="search"
          />
        </div>
        <div
          class="mi mi-24 mi-refresh toolbar-refresh"
          @click="refreshData"
        ></div>
        <div class="mi mi-24 mi-excel toolbar-excel" @click="exportData"></div>
      </div>
      <div class="employee-table">
        <base-table
          :tableHeaders="tableEmployeeHeaders"
          :urlAPI="employeeApiFilter"
          @getTableData="getTableData"
          :forceLoadTable="forceLoadTable"
          @editEntity="editEmployee"
          @copyEntity="copyEntity"
        />
      </div>
      <div class="employee-content-pagination">
        <base-pagination
          :totalRecord="totalRecord"
          :totalPage="totalPage"
          @getPagingData="getPagingData"
          :hasSearch="hasSearch"
        />
      </div>
    </div>
    <employee-popup
      :class="{ 'employee-popup--show': isShowPopup }"
      :employeeInfo="employeeData"
      :isFocusCode="isFocusCode"
      @closePopup="closePopup"
      :mode="mode"
      @loadTable="refreshData"
    />
  </div>
</template>

<script>
import {
  apiList,
  tableEmployeeHeaders,
} from "../../../js/resources/resourcevn.js";
import BaseTable from "../../base/BaseTable.vue";
import BaseInput from "../../base/BaseInput.vue";
import BasePagination from "../../base/BasePagination.vue";
import EmployeePopup from "../../views/popup/EmployeePopup.vue";
import { RepositoryFactory } from "../../../js/repository/repository.factory.js";
const EmployeesRepository = RepositoryFactory.get("employees");

export default {
  name: "EmployeeContent",
  components: {
    BaseTable,
    BaseInput,
    BasePagination,
    EmployeePopup,
  },
  data() {
    return {
      tableEmployeeHeaders: tableEmployeeHeaders,
      isShowPopup: false,
      employeeData: {},
      mode: -1,
      totalRecord: 0,
      totalPage: 0,
      pageIndex: 1,
      pageSize: 10,
      searchData: "",
      inputSearchValue: "",
      timeDelaySearch: null,
      hasSearch: false,
      forceLoadTable: false,
      isFocusCode: false,
    };
  },
  methods: {
    /**
     * Thêm mới nhân viên
     * CreatedBy: nvdien(30/8/2021)
     */
    addNewEmployee() {
      //lấy mã nhân viên mới
      EmployeesRepository.getNewEmployeeCode()
        .then((response) => {
          //set mode = 0: thêm mới
          this.mode = 0;
          //hiển thị form thêm mới nhân viên
          this.isShowPopup = true;
          //focus vào ô mã nhân viên
          this.isFocusCode = !this.isFocusCode;
          //truyền mã nhân viên mới lên popup
          this.employeeData = {
            EmployeeCode: response.data,
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
    closePopup() {
      this.isShowPopup = false;
    },
    /**
     * dữ liệu lấy được khi load table
     * CreatedBy: nvdien(31/8/2021)
     */
    getTableData(tableData) {
      this.totalRecord = tableData["TotalRecord"];
      this.totalPage = tableData["TotalPage"];
    },
    /**
     * dữ liệu lấy được từ phân trang
     */
    getPagingData(pageIndex, pageSize) {
      this.pageIndex = pageIndex;
      this.pageSize = pageSize;
    },
    /**
     * Tìm kiếm
     */
    search() {
      if (this.timeDelaySearch) {
        clearTimeout(this.timeDelaySearch);
      }

      this.timeDelaySearch = setTimeout(() => {
        this.pageIndex = 1;
        this.hasSearch = !this.hasSearch;
        this.searchData = this.inputSearchValue;
      }, 500);
    },
    /**
     * load lại toàn bộ dữ liệu
     */
    refreshData() {
      this.forceLoadTable = !this.forceLoadTable;
    },
    /**
     * export dữ liệu nhân viên
     */
    exportData() {
      EmployeesRepository.export()
        .then((response) => {
          const link = document.createElement("a");
          link.href = window.URL.createObjectURL(new Blob([response.data]));

          link.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
          document.body.appendChild(link);
          link.click();
        })
        .catch((response) => console.log(response));
    },
    /**
     * Chỉnh sửa nhân viên
     * @param {string} employeeId: Id của nhân viên
     * CreatedBy: nvdien(2/9/2021)
     */
    editEmployee(employeeId) {
      EmployeesRepository.getById(employeeId)
        .then((response) => {
          //set mode = 1: chỉnh sửa
          this.mode = 1;
          //lấy giá trị
          this.employeeData = response.data;
          //focus vào ô mã nhân viên
          this.isFocusCode = !this.isFocusCode;
          //mở form chi tiết
          this.isShowPopup = true;
        })
        .catch((error) => console.log(error));
    },
    /**
     * Nhân bản đối tượng
     * @param {object} entity: đối tượng nhân bản
     * CreatedByL nvdien(2/9/2021)
     */
    copyEntity(entity) {
      this.employeeData = Object.assign({},entity);
      EmployeesRepository.getNewEmployeeCode().then((response) => {
        //set mode = 0: Thêm mới
        this.mode = 0;
        this.$set(this.employeeData, "EmployeeCode", response.data);
        //focus vào ô mã nhân viên
        this.isFocusCode = !this.isFocusCode;
        //mở form chi tiết
        this.isShowPopup = true;
      });
    },
  },
  computed: {
    employeeApiFilter: function () {
      let api = apiList.employeeApi;
      api += `Filter?employeeFilter=${this.searchData}`;
      api += `&pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`;
      return api;
    },
  },
};
</script>

<style scoped>
@import url("../../../css/views/employees/employeecontent.css");
</style>