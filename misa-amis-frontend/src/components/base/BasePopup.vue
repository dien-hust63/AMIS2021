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
          <div class="mi mi-24 mi-help popup-help"></div>
          <div class="mi mi-24 mi-close" @click="confirmClosePopup"></div>
        </div>
      </div>
      <div class="ms-popup__content">
        <div class="popup-content">
          <employee-detail
            :employeeInfo="popupData"
            :isFocusCode="isShowPopup"
            @checkChangeData="checkChangeData"
          />
        </div>
        <div class="popup-footer">
          <div class="popup-footer-divide"></div>
          <div class="popup-footer-content">
            <div class="popup-footer--right">
              <div class="ms-button ms-button-secondary">Hủy</div>
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
    <base-message />
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
  },
  components: {
    BaseCheckbox,
    EmployeeDetail,
    BaseMessage,
  },
  methods: {
    /**
     * Hiển thị cảnh báo nếu dữ liệu thay đổi , đóng popup
     * CreatedBy: nvdien(30/8/2021)
     */
    confirmClosePopup() {
      //Kiểm tra dữ liệu có bị thay đổi không
      if(this.checkChangeData()){
        console.log("close change oke");
      }
      else{
        console.log("close not change data nha");
      }
    },
    /**
     * kiểm tra dữ liệu có bị thay đổi không
     */
    checkChangeData(isChangeData){
      return isChangeData;
    }
  },
};
</script>

<style scoped>
@import url("../../css/base/popup.css");
</style>