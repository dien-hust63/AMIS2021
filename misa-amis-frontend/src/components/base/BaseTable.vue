<template>
  <div class="ms-table-wrap">
    <table class="ms-table">
      <thead class="ms-thead">
        <tr>
          <th v-for="(item, index) in tableHeaders" :key="index">
            <base-checkbox
              v-if="item.type == 'checkbox'"
              @click.native="chooseCheckboxAll"
              :class="{ 'ms-checkbox--active': isChooseCheckboxAll }"
            />
            <div v-if="item.label">{{ item.label }}</div>
          </th>
        </tr>
      </thead>
      <tbody class="ms-tbody">
        <tr
          v-for="(tableContent, index) in tableContents"
          :key="index"
          :class="{ 'ms-table-row--selected': isSelectedRow(index) }"
        >
          <td
            v-for="(tableHeader, headerIndex) in tableHeaders"
            :key="headerIndex"
          >
            <base-checkbox
              v-if="tableHeader.type == 'checkbox'"
              @click.native="chooseTableRow(tableContent, index)"
              :class="{ 'ms-checkbox--active': isSelectedRow(index) }"
            />
            <div
              v-if="tableHeader.type == 'normal'"
              :class="getClass(tableContent, tableHeader)"
            >
              {{ formatTableContent(tableContent, tableHeader) }}
            </div>
            <div v-if="tableHeader.type == 'contextmenu'">
              <div class="row-context-menu">
                <div class="row-context-menu__text">Xem</div>
                <div
                  class="row-context-menu__icon"
                  @click="toogleContextMenu($event, tableContent, index)"
                  :class="{
                    'context-menu--selected': currentSelectedRow == index,
                  }"
                  :tabindex="0"
                >
                  <div class="mi mi-16 mi-arrow-up--blue"></div>
                </div>
              </div>
            </div>
            <div v-if="tableHeader.type == 'number'">{{ index + 1 }}</div>
            <div v-if="tableHeader.type == 'delete'" @click="deleteRow(index)">
              <div class="mi mi-16 mi-delete"></div>
            </div>
            <div v-if="tableHeader.type == 'combobox'">
              <base-combobox-custom :hasFooter="true" :hideAddIcon="true" />
            </div>
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
import BaseComboboxCustom from "./BaseComboboxCustom.vue";
import BaseCheckbox from "./BaseCheckbox.vue";
import moment from "moment";
import MixinListener from "../../mixins/listeners/listeners";
export default {
  name: "BaseTable",
  mixins: [MixinListener],
  components: {
    BaseCheckbox,
    BaseComboboxCustom,
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
  },
  data() {
    return {
      isShowContextMenu: false,
      currentSelectedRow: -1,
      customStyle: "",
      isChooseCheckboxAll: false,
      listSelectedRow: [],
      listSelectedContent: [],
      /**combobox props */
     
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
      // if(tableContent['is_mention'] == 1) this.customStyle = 'text--green ';
      // if(tableContent['is_mention'] == 0) this.customStyle = 'text--red ';

      let cellData;
      cellData = tableContent[tableHeader.fieldName];
      if (tableHeader.format == "date") {
        //format date
        cellData = moment(tableContent[tableHeader.fieldName]).format(
          "DD/MM/YYYY"
        );
      }
      if (tableHeader.format == "number") {
        if (cellData == "0") cellData = "0,0";
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
        this.currentSelectedRow = -1;
        this.$eventBus.$emit("hideContextMenu");
      }
    },
    /**trả về custom style cho cell */
    getClass(tableContent, tableHeader) {
      let customClass = "";
      if (tableContent["is_mention"] == 1) customClass = "text--green";
      if (tableContent["is_mention"] == 0) customClass = "text--black";
      let listCustomClass = `${customClass} ${tableHeader.textAlign}`;
      return listCustomClass;
    },
    /**
     * chọn tất cả check box
     * CreatedBy: nvdien(26/9/2021)
     */
    chooseCheckboxAll() {
      this.isChooseCheckboxAll = !this.isChooseCheckboxAll;
      if (this.isChooseCheckboxAll) {
        for (let i = 0; i < this.tableContents.length; ++i) {
          this.listSelectedRow.push(i);
          this.listSelectedContent.push(this.tableContents[i]);
        }
      } else {
        this.listSelectedRow = [];
        this.listSelectedContent = [];
      }
      this.$emit("getSelectedRowList", this.listSelectedContent);
    },
    /**
     * thêm hoặc bớt chỉ số của hàng vào trong danh sách các hàng được chọn
     * @param {Int} index chỉ số của hàng chọn
     * @param {object} tableContent nội dung hàng
     * CreatedBy: nvdien(26/9/2021)
     */
    chooseTableRow(tableContent, index) {
      const position = this.listSelectedRow.indexOf(index);
      if (position == -1) {
        this.listSelectedRow.push(index);
        this.listSelectedContent.push(tableContent);
      } else {
        this.listSelectedRow.splice(position, 1);
        this.listSelectedContent.splice(tableContent, 1);
      }
      this.$emit("getSelectedRowList", this.listSelectedContent);
    },
    /**
     * Kiểm tra xem index đã có trong danh sách hàng được chọn chưa
     * @param {Int} index index của hàng được chọn
     * @return {boolean} đúng nếu đã có trong đanh sách, sai nếu chưa co
     * author: nvdien(26/9/2021)
     */
    isSelectedRow(index) {
      return this.listSelectedRow.includes(index);
    },
    /**Xóa 1 dòng */
    deleteRow(index) {
      this.$emit("deleteRow", index);
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/table.css");
</style>
