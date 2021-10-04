<template>
  <div class="ms-combobox-custom">
    <label class="combobox-custom-label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div
      class="combobox-custom-content"
      :class="{
        'combobox-custom--active': isActive,
        'combobox-custom--error': isError,
      }"
      @blur="isActive = false"
      :tabindex="0"
    >
      <div class="combobox-custom-input">
        <input type="text" v-on="inputListeners" :value="value" ref="input" />
      </div>
      <div class="combobox-custom-actions">
        <div
          class="btn-add btn-actions"
          @click="showAddForm"
          v-show="!hideAddIcon"
        >
          <div class="mi mi-16 mi-plus--success"></div>
        </div>
        <div
          class="btn-dropdown btn-actions"
          @click="showComboDropdown($event)"
          v-show="!hideDropdownIcon"
        >
          <div class="mi mi-16 mi-arrow-dropdown"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import MethodMixin from "../../mixins/methods";
export default {
  name: "BaseComboboxCustom",
  mixins: [MethodMixin],
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
    formName: {
      type: String,
      default: "",
    },
    value: {
      type: String,
      default: "",
    },
    hasFooter: {
      type: Boolean,
      default() {
        return false;
      },
    },
    hideDropdownIcon: {
      type: Boolean,
      default() {
        return false;
      },
    },
    hideAddIcon: {
      type: Boolean,
      default() {
        return false;
      },
    },
    comboboxProps: {
      type: Object,
      default() {
        return {
          api: "",
          tableObject: "",
          valueField: "",
          mode: "",
        };
      },
    },
  },
  data() {
    return {
      isShowComboDropdown: false,
      inputValue: "",
      timeDelaySearch: null,
      posistion: {},
      isActive: false,
      isError: false,
      errorMessage: "",
    };
  },

  methods: {
    /**
     * focus vào ô input
     * CreatedBy: nvdien(2/10/2021)
     */
    focusInput() {
      this.$nextTick(() => {
        this.$refs.input.focus();
      });
    },
    /**
     * Hiển thị combo dropdown panel
     * CreatedBy: nvdien(26/9/2021)
     */
    showComboDropdown(event) {
      this.isError = false;
      this.$eventBus.$emit("hideTooltip");
      this.isShowComboDropdown = !this.isShowComboDropdown;
      if (this.isShowComboDropdown) {
        this.isActive = true;
        let element = event.currentTarget;
        let elementRect = element.getBoundingClientRect();
        let topChange = element.clientHeight + 4;
        let leftChange =
          -this.findParentByClass(element, "combobox-custom-content")
            .clientWidth + element.clientWidth;
        let elementPos = {
          top: elementRect.top,
          left: elementRect.left,
          topChange: topChange,
          leftChange: leftChange,
        };
        this.position = elementPos;
        if (this.comboboxProps.mode == "api") {
          axios
            .get(this.formatString(this.comboboxProps.api, "", 1, 20))
            .then((response) => {
              let comboDropdownData = {
                tableHeaders: this.comboboxProps.tableHeaders,
                tableContents: response.data[this.comboboxProps.tableObject],
                hasFooter: this.hasFooter,
                position: elementPos,
                comboboxProps: this.comboboxProps,
              };
              this.$eventBus.$emit("showComboDropdown", comboDropdownData);
              this.$eventBus.$on("comboboxListener", (data) => {
                this.$emit("getDataEventBus", data);
                this.$eventBus.$emit("hideComboDropdown");
              });
            })
            .catch((response) => console.log(response));
        }
        if (this.comboboxProps.mode == "manual") {
          let comboDropdownData = {
            tableHeaders: this.comboboxProps.tableHeaders,
            tableContents: this.comboboxProps.tableContents,
            hasHeader: false,
            hasFooter: false,
            position: elementPos,
          };
          this.$eventBus.$emit("showComboDropdown", comboDropdownData);
          this.$eventBus.$on("comboboxListener", (data) => {
            this.$emit("getDataEventBus", data);
            this.$eventBus.$emit("hideComboDropdown");
          });
        }
      } else {
        this.$eventBus.$emit("hideComboDropdown");
      }
    },
    /**Hiển thị form thêm */
    showAddForm() {
      
      this.$emit("showAddForm");
    },
    /**filter combobox khách hàng */
    searchCombobox(event, searchValue) {
      //form chưa mở
      if (!this.isShowComboDropdown) {
        this.isShowComboDropdown;
        if (this.timeDelaySearch) {
          clearTimeout(this.timeDelaySearch);
        }
        this.timeDelaySearch = setTimeout(() => {
          let element = event.target;
          let elementRect = element.getBoundingClientRect();
          let elementPos = {
            top: elementRect.top,
            left: elementRect.left,
            topChange: 32,
            leftChange: -12,
          };
          //filter
          axios
            .get(this.formatString(this.comboboxProps.api, searchValue, 1, 20))
            .then((response) => {
              let comboDropdownData = {
                tableHeaders: this.comboboxProps.tableHeaders,
                tableContents: response.data[this.comboboxProps.tableObject],
                hasFooter: this.hasFooter,
                position: elementPos,
              };
              this.$eventBus.$emit("showComboDropdown", comboDropdownData);

              //bắt sự kiện khi click vào dòng trên combo dropdown panel
              this.$eventBus.$on("comboboxListener", (data) => {
                this.$emit("getDataEventBus", data);
                this.$eventBus.$emit("hideComboDropdown");
                this.isShowComboDropdown = false;
              });
            })
            .catch((response) => console.log(response));
        }, 500);
        return;
      }
      //form mở sẵn rồi
      //không cập nhật header
      //không cần cập nhật lại vị trí
      if (this.isShowComboDropdown) {
        if (this.timeDelaySearch) {
          clearTimeout(this.timeDelaySearch);
        }
        this.timeDelaySearch = setTimeout(() => {
          axios
            .get(this.formatString(this.comboboxProps.api, searchValue, 1, 20))
            .then((response) => {
              this.$eventBus.$emit("loadingComboDropdown", false);
              let comboDropdownData = {
                tableContents: response.data[this.comboboxProps.tableObject],
                hasFooter: this.hasFooter,
              };
              console.log(comboDropdownData);
              this.$eventBus.$emit("showComboDropdown", comboDropdownData);

              //bắt sự kiện khi click vào dòng trên combo dropdown panel
              this.$eventBus.$on("comboboxListener", (data) => {
                this.$emit("getDataEventBus", data);
                this.$eventBus.$emit("hideComboDropdown");
                this.isShowComboDropdown = false;
              });
            })
            .catch((response) => console.log(response));
          this.$eventBus.$emit("loadingComboDropdown", true);
        }, 500);
      }
    },
    /**
     * Validate Combobox
     * CreatedBy: nvdien(3/10/2021)
     */
    validateCombobox() {
      if (
        this.comboboxProps.isRequired &&
        (this.value === null || this.value === "")
      ) {
        this.isError = true;
        this.errorMessage = this.formatString(
          this.$resourcesVN.message.messageRequired,
          this.comboboxProps.fieldName
        );
      }
    },
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.isError = false;
          self.$eventBus.$emit("hideTooltip");
          self.searchCombobox(event, event.target.value);
          self.$emit("input", event.target.value);
        },
        focus: function (event) {
          event.target.select();
          self.isActive = true;
        },
        blur: function () {
          self.isActive = false;
        },
        mouseover: function (event) {
          if (self.isError) {
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
      });
    },
  },
  watch:{
    value: function(newvalue){
      if(this.comboboxProps.isRequired &&
        (newvalue != null)){
        this.isError = false;
        this.$eventBus.$emit("hideTooltip");
      }
    }
  },
  created() {
    this.$eventBus.$on("validateCombobox" + this.formName, () => {
      this.validateCombobox();
      if (this.isError) {
        this.$eventBus.$emit("catchError" + this.formName, this.errorMessage);
      }
    });
  },
  destroyed() {
    this.$eventBus.$off("validateCombobox"+this.formName);
  },
};
</script>

<style scoped>
@import url("../../css/base/comboboxcustom.css");
</style>