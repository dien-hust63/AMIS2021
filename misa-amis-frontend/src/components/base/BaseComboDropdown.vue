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
        <tbody>
          <tr
            class="combo-dropdown-row"
            v-for="(tableContent, index) in tableContents"
            :key="index"
            @click="bindValue(tableContent)"
          >
            <td
              class="combo-dropdown-row-td"
              v-for="(tableHeader, headerIndex) in tableHeaders"
              :key="headerIndex"
              :style="{ width: tableHeader.width }"
            >
              {{tableContent[tableHeader.fieldName]}}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="combo-dropdown-loading" v-if="isLoading">
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
export default {
  name: "BaseComboDropdown",
  created() {
    /**
     * Tạo event hiển thị context menu
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("showComboDropdown", (data) => {
      this.isShowPanel = true;
      /**loading */
      this.isLoading = data["position"]["isLoading"];
      /**table */
      this.tableHeaders = data["tableHeaders"];
      if(!this.isLoading){
        this.tableContents = data["tableContents"];
      }
      /**footer */
      this.hasFooter = data["hasFooter"];
      /**position */
      this.top = data["position"]["top"];
      this.left = data["position"]["left"];
      this.topChange = data["position"]["topChange"];
      this.leftChange = data["position"]["leftChange"];
      /**element call */
      this.functionEmit = data["functionEmit"];
      console.log(this.functionEmit);
      // this.$nextTick(() => {
      //   this.setPositionPanel();
      // });
    });
    /**
     * Tạo event ẩn context menu
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("hideComboDropdown", () => {
      this.$eventBus.$off(this.functionEmit);
      this.isShowPanel = false;
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
      listenFunction: "",
    };
  },
  methods:{
    /**gán giá trị */
    bindValue(tableContent){
      this.$eventBus.$emit(this.functionEmit,tableContent);
    }
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
		},
};
</script>

<style scoped>
@import url("../../css/base/combodropdown.css");
</style>