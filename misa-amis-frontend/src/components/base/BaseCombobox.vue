<template>
  <div
    class="ms-combobox"
    :class="{
      'ms-combox-list--show': isShowList,
      'ms-combobox--focus': isFocusInput,
      'ms-combobox--error': isError,
    }"
  >
    <label class="ms-combobox__label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div class="ms-combobox-content" v-on="comboboxListeners">
      <div class="ms-combobox-input">
        <input
          type="text"
          v-model="comboboxValue"
          ref="input"
          v-bind="$attrs"
          v-on="inputListeners"
        />
      </div>
      <div class="ms-combobox-icon" @click="toogleList">
        <div class="mi mi-16 mi-arrow-dropdown"></div>
      </div>
      <div class="ms-combobox-list">
        <table class="combobox-table">
          <thead class="combobox-table-thead">
            <tr class="combobox-table-row">
              <th class="combobox-table-header">Mã đơn vị</th>
              <th class="combobox-table-header">Tên đơn vị</th>
            </tr>
          </thead>
          <tbody class="combobox-table-tbody">
            <tr
              class="combobox-table-row"
              v-for="(item, index) in comboboxListShow"
              :key="index"
              @click="
                chooseItem(index, item['DepartmentId'], item['DepartmentName'])
              "
              :class="{ 'item--selected': currentIndex == index }"
            >
              <td class="combobox-table-cell">{{ item["DepartmentCode"] }}</td>
              <td class="combobox-table-cell">{{ item["DepartmentName"] }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="block-error" :class="{ 'block-error--show': isShowError }">
        {{ comboboxError }}
      </div>
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../js/repository/repository.factory.js";
const DepartmentApi = RepositoryFactory.get("departments");
export default {
  name: "BaseCombobox",
  props: {
    label: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
    comboboxDataProp: {
      type: String,
      default: "",
    },
    comboboxCheck: {
      type: Boolean,
      default: false,
    },
    comboboxReset: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      isShowList: false,
      comboboxListData: [],
      comboboxListShow: [],
      comboboxValue: "",
      comboboxData: "",
      currentIndex: -1,
      isFocusInput: false,
      isError: false,
      isShowError: false,
      comboboxError: "",
    };
  },
  mounted() {
     DepartmentApi.getAll()
        .then((response) => (this.comboboxListData = response.data))
        .catch((response) => console.log(response));
  },
  methods: {
    /**
     * Ẩn hiện combobox list
     * CreatedBy: nvdien(1/9/2021)
     */
    toogleList() {
      this.isError = false;
      this.isShowError = false;
      this.isShowList = !this.isShowList;
      if(this.isShowList){
        this.comboboxListShow = this.comboboxListData;
      }
      this.isFocusInput = true;
      this.$nextTick(() => {
        this.$refs.input.focus();
      });
    },
    /**
     * Chọn item
     * @param index: index của item trong list combobox
     * @param itemData: giá trị của item
     * @param itemVaue: id của item
     * CreatedBy:nvdien(1/9/2021)
     */
    chooseItem(index, itemData, itemValue) {
      this.currentIndex = index;
      this.comboboxValue = itemValue;
      this.comboboxData = itemData;
      this.isShowList = false;
      this.isError = false;
      this.$emit("getComboboxData", this.comboboxData);
    },
    /**
     * validate input
     * CreatedBy: nvdien(2/9/2021)
     */
    validateInput(self) {
      if (
        self.required &&
        (self.comboboxValue === null || self.comboboxValue === "")
      ) {
        this.isError = true;
        this.comboboxError = `${this.label} không được phép để trống`;
        this.$emit("getComboboxError", this.comboboxError);
      }
    },
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.isShowList = true;
          let filterString = event.target.value;
          let listData = self.comboboxListData;
          self.comboboxListShow = listData.filter(item => item['DepartmentName'].toLowerCase().includes(filterString.toLowerCase()));
        },
        focus: function () {
          self.isFocusInput = true;
        },
        blur: function () {
          self.isFocusInput = false;
          // self.isShowList = false;
        },
      });
    },
    comboboxListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        mouseover: function () {
          if (self.isError) {
            self.isShowError = true;
          }
        },
        mouseout: function () {
          self.isShowError = false;
        },
      });
    },
  },
  watch: {
    comboboxDataProp: function (newValue) {
      //update combobox value
      DepartmentApi.getById(newValue)
        .then((response) => {
          this.comboboxValue = response.data["DepartmentName"];
        })
        .catch((response) => console.log(response));
    },
    comboboxCheck: function () {
      //validate combobox Input
      this.validateInput(this);
    },
    comboboxReset: function (newValue) {
      if (newValue) {
        //reset input
        this.comboboxValue = "";
        this.comboboxData = "";
        this.isError = false;
        this.isShowError = false;
      }
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/combobox.css");
</style>