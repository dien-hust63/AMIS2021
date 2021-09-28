<template>
  <div
    class="ms-combo-dropdown-panel"
    :style="positionOfPanel"
    :class="{ 'combo-dropdown-panel--show': isShowPanel }"
  >
    <div class="combo-dropdown-header">
      <table>
        <thead>
          <tr>
            <th
              v-for="(tableHeader, index) in tableHeaders"
              :key="index"
              :style="{ width: tableHeader.width }"
            >
              {{ tableHeader.label }}
            </th>
          </tr>
        </thead>
      </table>
    </div>
    <div class="combo-dropdown-body">
      <table>
        <tbody v-if="tableContents.length != 0">
          <tr
            class="combo-dropdown-row"
            v-for="(tableContent, index) in tableContents"
            :key="index"
            @click="bindValue(tableContent, index)"
            :class="{'combo-dropdown-row--active': currentIndex == index}"
          >
            <td
              class="combo-dropdown-row-td"
              v-for="(tableHeader, headerIndex) in tableHeaders"
              :key="headerIndex"
              :style="{ width: tableHeader.width }"
            >
              {{ tableContent[tableHeader.fieldName] }}
            </td>
          </tr>
        </tbody>
        <tbody v-if="tableContents.length == 0">
          <tr
            class="combo-dropdown-row null-data"
          >
            <td>
             Không có dữ liệu hiển thị
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="combo-dropdown-loading" v-show="isLoading">
      <div class="combo-dropdown-loading-icon"></div>
    </div>
    <div class="combo-dropdown-footer" v-if="hasFooter">
      <div class="btn-add-combo-dropdown">
        <div class="mi mi-16 mi-plus--success"></div>
        <div class="btn-add-text">Thêm mới</div>
      </div>
    </div>
  </div>
</template>

<script>
import MethodMixin from "../../mixins/methods";
export default {
  name: "BaseComboDropdown",
  mixins: [MethodMixin],
  created() {
    /**
     * Tạo event hiển thị context menu
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("showComboDropdown", (data) => {
      this.isShowPanel = true;
      /**loading */
      this.isLoading = data["isLoading"];
      /**table */
      // console.log( data["tableHeaders"]);
      if (data["tableHeaders"] != null) {
        this.tableHeaders = data["tableHeaders"];
      }
      this.tableContents = data["tableContents"];
      /**footer */
      this.hasFooter = data["hasFooter"];
      /**position */
      
      if (data["position"] != null) {
        this.top = data["position"]["top"];
        this.left = data["position"]["left"];
        this.topChange = data["position"]["topChange"];
        this.leftChange = data["position"]["leftChange"];
      }
      /**element call */
      // this.$nextTick(() => {
      //   this.setPositionPanel();
      // });
    });
    /**
     * Tạo event ẩn context menu
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("hideComboDropdown", () => {
      this.$eventBus.$off("comboboxListener");
      this.isShowPanel = false;
    });
    /**
     * loading Combo dropdown
     */
    this.$eventBus.$on("loadingComboDropdown", (data) => {
      this.isLoading = data;
    });
  },
  data() {
    return {
      isLoading: false,
      isShowPanel: false,
      /**table */
      tableContents: [],
      tableHeaders: [],
      /**footer */
      hasFooter: null,
      /**position */
      top: 0,
      left: 0,
      topChange: 0,
      leftChange: 0,
      /*element call*/
      elementCall: null,
      /**listenFunction */
      currentIndex: -1,
    };
  },
  methods: {
    /**gán giá trị */
    bindValue(tableContent, index) {
      this.currentIndex = index;
      this.$eventBus.$emit("comboboxListener", tableContent);
    },
  },
  computed: {
    positionOfPanel: function () {
      return {
        top: this.top + this.topChange + "px",
        left: this.left + this.leftChange + "px",
      };
    },
  },
  destroyed() {
    /**
     * Huỷ các sự kiện
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$off("showComboDropdown");
    this.$eventBus.$off("hideComboDropdown");
    this.$eventBus.$off("loadingComboDropdown");
  },
};
</script>

<style scoped>
@import url("../../css/base/combodropdown.css");
</style>