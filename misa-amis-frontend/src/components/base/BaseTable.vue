<template>
  <div class="ms-table-wrap">
    <table class="ms-table">
      <thead class="ms-thead">
        <tr>
          <th v-for="(item, index) in tableHeaders" :key="index">
            <base-checkbox v-if="item.type == 'checkbox'" />
            <div v-if="item.label">{{ item.label }}</div>
          </th>
        </tr>
      </thead>
      <tbody class="ms-tbody">
        <tr v-for="(tableContent, index) in tableContents" :key="index">
          <td v-for="(tableHeader, headerIndex) in tableHeaders" :key="headerIndex">
            <base-checkbox v-if="tableHeader.type == 'checkbox'" />
            <div
              v-if="tableHeader.type == 'normal'"
              :class="[tableHeader.textAlign, styleCustom]"
            >
              {{ formatTableContent(tableContent, tableHeader) }}
            </div>
            <div v-if="tableHeader.type == 'contextmenu'">
              <div class="row-context-menu">
                <div class="row-context-menu__text">Xem</div>
                <div
                  class="row-context-menu__icon"
                  @click="toogleContextMenu($event, tableContent, index)"
                  :class="{ 'context-menu--selected': currentSelectedRow == index }"
                  :tabindex="0"
                >
                  <div class="mi mi-16 mi-arrow-up--blue"></div>
                </div>
              </div>
            </div>
            <div v-if="tableHeader.type == 'number'">{{ index + 1 }}</div>
          </td>
        </tr>
      </tbody>
      <tfoot class="ms-tfoot" v-if="hasFooter">
        <tr>
          <td
            v-for="(item, index) in tableHeaders"
            :key="index"
            :class="[item.textAlign]"
          >
            <div v-if="item.footerValue != null">{{ item.footerValue }}</div>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</template>


<script>
import BaseCheckbox from "./BaseCheckbox.vue";
import moment from "moment";
import MixinListener from "../../mixins/listeners/listeners";
export default {
  name: "BaseTable",
  mixins: [MixinListener],
  components: {
    BaseCheckbox,
  },
  props: {
    tableHeaders: {
      type: Array,
      default() {
        return [];
      },
    },
    tableContents: {
      type: Array,
      default() {
        return [];
      },
    },
    hasFooter: {
      type: Boolean,
      default() {
        return false;
      },
    },
    contextMenuData: {
      type: Object,
      default() {
        return {
          topChange: 0,
          leftChange: 0,
          contextFunction: [],
        };
      },
    },
    styleCustom: {
      type: String,
      default(){
        return "";
      }
    }
  },
  data() {
    return {
      isShowContextMenu: false,
      currentSelectedRow: -1,
    };
  },
  methods: {
    /**
     * Định dạng lại giá trị trong ô bảng
     * @param {Object} tableContent : chứa thông tin api trả về
     * @param {Object} tableHeader: chứa thông tin  Header của bảng
     * return chuỗi chứa giá trị được đổ lên bảng theo đúng định dạng
     * CreatedBy: nvdien(24/9/2021)
     */
    formatTableContent(tableContent, tableHeader) {
      let cellData;
      cellData = tableContent[tableHeader.fieldName];
      if (tableHeader.format == "date") {
        //format date
        cellData = moment(tableContent[tableHeader.fieldName]).format(
          "DD/MM/YYYY"
        );
      }
      return cellData;
    },
    /**
     * hiển thị context menu
     * CreatedBy: nvdien(25/9/2021)
     */
    toogleContextMenu(event, tableContent, index) {
      this.currentSelectedRow = index;
      this.$emit("getContextTableData", tableContent);
      this.isShowContextMenu = !this.isShowContextMenu;
      if (this.isShowContextMenu) {
        let element = event.target;
        let elementRect = element.getBoundingClientRect();
        let elementPos = {
          top: elementRect.top,
          left: elementRect.left,
          topChange: this.contextMenuData["topChange"],
          leftChange: this.contextMenuData["leftChange"],
        };
        this.$eventBus.$emit("showContextMenu", {
          contextFunction: this.contextMenuData["contextFunction"],
          pos: elementPos,
        });
      } else {
        this.$eventBus.$emit("hideContextMenu");
      }
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/table.css");
</style>
