<template>
  <div class="inward-detail-wrap">
    <div class="inward-detail">
      <div class="inward-detail-header">
        <div class="recent-log-btn">
          <div class="mi mi-24 mi-recent-log"></div>
        </div>
        <div class="title">Phiếu nhập kho BK025</div>
        <div class="header-detail-input">
          <base-dropdown
            v-model="dropdownInwardTypeData"
            :dropdownList="inwardTypeList"
            :defaultData="dropdownInwardTypeDefault"
          />
          <!-- <div class="combobox"></div> -->
        </div>
        <div class="header-detail-button">
          <div class="mi mi-24 mi-tour"></div>
          <div class="tour-label">Hướng dẫn</div>
          <div class="button-setting button-group">
            <div class="mi mi-24 mi-setting__detail"></div>
          </div>
          <div class="button-help button-group">
            <div class="mi mi-24 mi-help"></div>
          </div>
          <div class="button-close button-group">
            <div class="mi mi-24 mi-close" @click="closeInwardDetail()"></div>
          </div>
        </div>
      </div>
      <div class="inward-detail-body">
        <div class="main-infomation flex">
          <div class="w-3/4">
            <div class="basic-information flex">
              <div class="w-4/5">
                <div class="row-input">
                  <div class="w-3/7">
                    <base-combobox-custom
                      label="Khách hàng"
                      @showComboDropdown="showCustomerDropdownPanel"
                      @showAddForm="addCustomer"
                      v-model="customerComboboxValue"
                      @keyup="searchCustomerCombobox($event)"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input label="Địa chỉ" />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input label="Người giao hàng" />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input label="Diễn giải" />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-combobox-custom
                      label="Khách hàng"
                      @showComboDropdown="showEmployeeDropdownPanel"
                      @showAddForm="addEmployee"
                      v-model="employeeComboboxValue"
                      @keyup="searchEmployeeCombobox($event)"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input label="Kèm theo" />
                  </div>
                </div>
                <div class="row-input flex">
                  <div class="reference-title">Tham chiếu</div>
                  <div class="reference-show-more">...</div>
                </div>
              </div>
              <div class="w-1/5">
                <div class="row-input-right">
                  <base-input label="Ngày hạch toán" />
                </div>
                <div class="row-input-right">
                  <base-input label="Ngày chứng từ" />
                </div>
                <div class="row-input-right voucher-number">
                  <base-input label="Số chứng từ" />
                </div>
              </div>
            </div>
          </div>
          <div class="w-1/4 summary-info">
            <div class="summary-info-title">Tổng tiền</div>
            <div class="summary-info-number">0,00</div>
          </div>
        </div>
        <div class="tab-inward-detail">
          <div class="inward-detail-table">
            <div class="table-above">
              <div class="table-above-title">Hàng tiền</div>
              <div class="table-above-control">
                <div>Đơn giá nhập kho</div>
                <base-dropdown
                  :dropdownList="inwardMethodList"
                  :defaultData="dropdownInwardMethodDefault"
                />
              </div>
            </div>
            <base-table
              :tableHeaders="tableInwardDetailHeaders"
              :tableContents="tableInwardDetailContents"
            />
            <div class="inward-detail-pagination">
              <base-pagination />
            </div>
          </div>
        </div>
        <div class="grid-control-item">
          <div class="btn-grid-control">
            <base-button value="Thêm dòng" />
            <base-button value="Thêm ghi chú" />
            <base-button value="Xóa hết dòng" />
          </div>
          <div class="input-grid-control">
            <div class="flex">
              <div class="mi mi-18 mi-attach"></div>
              <div class="mi-attach-title">Đính kèm</div>
              <div class="capacity-attach">Dung lượng tối đa 5MB</div>
            </div>
            <div class="input-attach-content">
              Kéo/Thả tệp vào đây hoặc bấm vào đây
            </div>
          </div>
        </div>
      </div>
      <div class="inward-detail-footer">
        <div class="left-group-button">
          <base-button value="Hủy" class="ms-button-secondary" />
        </div>
        <div class="right-group-button">
          <base-button value=" Cất" class="ms-button-secondary" />
          <base-button value="Cất và in" class="ms-button-primary" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";
import BaseButton from "../../base/BaseButton.vue";
import BaseTable from "../../base/BaseTable.vue";
import BasePagination from "../../base/BasePagination.vue";
import BaseDropdown from "../../base/BaseDropdown.vue";
import BaseComboboxCustom from "../../base/BaseComboboxCustom.vue";
import MethodMixin from "../../../mixins/methods";
import { RepositoryFactory } from "../../../js/repository/repository.factory";
const AccountObjectRepository = RepositoryFactory.get("accountobjects");
const EmployeeRepository = RepositoryFactory.get("employees");
import axios from "axios";
export default {
  name: "InwardDetail",
  mixins: [MethodMixin],
  components: {
    BaseInput,
    BaseButton,
    BaseTable,
    BasePagination,
    BaseDropdown,
    BaseComboboxCustom,
  },
  data() {
    return {
      tableInwardDetailHeaders: this.$resourcesVN.tableInwardDetailHeaders,
      dropdownInwardTypeDefault: this.$resourcesVN.inwardTypeList[1],
      dropdownInwardTypeData: this.$resourcesVN.inwardTypeList[1].data,
      inwardTypeList: this.$resourcesVN.inwardTypeList,

      dropdownInwardMethodDefault: this.$resourcesVN.inwardMethodList[0],
      inwardMethodList: this.$resourcesVN.inwardMethodList,
      isShowInwardDetail: false,
      /**table */
      tableInwardDetailContents: [],
      /**combobox customer*/
      customerComboboxValue: "",
      timeDelaySearch: null,
      /**combobox employee */
      employeeComboboxValue: "",
    };
  },
  methods: {
    /**Đóng form popup
     * CreaedBy: nvdien(17/9/2021)
     */
    closeInwardDetail() {
      // this.dropdownData = 20;
      this.$emit("closeInwardDetail");
    },
    /**
     * Hàm tính ra element cha chứa lớp được truyền vào
     * @param {Element} childE
     * @param {String} parentClass
     */
    findParentByClass(childE, parentClass) {
      var parentE = childE;
      if (parentE) {
        // Nếu không chứa class thì tiếp tục vòng lặp
        while (parentE.classList.contains(parentClass) == false) {
          // Đi ra một element cha
          parentE = parentE.parentElement;
          // Khi đã duyệt hết mà không có thì set null và thoát vòng lặp
          if (parentE.tagName == "BODY") {
            parentE = null;
            break;
          }
        }
      }
      return parentE;
    },
    //#region Combobox Customer

    /**hiển thị combo dropdown panel của khách hàng */
    showCustomerDropdownPanel(event) {
      let element = event.target;
      let elementRect = element.getBoundingClientRect();
      let leftChange =
        -this.findParentByClass(element, "combobox-custom-content")
          .clientWidth +
        element.clientWidth +
        6;
      let elementPos = {
        top: elementRect.top,
        left: elementRect.left,
        topChange: 30,
        leftChange: leftChange,
      };
      axios.get(this.formatString(this.$resourcesVN.apiList.accountobjectPagingFilter, "", 1, 20))
        .then((response) => {
          console.log(response);
          let functionEmit = "bindAccountObjectCombobox";
          let comboDropdownData = {
            tableHeaders: this.$resourcesVN.tableCustomerHeaders,
            tableContents: response.data["AccountObjects"],
            hasFooter: false,
            position: elementPos,
            functionEmit: functionEmit,
          };
          this.$eventBus.$emit("showComboDropdown", comboDropdownData);
          this.$eventBus.$on(functionEmit, (data) =>{
            this.customerComboboxValue = data["account_object_name"];
           this.$eventBus.$emit("hideComboDropdown");
          })
          
           
        })
        .catch((response) => console.log(response));
    },
    /**filter combobox khách hàng */
    searchCustomerCombobox(event) {
      if (this.timeDelaySearch) {
        clearTimeout(this.timeDelaySearch);
      }
      this.timeDelaySearch = setTimeout(() => {
        let element = event.target;
        let elementRect = element.getBoundingClientRect();
        let elementPos = {
          top: elementRect.top,
          left: elementRect.left,
          topChange: 30,
          leftChange: -10,
        };
        AccountObjectRepository.getAccountObjectPagingFilter(this.customerComboboxValue, 1, 20)
          .then((response) => {
            let comboDropdownData = {
              tableHeaders: this.$resourcesVN.tableCustomerHeaders,
              tableContents: response.data["AccountObjects"],
              hasFooter: false,
              position: elementPos,
              isLoading: false,
            };
            this.$eventBus.$emit("showComboDropdown", comboDropdownData);
          })
          .catch((response) => console.log(response));
        this.$eventBus.$emit("showComboDropdown", {position: elementPos,isLoading: true});
      }, 500);
      
    },
    
    /**Thêm mới khách hàng */
    addCustomer(){
      console.log("hiển thị khách hàng");
      this.$eventBus.$emit("showCustomerDetail");
    },
    //#endregion
    //#region Combobox Employee
  
    /**hiển thị combo dropdown panel của khách hàng */
    showEmployeeDropdownPanel(event) {
      let element = event.target;
      let elementRect = element.getBoundingClientRect();
      let leftChange =
        -this.findParentByClass(element, "combobox-custom-content")
          .clientWidth +
        element.clientWidth +
        6;
      let elementPos = {
        top: elementRect.top,
        left: elementRect.left,
        topChange: 30,
        leftChange: leftChange,
      };
      EmployeeRepository.getEmployeePagingFilter("", 1, 20)
        .then((response) => {
          let comboDropdownData = {
            tableHeaders: this.$resourcesVN.InwardEmployeeComboboxHeaders,
            tableContents: response.data["Employees"],
            hasFooter: false,
            position: elementPos,
          };
          this.$eventBus.$emit("showComboDropdown", comboDropdownData);
        })
        .catch((response) => console.log(response));
    },
    /**filter combobox khách hàng */
    searchEmployeeCombobox(event) {
      if (this.timeDelaySearch) {
        clearTimeout(this.timeDelaySearch);
      }
      this.timeDelaySearch = setTimeout(() => {
        let element = event.target;
        let elementRect = element.getBoundingClientRect();
        let elementPos = {
          top: elementRect.top,
          left: elementRect.left,
          topChange: 30,
          leftChange: -10,
        };
        EmployeeRepository.getEmployeePagingFilter(this.employeeComboboxValue, 1, 20)
          .then((response) => {
            let comboDropdownData = {
              tableHeaders: this.$resourcesVN.InwardEmployeeComboboxHeaders,
              tableContents: response.data["Employees"],
              hasFooter: false,
              position: elementPos,
              isLoading: false,
            };
            this.$eventBus.$emit("showComboDropdown", comboDropdownData);
          })
          .catch((response) => console.log(response));
        this.$eventBus.$emit("showComboDropdown", {position: elementPos,isLoading: true});
      }, 500);
      
    },
    
    /**Thêm mới nhân viên */
    addEmployee(){
      this.$eventBus.$emit("showEmployeeDetail");
    }
    //#endregion
  },
  // created() {
  //   /**
  //    * Tạo event hiển thị context menu
  //    * CreatedBy: nvdien (20/09/2021)
  //    */
  //   this.$eventBus.$on("bindComboboxValue", (data) => {
  //     this.customerComboboxValue = data["account_object_name"];
  //     this.employeeComboboxValue = data["employee_name"];
  //     this.$eventBus.$emit("hideComboDropdown");
  //   });
  // },
  // destroyed() {
	// 		/**
	// 		 * Huỷ các sự kiện
	// 		 * CreatedBy: nvdien (20/09/2021)
	// 		 */
	// 		this.$eventBus.$off("bindComboboxValue");
	// },
};
</script>

<style scoped>
@import url("../../../css/views/warehouse/inwarddetail.css");
</style>