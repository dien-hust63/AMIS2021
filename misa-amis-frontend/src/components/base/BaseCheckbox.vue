<template>
  <div class="base-checkbox">
    <div class="checkbox">
      <i class="fas fa-check"></i>
    </div>
    <div v-if="label" class="checkbox-label">{{ label }}</div>
  </div>
</template>

<script>
export default {
  name: "BaseCheckbox",
  props: {
    label: {
      type: String,
      default: "",
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/checkbox.css");
</style>