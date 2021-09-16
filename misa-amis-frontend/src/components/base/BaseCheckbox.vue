<template>
  <div class="ms-checkbox" @click="chooseCheckbox" >
    <div class="checkbox mi mi-16 " :class="{'mi-checkbox-active': isActive}">
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
      isActive:false,
    }
  },
  methods:{
    chooseCheckbox(){
      this.isActive = !this.isActive;
      this.$emit("chooseCheckbox", this.isActive);
    }
  }
};
</script>

<style scoped>
@import url("../../css/base/checkbox.css");
</style>