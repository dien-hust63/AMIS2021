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
        v-on="inputListeners"
      />
      <div class="ms-input__icon--after mi mi-16 mi-search"></div>
    </div>
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
    label:{
      type:String,
      default: "",
    }
  },
  methods:{
    /**
     * focus vào ô input
     * CreatedBy: nvdien(30/8/2021)
     */
    focusInput() {
      this.$nextTick(() => {
        this.$refs.input.focus();
      });
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

