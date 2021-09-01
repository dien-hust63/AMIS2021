<template>
  <div class="employee-popup-wrap">
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
                  required="true"
                  v-model="employeeData['EmployeeCode']"
                  ref="employeeCodeInput"
                />
              </div>
              <div class="w-3/5">
                <base-input
                  label="Tên"
                  required="true"
                  v-model="employeeData['FullName']"
                />
              </div>
            </div>
            <div class="row-input--right w-1/2">
              <div class="w-2/5 p-r-6 border-box">
                <base-input label="Ngày sinh" type="date" />
              </div>
              <div class="w-3/5"></div>
            </div>
          </div>
          <div class="row-input">
            <div class="row-input--left w-1/2">
              <base-input
                label="Đơn vị"
                required="true"
                v-model="employeeData['DepartmentName']"
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
                <base-input label="Ngày sinh" type="date" />
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
              <base-input label="Email" v-model="employeeData['Email']" />
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
              <div class="ms-button ms-button-secondary">Hủy</div>
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
      icon="mi-exclamation-question-48"
      :class="{ 'ms-message--show': isShowMessage }"
      :buttons="buttons"
    />
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";
import BaseCheckbox from "../../base/BaseCheckbox.vue";
import BaseMessage from "../../base/BaseMessage.vue";
import Mixin from "../../../mixins/methods.js";
import { RepositoryFactory } from "../../../js/repository/repository.factory.js";

const EmployeesRepository = RepositoryFactory.get("employees");

export default {
  name: "EmployeePopup",
  mixins: [Mixin],
  components: {
    BaseInput,
    BaseCheckbox,
    BaseMessage,
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
      messageText: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
      isShowMessage: false,
      isChangeData: false,
      isResetData: false,
      isForceNotChange: false,
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
      ],
    };
  },

  methods: {
    /**
     * Đóng message box
     * CreatedBy: nvdien(30/8/2021)
     */
    closeMessageBox() {
      this.isShowMessage = false;
    },
    /**
     * Đóng popup nhân viên
     * CreatedBy: nvdien(30/8/2021)
     */
    closePopup() {
      //Kiểm tra có thay đổi dữ liệu không
      if (this.isChangeData) {
        //Hiệm message cảnh báo
        this.isShowMessage = true;
      } else {
        //Reset dữ liệu popup nhân viên
        this.isResetData = true;
        this.isForceNotChange = false;
        this.employeeData = Object.assign({}, {});
        //Đóng popup nhân viên
        this.$emit("closePopup");
      }
    },
    /**
     * Đóng đồng thời message box và popup
     * CreatedBy: nvdien(1/9/2021)
     */
    closeMessageAndPopup() {
      this.isShowMessage = false;
      //Reset dữ liệu popup nhân viên
      this.isResetData = true;
       this.isForceNotChange = false;
      this.employeeData = Object.assign({}, {});
      //Đóng popup nhân viên
      this.$emit("closePopup");
    },
    /**
     * Lưu dữ liệu
     * CreatedBy: nvdien(1/9/2021)
     */
    saveData() {
      if (this.mode == 0) {
        //Thêm mới
        //Validate các trường
        //Thực hiện thêm mới
        EmployeesRepository.post(this.employeeData)
          .then((response) => {
            console.log(response);
            this.closeMessageAndPopup();
            this.$emit("loadTable");
          })
          .catch((response) => console.log(response));
      }
      if (this.mode == 1) {
        //Sửa
        //Validate các trường
        //Thực hiện sửa thông tin
        EmployeesRepository.put(
          this.employeeData["EmployeeId"],
          this.employeeData
        )
          .then((response) => {
            console.log(response);
            this.closeMessageAndPopup();
            this.$emit("loadTable");
          })
          .catch((response) => console.log(response));
      }
    },
    /**
     * Cất và thêm mới
     * CreatedBy: nvdien(1/9/2021)
     */
    saveAndInsert() {
      if (this.mode == 0) {
        //Thêm mới
        //Validate các trường
        //Thực hiện cất và thêm mới
        Promise.all([
          EmployeesRepository.post(this.employeeData),
          EmployeesRepository.getNewEmployeeCode(),
        ]).then((response) => console.log(response[1].data));
      }
      if (this.mode == 1) {
        //Sửa
        //Validate các trường
        //Thực hiện sửa thông tin va thêm mới
        if (this.isChangeData) {
          Promise.all([
            EmployeesRepository.put(
              this.employeeData["EmployeeId"],
              this.employeeData
            ),
            EmployeesRepository.getNewEmployeeCode(),
          ]).then((response) => {
            this.$refs.employeeCodeInput.focusInput();
            this.isForceNotChange = true;
            this.employeeData = Object.assign(
              {},
              { EmployeeCode: response[1].data }
            );
            this.$emit("loadTable");
          });
        } else {
          EmployeesRepository.getNewEmployeeCode()
            .then((response) => {
              this.$refs.employeeCodeInput.focusInput();
              this.isForceNotChange = true;
              this.employeeData = Object.assign(
                {},
                { EmployeeCode: response.data }
              );
              
            })
            .catch((error) => console.log(error));
        }
      }
    },
  },

  watch: {
    isFocusCode: function () {
      //focus vào ô mã nhân viên mỗi lần mở popup
      this.$refs.employeeCodeInput.focusInput();
      //Tắt chế độ reset dữ liệu
      this.isResetData = false;
      //đồng thời gán thông tin employee lên form
      this.employeeData = Object.assign({}, this.employeeInfo);
    },
    employeeData: {
      handler(newValue, oldValue) {
        if(this.isForceNotChange){
          return;
        }
        if (
          this.checkEmptyObject(oldValue) ||
          (this.isResetData && this.checkEmptyObject(newValue)) ||
          (this.mode == 1 &&
            this.deepEqual(newValue, this.employeeInfo) &&
            this.isObject(newValue))
        ) {
          this.isChangeData = false;
          return;
        }
        this.isChangeData = true;
      },
      deep: true,
    },
    isForceNotChange: function(newValue){
      if(newValue){
        this.isChangeData = false;
      }
    }
  },
};
</script>

<style scoped>
@import url("../../../css/views/popup/employeepopup.css");
</style>