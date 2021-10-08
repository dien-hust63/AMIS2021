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
        v-if="typeInput == ''"
      />
      <textarea
        ref="input"
        v-bind="$attrs"
        v-on="inputListeners"
        :value="valueInput"
        v-if="typeInput == 'textarea'"
      />

      <div class="ms-input__icon--after mi mi-16 mi-search"></div>
    </div>
  </div>
</template>

<script>
import Mixin from "../../mixins/methods.js";
import { message } from "../../js/resources/resourcevn.js";
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
    typeInput: {
      type: String,
      default: "",
    },
    fieldName: {
      type: String,
      default: "",
    },
    formName: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      isInputError: false,
      inputError: "",
      errorMessage: "",
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
    validateInput() {
      //các trường không để trống
      if (this.required && (this.value === null || this.value === "")) {
        this.isInputError = true;
        this.errorMessage = this.formatString(
          message.messageRequired,
          this.fieldName
        );
      }
      //validate email
      if (this.$attrs.type === "email" && this.value != "") {
        let isValidEmail = this.validateEmail(this.value);
        if (isValidEmail == false) {
          this.isInputError = true;
          this.inputError = message.messageEmailSyntax;
          this.$emit("getInputError", this.inputError);
        }
      }
    },
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.isInputError = false;
          self.$eventBus.$emit("hideTooltip");
          self.$emit("input", event.target.value);
        },
        blur: function (event) {
          if (self.required && (event.target.value === null || event.target.value === "")) {
            self.isInputError = true;
            self.errorMessage = self.formatString(
              message.messageRequired,
              self.fieldName
            );
          }
          self.$emit('blurInput', event.target.value);
        },
        mouseover: function (event) {
          if (self.isInputError) {
            // hiển thị tooltip báo lỗi
            let element = event.currentTarget;
            let elementRect = element.getBoundingClientRect();
            let top = elementRect.top;
            let left = elementRect.left;
            let tooltipData = {
              message: self.errorMessage,
              top: top,
              left: left,
              type: "error",
            };
            self.$eventBus.$emit("showTooltip", tooltipData);
          }
        },
        mouseout: function () {
          self.$eventBus.$emit("hideTooltip");
        },
        focus: function (event) {
          event.target.select();
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
    // value: function (newValue, oldValue) {
    //   if (this.required) {
    //     if (newValue != "") {
    //       this.isInputError = false;
    //     }
    //     if (oldValue != "" && newValue == "") {
    //       this.isInputError = true;
    //       this.errorMessage = this.formatString(
    //         message.messageRequired,
    //         this.fieldName
    //       );
    //     }
    //   }
    // },
    inputCheck: function () {
      this.validateInput();
    },
    inputErrorCustom: function (newValue) {
      this.isInputError = true;
      this.inputError = newValue;
    },
    inputReset: function (newValue) {
      if (newValue) {
        this.isInputError = false;
        this.inputError = false;
      }
    },
  },
  created() {
    this.$eventBus.$on("validateInput" + this.formName, () => {
      this.validateInput();
      if (this.isInputError) {
        let element = this.$refs.input;
        this.$eventBus.$emit("catchError" + this.formName, this.errorMessage, element);
      }
    });
    this.$eventBus.$on("showErrorInput" + this.inputName, ()=>{
      console.log("error custom");
    })
  },
  destroyed() {
    this.$eventBus.$off("validateInput" + this.formName);
    this.$eventBus.$off("showErrorInput"+this.inputName);
  },
};
</script>

<style scoped>
@import url("../../css/base/input.css");
</style>

