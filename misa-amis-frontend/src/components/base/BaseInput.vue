<template>
  <div class="ms-input">
    <label class="ms-input__label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div class="ms-input__input" :class="{ 'ms-input--error': isInputError }">
      <input
        ref="input"
        v-bind="$attrs"
        v-on="inputListeners"
        :value="valueInput"
      />
      <div class="ms-input__icon--after mi mi-16 mi-search"></div>
      <div
        class="ms-input__error"
        :class="{ 'ms-input__error--show': isShowError }"
      >
        {{ inputError }}
      </div>
    </div>
  </div>
</template>

<script>
import Mixin from "../../mixins/methods.js";
export default {
  name: "BaseInput",
  inheritAttrs: false,
  mixins: [Mixin],
  props: {
    required: {
      type: Boolean,
      default: false,
    },
    label: {
      type: String,
      default: "",
    },
    value: {
      type: [String, Number],
      default: "",
    },
    inputCheck: {
      type: Boolean,
      default() {
        return false;
      },
    },
    inputReset: {
      type: Boolean,
      default() {
        return false;
      },
    },
    inputErrorCustom: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      isInputError: false,
      isShowError: false,
      inputError: "",
    };
  },
  methods: {
    /**
     * focus vào ô input
     * CreatedBy: nvdien(30/8/2021)
     */
    focusInput() {
      this.$nextTick(() => {
        this.$refs.input.focus();
      });
    },
    /**
     * validate input
     * CreatedBy: nvdien(2/9/2021)
     */
    validateInput(self) {
      if (self.required && (self.value === null || self.value === "")) {
        this.isInputError = true;
        this.inputError = `${this.label} không được phép để trống`;
        this.$emit("getInputError", this.inputError);
      }
    },
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.$emit("input", event.target.value);
        },
        blur: function (event) {
          console.log(event);
        },
        mouseover: function () {
          if (self.isInputError) {
            // hiển thị báo lỗi
            self.isShowError = true;
          }
        },
        mouseout: function () {
          self.isShowError = false;
        },
      });
    },
    valueInput: function () {
      if (this.$attrs.type == "date") {
        return this.formatDate(this.value, "-");
      }
      return this.value;
    },
  },
  watch: {
    value: function (newValue, oldValue) {
      if (this.required) {
        if (this.newValue != "") {
          this.isInputError = false;
          this.isShowError = false;
        }
        if (oldValue != "" && newValue == "" && this.inputReset == false) {
          this.isInputError = true;
          this.inputError = `${this.label} không được phép để trống`;
        }
      }
    },
    inputCheck: function () {
      //validate input
      this.validateInput(this);
    },
    inputReset: function (newValue) {
      if (newValue) {
        //reset input
        this.isInputError = false;
        this.isShowError = false;
      }
    },
    inputErrorCustom: function (newValue) {
      this.isInputError = true;
      this.inputError = newValue;
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/input.css");
</style>

