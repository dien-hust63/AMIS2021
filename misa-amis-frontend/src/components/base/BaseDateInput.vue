<template>
  <div class="ms-input">
    <label class="ms-input__label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div
      class="ms-input__input"
      :class="{
        'ms-input--error': isInputError,
        'ms-input--focus': isFocusInput,
      }"
    >
      <input
        ref="input"
        v-bind="$attrs"
        v-on="inputListeners"
        :value="valueInput"
      />
      <input
        type="text"
        class="input-date"
        placeholder="DD/MM/YYYY"
        v-model="valueDateInput"
        @mouseover="mouseoverDateInput"
        @mouseout="mouseoutDateInput"
        v-on:keyup="validateDateInput"
        @focus="focusInputDate"
        @blur="blurDateInput"
      />
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
  name: "BaseDateInput",
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
      valueDateInput: "",
      notWatch: false,
      isFocusInput: false,
    };
  },
  methods: {
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
    /**
     * mouseover input date
     * CreatedBy: nvdien(3/9/2021)
     */
    mouseoverDateInput() {
      if (this.isInputError) {
        // hiển thị báo lỗi
        this.isShowError = true;
      }
    },
    /**
     * mouseout input date
     * CreatedBY: nvdien(2/9/2021)
     */
    mouseoutDateInput() {
      this.isShowError = false;
    },
    /**
     * validate input date
     * CreatedBY: nvdien(2/9/2021)
     */
    validateDateInput() {
      if (this.valueDateInput === "") return;
      if (this.validateDateString(this.valueDateInput)) {
        if (this.isFutureDate(this.valueDateInput)) {
          this.isInputError = true;
          this.inputError = `${this.label} vượt quá ngày hiện tại`;
          return;
        }
        this.isInputError = false;
        this.isShowError = false;
        this.$emit("getDate", this.valueDateInput);
      } else {
        this.isInputError = true;
        this.inputError = `Ngày chưa đúng định dạng`;
      }
    },
    /**
     * focus vào input date
     * CreatedBy: nvdien(2/9/2021)
     */
    focusInputDate() {
      this.isFocusInput = true;
    },
    /**
     * blur dateInput
     */
    blurDateInput() {
      this.isFocusInput = false;
    },
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.$emit("input", event.target.value);
        },
        // blur: function () {
        //     console.log("test");
        //   self.isFocusInput = false;
        // },
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
      if (this.$attrs.type == "date") {
        this.valueDateInput = this.formatDate(this.value, "/");
        this.validateDateInput();
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
        this.isFocusInput = false;
      }
    },
    inputErrorCustom: function (newValue) {
      this.isInputError = true;
      this.inputError = newValue;
    },
    isInputError: function (newValue) {
      this.isFocusInput = !newValue;
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/dateinput.css");
</style>

