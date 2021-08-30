<template>
  <div class="employee-detail">
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
        <base-input label="Đơn vị" required="true" />
      </div>
      <div class="row-input--right w-1/2">
        <div class="w-3/5">
          <base-input label="Số CMND" />
        </div>
        <div class="w-2/5 p-l-6 border-box">
          <base-input label="Ngày sinh" type="date" />
        </div>
      </div>
    </div>
    <div class="row-input row-input-third">
      <div class="row-input--left w-1/2">
        <base-input label="Chức danh" />
      </div>
      <div class="row-input--right w-1/2">
        <base-input label="Nơi cấp" />
      </div>
    </div>
    <div class="row-input">
      <base-input label="Địa chỉ" />
    </div>
    <div class="row-input">
      <div class="w-200 p-r-6">
        <base-input label="ĐT di động" />
      </div>
      <div class="w-200 p-r-6">
        <base-input label="ĐT cố định" />
      </div>
      <div class="w-200">
        <base-input label="Email" />
      </div>
    </div>
    <div class="row-input">
      <div class="w-200 p-r-6">
        <base-input label="Tài khoản ngân hàng" />
      </div>
      <div class="w-200 p-r-6">
        <base-input label="Tên ngân hàng" />
      </div>
      <div class="w-200">
        <base-input label="Chi nhánh" />
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";
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
  },
  data() {
    return {
      employeeData: {},
      // isChangeData: false,
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
    employeeData: function (newValue, oldValue) {
      if(this.isResetData && this.checkEmptyObject(newValue)) {
        //nếu ở chế độ reset data thì sẽ không hiện cảnh báo thay đổi dữ liệu
        this.$emit("resetDataDone");
        return;
      }
      if (this.checkEmptyObject(oldValue)) {
        console.log("giá trị trc đó rỗng");
      } else {
        console.log("not rỗng ");
        console.log(oldValue);
        this.$emit("checkChangeData", true);
      }
      console.log("new value ");
      console.log(newValue);
    },
    isResetData: function (newValue) {
      if (newValue) {
        //đánh dấu không có sự thay đổi dữ liệu
        this.$emit("checkChangeData", false);
        //reset employeeData
        this.employeeData = Object.assign({}, {});
      }
    },
  },
};
</script>

<style scoped>
@import url("../../../css/views/employees/employeedetail.css");
</style>