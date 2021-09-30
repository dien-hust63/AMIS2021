<template>
  <div class="inward-detail-wrap" v-show="isShowInwardDetail">
    <div class="inward-detail">
      <div class="inward-detail-header">
        <div class="recent-log-btn">
          <div class="mi mi-24 mi-recent-log"></div>
        </div>
        <div class="title">
          Phiếu nhập kho {{ masterContent["voucher_code"] }}
        </div>
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
              @changeTableContent="changeVoucherDetail"
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
        <div class="right-group-button" @click="saveVoucherDetail">
          <base-button value=" Cất" class="ms-button-secondary button-add" />
          <div class="base-button-custom">
            <div class="button-custom-left ms-button-primary">
              <div class="buttom-custom-text">Cất và thêm</div>
            </div>
            <div
              class="button-custom-right ms-button-primary"
              @click="showAddOptions"
            >
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
import moment from "moment";
import MethodMixin from "../../../mixins/methods";
import { RepositoryFactory } from "../../../js/repository/repository.factory";
const VoucherRepository = RepositoryFactory.get("vouchers");
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
        mode: "api",
      },
      /**combobox employee */
      employeeComboboxValue: "",
      employeeComboboxProps: {
        tableHeaders: this.$resourcesVN.InwardEmployeeComboboxHeaders,
        api: this.$resourcesVN.apiList.employeePagingFilter,
        tableObject: "Employees",
        mode: "api",
      },
      /**field in master */
      customerAddress: "",
      /**mode của phiếu */
      mode: this.$resourcesVN.mode.ADD,
      /**ẩn hiện form */
      isShowInwardDetail: false,
      /**Nội dung form */
      masterContent: {},
      /**đơn vị tính */
      units: [],
      /**dữ liệu chi tiết hàng tiền phiếu nhập  */
      voucherDetailContents: [],
    };
  },
  methods: {
    /**
     *hiển thị option cất
     */
    showAddOptions() {
      console.log("showAddOptions");
    },
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
        this.$set(this.tableInwardDetailContents, 0, {});
      }
    },
    /**xóa hết dòng */
    deleteAllRow() {
      let messageText = this.formatString(
        this.$resourcesVN.message.messageDeleteWarning,
        "tất cả các dòng đã nhập"
      );
      this.$eventBus.$emit("showMessageBox", {
        icon: "mi-exclamation-warning-48",
        messageText: messageText,
        buttons: [
          {
            feature: "left ms-button-secondary",
            callback: () => {
              console.log("close message box");
            },
            value: "Không",
          },
          {
            feature: "right-first ms-button-primary",
            callback: () => {
              console.log("xóa hết dòng");
            },
            value: "Có",
          },
        ],
      });
      // this.tableInwardDetailContents = [];
    },
    /**Xóa 1 dòng */
    deleteRow(index) {
      this.tableInwardDetailContents.splice(index, 1);
    },
    /**bind lên combobox khách hàng */
    bindingCustomerCombobox(content) {
      /**khách hàng */
      this.$set(
        this.masterContent,
        "account_object_name",
        content["account_object_name"]
      );
      this.$set(
        this.masterContent,
        "accountobject_id",
        content["accountobject_id"]
      );
      /**nhân viên bán hàng */
      this.$set(this.masterContent, "employee_name", content["employee_name"]);
      this.$set(this.masterContent, "employee_id", content["employee_id"]);
      /**địa chỉ */
      this.$set(
        this.masterContent,
        "contact_address",
        content["contact_address"]
      );
    },
    /**bind lên combobox nhân viên */
    bindingEmployeeCombobox(content) {
      this.$set(this.masterContent, "employee_name", content["employee_name"]);
      this.$set(this.masterContent, "employee_id", content["employee_id"]);
    },
    /**gán lại nội dung table */
    changeVoucherDetail(index, header, mode, content) {
      switch (mode) {
        case "input": {
          //thay đổi giá trị của chính nó
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            content
          );
          break;
        }
        case "blur": {
          if (
            header.fieldName == "quantity" ||
            header.fieldName == "debit_amount"
          ) {
            let quantity = content["quantity"];
            let debit_amount = content["debit_amount"];
            //Thay đổi thành tiền
            this.$set(
              this.tableInwardDetailContents[index],
              "total_price",
              quantity * debit_amount
            );
          }
          if (header.fieldName == "total_price") {
            let quantity = content["quantity"];
            let totalPrice = content["total_price"];
            //Thay đổi thành tiền
            this.$set(
              this.tableInwardDetailContents[index],
              "debit_amount",
              (totalPrice * 1.0) / quantity
            );
          }
          break;
        }
        case "comboboxapi": {
          let newContent = content[header.fieldName];
          if (
            header.fieldName == "debit_account_number" ||
            header.fieldName == "credit_account_number"
          ) {
            newContent = content["account_number"];
          }
          if (header.fieldName == "commodity_code") {
            //nếu chọn mã hàng => bind lên các trường khác
            //hàng hóa
            this.$set(
              this.tableInwardDetailContents[index],
              "commodity_name",
              content["commodity_name"]
            );
            this.$set(
              this.tableInwardDetailContents[index],
              "commodity_id",
              content["commodity_id"]
            );
            //kho
            this.$set(
              this.tableInwardDetailContents[index],
              "warehouse_code",
              content["warehouse_code"]
            );
            this.$set(
              this.tableInwardDetailContents[index],
              "warehouse_id",
              content["warehouse_id"]
            );
            //Tài khoản nợ
            this.$set(
              this.tableInwardDetailContents[index],
              "debit_account_number",
              content["debit_account_number"]
            );
            this.$set(
              this.tableInwardDetailContents[index],
              "debit_account_id",
              content["debit_account"]
            );
            //Tài khoản có
            this.$set(
              this.tableInwardDetailContents[index],
              "credit_account_number",
              content["credit_account_number"]
            );
            this.$set(
              this.tableInwardDetailContents[index],
              "credit_account_id",
              content["credit_account"]
            );
            //Đơn giá
            this.$set(
              this.tableInwardDetailContents[index],
              "debit_amount",
              content["debit_amount"]
            );
          }
          //thay đổi giá trị của chính nó
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            newContent
          );
          break;
        }
        case "date": {
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            content
          );
          break;
        }
      }
    },
    /**Cất dữ liệu */
    saveVoucherDetail() {
      //Thêm mới
      if (this.mode == this.$resourcesVN.mode.ADD) {
        //lấy dữ liệu diễn giải
        this.masterContent["description"] = this.descriptionVoucher;
        //Lấy loại phiếu nhập
        this.masterContent["voucher_type"] = this.dropdownInwardTypeData;
        //Trạng thái ghi sổ
        this.masterContent["is_mention"] = 1;
        //data post
        let voucherDetailContent = {
          in_inward: this.masterContent,
          in_inward_detail: this.tableInwardDetailContents,
        };
        //thêm dữ liệu
        VoucherRepository.addVoucher(voucherDetailContent)
          .then((response) => {
            this.$eventBus.$emit("loadVoucherTable");
            console.log(response);
          })
          .catch((response) => console.log(response));
      }
      if(this.mode ==
       this.$resourcesVN.mode.EDIT){
        console.log("edit form");
      }
    },
  },
  computed: {
    descriptionVoucher: function () {
      let inwardType = this.$resourcesVN.inwardTypeDropdownList.find(
        (o) => o.data === this.dropdownInwardTypeData
      ).value;
      if (this.masterContent["account_object_name"]) {
        return `${inwardType} của ${this.masterContent["account_object_name"]}`;
      } else {
        return inwardType;
      }
    },
  },
  created() {
    this.$eventBus.$on("showInwardDetail", (mode, content) => {
      this.mode = mode;
      if (mode == this.$resourcesVN.mode.ADD) {
        let currentDate = moment().format("YYYY-MM-DD");
        this.masterContent = {
          voucher_code: content,
          mathematics_date: currentDate,
          voucher_date: currentDate,
        };

        this.tableInwardDetailContents = [{}];
      }
      if (mode == this.$resourcesVN.mode.EDIT) {
        this.masterContent = content["in_inward"][0];
        this.tableInwardDetailContents = content["in_inward_detail"];
        this.voucherDetailContents = content["in_inward_detail"];
        // console.log(this.tableInwardDetailContents);
        // let test = this.tableInwardDetailContents[0]["units"];
        // console.log(JSON.parse(test));
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