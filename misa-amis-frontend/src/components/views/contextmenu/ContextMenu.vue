<template>
  <div class="context-menu">
    <div class="context-menu__text">Sửa</div>
    <div
      class="context-menu-dropdown"
      :class="{ 'dropdown--show': isShow }"
      @blur="hideDropDown"
      tabindex="0"
    >
      <div
        class="mi mi-16 mi-arrow-down--blue dropdown__icon"
        @click="showDropdown"
      ></div>
      <div class="dropdown__content">
        <ul>
          <li>Nhân bản</li>
          <li @click="deleteRow">Xóa</li>
          <li>Ngừng sử dụng</li>
        </ul>
      </div>
    </div>
    <base-message
      :class="{ 'ms-message--show': isShowMessage }"
      :messageText="messageText"
      icon="mi-exclamation-question-48"
      isLeftRightTwo
      :messageButtonData="messageButtonData"
    />
  </div>
</template>

<script>
// import EmployeeApi from "../../../js/component/employeeapi.js";
import BaseMessage from "../../base/BaseMessage.vue";
import { confirmChangeData } from "../../../js/resources/resourcevn.js";
export default {
  name: "ContextMenu",
  components: {
    BaseMessage,
  },
  props: {
    deleteData: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  data() {
    return {
      isChoose:true,
      isShow: false,
      isShowMessage: false,
      messageText: confirmChangeData,
      messageButtonData: ['Mở','','Test','Đóng'],
    };
  },
  methods: {
    /**
     * Hiển thị dropdown
     * CreatedBy: nvdien(30/8/2021)
     */
    showDropdown() {
      this.isShow = !this.isShow;
    },
    /**
     * Ẩn dropdown
     * CreatedBy: nvdien(30/8/2021)
     */
    hideDropDown() {
      this.isShow = false;
    },
    /**
     * Xóa thông tin của đối tượng tại hàng đó
     */
    deleteRow() {
      //Hiện form cảnh báo
      this.isShowMessage = true;
    },
  },
};
</script>
<style scoped>
@import url("../../../css/views/contextmenu/contextmenu.css");
</style>