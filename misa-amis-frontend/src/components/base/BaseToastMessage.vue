<template>
  <div
    class="base-toast-message"
    :class="{ 'base-toast-message--show': isShow }"
  >
    <div
      class="base-toast-message__icon mi mi-34 "
      :class="[icon]"
    ></div>
    <div class="base-toast-message__text">{{toastMessageText}}</div>
  </div>
</template>

<script>
export default {
  name: "BaseToastMessage",
  data() {
    return {
      isShow: false,
      icon: "",
      toastMessageText: "",
    };
  },
  created() {
    /**
     * Tạo event hiển thị toast message
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("showToastMessage", (messageContent) => {
      this.icon = messageContent.icon;
      this.toastMessageText = messageContent.text;
      this.isShow = true;
      setTimeout(() => {
        this.isShow = false;
      }, 1500);
    });
    
  },
  destroyed() {
    /**
     * Huỷ các sự kiện
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$off("showToastMessage");
  },
};
</script>

<style scoped>
@import url("../../css/base/toastmessage.css");
</style>    