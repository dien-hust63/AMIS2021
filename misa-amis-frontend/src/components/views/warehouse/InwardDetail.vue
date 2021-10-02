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
                  />
                </div>
                <div class="row-input-right">
                  <base-date-input
                    label="Ngày chứng từ"
                    type="date"
                    v-model="masterContent['voucher_date']"
                    :class="{ 'ms-input--readonly': isReadOnly }"
                  />
                </div>
                <div class="row-input-right voucher-number">
                  <base-input
                    label="Số chứng từ"
                    v-model="masterContent['voucher_code']"
                    :class="{ 'ms-input--readonly': isReadOnly }"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="w-1/4 summary-info">
            <div class="summary-info-title">Tổng tiền</div>
            <div class="summary-info-number">{{totalPrice}}</div>
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
            />
            <div class="inward-detail-pagination">
              <base-pagination />
            </div>
          </div>
        </div>
        <div class="grid-control-item">
          <div class="btn-grid-control" :class="{'read-only':isReadOnly}">
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
          <base-button value="Hủy" class="ms-button-secondary" />
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
      isReadOnly: false,
      /**footer */
      afterSave: false,
      afterEdit: true,
      afterUnmention: false,
      //add
      voucherId: "",
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
      //set button
      this.afterSave = false;
      this.afterEdit = true;
      this.afterUnmention = false;
      this.isReadOnly = false;
      this.isShowInwardDetail = false;
      //reset form
      this.voucherDetailContents = [];
      this.tableInwardDetailContents = [];
      this.masterContent = [];
    },
    /**Đóng message box */
    closeMessageBox() {
      this.$eventBus.$emit("hideMessageBox");
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
        let newRowContent = Object.assign(
          {},
          this.tableInwardDetailContents[totalRow - 1]
        );
        //set state dòng đó thành ADD
        newRowContent["state"] = this.$resourcesVN.mode.ADD;
        //Cập nhật giao diện
        this.$set(this.tableInwardDetailContents, totalRow, newRowContent);
      } else {
        this.$set(
          this.tableInwardDetailContents,
          0,
          Object.assign({}, this.$resourcesVN.inwardDetailContentsDefault)
        );
      }
    },
    /**xóa hết dòng */
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
    /**Xóa tất cả các dòng */
    deleteAllRow() {
      //Xóa trên giao diện, set lại mảng default
      this.tableInwardDetailContents = [];
      this.$set(
        this.tableInwardDetailContents,
        0,
        Object.assign({}, this.$resourcesVN.inwardDetailContentsDefault)
      );
      //Set trạng thái xóa trên các hàng của nội dung
      let lengthContent = this.voucherDetailContents.length;
      for (let i = 0; i < lengthContent; ++i) {
        this.voucherDetailContents[i]["state"] = this.$resourcesVN.mode.DELETE;
      }
      this.closeMessageBox();
    },
    /**Xóa 1 dòng */
    deleteRow(index) {
      let rowContent = this.tableInwardDetailContents[index];
      if (rowContent["state"] == this.$resourcesVN.mode.EDIT) {
        //chuyển state nội dung sang DELETE
        this.voucherDetailContents[index]["state"] =
          this.$resourcesVN.mode.DELETE;
      }
      //Xóa dòng đó trên giao diện
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
          let inputContent = content;
          //thay đổi giá trị của chính nó
          if (header.format == "number") {
            inputContent = this.formatSalary(content);
          }
          //Giao diện
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            inputContent
          );
          //Nội dung
          //Nếu state = 1 thì ms cập nhật nội dung
          if (
            this.tableInwardDetailContents[index].state ==
              this.$resourcesVN.mode.EDIT &&
            this.mode == this.$resourcesVN.mode.EDIT
          ) {
            this.$set(
              this.voucherDetailContents[index],
              header.fieldName,
              content
            );
          }

          break;
        }
        case "blur": {
          if (
            header.fieldName == "quantity" ||
            header.fieldName == "debit_amount"
          ) {
            let quantity = this.covertStringtoNumber(content["quantity"]);
            let debitMount = this.covertStringtoNumber(content["debit_amount"]);
            let totalPrice = quantity * debitMount;
            //Thay đổi thành tiền
            this.$set(
              this.tableInwardDetailContents[index],
              "total_price",
              this.formatSalary(totalPrice)
            );
            //Nội dung
            //Nếu state = 1 thì ms cập nhật nội dung
            if (
              this.tableInwardDetailContents[index].state ==
                this.$resourcesVN.mode.EDIT &&
              this.mode == this.$resourcesVN.mode.EDIT
            ) {
              this.$set(
                this.voucherDetailContents[index],
                "total_price",
                totalPrice
              );
            }
          }
          if (header.fieldName == "total_price") {
            let quantity = this.covertStringtoNumber(content["quantity"]);
            let totalPrice = this.covertStringtoNumber(content["total_price"]);
            let debitAmount = (totalPrice * 1.0) / quantity;
            //Thay đổi thành tiền
            //Giao diện
            this.$set(
              this.tableInwardDetailContents[index],
              "debit_amount",
              this.formatSalary(debitAmount)
            );
            //Nội dung
            //Nếu state = 1 thì ms cập nhật nội dung
            if (
              this.tableInwardDetailContents[index].state ==
                this.$resourcesVN.mode.EDIT &&
              this.mode == this.$resourcesVN.mode.EDIT
            ) {
              this.$set(
                this.voucherDetailContents[index],
                "debit_amount",
                debitAmount
              );
            }
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
            //Nội dung
            //cập nhật ID của chính combobox đó
            //Nếu state = 1 thì ms cập nhật nội dung
            if (
              this.tableInwardDetailContents[index].state ==
                this.$resourcesVN.mode.EDIT &&
              this.mode == this.$resourcesVN.mode.EDIT
            ) {
              this.$set(
                this.voucherDetailContents[index],
                header.dataField,
                content["account_id"]
              );
            }
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
            //#endregion
            /**Nội dung */
            //#region Nội dung
            //Nếu state giao diện = EDIT thì ms cập nhật nội dung
            if (
              this.tableInwardDetailContents[index].state ==
                this.$resourcesVN.mode.EDIT &&
              this.mode == this.$resourcesVN.mode.EDIT
            ) {
              let newChangeContent = Object.assign(
                {},
                this.tableInwardDetailContents[index]
              );
              newChangeContent["state"] = 1;
              this.$set(this.voucherDetailContents, index, newChangeContent);
            }
            //#endregion
          }
          //thay đổi giá trị của chính nó
          //Giao diện
          this.$set(
            this.tableInwardDetailContents[index],
            header.fieldName,
            newContent
          );
          //Nội dung
          //cập nhật ID của chính combobox đó
          //Nếu state = 1 thì ms cập nhật nội dung
          if (
            this.tableInwardDetailContents[index].state ==
              this.$resourcesVN.mode.EDIT &&
            this.mode == this.$resourcesVN.mode.EDIT
          ) {
            this.$set(
              this.voucherDetailContents[index],
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
          //Nội dung
          //Nếu state = 1 thì ms cập nhật nội dung
          if (
            this.tableInwardDetailContents[index].state ==
              this.$resourcesVN.mode.EDIT &&
            this.mode == this.$resourcesVN.mode.EDIT
          ) {
            this.$set(
              this.voucherDetailContents[index],
              header.fieldName,
              content
            );
          }
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
            //chuyển sang btn Bỏ ghi
            this.hasUnmentionButton();
            this.$eventBus.$emit("loadVoucherTable");
            this.isReadOnly = true;
            console.log(response);
          })
          .catch((response) => console.log(response));
      }
      if (this.mode == this.$resourcesVN.mode.EDIT) {
        //Gán các giá trị từ giao diện lên nội dung
        let inwardDetailContentLength = this.tableInwardDetailContents.length;
        let currentContentLength = this.voucherDetailContents.length;
        let countAddIndex = 0;
        for (let i = 0; i < inwardDetailContentLength; ++i) {
          let content = this.tableInwardDetailContents[i];
          if (content["state"] == this.$resourcesVN.mode.ADD) {
            this.$set(
              this.voucherDetailContents,
              currentContentLength + countAddIndex,
              content
            );
            countAddIndex++;
          }
        }
        //put dữ liệu
        let voucherId = this.masterContent["accountvoucher_id"];
        //data put
        let voucherDetailPutContent = {
          in_inward: this.masterContent,
          in_inward_detail: this.voucherDetailContents,
        };
        VoucherRepository.editInwardVoucher(voucherId, voucherDetailPutContent)
          .then((response) => {
            //chuyển sang btn Bỏ ghi
            this.hasUnmentionButton();
            this.$eventBus.$emit("loadVoucherTable");
            this.isReadOnly = true;
            console.log(response);
          })
          .catch((response) => console.log(response));
      }
    },
    /**Bỏ ghi */
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
    /**Ghi */
    mention() {
      this.$set(this.masterContent, "is_mention", 1);
      VoucherRepository.put(this.voucherId, this.masterContent)
        .then(() => {
          this.hasUnmentionButton();
          this.$eventBus.$emit("loadVoucherTable");
        })
        .catch((response) => console.log(response));
    },
    /**sửa */
    changeEditState() {
      this.mode = this.$resourcesVN.mode.EDIT;
      this.isReadOnly = false;
      this.hasSaveButton();
    },
    /**chuyển footer sửa */
    hasSaveButton() {
      this.afterSave = false;
      this.afterUnmention = false;
      this.afterEdit = true;
    },
    /**chuyển footer bỏ ghi */
    hasUnmentionButton() {
      this.afterSave = true;
      this.afterUnmention = false;
      this.afterEdit = false;
    },
    /*chuyển footer ghi sổ */
    hasMentionButton() {
      this.afterSave = false;
      this.afterUnmention = true;
      this.afterEdit = false;
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
    totalPrice: function(){
      let total = 0;
      for(let i=0; i<this.tableInwardDetailContents.length; ++i){
        let price;
        if(this.tableInwardDetailContents[i]['total_price'] != null){
           price = this.covertStringtoNumber(this.tableInwardDetailContents[i]["total_price"]);
        }
        else price = 0;
        total += price;
      }
      total = this.formatSalary(total);
      return total;
    }
  },
  created() {
    this.$eventBus.$on("showInwardDetail", (mode, content) => {
      if (mode == this.$resourcesVN.mode.ADD) {
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
        this.mode = mode;
        //focus vào ô đầu tiên của form
        this.$refs.customerCombobox.focusInput();
        this.isShowInwardDetail = true;
      }
      if (mode == this.$resourcesVN.mode.EDIT) {
        this.masterContent = content["in_inward"][0];
        this.voucherId = this.masterContent["accountvoucher_id"];
        let detailContent = content["in_inward_detail"];
        //Mảng chứa dữ liệu sẵn sàng để sửa
        this.tableInwardDetailContents = detailContent;
        //sửa thì state = 1
        for (let i = 0; i < detailContent.length; ++i) {
          detailContent[i].state = this.$resourcesVN.mode.EDIT;
          //nội
          this.voucherDetailContents[i] = Object.assign({}, detailContent[i]);
        }
        this.mode = mode;
        //Check trạng thái ghi sổ
        if (this.masterContent["is_mention"]) {
          //đã ghi sổ
          this.afterUnmention = false;
          this.afterEdit = false;
          this.afterSave = true;
        } else {
          //chưa ghi sổ
          this.afterUnmention = true;
          this.afterEdit = false;
          this.afterSave = false;
        }
        this.isReadOnly = true;
        //focus vào ô đầu tiên của form
        this.$refs.customerCombobox.focusInput();
        this.isShowInwardDetail = true;
      }
      if (mode == this.$resourcesVN.mode.DUPLICATE) {
        //mode set bằng add
        this.mode = this.$resourcesVN.mode.ADD;
        //Lấy nội dung
        this.masterContent = content["in_inward"][0];
        let detailContent = content["in_inward_detail"];
        this.tableInwardDetailContents = detailContent;
        //Lấy mã mới
        VoucherRepository.getNewVoucherCode()
          .then((response) => {
            let newVoucherCode = response.data;
            this.$set(this.masterContent, "voucher_code", newVoucherCode);
            this.isShowInwardDetail = true;
          })
          .catch((response) => {
            console.log(response);
            //focus vào ô đầu tiên của form
            this.$refs.customerCombobox.focusInput();
            this.isShowInwardDetail = true;
          });
      }
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