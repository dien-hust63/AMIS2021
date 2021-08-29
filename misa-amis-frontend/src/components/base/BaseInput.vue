<template>
  <div class="ms-input">
    <label class="ms-input__label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div
      class="ms-input__input"
    >
      <input
        ref="input"
        v-bind="$attrs"
        :value="valueInput"
        v-on="inputListeners"
        :tabindex="tabIndex"
      />
      <div class="ms-input__icon mi mi-16 mi-search" v-if="hasIcon"></div>
    </div>
    <div class="text--red" style="margin-top: 2px">{{ errors }}</div>
  </div>
</template>

<script>
export default {
  name: "BaseInput",
  inheritAttrs: false,
  props:{
    required:{
      type:String,
      default: "",
    },
    hasIcon:{
      type:String,
      default: "",
    },
  },
  computed: {
    // We add all the listeners from the parent
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.$emit("input", event.target.value);
        },
        blur: function (event) {
            console.log(event);
        },
      });
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/input.css");
</style>

