<template>
  <div class="base-message-wrap">
    <div class="base-message">
      <div class="message-content">
        <div class="content__icon">
          <div class="mi mi-48 mi-exclamation-question-48"></div>
        </div>
        <div class="content__text">
            <span> Dữ liệu đã bị thay đổi. Bạn có muốn cất không?</span>
        </div>
      </div>
      <div class="message-line"></div>
      <div class="message-footer">
        <div class="message-footer--left">
            <div class="ms-button ms-button-secondary">Hủy</div>
        </div>
        <div class="message-footer--center"></div>
        <div class="message-footer--right">
          <div class="footer-right-first">
               <div class="ms-button ms-button-secondary">Không</div>
          </div>
          <div class="footer-right-second">
               <div class="ms-button ms-button-primary">Có</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseMessage",
};
</script>

<style scoped>
@import url("../../css/base/message.css");
</style>