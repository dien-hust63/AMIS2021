<template>
  <div
    class="ms-combobox"
    :class="{
      'ms-combox-list--show': isShowList,
      'ms-combobox--focus': isFocusInput,
    }"
  >
    <label class="ms-combobox__label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div class="ms-combobox-content">
      <div class="ms-combobox-input">
        <input
          type="text"
          v-model="comboboxValue"
          ref="input"
          v-bind="$attrs"
          v-on="inputListeners"
        />
      </div>
      <div class="ms-combobox-icon">
        <div class="mi mi-16 mi-arrow-dropdown" @click="toogleList"></div>
      </div>
      <div class="ms-combobox-list">
        <table class="combobox-table">
          <thead>
            <tr>
              <th>Mã đơn vị</th>
              <th>Tên đơn vị</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(item, index) in comboboxListData"
              :key="index"
              @click="
                chooseItem(index, item['DepartmentId'], item['DepartmentName'])
              "
              :class="{ 'item--selected': currentIndex == index }"
            >
              <td>{{ item["DepartmentCode"] }}</td>
              <td>{{ item["DepartmentName"] }}</td>
            </tr>
          </tbody>
        </table>
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
      type: String,
      default: "",
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
    };
  },
  methods: {
    toogleList() {
      this.isShowList = !this.isShowList;
      this.isFocusInput = true;
      DepartmentApi.getAll()
        .then((response) => (this.comboboxListData = response.data))
        .catch((response) => console.log(response));
    },
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
        blur: function () {
            console.log("blur");
          self.isFocusInput = false;
        },
        focus: function () {
          self.isFocusInput = true;
        },
      });
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/combobox.css");
</style>