<template>
  <div class="inward-detail-wrap" v-show="isShowInwardDetail">
    <div class="inward-detail">
      <div class="inward-detail-header">
        <div class="recent-log-btn">
          <div class="mi mi-24 mi-recent-log"></div>
        </div>
        <div class="title">Phiếu nhập kho {{masterContent['voucher_code']}}</div>
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
          <div class="button-close button-group" @click="closeInwardDetail">
            <div class="mi mi-24 mi-close"></div>
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
                      v-model="masterContent['account_object_name']"
                      :comboboxProps="customerComboboxProps"
                      :hasFooter="false"
                      @showAddForm="showCustomerDetail"
                      @getDataEventBus="bindingCustomerCombobox"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input
                      label="Địa chỉ"
                      v-model="masterContent['contact_address']"
                    />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input
                      label="Người giao hàng"
                      v-model="masterContent['contact_name']"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input
                      label="Diễn giải"
                      v-model="descriptionVoucher"
                    />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-combobox-custom
                      label="Nhân viên bán hàng"
                      v-model="masterContent['employee_name']"
                      :comboboxProps="employeeComboboxProps"
                      :hasFooter="false"
                      @showAddForm="showEmployeeDetail"
                      @getDataEventBus="bindingEmployeeCombobox"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <div class="w-2/3 flex">
                      <base-input
                        label="Kèm theo"
                        class="voucher-attach"
                        placeholder="Số lượng"
                        v-model="masterContent['voucher_attach']"
                      />
                      <div class="voucher-attach-title">
                        <div class="text">chứng từ gốc</div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row-input flex">
                  <div class="reference-title">Tham chiếu</div>
                  <div class="reference-show-more">...</div>
                </div>
              </div>
              <div class="w-1/5">
                <div class="row-input-right">
                  <base-date-input
                    label="Ngày hạch toán"
                    type="date"
                    v-model="masterContent['mathematics_date']"
                  />
                </div>
                <div class="row-input-right">
                  <base-date-input
                    label="Ngày chứng từ"
                    type="date"
                    v-model="masterContent['voucher_date']"
                  />
                </div>
                <div class="row-input-right voucher-number">
                  <base-input
                    label="Số chứng từ"
                    v-model="masterContent['voucher_code']"
                  />
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
              @deleteRow="deleteRow"
              @changeTableContent='changeVoucherDetail'
            />
            <div class="inward-detail-pagination">
              <base-pagination />
            </div>
          </div>
        </div>
        <div class="grid-control-item">
          <div class="btn-grid-control">
            <base-button value="Thêm dòng" @clickButton="addNewRow" />
            <base-button value="Thêm ghi chú" />
            <base-button value="Xóa hết dòng" @clickButton="deleteAllRow" />
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
          <base-button value=" Cất" class="ms-button-secondary button-add" />
          <div class="base-button-custom">
            <div class="button-custom-left ms-button-primary">
              <div class="buttom-custom-text">Cất và in</div>
            </div>
            <div class="button-custom-right ms-button-primary">
              <div class="line"></div>
              <div class="mi mi-16 mi-arrow-up--white"></div>
            </div>
          </div>
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
import BaseDateInput from "../../base/BaseDateInput.vue";
export default {
  name: "InwardDetail",
  components: {
    BaseInput,
    BaseButton,
    BaseTable,
    BasePagination,
    BaseDropdown,
    BaseComboboxCustom,
    BaseDateInput,
  },
  data() {
    return {
      tableInwardDetailHeaders: this.$resourcesVN.tableInwardDetailHeaders,
      dropdownInwardTypeDefault: this.$resourcesVN.inwardTypeList[1],
      dropdownInwardTypeData: this.$resourcesVN.inwardTypeList[1].data,
      inwardTypeList: this.$resourcesVN.inwardTypeList,

      dropdownInwardMethodDefault: this.$resourcesVN.inwardMethodList[0],
      inwardMethodList: this.$resourcesVN.inwardMethodList,
      /**table */
      tableInwardDetailContents: this.$resourcesVN.tableInwardDetailContents,
      /**combobox customer*/
      customerComboboxValue: "",
      customerComboboxProps: {
        tableHeaders: this.$resourcesVN.tableCustomerHeaders,
        api: this.$resourcesVN.apiList.accountobjectPagingFilter,
        tableObject: "AccountObjects",
        valueField: "account_object_name",
      },
      /**combobox employee */
      employeeComboboxValue: "",
      employeeComboboxProps: {
        tableHeaders: this.$resourcesVN.InwardEmployeeComboboxHeaders,
        api: this.$resourcesVN.apiList.employeePagingFilter,
        tableObject: "Employees",
        valueField: "employee_name",
      },
      /**field in master */
      customerAddress: "",
      /**mode của phiếu */
      mode: this.$resourcesVN.mode.ADD,
      /**ẩn hiện form */
      isShowInwardDetail: false,
      /**Nội dung form */
      masterContent: {},
    };
  },
  methods: {
    /**Đóng form popup
     * CreaedBy: nvdien(17/9/2021)
     */
    closeInwardDetail() {
      this.isShowInwardDetail = false;
    },

    /**Mở form Thêm mới khách hàng*/
    showCustomerDetail() {
      this.$eventBus.$emit("showCustomerDetail");
    },
    /**Mở form Thêm mới nhân viên*/
    showEmployeeDetail() {
      this.$eventBus.$emit("showEmployeeDetail");
    },
    /**Thêm dòng table */
    addNewRow() {
      let totalRow = this.tableInwardDetailContents.length;
      if (totalRow > 0) {
        let newRowContent = this.tableInwardDetailContents[totalRow - 1];
        this.tableInwardDetailContents.push(newRowContent);
      } else {
        let tableContent =
          this.$resourcesVN.tableInwardDetailContentsDefault[0];
        this.$set(this.tableInwardDetailContents, 0, tableContent);
      }
    },
    /**xóa hết dòng */
    deleteAllRow() {
      this.tableInwardDetailContents = [];
    },
    /**Xóa 1 dòng */
    deleteRow(index) {
      this.tableInwardDetailContents.splice(index, 1);
    },
    /**bind lên combobox khách hàng */
    bindingCustomerCombobox(content) {
      this.$set(
        this.masterContent,
        "account_object_name",
        content["account_object_name"]
      );
      this.$set(this.masterContent, "employee_name", content["employee_name"]);
      this.$set(this.masterContent, "contact_address", content["contact_address"]);
    },
    /**bind lên combobox nhân viên */
    bindingEmployeeCombobox(content) {
      this.$set(this.masterContent, "employee_name", content["employee_name"]);
    },
    /**gán lại nội dung table */
  changeVoucherDetail(index, header, content){
    let newContent = content;
    if(header.type == "combobox") newContent = content[header.fieldName];
    this.$set(this.tableInwardDetailContents[index], header.fieldName, newContent);
    }
  },
  computed:{
    descriptionVoucher: function(){
      let inwardType = this.$resourcesVN.inwardTypeDropdownList.find(o => o.data === this.dropdownInwardTypeData).value;
      if(this.masterContent["account_object_name"]){
        return `${inwardType} của ${this.masterContent["account_object_name"]}`;
      }else{
        return inwardType;
      }
     
    }
  },
  created() {
    this.$eventBus.$on("showInwardDetail", (mode, content) => {
      this.mode = mode;
      if (mode == this.$resourcesVN.mode.ADD) {
        this.masterContent = {};
      }
      if (mode == this.$resourcesVN.mode.EDIT) {
        this.masterContent = content["in_inward"][0];
        this.tableInwardDetailContents = content["in_inward_detail"];
        console.log("test");
        console.log(this.tableInwardDetailContents);
      }
      this.isShowInwardDetail = true;
    });
  },
  destroyed() {
    this.$eventBus.$off("showInwardDetail");
  },
};
</script>

<style scoped>
@import url("../../../css/views/warehouse/inwarddetail.css");
</style>