<template>
  <div class="ms-combobox-custom">
    <label class="combobox-custom-label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div class="combobox-custom-content">
      <div class="combobox-custom-input">
        <input type="text" v-on="inputListeners" />
      </div>
      <div class="combobox-custom-actions">
        <div class="btn-add btn-actions">
          <div class="mi mi-16 mi-plus--success"></div>
        </div>
        <div class="btn-dropdown btn-actions" @click="showComboDropdown">
          <div class="mi mi-16 mi-arrow-dropdown"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseComboboxCustom",
  props: {
    required: {
      type: Boolean,
      default() {
        return false;
      },
    },
    label: {
      type: String,
      default: "",
    },
  },
 
  methods:{
      /**
       * Hiển thị combo dropdown panel
       * CreatedBy: nvdien(26/9/2021)
       */
      showComboDropdown(){
          this.$emit("showComboDropdown");
      }
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.$emit("input", event.target.value);
        },
        focus: function (event) {
          event.target.select();
        },
      });
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/comboboxcustom.css");
</style>