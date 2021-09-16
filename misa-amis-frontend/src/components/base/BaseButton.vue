<template>
  <div class="ms-button" @click="handleClick">
    {{ value }}
  </div>
</template>

<script>
import { mode } from "../../js/resources/resourcevn.js";
export default {
  name: "BaseButton",
  props: {
    value: {
      type: String,
      required: true,
    },
    onclick: {
      type: Function,
    },
    buttonMode: {
      type: Number,
      default: mode.BUTTONNORMAL
    },
  },
  methods: {
    handleClickNormal() {
      console.log(this.value);
    },
  },
  computed: {
    handleClick: function () {
      /**Button ở chết độ callback */
      if (this.buttonMode == mode.BUTTONCALLBACK) {
        return this.onclick;
      }
      /**button bình thường */
      return this.handleClickNormal;
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/button.css");
</style>