<template>
  <div class="employee-detail">
    <base-popup>
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
          <base-input label="Nơi cấp" v-model="employeeData['IdentityPlace']" />
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
          <base-input label="Chi nhánh" v-model="employeeData['BankBranch']" />
        </div>
      </div>
    </base-popup>
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";
import BasePopup from "../../base/BasePopup.vue";
export default {
  name: "EmployeeDetail",
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
    isResetData: {
      type: Boolean,
      default: false,
    },
    isSaveData: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      employeeData: {},
    };
  },

  methods: {
    /**
     * kiểm tra xem object có empty không
     * CreatedBy: nvdien(30/8/2021)
     */
    checkEmptyObject(obj) {
      return obj && Object.keys(obj).length === 0 && obj.constructor === Object;
    },
  },
  components: {
    BaseInput,
    BasePopup,
  },
  watch: {
    isFocusCode: function (newValue) {
      if (newValue) {
        //focus vào ô mã nhân viên mỗi lần mở popup
        this.$refs.employeeCodeInput.focusInput();
        //đồng thời gán thông tin employee lên form
        this.employeeData = Object.assign({}, this.employeeInfo);
      }
    },
    // employeeData: function (newValue, oldValue) {
    //   if (this.isResetData && this.checkEmptyObject(newValue)) {
    //     //nếu ở chế độ reset data thì sẽ không hiện cảnh báo thay đổi dữ liệu
    //     this.$emit("resetDataDone");
    //     return;
    //   }
    //   if (this.checkEmptyObject(oldValue)) {
    //     return;
    //   } else {
    //     this.$emit("checkChangeData", true);
    //   }
    // },
    // isResetData: function (newValue) {
    //   if (newValue) {
    //     //đánh dấu không có sự thay đổi dữ liệu
    //     this.$emit("checkChangeData", false);
    //     //reset employeeData
    //     this.employeeData = Object.assign({}, {});
    //   }
    // },
  },
};
</script>

<style scoped>
@import url("../../../css/views/employees/employeedetail.css");
</style>