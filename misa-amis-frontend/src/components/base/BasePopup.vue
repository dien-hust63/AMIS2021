<template>
  <div class="ms-popup-wrap" :class="{ 'ms-popup--show': isShowPopup }">
    <div class="ms-popup">
      <div class="ms-popup__header">
        <div class="ms-popup-title">
          <div class="popup-title">Thông tin nhân viên</div>
          <base-checkbox label="Là khách hàng" />
          <base-checkbox label="Là nhà cung cấp" />
        </div>
        <div class="ms-popup-close">
          <div class="mi mi-24 mi-help popup-help-icon"></div>
          <div
            class="mi mi-24 mi-close popup-close-icon"
            @click="confirmClosePopup"
          ></div>
        </div>
      </div>
      <div class="ms-popup__content">
        <div class="popup-content">
          <employee-detail
            :employeeInfo="popupData"
            :isFocusCode="isShowPopup"
            @checkChangeData="checkChangeData"
            :mode="mode"
            :isResetData="isResetData"
            @resetDataDone="resetDataDone"
          />
        </div>
        <div class="popup-footer">
          <div class="popup-footer-divide"></div>
          <div class="popup-footer-content">
            <div class="popup-footer--right">
              <div class="ms-button ms-button-secondary" @click="closePopup">Hủy</div>
            </div>
            <div class="popup-footer--left">
              <div class="ms-button ms-button-secondary first-right-button">
                Cất
              </div>
              <div class="ms-button ms-button-primary">Cất và thêm</div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <base-message :class="{ 'ms-message--show': isShowMessage }" />
  </div>
</template>

<script>
import BaseCheckbox from "../base/BaseCheckbox.vue";
import EmployeeDetail from "../views/employees/EmployeeDetail.vue";
import BaseMessage from "../base/BaseMessage.vue";
export default {
  name: "BasePopup",
  props: {
    popupData: {
      type: Object,
      default() {
        return {};
      },
    },
    isShowPopup: {
      type: Boolean,
      default: false,
    },
    mode: {
      type: Number,
      default: -1
    }
  },
  components: {
    BaseCheckbox,
    EmployeeDetail,
    BaseMessage,
  },
  data() {
    return {
      isChangeData: false,
      isShowMessage: false,
      isResetData:false,
    };
  },
  methods: {
    /**
     * Hiển thị cảnh báo nếu dữ liệu thay đổi , đóng popup
     * CreatedBy: nvdien(30/8/2021)
     */
    confirmClosePopup() {
      //Kiểm tra dữ liệu có bị thay đổi không
      if (this.isChangeData) {
        // hiển thị cảnh báo
        this.isShowMessage = true;
      } else {
        // đóng popup
        this.isResetData = true;
        this.$emit("closePopup");
      }
    },
    /**
     * kiểm tra dữ liệu có bị thay đổi không
     */
    checkChangeData(isChangeData) {
      this.isChangeData = isChangeData;
    },
    /**
     * xử lí khi đã reset xong data
     * CreatedBy: nvdien(30/8/2021)
     */
    resetDataDone(){
      this.isResetData = false;
    },
    /**
     * Đóng popup
     * CreatedBy: nvdien(30/8/2021)
     */
    closePopup(){
      this.isResetData = true;
      this.$emit("closePopup");
    }
  },
};
</script>

<style scoped>
@import url("../../css/base/popup.css");
</style>