<template>
  <div class="context-menu">
    <div class="context-menu__text" @click="editEntity">Sửa</div>
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
          <li @click="copyEntity">Nhân bản</li>
          <li @click="confirmDeleteRow">Xóa</li>
          <li>Ngừng sử dụng</li>
        </ul>
      </div>
    </div>
    <base-message
      :messageText="messageText"
      icon="mi-exclamation-warning-48"
      :class="{ 'ms-message--show': isShowMessage }"
      :buttons="buttons"
    />
  </div>
</template>

<script>
import BaseMessage from "../../base/BaseMessage.vue";
import { RepositoryFactory } from "../../../js/repository/repository.factory.js";
const EmployeesRepository = RepositoryFactory.get("employees");
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
      buttons: [
        {
          feature: "left ms-button-secondary",
          callback: () => {
            this.closeMessageBox();
          },
          value: "Không",
        },
        {
          feature: "right-first ms-button-primary",
          callback: () => {
            this.deleteRow();
          },
          value: "Có",
        },
      ],
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
      EmployeesRepository.delete(this.deleteData["EmployeeId"])
        .then(() => {
          this.$emit("loadTable");
        })
        .catch((response) => console.log(response));
      this.loadData = true;
  },
    /**
     * Sửa nhân viên
     * CreatedBy: nvdien(1/9/2021)
     */
    editEntity() {
      // hiển thị popup nhân viên và đổ các dữ liệu lên popup
      this.$emit("editEntity", this.deleteData["EmployeeId"]);
    },
    /**
     * Nhân bản đối tượng
     * CreatedBy: nvdien(2/9/2021)
     */
    copyEntity() {
      this.$emit("copyEntity", this.deleteData);
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