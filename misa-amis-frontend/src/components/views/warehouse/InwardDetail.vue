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
            :class="{ 'ms-dropdown--readonly': isReadOnly }"
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
                      ref="customerCombobox"
                      :class="{ 'ms-combobox-custom--readonly': isReadOnly }"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input
                      label="Địa chỉ"
                      v-model="masterContent['contact_address']"
                      :class="{ 'ms-input--readonly': isReadOnly }"
                    />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input
                      label="Người giao hàng"
                      v-model="masterContent['contact_name']"
                      :class="{ 'ms-input--readonly': isReadOnly }"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input
                      label="Diễn giải"
                      v-model="descriptionVoucher"
                      :class="{ 'ms-input--readonly': isReadOnly }"
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
                      :class="{ 'ms-combobox-custom--readonly': isReadOnly }"
                    />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <div class="w-2/3 flex">
                      <base-input
                        label="Kèm theo"
                        class="voucher-attach"
                        placeholder="Số lượng"
                        v-model="masterContent['voucher_attach']"
                        :class="{ 'ms-input--readonly': isReadOnly }"
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
                    :class="{ 'ms-input--readonly': isReadOnly }"
                    :required="true"
                    formName="InwardDetail"
                    fieldName="Ngày hạch toán"
                    @getDate="getMathematicsDate"
                    inputName="MathematicsDate"
                    ref="mathematicsdate"
                  />
                </div>
                <div class="row-input-right">
                  <base-date-input
                    label="Ngày chứng từ"
                    type="date"
                    v-model="masterContent['voucher_date']"
                    :class="{ 'ms-input--readonly': isReadOnly }"
                    :required="true"
                    formName="InwardDetail"
                    fieldName="Ngày chứng từ"
                    @getDate="getVoucherDate"
                  />
                </div>
                <div class="row-input-right voucher-number">
                  <base-input
                    label="Số chứng từ"
                    v-model="masterContent['voucher_code']"
                    :class="{ 'ms-input--readonly': isReadOnly }"
                    :required="true"
                    :inputCheck="inputCheck"
                    @getInputError="getInputError"
                    fieldName="Số chứng từ"
                    formName="InwardDetail"
                    inputName="VoucherCode"
                    ref="vouchercode"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="w-1/4 summary-info">
            <div class="summary-info-title">Tổng tiền</div>
            <div class="summary-info-number">{{ totalPrice }}</div>
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
              :isReadOnly="isReadOnly"
              formName="InwardDetail"
            />
            <div class="inward-detail-pagination">
              <base-pagination
                :class="{ 'ms-pagination--readonly': isReadOnly }"
              />
            </div>
          </div>
        </div>
        <div class="grid-control-item">
          <div class="btn-grid-control" :class="{ 'read-only': isReadOnly }">
            <base-button value="Thêm dòng" @clickButton="addNewRow" />
            <base-button value="Thêm ghi chú" />
            <base-button
              value="Xóa hết dòng"
              @clickButton="warningDeleteAllRow"
            />
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
        <div class="white-space"></div>
      </div>
      <div class="inward-detail-footer">
        <div class="left-group-button">
          <div class="ms-button ms-button-secondary" @click="closeInwardDetail">
            Hủy
          </div>
        </div>
        <div class="right-group-button">
          <div
            class="ms-button ms-button-secondary button-add"
            @click="saveVoucherDetail"
            v-show="afterEdit"
          >
            Cất
          </div>
          <div class="base-button-custom" v-show="afterEdit">
            <div
              class="button-custom-left ms-button-primary"
              @click="saveAndInsert"
            >
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
          <div
            class="ms-button ms-button-primary"
            @click="unMention"
            v-show="afterSave"
          >
            Bỏ ghi
          </div>
          <div
            class="ms-button ms-button-secondary"
            @click="changeEditState"
            v-show="afterUnmention"
          >
            Sửa
          </div>
          <div
            class="ms-button ms-button-primary btn-mention"
            @click="mention"
            v-show="afterUnmention"
          >
            Ghi sổ
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
import { mode as MSMODE } from "../../../js/resources/resourcevn";
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
      mode: MSMODE.ADD,
      /**ẩn hiện form */
      isShowInwardDetail: false,
      /**Nội dung form */
      masterContent: {},
      /**đơn vị tính */
      units: [],
      isReadOnly: false,
      /**footer */
      afterSave: false,
      afterEdit: true,
      afterUnmention: false,
      //add
      voucherId: "",
      /**validate */
      inputCheck: false,
      errorList: [],
      isValid: true,
      formName: "InwardDetail",
      /**data clone */
      dataClone: {},
    };
  },
  methods: {
    /**
     * thay đổi nội dung ngày hạch toán
     * @param {string} content
     * CreatedBy: nvdien(11/10/2021)
     */
    getMathematicsDate(content) {
      this.masterContent["mathematics_date"] = content;
    },
    /**
     * thay đổi nội dung ngày chứng từ
     * @param {string} content
     * CreatedBy: nvdien(11/10/2021)
     */
    getVoucherDate(content) {
      this.masterContent["voucher_date"] = content;
    },
    /**
     *hiển thị option cất
     CreatedBy: nvdien(5/10/2021)
     */
    showAddOptions() {
      console.log("showAddOptions");
    },
    /**Đóng form popup
     * CreaedBy: nvdien(17/9/2021)
     */
    closeInwardDetail() {
      /**Kiểm tra dữ liệu thay đổi */
      var data = {
        in_inward: this.masterContent,
        in_inward_detail: this.tableInwardDetailContents,
      };
      // Kiểm tra đã thay đổi thì cảnh báo cất
      if (
        !this.deepEqualObject(data, this.dataClone, ["is_mention"]) &&
        this.afterEdit
      ) {
        this.$eventBus.$emit("showMessageBox", {
          icon: "mi-exclamation-question-48",
          messageText: this.$resourcesVN.message.messageConfirmChange,
          buttons: [
            {
              feature: "left ms-button-secondary",
              callback: () => {
                this.closeMessageBox();
              },
              value: "Hủy",
            },
            {
              feature: "right-first ms-button-primary",
              callback: () => {
                this.saveVoucherDetail(MSMODE.CLOSE);
              },
              value: "Có",
            },
            {
              feature: "right-second ms-button-secondary",
              callback: () => {
                this.closeMessageAndPopup();
              },
              value: "Không",
            },
          ],
        });
      } else {
        //set button
        this.hasSaveButton();
        this.isReadOnly = false;
        this.isShowInwardDetail = false;
        //reset form
        this.tableInwardDetailContents = [];
        this.masterContent = {};
      }
    },
    /**
     * Đóng form và messageBox
     * CreatedBy: nvdien(11/10/2021)
     */
    closeMessageAndPopup() {
      this.closeMessageBox();
      //set button
      this.hasSaveButton();
      this.isReadOnly = false;
      this.isShowInwardDetail = false;
      //reset form
      this.tableInwardDetailContents = [];
      this.masterContent = {};
    },
    /**Đóng message box
     * CreatedBy: nvdien(5/10/2021)
     */
    closeMessageBox() {
      this.$eventBus.$emit("hideMessageBox");
    },
    /**Mở form Thêm mới khách hàng
     * CreatedBy: nvdien(5/10/2021)
     */
    showCustomerDetail() {
      this.$eventBus.$emit("showCustomerDetail");
    },
    /**Mở form Thêm mới nhân viên
     * CreatedBy: nvdien(5/10/2021)
     */
    showEmployeeDetail() {
      this.$eventBus.$emit("showEmployeeDetail");
    },
    /**Thêm dòng table
     * createdBy:L nvdien(5/10/2021)
     */
    addNewRow() {
      let contentDetail = this.tableInwardDetailContents.filter((x) => {
        return x.state != MSMODE.DELETE;
      });
      let totalRow = contentDetail.length;
      if (totalRow > 0) {
        let newRowContent = Object.assign({}, contentDetail[totalRow - 1]);
        //set state dòng đó thành ADD
        newRowContent["state"] = MSMODE.ADD;
        newRowContent["quantity"] = 1;
        newRowContent["debit_amount"] = 0;
        newRowContent["total_price"] = 0;
        //Cập nhật giao diện
        this.tableInwardDetailContents.push({ ...newRowContent });
      } else {
        this.tableInwardDetailContents.push({
          ...this.$resourcesVN.inwardDetailContentsDefault,
        });
      }
    },
    /**xóa hết dòng
     * CreatedBy: nvdien(3/10/2021)
     */
    warningDeleteAllRow() {
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
              //đóng form cảnh báo
              this.closeMessageBox();
            },
            value: "Không",
          },
          {
            feature: "right-first ms-button-primary",
            callback: () => {
              this.deleteAllRow();
            },
            value: "Có",
          },
        ],
      });
    },
    /**Xóa tất cả các dòng
     * CreatedBy: nvdien(3/10/2021)
     */
    deleteAllRow() {
      let defaultRow = this.$resourcesVN.inwardDetailContentsDefault;
      if (this.mode == MSMODE.ADD) {
        this.tableInwardDetailContents = [{ ...defaultRow }];
      }
      if (this.mode == MSMODE.EDIT) {
        this.tableInwardDetailContents = this.tableInwardDetailContents.filter(
          (x) => x.state == 1
        );
        this.tableInwardDetailContents.map((x) => (x.state = 2));
        this.tableInwardDetailContents.push({ ...defaultRow });
      }
      this.closeMessageBox();
    },
    /**Xóa 1 dòng
     * @param index: index của dòng xóa
     * CreatedBy: nvdien(5/10/2021)
     */
    deleteRow(index) {
      if (this.tableInwardDetailContents[index]["state"] == MSMODE.EDIT) {
        //chuyển state nội dung sang DELETE
        this.tableInwardDetailContents[index]["state"] = MSMODE.DELETE;
      } else {
        this.tableInwardDetailContents.splice(index, 1);
      }
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
    /**bind lên combobox nhân viên
     * CreatedBy: nvdien(5/10/2021)
     */
    bindingEmployeeCombobox(content) {
      this.$set(this.masterContent, "employee_name", content["employee_name"]);
      this.$set(this.masterContent, "employee_id", content["employee_id"]);
    },
    /**gán lại nội dung table
     * @param index: index của dòng hiện tại trong băng
     * @param header: thông tin cột trong bảng
     * @param mode: loạt thông tin
     * @param content: nội dung thay đổi
     * CreatedBy: nvdien(5/20/2021)
     */
    changeVoucherDetail(index, header, mode, content) {
      switch (mode) {
        case "input": {
          let inputContent = content;
          //Giao diện
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            inputContent
          );
          break;
        }
        case "blur": {
          if (header.format == "number") content = Number(content);
          if (
            header.fieldName == "quantity" ||
            header.fieldName == "debit_amount"
          ) {
            let firstVal = content;
            let secondVal = 0;
            if (header.fieldName == "quantity")
              secondVal = this.tableInwardDetailContents[index]["debit_amount"];
            else {
              secondVal = this.tableInwardDetailContents[index]["quantity"];
            }
            let totalPrice = firstVal * secondVal;
            //Thay đổi thành tiền
            this.$set(
              this.tableInwardDetailContents[index],
              "total_price",
              totalPrice
            );
          }
          if (header.fieldName == "total_price") {
            let quantity = this.tableInwardDetailContents[index]["quantity"];
            let totalPrice = content;
            let debitAmount = (totalPrice * 1.0) / quantity;
            //Thay đổi thành tiền
            //Giao diện
            this.$set(
              this.tableInwardDetailContents[index],
              "debit_amount",
              debitAmount
            );
          }
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            content
          );
          break;
        }
        case "comboboxapi": {
          let newContent = content[header.fieldName];
          if (
            header.fieldName == "debit_account_number" ||
            header.fieldName == "credit_account_number"
          ) {
            newContent = content["account_number"];
            //thay đổi giá trị của chính nó
            //Giao diện
            this.$set(
              this.tableInwardDetailContents[index],
              header.fieldName,
              newContent
            );
            this.$set(
              this.tableInwardDetailContents[index],
              header.dataField,
              content["account_id"]
            );
            return;
          }
          if (header.fieldName == "commodity_code") {
            //nếu chọn mã hàng => bind lên các trường khác
            //hàng hóa
            //#region Giao diện
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
            //Tổng tiền
            let quantity = this.tableInwardDetailContents[index]["quantity"];
            let debitAmount =
              this.tableInwardDetailContents[index]["debit_amount"];
            this.$set(
              this.tableInwardDetailContents[index],
              "total_price",
              quantity * debitAmount
            );
            //Đơn vị tính
            this.$set(
              this.tableInwardDetailContents[index],
              "units",
              content["units"]
            );
            this.$set(
              this.tableInwardDetailContents[index],
              "unit_name",
              content["unit_name"]
            );
            this.$set(
              this.tableInwardDetailContents[index],
              "unit_id",
              content["unit_id"]
            );
            //#endregion
          }
          //thay đổi giá trị của chính nó
          //Giao diện
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            newContent
          );
          this.$set(
            this.tableInwardDetailContents[index],
            header.dataField,
            content[header.dataField]
          );

          break;
        }
        case "comboboxmanual": {
          if (header.fieldName == "unit_name") {
            this.$set(
              this.tableInwardDetailContents[index],
              header.fieldName,
              content[header.fieldName]
            );
            this.$set(
              this.tableInwardDetailContents[index],
              header.dataField,
              content[header.dataField]
            );
          }

          break;
        }
        case "date": {
          //Giao diện
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            content
          );
          break;
        }
      }
    },
    /**lấy lỗi emit lên từ các base
     * @param {string} error
     * CreatedBy: nvdien(3/10/2021)
     */
    getInputError(error) {
      // this.errorList.push(error);
      //show Message box
      this.$eventBus.$emit("showMessageBox", {
        icon: "mi-exclamation-error-48-2",
        messageText: error,
        buttons: [
          {
            feature: "center ms-button-primary",
            callback: () => {
              this.closeMessageBox();
            },
            value: "Đóng",
          },
        ],
      });
      this.isValid = false;
    },
    /**
     * Kiểm tra ngày hạch toán lớn hơn hoặc bằng ngày chứng từ
     * CreatedBy: nvdien(3/10/2021)
     */
    checkVoucherDate() {
      let mathematicDate = new Date(this.masterContent["mathematics_date"]);
      let voucherDate = new Date(this.masterContent["voucher_date"]);
      if (mathematicDate < voucherDate) {
        // this.$eventBus.$emit("validateInputMethematicsDate", this.$re);
        this.$eventBus.$emit("showMessageBox", {
          icon: "mi-exclamation-warning-48",
          messageText: this.$resourcesVN.message.messageVoucherDate,
          buttons: [
            {
              feature: "center ms-button-primary",
              callback: () => {
                this.$eventBus.$emit(
                  "showErrorDateMathematicsDate",
                  this.$resourcesVN.message.messageVoucherDateInput
                );
                this.$refs.mathematicsdate.focusInput();
                this.closeMessageBox();
              },
              value: "Đóng",
            },
          ],
        });
        return false;
      }
      return true;
    },
    /**
     * Kiểm tra có hàng tiền nào trên form không
     * CreatedBy: nvdien(11/10/2021)
     */
    checkRequiredDetail() {
      let tempDetail = this.tableInwardDetailContents.filter((element) => {
        return element.state != MSMODE.DELETE;
      });
      if (tempDetail.length < 1) {
        this.tableInwardDetailContents.push({
          ...this.$resourcesVN.inwardDetailContentsDefault,
        });
        let errorMessage =
          this.$resourcesVN.message.messagevoucherDetailRequired;
        //hiển thị cảnh báo
        this.$eventBus.$emit("showMessageBox", {
          icon: "mi-exclamation-warning-48",
          messageText: errorMessage,
          buttons: [
            {
              feature: "center ms-button-primary",
              callback: () => {
                this.closeMessageBox();
              },
              value: "Đóng",
            },
          ],
        });
        return false;
      }
      return true;
    },
    /**validate form
     *
     * CreatedBy: nvdien(3/10/2021)
     */
    validateForm() {
      let checkRequiredDetail = this.checkRequiredDetail();
      if (!checkRequiredDetail) return false;
      let checkArray = [
        this.masterContent["voucher_code"],
        this.masterContent["mathematics_date"],
        this.masterContent["voucher_date"],
      ];
      this.tableInwardDetailContents.forEach((element) => {
        checkArray.push(
          element["commodity_name"],
          element["commodity_id"],
          element["warehouse_code"],
          element["warehouse_id"],
          element["debit_account_number"],
          element["debit_account_id"],
          element["credit_account_number"],
          element["credit_account_id"]
        );
      });
      let checkRequired = this.checkRequiredField(checkArray);
      if (!checkRequired) {
        this.$eventBus.$emit("validateInputInwardDetail");
        this.$eventBus.$emit("validateComboboxInwardDetail");
        return false;
      }
      //Check ngày hạch toán lớn hơn hoặc bằng ngày chứng từ
      let checkDate = this.checkVoucherDate();
      if (!checkDate) return false;
      return true;
    },
    /**Cất dữ liệu
     * @return {Boolean}
     * CreatedBy: nvdien(3/10/2021)
     */
    saveVoucherDetail(extraPram) {
      let isValid = this.validateForm();
      if (isValid == false) {
        return;
      }
      //lấy dữ liệu diễn giải
      this.masterContent["description"] = this.descriptionVoucher;
      //Lấy loại phiếu nhập
      this.masterContent["voucher_type"] = this.dropdownInwardTypeData;
      //Trạng thái ghi sổ
      this.masterContent["is_mention"] = 1;
      //Thêm mới
      if (this.mode == MSMODE.ADD) {
        //data post
        let voucherDetailContent = {
          in_inward: this.masterContent,
          in_inward_detail: this.tableInwardDetailContents,
        };
        //thêm dữ liệu
        VoucherRepository.addVoucher(voucherDetailContent)
          .then((response) => {
            if (extraPram == MSMODE.CLOSE) this.closeMessageAndPopup();
            let content = response.data.Data;
            this.updateDataAfterSave(content);
          })
          .catch((error) => {
            let response = error.response.data;
            if (response["code"] == "MS001") {
              this.showVoucherCodeDuplication(response["message"]);
            }
          });
      }
      if (this.mode == MSMODE.EDIT) {
        //Gán các giá trị từ giao diện lên nội dung
        //put dữ liệu
        this.voucherId = this.masterContent["accountvoucher_id"];
        let inwardDetailContentLength = this.tableInwardDetailContents.length;
        for (let i = 0; i < inwardDetailContentLength; ++i) {
          let content = this.tableInwardDetailContents[i];
          this.$set(content, "accountvoucher_id", this.voucherId);
        }

        //data put
        let voucherDetailPutContent = {
          in_inward: this.masterContent,
          in_inward_detail: this.tableInwardDetailContents,
        };
        VoucherRepository.editInwardVoucher(
          this.voucherId,
          voucherDetailPutContent
        )
          .then((response) => {
            if (extraPram == MSMODE.CLOSE) this.closeMessageAndPopup();
            let content = response.data.Data;
            this.updateDataAfterSave(content);
          })
          .catch((error) => {
            let response = error.response.data;
            if (response["code"] == "MS001") {
              this.showVoucherCodeDuplication(response["message"]);
            }
          });
      }
    },
    /**
     * Gán lại các giá trị sau khi thêm và sửa
     * @param {object} content
     * CreatedBy: nvdien(11/10/2021)
     */
    updateDataAfterSave(content) {
      //chuyển sang btn Bỏ ghi
      this.hasUnmentionButton();
      this.$eventBus.$emit("loadVoucherTable");
      this.isReadOnly = true;

      this.masterContent = content["in_inward"];
      this.voucherId = this.masterContent["accountvoucher_id"];
      let detailContent = content["in_inward_detail"];
      //Mảng chứa dữ liệu sẵn sàng để sửa
      this.tableInwardDetailContents = [...detailContent];
      //sửa thì state = 1
      for (let i = 0; i < this.tableInwardDetailContents.length; ++i) {
        this.tableInwardDetailContents[i].state = MSMODE.EDIT;
      }
    },
    /**Hiển thị lỗi trùng mã chứng từ
     * @param {string} errorMessage string báo lỗi
     * CreatedBy: nvdien(11/10/2021)
     */
    showVoucherCodeDuplication(errorMessage) {
      //show message trùng mã
      this.$eventBus.$emit("showMessageBox", {
        icon: "mi-exclamation-warning-48",
        messageText: errorMessage,
        buttons: [
          {
            feature: "center ms-button-primary",
            callback: () => {
              this.$eventBus.$emit(
                "showErrorInputVoucherCode",
                this.$resourcesVN.message.messageVoucherCodeDuplication
              );
              this.$refs.vouchercode.focusInput();
              this.closeMessageBox();
            },
            value: "Đóng",
          },
        ],
      });
    },
    /**Cất và thêm
     * CreatedBY: nvdien(3/10/2021)
     */
    saveAndInsert() {
      let isValid = this.validateForm();
      if (isValid == false) {
        return false;
      }
      //lấy dữ liệu diễn giải
      this.masterContent["description"] = this.descriptionVoucher;
      //Lấy loại phiếu nhập
      this.masterContent["voucher_type"] = this.dropdownInwardTypeData;
      //Trạng thái ghi sổ
      this.masterContent["is_mention"] = 1;
      //Thêm mới
      if (this.mode == MSMODE.ADD) {
        //data post
        let voucherDetailContent = {
          in_inward: this.masterContent,
          in_inward_detail: this.tableInwardDetailContents,
        };
        //thêm dữ liệu
        VoucherRepository.addVoucher(voucherDetailContent)
          .then(() => {
            this.$eventBus.$emit("loadVoucherTable");
            VoucherRepository.getNewVoucherCode()
              .then((response) => {
                let newVoucherCode = response.data;
                let currentDate = moment().format("YYYY-MM-DD");
                this.masterContent = {
                  voucher_code: newVoucherCode,
                  mathematics_date: currentDate,
                  voucher_date: currentDate,
                  is_mention: 1,
                };
                //set default
                this.tableInwardDetailContents = [
                  { ...this.$resourcesVN.inwardDetailContentsDefault },
                ];
                //set mode
                this.mode = MSMODE.ADD;
                //clone dữ liệu để khi đóng form kiểm tra xem form có thay đổi dữ liệu không
                this.cloneData();
                //focus vào ô đầu tiên của form
                this.$refs.customerCombobox.focusInput();
                //show thông báo thêm mới thành công
                let toastMessageText =
                  this.$resourcesVN.message.messageSaveSuccess;
                this.$eventBus.$emit("showToastMessage", {
                  icon: "mi-notifications–success",
                  text: toastMessageText,
                });
              })
              .catch((response) => console.log(response));
          })
          .catch((error) => {
            let response = error.response.data;
            console.log(response);
            if (response["code"] == "MS001") {
              //show message trùng mã
              this.$eventBus.$emit("showMessageBox", {
                icon: "mi-exclamation-warning-48",
                messageText: response["message"],
                buttons: [
                  {
                    feature: "center ms-button-primary",
                    callback: () => {
                      this.closeMessageBox();
                    },
                    value: "Đóng",
                  },
                ],
              });
            }
          });
      }
      if (this.mode == MSMODE.EDIT) {
        //Gán các giá trị từ giao diện lên nội dung
        //put dữ liệu
        this.voucherId = this.masterContent["accountvoucher_id"];
        let inwardDetailContentLength = this.tableInwardDetailContents.length;
        for (let i = 0; i < inwardDetailContentLength; ++i) {
          this.$set(
            this.tableInwardDetailContents[i],
            "accountvoucher_id",
            this.voucherId
          );
        }

        //data put
        let voucherDetailPutContent = {
          in_inward: this.masterContent,
          in_inward_detail: this.tableInwardDetailContents,
        };
        VoucherRepository.editInwardVoucher(
          this.voucherId,
          voucherDetailPutContent
        )
          .then(() => {
            this.$eventBus.$emit("loadVoucherTable");
            VoucherRepository.getNewVoucherCode()
              .then((response) => {
                let newVoucherCode = response.data;
                let currentDate = moment().format("YYYY-MM-DD");
                this.masterContent = {
                  voucher_code: newVoucherCode,
                  mathematics_date: currentDate,
                  voucher_date: currentDate,
                  is_mention: 1,
                };
                //set default
                this.tableInwardDetailContents = [
                  { ...this.$resourcesVN.inwardDetailContentsDefault },
                ];

                //set mode
                this.mode = MSMODE.ADD;
                //clone dữ liệu để khi đóng form kiểm tra xem form có thay đổi dữ liệu không
                this.cloneData();
                //focus vào ô đầu tiên của form
                this.$refs.customerCombobox.focusInput();
                //Show thông báo cất dữ liệu thành công
                let toastMessageText =
                  this.$resourcesVN.message.messageSaveSuccess;
                this.$eventBus.$emit("showToastMessage", {
                  icon: "mi-notifications–success",
                  text: toastMessageText,
                });
              })
              .catch((response) => console.log(response));
          })
          .catch((error) => {
            let response = error.response.data;
            console.log(response);
            if (response["code"] == "MS001") {
              //show message trùng mã
              this.$eventBus.$emit("showMessageBox", {
                icon: "mi-exclamation-warning-48",
                messageText: response["message"],
                buttons: [
                  {
                    feature: "center ms-button-primary",
                    callback: () => {
                      this.closeMessageBox();
                    },
                    value: "Đóng",
                  },
                ],
              });
            }
          });
      }
    },
    /**Bỏ ghi
     * CreatedBy: nvdien(5/10/2021)
     */
    unMention() {
      this.$set(this.masterContent, "is_mention", 0);
      VoucherRepository.put(this.voucherId, this.masterContent)
        .then(() => {
          //chuyển sang btn Bỏ ghi
          this.hasMentionButton();
          this.$eventBus.$emit("loadVoucherTable");
        })
        .catch((response) => console.log(response));
    },
    /**Ghi sổ
     * CreatedBy:nvdien(5/10/2021)
     */
    mention() {
      this.$set(this.masterContent, "is_mention", 1);
      VoucherRepository.put(this.voucherId, this.masterContent)
        .then(() => {
          this.hasUnmentionButton();
          this.$eventBus.$emit("loadVoucherTable");
        })
        .catch((response) => console.log(response));
    },
    /**hanle khi ấn nút sửa
     * CreatedBy nvdien(3/10/2012)
     */
    changeEditState() {
      //focus vào ô đầu tiên của form
      this.$refs.customerCombobox.focusInput();
      this.mode = MSMODE.EDIT;
      this.isReadOnly = false;
      this.hasSaveButton();
    },
    /**chuyển footer sửa
     * CreatedBY: nvdien(3/10/2021)
     */
    hasSaveButton() {
      this.afterSave = false;
      this.afterUnmention = false;
      this.afterEdit = true;
    },
    /**chuyển footer bỏ ghi
     * CreatedBy: nvdien(5/10/2021)
     */
    hasUnmentionButton() {
      this.afterSave = true;
      this.afterUnmention = false;
      this.afterEdit = false;
    },
    /*chuyển footer ghi sổ 
    CreatedBy: nvdien(5/10/2021)
    */
    hasMentionButton() {
      this.afterSave = false;
      this.afterUnmention = true;
      this.afterEdit = false;
    },
    /**
     * Clone dữ liệu hiện tại
     * CreatedBy: nvdien (02/10/2021)
     */
    cloneData() {
      this.dataClone = {
        in_inward: { ...this.masterContent },
        in_inward_detail: [],
      };
      this.tableInwardDetailContents.forEach((item, index) => {
        this.dataClone.in_inward_detail[index] = { ...item };
      });
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
    totalPrice: function () {
      let total = 0;
      let currentContents = this.tableInwardDetailContents.filter((x) => {
        return x.state != MSMODE.DELETE;
      });
      total = currentContents.reduce((prevValue, currentElement) => {
        return prevValue + currentElement["total_price"];
      }, 0);
      this.$set(this.masterContent, "total_price", Number(total).toFixed(1));
      total = this.formatMoney(total);
      return total;
    },
  },
  created() {
    /**
     * Hiển thị form chi tiết nhập kho và lấy dữ liệu
     * CreatedBy: nvdien(5/10/2021)
     */
    this.$eventBus.$on("showInwardDetail", (mode, content) => {
      if (mode == MSMODE.ADD) {
        let currentDate = moment().format("YYYY-MM-DD");
        this.masterContent = {
          voucher_code: content,
          mathematics_date: currentDate,
          voucher_date: currentDate,
        };
        //set default
        this.tableInwardDetailContents = [];
        this.$set(
          this.tableInwardDetailContents,
          0,
          Object.assign({}, this.$resourcesVN.inwardDetailContentsDefault)
        );
        //set mode
        this.mode = mode;
        //clone dữ liệu để khi đóng form kiểm tra xem form có thay đổi dữ liệu không
        this.cloneData();
        //focus vào ô đầu tiên của form
        this.$refs.customerCombobox.focusInput();
        this.isShowInwardDetail = true;
      }
      if (mode == MSMODE.EDIT) {
        this.masterContent = content["in_inward"][0];
        this.voucherId = this.masterContent["accountvoucher_id"];
        let detailContent = content["in_inward_detail"];
        this.tableInwardDetailContents = [...detailContent];
        //sửa thì state = 1
        for (let i = 0; i < this.tableInwardDetailContents.length; ++i) {
          this.tableInwardDetailContents[i].state = MSMODE.EDIT;
        }
        this.mode = mode;
        //clone dữ liệu để khi đóng form kiểm tra xem form có thay đổi dữ liệu không
        this.cloneData();
        //Check trạng thái ghi sổ
        if (this.masterContent["is_mention"]) {
          //đã ghi sổ
          this.hasUnmentionButton();
        } else {
          //chưa ghi sổ
          this.hasMentionButton();
        }
        this.isReadOnly = true;
        this.isShowInwardDetail = true;
      }
      if (mode == MSMODE.DUPLICATE) {
        //mode set bằng add
        this.mode = MSMODE.ADD;
        //Lấy nội dung
        this.masterContent = content["in_inward"][0];
        let detailContent = content["in_inward_detail"];
        this.tableInwardDetailContents = [...detailContent];
        //Lấy mã mới
        VoucherRepository.getNewVoucherCode()
          .then((response) => {
            let newVoucherCode = response.data;
            this.$set(this.masterContent, "voucher_code", newVoucherCode);
            this.cloneData();
            //focus vào ô đầu tiên của form
            this.$refs.customerCombobox.focusInput();
            this.isShowInwardDetail = true;
          })
          .catch((response) => {
            console.log(response);
            //focus vào ô đầu tiên của form
            this.cloneData();
            this.$refs.customerCombobox.focusInput();
            this.isShowInwardDetail = true;
          });
      }
    });
    /**
     * Bind dữ liệu sau khi thêm mới form khách hàng
     * createdBy: nvdien(5/10/2021)
     */
    this.$eventBus.$on("getCustomerDetail", (content) => {
      console.log(content);
      this.$set(
        this.masterContent,
        "accountobject_id",
        content["accountobject_id"]
      );
      this.$set(
        this.masterContent,
        "account_object_name",
        content["account_object_name"]
      );
      this.$set(
        this.masterContent,
        "contact_address",
        content["contact_address"]
      );
      this.$set(this.masterContent, "employee_name", content["employee_name"]);
    });
    /**
     * Bind dữ liệu sau khi thêm mới form nhân viên
     * createdBy: nvdien(5/10/2021)
     */
    this.$eventBus.$on("getEmployeeDetail", (content) => {
      this.$set(this.masterContent, "employee_name", content["employee_name"]);
    });
    /**
     * Bind dữ liệu sau khi thêm mới hàng hóa
     * CreatedBy: nvdien(6/10/2021)
     */
    this.$eventBus.$on("getCommodityDetail", (content, rowTable) => {
      //set mã hàng
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "commodity_id",
        content["commodity_id"]
      );
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "commodity_code",
        content["commodity_code"]
      );
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "commodity_name",
        content["commodity_name"]
      );
      //set kho
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "warehouse_code",
        content["warehouse_code"]
      );
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "warehouse_id",
        content["warehouse_id"]
      );
      //set tài khoản nợ
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "debit_account_id",
        content["debit_account"]
      );
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "debit_account_number",
        content["debit_account_number"]
      );
      //set tài khoản có
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "credit_account_id",
        content["credit_account"]
      );
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "credit_account_number",
        content["credit_account_number"]
      );
      //Set đơn vị tính chính
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "unit_name",
        content["unit_name"]
      );
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "unit_id",
        content["unit_id"]
      );
      //set đơn vị tính
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "units",
        content["units"]
      );
      //Đơm giá
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "debit_amount",
        content["debit_amount"]
      );
      //Thành tiền
      let quantity = this.tableInwardDetailContents[rowTable]["quantity"];
      let debitAmount =
        this.tableInwardDetailContents[rowTable]["debit_amount"];
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "total_price",
        quantity * debitAmount
      );
    });
    /**
     * Handle lỗi bắt được từ các input và combobox
     * CreatedBy; nvdien(5/10/2021)
     */
    this.$eventBus.$on("catchErrorInwardDetail", (content, element) => {
      if (content != null && content != "") {
        //show message box
        this.$eventBus.$emit("showMessageBox", {
          icon: "mi-exclamation-error-48-2",
          messageText: content,
          buttons: [
            {
              feature: "center ms-button-primary",
              callback: () => {
                this.closeMessageBox();
                element.focus();
              },
              value: "Đóng",
            },
          ],
        });

        this.isValid = false;
      } else {
        this.isValid = true;
      }
    });
    /**
     * Lấy thông tin sau khi thêm kho
     * CreatedBY: nvdien(9/10/2021)
     */
    this.$eventBus.$on("getWarehouseAdd", (content, rowTable) => {
      //set kho
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "warehouse_code",
        content["warehouse_code"]
      );
      this.$set(
        this.tableInwardDetailContents[rowTable],
        "warehouse_id",
        content["warehouse_id"]
      );
    });
  },
  destroyed() {
    this.$eventBus.$off("showInwardDetail");
    this.$eventBus.$off("getEmployeeDetail");
    this.$eventBus.$off("getCustomerDetail");
    this.$eventBus.$off("getWarehouseAdd");
    this.$eventBus.$off("catchErrorInwardDetail");
  },
};
</script>

<style scoped>
@import url("../../../css/views/warehouse/inwarddetail.css");
</style>