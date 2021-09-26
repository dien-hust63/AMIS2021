<template>
  <div class="ms-message-wrap" :class="{ 'ms-message--show': isShowMessage }">
    <div class="ms-message">
      <div class="message-content">
        <div class="content__icon">
          <div class="mi mi-48" :class="[icon]"></div>
        </div>
        <div class="content__text">
          <span>{{ messageText }}</span>
        </div>
      </div>
      <div class="message-line"></div>
      <div class="message-footer">
        <base-button
          :value="btn.value"
          :class="[btn.feature]"
          v-for="(btn, i) in buttons"
          :key="i"
          :onclick="btn.callback"
          :buttonMode="buttonMode"
        />
      </div>
    </div>
  </div>
</template>

<script>
import BaseButton from "../base/BaseButton.vue";
import { mode } from "../../js/resources/resourcevn.js";
export default {
  name: "BaseMessageTest",
  components: {
    BaseButton,
  },

  created() {
    /**
     * Tạo event hiển thị toast message
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("showMessageBox", (messageContent) => {
      this.icon = messageContent.icon;
      this.messageText = messageContent.messageText;
      this.buttons = messageContent.buttons;
      this.isShowMessage = true;
    });
    /**
     * Tạo event ẩn toast message
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("hideMessageBox", () => {
      console.log("ttes");
      this.isShowMessage = false;
    });
  },
  destroyed() {
    /**
     * Huỷ các sự kiện
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$off("showMessageBox");
    this.$eventBus.$off("hideMessageBox");
  },
  data() {
    return {
      buttonMode: mode.BUTTONCALLBACK,
      isShowMessage: false,
      icon: "",
      messageText: "",
      buttons: [],
    };
  },
  methods: {},
};
</script>

<style scoped>
@import url("../../css/base/message.css");
</style>