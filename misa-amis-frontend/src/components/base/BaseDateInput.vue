<template>
  <div class="ms-input ms-input-date">
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
      <v2-datepicker
        class="datepicker"
        format="DD/MM/YYYY"
        :lang="lang"
        :customLocals="locals"
        :picker-options="datePickerOptions"
        placeholder="DD/MM/YYYY"
        v-on:input="inputDatePicker($event)"
        ref="datePicker"
      >
      </v2-datepicker>
      <input
        type="text"
        class="input-date"
        placeholder="DD/MM/YYYY"
        v-model="valueDateInput"
        @mouseover="mouseoverDateInput($event)"
        @mouseout="mouseoutDateInput()"
        v-on:keyup="validateDateInput"
        @focus="focusInputDate($event)"
        @blur="blurDateInput"
        ref="input"
      />
      <!-- <div
        class="ms-input__error"
        :class="{ 'ms-input__error--show': isShowError }"
      >
        {{ inputError }}
      </div> -->
    </div>
  </div>
</template>

<script>
import Mixin from "../../mixins/methods.js";
import { message } from "../../js/resources/resourcevn.js";
import locals from "../../js/locales/locales.js";
import moment from "moment";
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
    formName: {
      type: String,
      default: "",
    },
    fieldName: {
      type: String,
      default: "",
    },
    value: {
      type: [String, Number],
      default: "",
    },
    defaultValue: {
      type: String,
      default: "",
    },
    inputName: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      isInputError: false,
      isShowError: false,
      errorMessage: "",
      valueDateInput: this.defaultValue,
      notWatch: false,
      isFocusInput: false,
      lang: "it",
      locals,
      //Max date là ngày hiện tại
      datePickerOptions: {
        disabledDate(date) {
          return date > new Date();
        },
      },
    };
  },
  methods: {
    /**
     * lấy giá trị từ date picker và emit lên
     * @param {datetime} e: datetime lấy được từ datepicker
     * CreatedBy: nvdien(3/9/2021)
     */
    inputDatePicker(e) {
      this.$emit("input", moment(e).format("YYYY-MM-DD"));
    },

    /**
     * validate input
     * CreatedBy: nvdien(2/9/2021)
     */
    // validateInput() {
    //   //các trường không để trống
    //   if (this.required && (this.value === null || this.value === "")) {
    //     this.isInputError = true;
    //     this.errorMessage = this.formatString(
    //       message.messageRequired,
    //       this.fieldName
    //     );
    //   }
    // },
    /**
     * mouseover input date
     * CreatedBy: nvdien(3/9/2021)
     */
    mouseoverDateInput(event) {
      if (this.isInputError) {
        // hiển thị tooltip báo lỗi
        let element = event.currentTarget;
        let elementRect = element.getBoundingClientRect();
        let top = elementRect.top;
        let left = elementRect.left;
        let tooltipData = {
          message: this.errorMessage,
          top: top,
          left: left,
          type: "error",
        };
        this.$eventBus.$emit("showTooltip", tooltipData);
      }
    },
    /**
     * mouseout input date
     * CreatedBY: nvdien(2/9/2021)
     */
    mouseoutDateInput() {
          this.$eventBus.$emit("hideTooltip");
    },
    /**
     * validate input date
     * CreatedBY: nvdien(2/9/2021)
     */
    validateDateInput() {
      //các trường không để trống
      if (
        this.required &&
        (this.valueDateInput === null || this.valueDateInput === "")
      ) {
        this.isInputError = true;
        this.isShowError = true;
        this.errorMessage = this.formatString(
          message.messageRequired,
          this.fieldName
        );
        this.$emit("getDate", null);
        return;
      }
      if (this.validateDateString(this.valueDateInput)) {
        if (this.isFutureDate(this.valueDateInput)) {
          this.isInputError = true;
          this.errorMessage = this.formatString(
            message.messageDateFuture,
            this.label
          );
          return;
        }
        this.isInputError = false;
        this.isShowError = false;
        this.$emit(
          "getDate",
          moment(this.valueDateInput, "DD/MM/YYYY").format("YYYY-MM-DD")
        );
      } else {
        this.isInputError = true;
        this.errorMessage = message.messageDateSyntax;
      }
    },
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
     * focus vào input date
     * CreatedBy: nvdien(2/9/2021)
     */
    focusInputDate(event) {
      this.isFocusInput = true;
      event.target.select();
    },
    /**
     * blur dateInput
     */
    blurDateInput() {
      this.isFocusInput = false;
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
          this.errorMessage = this.formatString(
            message.messageRequired,
            this.fieldName
          );
        }
      }
      if (this.$attrs.type == "date") {
        if (newValue != null && newValue != "") {
          this.valueDateInput = moment(newValue, "YYYY-MM-DD").format(
            "DD/MM/YYYY"
          );
        } else {
          this.valueDateInput = "";
        }
      }
    },
    valueDateInput(newValue) {
      if (newValue) {
        if (this.validateDateString(newValue)) {
          this.$emit(
            "getDate",
            moment(newValue, "DD/MM/YYYY").format("YYYY-MM-DD")
          );
          return;
        }
      }
      this.$emit("getDate", null);
    },
  },
  created() {
    this.$eventBus.$on("validateInput" + this.formName, () => {
      this.validateDateInput();
      if (this.isInputError) {
        let element = this.$refs.input;
        this.$eventBus.$emit(
          "catchError" + this.formName,
          this.errorMessage,
          element
        );
      }
    });
    this.$eventBus.$on("showErrorDate" + this.inputName, (content) => {
      this.isInputError = true;
      this.errorMessage = content;
    });
  },
  destroyed() {
    this.$eventBus.$off("validateInput" + this.formName);
    this.$eventBus.$off("showErrorDate" + this.inputName);
  },
};
</script>

<style scoped>
@import url("../../css/base/dateinput.css");
</style>

