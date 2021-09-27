<template>
  <div class="employee-popup-wrap" :class="{'employee-popup--show':isShowEmployeeDetail}">
    <div class="employee-popup">
      <div class="employee-popup__header">
        <div class="employee-popup-title">
          <div class="popup-title">Thông tin nhân viên</div>
          <base-checkbox label="Là khách hàng" />
          <base-checkbox label="Là nhà cung cấp" />
        </div>
        <div class="employee-popup-close">
          <div class="mi mi-24 mi-help popup-help-icon"></div>
          <div
            class="mi mi-24 mi-close popup-close-icon"
            @click="closePopup"
          ></div>
        </div>
      </div>
      <div class="employee-popup__content">
        <div class="popup-content">
          <div class="row-input">
            <div class="row-input--left w-1/2">
              <div class="w-2/5 p-r-6 border-box">
                <base-input
                  label="Mã"
                  :required="true"
                  v-model="employeeData['EmployeeCode']"
                  ref="employeeCodeInput"
                  @getInputError="getInputError"
                  :inputCheck="inputCheck"
                  :inputReset="inputReset"
                  :inputErrorCustom="inputErrorCustom"
                />
              </div>
              <div class="w-3/5">
                <base-input
                  label="Tên"
                  :required="true"
                  v-model="employeeData['FullName']"
                  @getInputError="getInputError"
                  :inputCheck="inputCheck"
                  :inputReset="inputReset"
                />
              </div>
            </div>
            <div class="row-input--right w-1/2">
              <div class="w-2/5 p-r-6 border-box">
                <base-date-input
                  label="Ngày sinh"
                  type="date"
                  v-model="employeeData['DateOfBirth']"
                  @getDate="getDateOfBirth"
                  :inputReset="inputReset"
                />
              </div>
              <div class="w-3/5">
                <base-radio
                  label="Giới tính"
                  :options="genderRadioData"
                  @chooseRadio="chooseGenderRadio"
                  :value="employeeData['Gender']"
                  :radioReset="inputReset"
                />
              </div>
            </div>
          </div>
          <div class="row-input">
            <div class="row-input--left w-1/2">
              <base-combobox
                label="Đơn vị"
                :required="true"
                @getComboboxData="getComboboxData"
                :comboboxDataProp="employeeData['DepartmentId']"
                :comboboxCheck="inputCheck"
                :comboboxReset="inputReset"
                @getComboboxError="getInputError"
              />
            </div>
            <div class="row-input--right w-1/2">
              <div class="w-3/5">
                <base-input
                  label="Số CMND"
                  v-model="employeeData['IdentityNumber']"
                />
              </div>
              <div class="w-2/5 p-l-6 border-box">
                <base-date-input
                  label="Ngày cấp"
                  type="date"
                  v-model="employeeData['IdentityDate']"
                  @getDate="getIdentityDate"
                  :inputReset="inputReset"
                />
              </div>
            </div>
          </div>
          <div class="row-input row-input-third">
            <div class="row-input--left w-1/2">
              <base-input
                label="Chức danh"
                v-model="employeeData['PositionName']"
              />
            </div>
            <div class="row-input--right w-1/2">
              <base-input
                label="Nơi cấp"
                v-model="employeeData['IdentityPlace']"
              />
            </div>
          </div>
          <div class="row-input">
            <base-input label="Địa chỉ" v-model="employeeData['Address']" />
          </div>
          <div class="row-input">
            <div class="w-200 p-r-6">
              <base-input
                label="ĐT di động"
                v-model="employeeData['PhoneNumber']"
              />
            </div>
            <div class="w-200 p-r-6">
              <base-input
                label="ĐT cố định"
                v-model="employeeData['FixPhoneNumber']"
              />
            </div>
            <div class="w-200">
              <base-input
                label="Email"
                v-model="employeeData['Email']"
                type="email"
                :inputCheck="inputCheck"
                :inputReset="inputReset"
              />
            </div>
          </div>
          <div class="row-input">
            <div class="w-200 p-r-6">
              <base-input
                label="Tài khoản ngân hàng"
                v-model="employeeData['BankAccount']"
              />
            </div>
            <div class="w-200 p-r-6">
              <base-input
                label="Tên ngân hàng"
                v-model="employeeData['BankName']"
              />
            </div>
            <div class="w-200">
              <base-input
                label="Chi nhánh"
                v-model="employeeData['BankBranch']"
              />
            </div>
          </div>
        </div>
        <div class="popup-footer">
          <div class="popup-footer-divide"></div>
          <div class="popup-footer-content">
            <div class="popup-footer--right">
              <div class="ms-button ms-button-secondary" @click="closePopup">
                Hủy
              </div>
            </div>
            <div class="popup-footer--left">
              <div
                class="ms-button ms-button-secondary first-right-button"
                @click="saveData"
              >
                Cất
              </div>
              <div class="ms-button ms-button-primary" @click="saveAndInsert">
                Cất và thêm
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <base-message
      :messageText="messageText"
      :icon="iconMessage"
      :class="{ 'ms-message--show': isShowMessage }"
      :buttons="buttons"
    />
  </div>
</template>

<script>
import BaseDateInput from "../../base/BaseDateInput.vue";
import BaseInput from "../../base/BaseInput.vue";
import BaseCheckbox from "../../base/BaseCheckbox.vue";
import BaseMessage from "../../base/BaseMessage.vue";
import BaseCombobox from "../../base/BaseCombobox.vue";
import BaseRadio from "../../base/BaseRadio.vue";
import Mixin from "../../../mixins/methods.js";
import { RepositoryFactory } from "../../../js/repository/repository.factory.js";
import { mode, message } from "../../../js/resources/resourcevn";

const EmployeesRepository = RepositoryFactory.get("employees");

export default {
  name: "EmployeePopup",
  mixins: [Mixin],
  components: {
    BaseInput,
    BaseCheckbox,
    BaseMessage,
    BaseCombobox,
    BaseRadio,
    BaseDateInput,
  },
  props: {
    employeeInfo: {
      type: Object,
      default() {
        return {};
      },
    },
    isFocusCode: {
      type: Boolean,
      default: false,
    },
    mode: {
      type: Number,
      default: -1,
    },
  },
  data() {
    return {
      employeeData: {},
      messageText: message.messageConfirmChange,
      isShowMessage: false,
      isChangeData: false,
      isResetData: false,
      isForceDataNotChange: false,
      iconMessage: "mi-exclamation-question-48",
      buttons: [],
      genderRadioData: [
        { data: 1, value: "Nam" },
        { data: 0, value: "Nữ" },
        { data: 2, value: "Khác" },
      ],
      errorList: [],
      inputCheck: false,
      inputReset: false,
      inputErrorCustom: "",
      /**show form */
      isShowEmployeeDetail: false
    };
  },
  created() {
    this.$eventBus.$on("showEmployeeDetail", () => {
      this.isShowEmployeeDetail = true;
    });
  },
  destroyed() {
    this.$eventBus.$off("showEmployeeDetail");
  },
  methods: {
    /**
     * Đóng message box
     * CreatedBy: nvdien(30/8/2021)
     */
    closeMessageBox() {
      //Clear danh sách lỗi
      this.errorList = [];
      this.isShowMessage = false;
    },
    /**
     * Kiểm tra thay đổi dữ liệu và Đóng popup nhân viên
     * CreatedBy: nvdien(30/8/2021)
     */
    closePopupAndCheck() {
      //Kiểm tra có thay đổi dữ liệu không
      if (this.isChangeData) {
        //Hiệm message cảnh báo
        this.messageText = message.messageConfirmChange;
        this.iconMessage = "mi-exclamation-question-48";
        this.buttons = [
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
              this.saveData();
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
        ];
        this.isShowMessage = true;
      } else {
        this.closePopup();
      }
    },
    /**
     * Hủy dữ liệu và đóng form
     * CreatedBy: nvdien(3/9/2021)
     */
    closePopup() {
      //Reset dữ liệu popup nhân viên
      // this.isResetData = true;
      // this.employeeData = Object.assign({}, {});
      // //Reset các input
      // this.inputReset = true;
      // //Clear danh sách lỗi
      // this.errorList = [];
      // //Đóng popup nhân viên
      // this.$emit("closePopup");
      this.isShowEmployeeDetail = false;
    },
    /**
     * Đóng đồng thời message box và popup
     * CreatedBy: nvdien(1/9/2021)
     */
    closeMessageAndPopup() {
      this.isShowMessage = false;
      //Reset dữ liệu popup nhân viên
      this.isResetData = true;
      this.employeeData = Object.assign({}, {});
      //Reset lại các input
      this.inputReset = true;
      //Clear danh sách lỗi
      this.errorList = [];
      //Đóng popup nhân viên
      this.$emit("closePopup");
    },
    /**
     * Lưu dữ liệu
     * CreatedBy: nvdien(1/9/2021)
     */
    async saveData() {
      try {
        //Validate các trường
        let isValid = await this.validateBeforeSave();
        if (isValid == false) {
          return;
        }
        if (this.mode == mode.ADD) {
          //Thực hiện thêm mới
          if (this.employeeData["Gender"] == null) {
            //mặc định ngươi dùng không thay đổi gì thì giới tính sẽ là nam
            this.$set(this.employeeData, "Gender", 1);
          }
          await EmployeesRepository.post(this.employeeData);
        }
        if (this.mode == mode.INSERT) {
          //Thực hiện sửa thông tin
          await EmployeesRepository.put(
            this.employeeData["EmployeeId"],
            this.employeeData
          );
        }
        this.closeMessageAndPopup();
        this.$emit("loadTable");
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Cất và thêm mới
     * CreatedBy: nvdien(1/9/2021)
     */
    async saveAndInsert() {
      try {
        //Validate các trường
        let isValid = await this.validateBeforeSave();
        if (isValid == false) {
          return;
        }
        if (this.mode == mode.ADD) {
          //Thực hiện thêm mới
          if (this.employeeData["Gender"] == null) {
            //mặc định ngươi dùng không thay đổi gì thì giới tính sẽ là nam
            this.$set(this.employeeData, "Gender", 1);
          }
          //Thực hiện thêm mới
          await EmployeesRepository.post(this.employeeData);
        }
        if (this.mode == mode.INSERT) {
          //Thực hiện sửa thông tin
          if (this.isChangeData) {
            await EmployeesRepository.put(
              this.employeeData["EmployeeId"],
              this.employeeData
            );
          } else {
            await this.insertAfterSave();
            return;
          }
        }
        await this.insertAfterSave();
        this.$emit("loadTable");
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Lấy mã mới sau khi cất khi thực hiện cất và thêm mới
     * CreatedBy: nvdien(2/9/2021)
     */
    async insertAfterSave() {
      try {
        this.$emit("changeMode", mode.ADD);
        let newEmployeeCode = await EmployeesRepository.getNewEmployeeCode();
        this.inputReset = true;
        this.$refs.employeeCodeInput.focusInput();
        this.isForceDataNotChange = true;
        this.employeeData = Object.assign(
          {},
          { EmployeeCode: newEmployeeCode.data }
        );
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Validate dữ liệu trước khi cất
     * CreatedBy: nvdien(2/9/2021)
     */
    async validateBeforeSave() {
      try {
        //Kiểm tra định dạng email nếu có
        let isValidEmail = true;
        if (this.employeeData["Email"]) {
          isValidEmail = this.validateEmail(this.employeeData["Email"]);
        }
        //Kiểm tra các trường không được để trống
        if (
          !(
            this.employeeData["EmployeeCode"] &&
            this.employeeData["FullName"] &&
            this.employeeData["DepartmentId"] &&
            isValidEmail
          )
        ) {
          this.inputCheck = !this.inputCheck;
          return false;
        }
        //Kiểm tra trùng mã
        if (
          this.mode == mode.INSERT &&
          this.employeeData["EmployeeCode"] == this.employeeInfo["EmployeeCode"]
        ) {
          return true;
        }
        let employeeCodeExist =
          await EmployeesRepository.checkEmployeeCodeExist(
            this.employeeData["EmployeeCode"]
          );
        if (employeeCodeExist.data == 1) {
          this.inputErrorCustom = this.formatString(
            message.messageDuplication,
            this.employeeData["EmployeeCode"]
          );
          this.errorList.push(this.inputErrorCustom);
          //Hiển thị thông báo
          this.messageText = this.errorList[0];
          this.buttons = [
            {
              feature: "right-first ms-button-primary",
              callback: () => {
                this.closeMessageBox();
                this.errorList.shift();
              },
              value: "Đóng",
            },
          ];
          this.iconMessage = "mi-exclamation-warning-48";
          this.isShowMessage = true;
          return false;
        }
        return true;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Lấy những lỗi từ input
     * CreatedBy: nvdien(2/9/2021)
     */
    getInputError(error) {
      this.errorList.push(error);
      //Hiển thị thông báo
      this.messageText = this.errorList[0];
      this.buttons = [
        {
          feature: "center ms-button-primary",
          callback: () => {
            this.closeMessageBox();
            this.errorList.shift();
          },
          value: "Đóng",
        },
      ];
      this.iconMessage = "mi-exclamation-error-48-2";
      this.isShowMessage = true;
    },
    /**
     * lấy dữ liệu từ combobox đơn vị
     * CreatedBy: nvdien(2/9/2021)
     */
    getComboboxData(departmentData) {
      this.$set(this.employeeData, "DepartmentId", departmentData);
    },
    /**
     * Cập nhật giá trị gender khi chọn
     * CreatedBy: nvdien(1/9/2021)
     */
    chooseGenderRadio(options) {
      this.$set(this.employeeData, "Gender", options["Gender"]);
    },
    /**
     * Lấy giá trị ngày từ input ngày sinh
     * CreatedBy: nvdien(3/9/2021)
     */
    getDateOfBirth(dateValue) {
      this.$set(this.employeeData, "DateOfBirth", dateValue);
    },
    /**
     * Lấy giá trị ngày từ input nơi cấp
     * CreatedBy: nvdien(3/9/2021)
     */
    getIdentityDate(dateValue) {
      this.$set(this.employeeData, "IdentityDate", dateValue);
    },
  },

  watch: {
    //khi mở form
    isFocusCode: function () {
      //focus vào ô mã nhân viên mỗi lần mở popup
      this.$refs.employeeCodeInput.focusInput();
      //Tắt chế độ reset dữ liệu
      this.isResetData = false;
      //Tắt chế độ reset input
      this.inputReset = false;
      //đồng thời gán thông tin employee lên form
      this.employeeData = Object.assign({}, this.employeeInfo);
    },
    employeeData: {
      handler(newValue, oldValue) {
        if (
          this.isForceDataNotChange ||
          this.checkEmptyObject(oldValue) ||
          (this.isResetData && this.checkEmptyObject(newValue)) ||
          (this.mode == mode.INSERT &&
            this.deepEqual(newValue, this.employeeInfo) &&
            this.isObject(newValue))
        ) {
          this.isChangeData = false;
          this.isForceDataNotChange = false;
          return;
        }
        this.isChangeData = true;
      },
      deep: true,
    },
  },
};
</script>

<style scoped>
@import url("../../../css/views/employees/employeedetail.css");
</style>