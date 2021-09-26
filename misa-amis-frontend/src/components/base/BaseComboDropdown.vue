<template>
  <div class="ms-combo-dropdown-panel" :style="positionOfPanel" :class="{ 'combo-dropdown-panel--show': isShowPanel}" >
    <div class="combo-dropdown-header">
      <table>
        <thead>
          <tr>
            <th :style="{ width: '100px' }">Mã kho</th>
            <th :style="{ width: '200px' }">Tên kho</th>
          </tr>
        </thead>
      </table>
    </div>
    <div class="combo-dropdown-body">
      <table>
        <tbody>
          <tr class="combo-dropdown-row">
            <td class="combo-dropdown-row-td" :style="{ width: '100px' }">
              001
            </td>
            <td class="combo-dropdown-row-td" :style="{ width: '200px' }">
              KHO THỰC PHẨM TƯƠI
            </td>
          </tr>

          <tr class="combo-dropdown-row">
            <td class="combo-dropdown-row-td" :style="{ width: '100px' }">
              001
            </td>
            <td class="combo-dropdown-row-td" :style="{ width: '200px' }">
              KHO THỰC PHẨM TƯƠI
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="combo-dropdown-loading" v-if="isLoading">
      <div class="combo-dropdown-loading-icon"></div>
    </div>
    <div class="combo-dropdown-footer">
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
      /**table */
      this.tableHeaders = data["tableHeaders"];
      this.tableContents = data["tableContents"];
      /**footer */
      this.hasFooter = data["hasFooter"];
      /**position */
      this.top = data["position"]["top"];
      this.left = data["position"]["left"];
      this.topChange = data["position"]["topChange"];
      this.leftChange = data["position"]["leftChange"];

      this.$nextTick(() => {
        this.setPositionPanel();
      });
    });
    /**
     * Tạo event ẩn context menu
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("hideComboDropdown", () => {
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
      left:0,
      topChange: 0,
      leftChange: 0,
    };
  },
  computed: {
      positionOfPanel: function(){
          return {
                top: this.top + this.topChange +'px',
                left: this.left + this.leftChange + 'px'
            }
      }
  }
};
</script>

<style scoped>
@import url("../../css/base/combodropdown.css");
</style>