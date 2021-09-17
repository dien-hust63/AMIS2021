<template>
  <div
    class="base-button-custom"
    :class="{ 'button-dropdown-list--show': isShowList }"
  >
    <div class="button-custom-left" :class="[color]">
      <div class="buttom-custom-text">{{ value }}</div>
    </div>
    <div class="button-custom-right" :class="[color]" @click="toogleList">
      <div class="line"></div>
      <div class="mi mi-16 mi-arrow-up--white"></div>
    </div>
    <ul class="button-dropdown-list">
      <li>Test</li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "BaseButtonCustom",
  props: {
    value: {
      type: String,
      default: "",
    },
    color: {
      type: String,
      default: "ms-button-primary",
    },
  },
  data() {
    return {
      isShowList: false,
    };
  },
  methods: {
    /**Hiển thị danh sách */
    toogleList() {
      this.isShowList = !this.isShowList;
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/buttoncustom.css");
</style>