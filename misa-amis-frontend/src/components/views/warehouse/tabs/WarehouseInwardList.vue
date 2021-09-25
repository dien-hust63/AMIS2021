<template>
  <div class="inward-list">
    <div class="toolbar">
      <div class="toolbar__left">
        <div class="mi mi-24 mi-arrow-check-all"></div>
        <div
          class="batch-execution ms-button ms-button-around ms-button-secondary"
        >
          <div class="ms-button-content">
            <div class="ms-button__text">Thực hiện hàng loạt</div>
            <div class="mi mi-16 mi-arrow-up--black"></div>
          </div>
        </div>
        <div class="filter">
          <div
            class="button-filter ms-button ms-button-around ms-button-secondary"
            @click="toogleFilterPanel"
          >
            <div class="ms-button-content">
              <div class="ms-button__text">Lọc</div>
              <div class="mi mi-16 mi-arrow-up--black"></div>
            </div>
          </div>

          <div class="filter-panel" v-show="showFilterPanel">
            <div class="filter-panel-content">
              <div class="row-input">
                <base-dropdown
                  label="Trạng thái ghi sổ"
                  :dropdownList="$resourcesVN.mentionStateDropdownList"
                  :defaultData="$resourcesVN.mentionStateDropdownList[0]"
                  v-model="mentionState"
                />
              </div>
              <div class="row-input">
                <base-dropdown
                  label="Loại nhập"
                  :dropdownList="$resourcesVN.inwardTypeDropdownList"
                  :defaultData="$resourcesVN.inwardTypeDropdownList[0]"
                  v-model="voucherType"
                />
              </div>
              <div class="row-input">
                <div class="w-3/7">
                  <base-dropdown
                    label="Kỳ báo cáo"
                    :dropdownList="$resourcesVN.timeReportDropdownList"
                    :defaultData="$resourcesVN.timeReportDropdownList[2]"
                    v-model="timeReportDropdownData"
                  />
                </div>
                <div class="w-4/7 flex">
                  <div class="w-1/2 p-l-12 box-sizing">
                    <base-date-input
                      label="Từ ngày"
                      type="date"
                      v-model="startDateFilter"
                      :defaultValue="startDateFilterDefault"
                    />
                  </div>
                  <div class="w-1/2 p-l-12 box-sizing">
                    <base-date-input
                      label="Đến ngày"
                      v-model="endDateFilter"
                      type="date"
                      :defaultValue="endDateFilterDefault"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="filter-panel-footer">
              <div class="ms-button ms-button-secondary">Đặt lại</div>
              <div
                class="ms-button ms-button-primary"
                @click="filterVoucherData"
              >
                Lọc
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="toolbar__right">
        <div class="toolbar-search">
          <base-input
            placeholder="Nhập từ khóa tìm kiếm"
            class="input-search"
            v-model="searchData"
            @keyup="search"
          />
        </div>
        <div class="mi mi-24 mi-refresh toolbar-refresh"></div>
        <div class="mi mi-24 mi-excel toolbar-excel"></div>
      </div>
    </div>
    <div class="warehouse-table">
      <base-table
        :tableHeaders="tableInwardListHeaders"
        :tableContents="tableInwardListContents"
        :hasFooter="true"
        :contextMenuData="contextMenuData"
        @getContextTableData="getContextTableData"
      />
      <div class="warehouse-content-pagination">
        <base-pagination
          :totalRecord="totalRecord"
          :totalPage="totalPage"
          @getPagingData="getPagingData"
        />
      </div>
    </div>
    <base-loading :class="{ 'ms-loading--show': isLoading }" />
  </div>
</template>

<script>
import BaseTable from "../../../base/BaseTable.vue";
import BasePagination from "../../../base/BasePagination.vue";
import BaseInput from "../../../base/BaseInput.vue";
import BaseLoading from "../../../base/BaseLoading.vue";
import { RepositoryFactory } from "../../../../js/repository/repository.factory";
import BaseDropdown from "../../../base/BaseDropdown.vue";
import BaseDateInput from "../../../base/BaseDateInput.vue";
import DateMixin from "../../../../mixins/date/date";
const VoucherRepository = RepositoryFactory.get("vouchers");
import moment from "moment";
export default {
  name: "WarehouseInwardList",
  mixins: [DateMixin],
  components: {
    BaseTable,
    BasePagination,
    BaseInput,
    BaseLoading,
    BaseDropdown,
    BaseDateInput,
  },
  created() {
    //lấy danh sách chứng từ
    this.loadData();
  },
  data() {
    return {
      tableInwardListHeaders: this.$resourcesVN.tableInwardListHeaders,
      tableInwardListContents: [],
      showFilterPanel: false,
      /**base date uinput */
      startDateFilterDefault: moment().startOf("month").format("DD/MM/YYYY"),
      startDateFilter: moment().startOf("month").format("YYYY-MM-DD"),
      endDateFilterDefault: moment().endOf("month").format("DD/MM/YYYY"),
      endDateFilter: moment().endOf("month").format("YYYY-MM-DD"),
      /**search value */
      searchData: "",
      timeReportDropdownData: "month",
      /**paging */
      totalRecord: 0,
      totalPage: 0,
      pageIndex: 1,
      pageSize: 20,
      mentionState: "",
      voucherType: "",
      /**search */
      timeDelaySearch: null,
      hasSearch: false,
      /**loading */
      isLoading: false,
      /**context menu */
      contextMenuData: {
        topChange: 20,
        leftChange: -80,
        contextFunction: [
          {
            name: "hello",
            function: () => {
              this.functionTest("hello");
            },
          },
        ],
      },
      /**dữ liệu hàng được chọn */
      currentRowData: {},
    };
  },
  methods: {
    /**
     * context test
     */
    functionTest(index) {
      console.log(index);
    },
    /**Hiển thị panel lọc */
    toogleFilterPanel() {
      this.showFilterPanel = !this.showFilterPanel;
    },
    /**lấy danh sách chứng từ */
    loadData() {
      VoucherRepository.getAccountVoucherPagingFilter(
        this.searchData,
        this.mentionState,
        this.voucherType,
        this.startDateFilter,
        this.endDateFilter,
        this.pageIndex,
        this.pageSize
      )
        .then((response) => {
          this.tableInwardListContents = response.data["Vouchers"];
          this.tableInwardListHeaders[4]["footerValue"] =
            response.data["TotalPrices"];
          this.totalRecord = response.data["TotalRecord"];
          this.totalPage = response.data["TotalPage"];
          this.isLoading = false;
        })
        .catch((response) => {
          console.log(response);
          this.isLoading = true;
        });
      this.isLoading = true;
    },
    /**
     * dữ liệu lấy được từ phân trang
     */
    getPagingData(pageIndex, pageSize) {
      this.pageIndex = pageIndex;
      this.pageSize = pageSize;
      this.loadData();
    },
    /**lọc dữ liệu */
    filterVoucherData() {
      this.showFilterPanel = false;
      this.loadData();
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
        this.loadData();
      }, 500);
    },
    /**
     * lấy dữ liệu hàng khi ấn chọn context menu trong table
     * CreatedBy: nvdien(25/9/2021)
     */
    getContextTableData(rowContent) {
      console.log(rowContent);
      Object.assign(this.currentRowData, rowContent);
      if (rowContent["is_mention"] == 1) {
        //trạng thái đã ghi sổ
        this.contextMenuData["contextFunction"] = [
          {
            name: "Bỏ ghi",
            function: () => {
              this.functionTest("hello");
            },
          },
          {
            name: "Nhân bản",
            function: () => {
              this.functionTest("hello");
            },
          },
        ];
      } else {
        //trạng thái chưa ghi sổ
         this.contextMenuData["contextFunction"] = [
          {
            name: "Ghi sổ",
            function: () => {
              this.functionTest("hello");
            },
          },
          {
            name: "Xóa",
            function: () => {
              this.functionTest("hello");
            },
          },
          {
            name: "Nhân bản",
            function: () => {
              this.functionTest("hello");
            },
          },
        ];
      }
    },
  },
  watch: {
    timeReportDropdownData(newValue) {
      let dateRange = this.changeDatefromDropdown(newValue);
      this.endDateFilter = dateRange[1];
      this.startDateFilter = dateRange[0];
    },
  },
};
</script>

<style scoped>
@import url("../../../../css/views/warehouse/warehouseinwardlist.css");
</style>