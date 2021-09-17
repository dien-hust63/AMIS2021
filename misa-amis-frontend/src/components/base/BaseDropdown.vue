<template>
  <div class="ms-dropdown-wrap">
    <div class="ms-dropdown-label" v-if="label">{{ label }}</div>
    <div
      class="ms-dropdown"
      :class="{
        'seleted-options--active': isFocusOptions,
        'selected-options--show': isShowList,
      }"
      @blur="blurOptions"
      @focus="focusOptions"
      tabindex="0"
    >
      <div class="selected-options">
        <span>{{ currentValue }}</span>
      </div>
      <div class="dropdown-icon" @click="toogleList">
        <div class="mi mi-16 mi-arrow-dropdown"></div>
      </div>
      <ul class="selected-options__list">
        <li
          v-for="(item, index) in dropdownList"
          :key="index"
          class="selected-options__item"
          @click="chooseOptions(index)"
          :class="{ 'selected-options__item--active': currentIndex == index }"
        >
          {{ item["value"] }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseDropdown",
  mounted() {
    this.currentValue = this.defaultData.value;
    this.dropdownData = this.defaultData.data;
  },
  props: {
    dropdownList: {
      type: Array,
      default: function () {
        return [];
      },
    },
    label: {
      type: String,
      default: "",
    },
    defaultData: {
      type: Object,
      default: function () {
        return {};
      },
    },
  },
  data() {
    return {
      currentIndex: this.dropdownList.indexOf(this.defaultData),
      isShowList: false,
      isFocusOptions: false,
      currentValue: "",
      dropdownData: "",
    };
  },
  methods: {
    /**Chon item từ dropdown
     * @param {int} index
     * CreatedBy: ndien(17/9/2021)
     */
    chooseOptions(index) {
      this.isShowList = false;
      this.currentIndex = index;
      this.currentValue = this.dropdownList[index].value;
      this.dropdownData = this.dropdownList[index].data;
      this.$emit("input", this.dropdownData);
    },
    /**
     * Xử lí khi blur ra khỏi dropdown
     * CreatedBy: nvdien(17/9/2021)
     */
    blurOptions() {
      this.isShowList = false;
      this.isFocusOptions = false;
    },
    /**
     * Xử lí khi focus vào dropdown
     * CreatedBy: nvdien(31/8/2021)
     */
    focusOptions() {
      this.isFocusOptions = true;
    },

    /**
     * ẩn hiện danh sách options khi click vào icon dropdown
     * CreatedBy: nvdien(30/8/2021)
     */
    toogleList() {
      this.isShowList = !this.isShowList;
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/dropdown.css");
</style>