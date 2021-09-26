<template>
  <div class="ms-checkbox" >
    <div class="ms-checkbox-icon" >
      <div class="mi mi-16 mi-checkbox-active"></div>
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
  data(){
    return{
    }
  },
  methods:{
  }
};
</script>

<style scoped>
@import url("../../css/base/checkbox.css");
</style>