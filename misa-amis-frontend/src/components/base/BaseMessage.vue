<template>
  <div class="ms-message-wrap">
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
        <base-button :value="btn.value" :class="[btn.feature]" v-for="(btn, i) in buttons" :key="i" :onclick="btn.callback"/>
      </div>
    </div>
  </div>
</template>

<script>
import BaseButton from "../base/BaseButton.vue";
export default {
  name: "BaseMessageTest",
  components: {
    BaseButton,
  },
  props: {
    icon: {
      type: String,
      required: true,
    },
    messageText: {
      type: String,
      required: true,
    },
    buttons: {
      type: Array,
      required: false,
      default() {
        return [
          {
            // position:"center",
            // color: "ms-button-primary",
            feature:"center ms-button-primary",
            callback: () => {
              this.$emit("closePopup");
            },
            value: "Đóng",
          },
        ];
      },
    },
  },
  data() {
    return {};
  },
  methods: {},
};
</script>

<style scoped>
@import url("../../css/base/message.css");
</style>