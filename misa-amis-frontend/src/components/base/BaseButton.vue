<template>
  <div class="ms-button" @click="onclick">
    {{ value }}
  </div>
</template>

<script>
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
  },
};
</script>

<style scoped>
@import url("../../css/base/button.css");
</style>