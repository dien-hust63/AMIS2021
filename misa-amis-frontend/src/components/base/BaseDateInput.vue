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
        @mouseover="mouseoverDateInput"
        @mouseout="mouseoutDateInput"
        v-on:keyup="validateDateInput"
        @focus="focusInputDate($event)"
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
import {message} from "../../js/resources/resourcevn.js";
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
    validateInput(self) {
      if (self.required && (self.value === null || self.value === "")) {
        this.isInputError = true;
        this.inputError = this.formatString(message.messageRequired, this.label);
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
      if (this.valueDateInput === "") {
        this.isInputError = false;
        this.isShowError = false;
        this.$emit("getDate", null);
        return;
      }
      if (this.validateDateString(this.valueDateInput)) {
        if (this.isFutureDate(this.valueDateInput)) {
          this.isInputError = true;
          this.inputError =this.formatString(message.messageDateFuture, this.label);
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
        this.inputError = message.messageDateSyntax;
      }
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
           this.inputError = this.formatString(message.messageRequired, this.label);
        }
      }
      if (this.$attrs.type == "date") {
        if (newValue != null && newValue != "") {
          this.valueDateInput = moment(newValue, "YYYY-MM-DD").format(
            "DD/MM/YYYY"
          );
        }
        else{
          this.valueDateInput = "";
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

