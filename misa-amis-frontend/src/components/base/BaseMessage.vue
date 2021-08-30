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
      <div class="message-footer" :class="classObject">
        <div class="message-footer--left">
          <div class="ms-button ms-button-secondary">{{messageButtonData[0]}}</div>
        </div>
        <div class="message-footer--center">
          <div class="ms-button ms-button-primary">{{messageButtonData[1]}}</div>
        </div>
        <div class="message-footer--right">
          <div class="footer-right-first">
            <div class="ms-button ms-button-secondary">{{messageButtonData[2]}}</div>
          </div>
          <div class="footer-right-second">
            <div class="ms-button ms-button-primary">{{messageButtonData[3]}}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseMessage",
  props: {
    messageText: {
      type: String,
      default() {
        return "";
      },
    },
    icon: {
      type: String,
      default() {
        return "";
      },
    },
    isCenter: {
      type: Boolean,
      default: false,
    },
    isRightOnly: {
      type: Boolean,
      default: false,
    },
    isLeftRightOne: {
      type: Boolean,
      default: false,
    },
    isLeftRightTwo: {
      type: Boolean,
      default: false,
    },
    messageButtonData: {
      type: Array,
      default(){
        return [];
      }
    }
  },
  data() {
    return {
      classObject: {
        "center": this.isCenter,
        "right-only": this.isRightOnly,
        "left-right-one": this.isLeftRightOne,
        "left-right-two": this.isLeftRightTwo,
      },
    };
  },
};
</script>

<style scoped>
@import url("../../css/base/message.css");
</style>