<template>
  <div class="ms-popup-wrap" >
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
          ></div>
        </div>
      </div>
      <div class="ms-popup__content">
        <div class="popup-content">
          <slot></slot>
        </div>
        <div class="popup-footer">
          <div class="popup-footer-divide"></div>
          <div class="popup-footer-content">
            <div class="popup-footer--right">
              <div class="ms-button ms-button-secondary" >
                Hủy
              </div>
            </div>
            <div class="popup-footer--left">
              <div class="ms-button ms-button-secondary first-right-button" >
                Cất
              </div>
              <div class="ms-button ms-button-primary" >Cất và thêm</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseCheckbox from "../base/BaseCheckbox.vue";
export default {
  name: "BasePopup",
  props: {
  },
  components: {
    BaseCheckbox,
  },
  data() {
    return {
    };
  },
  methods: {
  },
};
</script>

<style scoped>
@import url("../../css/base/popup.css");
</style>