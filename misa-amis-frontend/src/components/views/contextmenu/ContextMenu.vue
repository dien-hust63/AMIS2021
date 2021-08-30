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
          <li @click="confirmDeleteRow">Xóa</li>
          <li>Ngừng sử dụng</li>
        </ul>
      </div>
    </div>
    <base-message
      :class="{ 'ms-message--show': isShowMessage }"
      :messageText="messageText"
      icon="mi-exclamation-warning-48"
      isLeftRightOne
      :messageButtonData="messageButtonData"
      @closeMessageBox="closeMessageBox"
      @handleSecondRight="deleteRow"
    />
  </div>
</template>

<script>
import EmployeeApi from "../../../js/component/employeeapi.js";
import BaseMessage from "../../base/BaseMessage.vue";
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
      isChoose: true,
      isShow: false,
      isShowMessage: false,
      messageButtonData: ["Không", "", "", "Có"],
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
     * Hiển thị form cảnh báo xóa
     * CreatedBy: nvdien(30/8/2021)
     */
    confirmDeleteRow() {
      //Hiện form cảnh báo
      this.isShowMessage = true;
    },
    /**
     * Đóng message box
     * CreatedBy: nvdien(30/8/2021)
     */
    closeMessageBox() {
      this.isShowMessage = false;
    },

    /**
     * Xóa dữ liệu tại hàng đó
     * CreatedBy: nvdien(30/8/2021)
     */
    deleteRow() {
      this.isShowMessage = false;
      EmployeeApi.delete(this.deleteData["EmployeeId"])
        .then((response) => {
          console.log(response);
          this.$emit("loadTable");
        })
        .catch((response) => console.log(response));
      this.loadData = true;
    },
  },
  computed: {
    messageText: function () {
      return `Bạn có thực sự muốn xóa Nhân viên <${this.deleteData["EmployeeCode"]}> không?`;
    },
  },
};
</script>
<style scoped>
@import url("../../../css/views/contextmenu/contextmenu.css");
</style>