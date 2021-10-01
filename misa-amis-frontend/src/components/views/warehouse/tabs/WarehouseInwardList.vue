<template>
  <div class="inward-list">
    <div class="toolbar">
      <div class="toolbar__left">
        <div class="mi mi-24 mi-arrow-check-all"></div>
        <div
          class="batch-execution ms-button ms-button-around ms-button-secondary"
          :class="{ 'ms-button--disabled': isDisabled }"
          @click="batchExecution($event)"
        >
          <div class="ms-button-content" >
            <div class="ms-button__text" >
              Thực hiện hàng loạt
            </div>
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
        <div
          class="mi mi-24 mi-refresh toolbar-refresh"
          @click="loadData"
        ></div>
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
        @getSelectedRowList="getSelectedRowList"
        @handleDoubleClickRow="editVoucher"
        @clickCellVoucherCode="editVoucher"
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
import MethodMixin from "../../../../mixins/methods";
const VoucherRepository = RepositoryFactory.get("vouchers");
import moment from "moment";
export default {
  name: "WarehouseInwardList",
  mixins: [DateMixin, MethodMixin],
  components: {
    BaseTable,
    BasePagination,
    BaseInput,
    BaseLoading,
    BaseDropdown,
    BaseDateInput,
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
      isShowContextMenu: false,
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
      /**Button thực hiện hàng loạt */
      isDisabled: true,
      listSelectedId: [],
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
          //format tiền
          if (response.data["TotalPrices"] == "0")
            this.tableInwardListHeaders[4]["footerValue"] = "0,0";
          else {
            this.tableInwardListHeaders[4]["footerValue"] =
              response.data["TotalPrices"];
          }
          //total record
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
      Object.assign(this.currentRowData, rowContent);
      if (rowContent["is_mention"] == 1) {
        //trạng thái đã ghi sổ
        this.contextMenuData["contextFunction"] = [
          {
            name: "Bỏ ghi",
            function: () => {
              this.unSaveBook(rowContent);
            },
          },
          {
            name: "Nhân bản",
            function: () => {
              this.duplicateItem(rowContent);
            },
          },
        ];
      } else {
        //trạng thái chưa ghi sổ
        this.contextMenuData["contextFunction"] = [
          {
            name: "Ghi sổ",
            function: () => {
              this.saveBook(this.currentRowData);
            },
          },
          {
            name: "Xóa",
            function: () => {
              this.showWarningDelete(this.currentRowData);
            },
          },
          {
            name: "Nhân bản",
            function: () => {
              this.duplicateItem(rowContent);
            },
          },
        ];
      }
    },
    /**hiển thị cảnh báo khi xóa
     * @param {object} rowContent
     * CreatedBy: nvdien(26/9/2021)
     */
    showWarningDelete(rowContent) {
      let messageText = this.formatString(
        this.$resourcesVN.message.messageDeleteWarning,
        `chứng từ <${rowContent["voucher_code"]}>`
      );
      this.$eventBus.$emit("showMessageBox", {
        icon: "mi-exclamation-warning-48",
        messageText: messageText,
        buttons: [
          {
            feature: "left ms-button-secondary",
            callback: () => {
              this.closeMessageBox();
            },
            value: "Không",
          },
          {
            feature: "right-first ms-button-primary",
            callback: () => {
              this.deleteRow(rowContent);
            },
            value: "Có",
          },
        ],
      });
    },
    /**xóa chứng từ */
    deleteRow(rowContent) {
      let rowId = rowContent["accountvoucher_id"];
      VoucherRepository.delete(rowId)
        .then(() => {
          //thông báo xóa thành công
          let toastMessageText = this.formatString(
            this.$resourcesVN.message.messageDeleteSuccess,
            `chứng từ`
          );
          this.$eventBus.$emit("showToastMessage", {
            icon: "mi-notifications–success",
            text: toastMessageText,
          });
          this.closeMessageBox();
          this.loadData();
        })
        .catch((response) => console.log(response));
    },
    /**Đóng message box */
    closeMessageBox() {
      this.$eventBus.$emit("hideMessageBox");
    },
    /**ghi sổ
     * @param {object} rowContent
     * CreatedBy: nvdien(26/9/2021)
     */
    saveBook(rowContent) {
      rowContent["is_mention"] = 1;
      VoucherRepository.put(rowContent["accountvoucher_id"], rowContent)
        .then(() => {
          this.loadData();
        })
        .catch((response) => console.log(response));
    },
    /**bỏ ghi sổ
     * @param {object} rowContent
     * CreatedBy: nvdien(26/9/2021)
     */
    unSaveBook(rowContent) {
      rowContent["is_mention"] = 0;
      VoucherRepository.put(rowContent["accountvoucher_id"], rowContent)
        .then(() => {
          this.loadData();
        })
        .catch((response) => console.log(response));
    },
    /**Thực hiện hàng loạt
     * CreatedBy: nvdien(26/9/2021)
     */
    batchExecution(event) {
      if (!this.isDisabled) {
        this.toogleContextMenu(event);
      }
    },
    /**
     * hiển thị context menu
     * CreatedBy: nvdien(25/9/2021)
     */
    toogleContextMenu(event) {
      let contextMenuData = [
        {
          name: "Ghi sổ",
          function: () => {
            this.mentionMany(this.listSelectedId);
          },
        },
        {
          name: "Bỏ ghi",
          function: () => {
            this.unMentionMany(this.listSelectedId);
          },
        },
        {
          name: "Xóa",
          function: () => {
            this.warningDeleteMany();
          },
        },
      ];
      this.isShowContextMenu = !this.isShowContextMenu;
      if (this.isShowContextMenu) {
        let element = event.target;
        let elementRect = element.getBoundingClientRect();
        let elementPos = {
          top: elementRect.top,
          left: elementRect.left,
          topChange: 36,
          leftChange: 30,
        };
        this.$eventBus.$emit("showContextMenu", {
          contextFunction: contextMenuData,
          pos: elementPos,
        });
      } else {
        this.$eventBus.$emit("hideContextMenu");
      }
    },
    /**Lấy danh sách các selected row */
    getSelectedRowList(selectedRowList) {
      if (selectedRowList.length > 1) {
        this.isDisabled = false;
        for (let i = 0; i < selectedRowList.length; ++i) {
          this.listSelectedId.push(selectedRowList[i]["accountvoucher_id"]);
        }
      } else {
        this.isDisabled = true;
      }
    },
    /**Mở form phiếu nhập kho */
    editVoucher(content) {
      let voucherId = content["accountvoucher_id"];
      VoucherRepository.getVoucherDetail(voucherId)
        .then((response) => {
          let mode = this.$resourcesVN.mode.EDIT;
          this.$eventBus.$emit("showInwardDetail", mode, response.data.Data);
        })
        .catch((response) => console.log(response));
    },
    /**Ghi sổ hàng loạt */
    mentionMany(listId) {
      VoucherRepository.mentionMany(listId)
        .then(() => {this.loadData()})
        .catch((response) => console.log(response));
    },
    /**Bỏ ghi hàng loạt */
    unMentionMany(listId){
      VoucherRepository.unMentionMany(listId)
        .then(() => {this.loadData()})
        .catch((response) => console.log(response));
    },
    /**cảnh báo xóa hàng loạt */
    warningDeleteMany(){
      let messageText = this.formatString(
        this.$resourcesVN.message.messageDeleteWarning,
        "những chứng từ này"
      );
      this.$eventBus.$emit("showMessageBox", {
        icon: "mi-exclamation-warning-48",
        messageText: messageText,
        buttons: [
          {
            feature: "left ms-button-secondary",
            callback: () => {
              this.closeMessageBox();
            },
            value: "Không",
          },
          {
            feature: "right-first ms-button-primary",
            callback: () => {
              this.deleteMultiple(this.listSelectedId);
            },
            value: "Có",
          },
        ],
      });
    },
    /**Xóa hàng loạt */
    deleteMultiple(listId){
      VoucherRepository.deleteMultiple(listId).then(
        ()=>{//thông báo xóa thành công
          let toastMessageText = this.formatString(
            this.$resourcesVN.message.messageDeleteSuccess,
            `chứng từ`
          );
          this.$eventBus.$emit("showToastMessage", {
            icon: "mi-notifications–success",
            text: toastMessageText,
          });
          this.closeMessageBox();
          this.loadData()}
      ).catch(response => console.log(response))
    },
    /**Nhân bản */
    duplicateItem(rowContent){
      let voucherId = rowContent["accountvoucher_id"];
       VoucherRepository.getVoucherDetail(voucherId)
        .then((response) => {
          let mode = this.$resourcesVN.mode.DUPLICATE;
          this.$eventBus.$emit("showInwardDetail", mode, response.data.Data);
        })
        .catch((response) => console.log(response));
    }
  },
  watch: {
    timeReportDropdownData(newValue) {
      let dateRange = this.changeDatefromDropdown(newValue);
      this.endDateFilter = dateRange[1];
      this.startDateFilter = dateRange[0];
    },
  },
  created() {
    //lấy danh sách chứng từ
    this.loadData();
    this.$eventBus.$on("loadVoucherTable", () => {
      console.log("test");
      this.$nextTick(() => {this.loadData()});
    });
  },
  destroyed() {
    this.$eventBus.$off("loadVoucherTable");
  },
};
</script>

<style scoped>
@import url("../../../../css/views/warehouse/warehouseinwardlist.css");
</style>