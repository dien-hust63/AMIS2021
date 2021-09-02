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
              v-for="(item, index) in comboboxListData"
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
        Tên không được phép để trông
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
  },
  data() {
    return {
      isShowList: false,
      comboboxListData: [],
      comboboxValue: "",
      comboboxData: "",
      currentIndex: -1,
      isFocusInput: false,
      isError: false,
      isShowError: false,
    };
  },
  methods: {
    /**
     * Ẩn hiện combobox list
     * CreatedBy: nvdien(1/9/2021)
     */
    toogleList() {
      this.isShowList = !this.isShowList;
      this.isFocusInput = true;
      this.$nextTick(() => {
        this.$refs.input.focus();
      });
      DepartmentApi.getAll()
        .then((response) => (this.comboboxListData = response.data))
        .catch((response) => console.log(response));
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
      this.$emit("getComboboxData", this.comboboxData);
    },
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.$emit("input", event.target.value);
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
};
</script>

<style scoped>
@import url("../../css/base/combobox.css");
</style>