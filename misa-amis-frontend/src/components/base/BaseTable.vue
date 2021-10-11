<template>
  <div class="ms-table-wrap">
    <table class="ms-table" :class="{ 'ms-table-readonly': isReadOnly }">
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
          v-show="tableContent['state'] != 2"	
        >
          <td
            v-for="(tableHeader, headerIndex) in tableHeaders"
            :key="headerIndex"
            @click="handleClickCell(tableHeader, tableContent)"
          >
            <base-checkbox
              v-if="tableHeader.type == 'checkbox'"
              @click.native="chooseTableRow(tableContent, index)"
              :class="{ 'ms-checkbox--active': isSelectedRow(index) }"
            />
            <div
              v-if="tableHeader.type == 'normal'"
              :class="[
                { 'text--green': !tableContent['is_mention'] },
                tableHeader['textAlign'],
              ]"
            >
              {{ formatTableContent(tableContent, tableHeader) }}
            </div>
            <div v-if="tableHeader.type == 'contextmenu'">
              <div class="row-context-menu">
                <div
                  class="row-context-menu__text"
                  @click="showInContext(tableContent)"
                >
                  Xem
                </div>
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
            <div v-if="tableHeader.type == 'number'">{{tableDisplay.indexOf(tableContent) + 1}}</div>
            <div v-if="tableHeader.type == 'delete'" @click="deleteRow(index)">
              <div class="mi mi-16 mi-delete"></div>
            </div>
            <div v-if="tableHeader.type == 'comboboxapi'">
              <base-combobox-custom
                :hasFooter="true"
                :hideAddIcon="true"
                :value="tableContents[index][tableHeader.fieldName]"
                :comboboxProps="renderComboboxProps(tableHeader, index)"
                @getDataEventBus="
                  bindingCombobox(index, tableHeader, ...arguments)
                "
                :class="{ 'ms-combobox-custom--readonly': isReadOnly }"
                :formName="formName"
              />
            </div>
            <div v-if="tableHeader.type == 'comboboxmanual'">
              <base-combobox-custom
                :hideAddIcon="true"
                :value="tableContents[index][tableHeader.fieldName]"
                :comboboxProps="renderComboboxProps(tableHeader, index)"
                @getDataEventBus="
                  renderComboboxManualValue(tableHeader, index, ...arguments)
                "
                :class="{ 'ms-combobox-custom--readonly': isReadOnly }"
              />
            </div>
            <div
              v-if="tableHeader.type == 'input'"
              :class="[tableHeader.textAlign]"
            >
              <base-input
                :value="formatTableContent(tableContent, tableHeader)"
                @input="changeInputValue(index, tableHeader, ...arguments)"
                @blurInput="blurInput(index, tableHeader, ...arguments)"
                :class="{ 'ms-input--readonly': isReadOnly }"
                :format="tableHeader.format"
              />
            </div>
            <div v-if="tableHeader.type == 'date'">
              <base-date-input
                :value="tableContent[tableHeader.fieldName]"
                @input="changeDate(index, tableHeader, ...arguments)"
                type="date"
                :class="{ 'ms-input--readonly': isReadOnly }"
              />
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
            <div v-if="item.footerValue != null">{{formatFooter(item)}}</div>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</template>


<script>
import BaseInput from "./BaseInput.vue";
import BaseDateInput from "./BaseDateInput.vue";
import BaseComboboxCustom from "./BaseComboboxCustom.vue";
import BaseCheckbox from "./BaseCheckbox.vue";
import moment from "moment";
import MixinListener from "../../mixins/listeners/listeners";
import MixinMethod from "../../mixins/methods";
export default {
  name: "BaseTable",
  mixins: [MixinListener, MixinMethod],
  components: {
    BaseCheckbox,
    BaseComboboxCustom,
    BaseDateInput,
    BaseInput,
  },
  props: {
    formName: {
      type: String,
      default() {
        return "";
      },
    },
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
    isReadOnly: {
      type: Boolean,
      default() {
        return false;
      },
    },
  },
  data() {
    return {
      /**data test */
      comboboxUnitProps: {
        tableObject: "Units",
        mode: "manual",
      },

      /** */
      isShowContextMenu: false,
      currentSelectedRow: -1,
      isChooseCheckboxAll: false,
      listSelectedRow: [],
      listSelectedContent: [],
    };
  },
  methods: {
    /**thay đổi nội dung của input  
     * @param index: index của row
     * @param header: header hiện tại
     * @param content: nội dung ô hiện tại
     * CreatedBY: nvdien(9/10/2021)
    */
    changeInputValue(index, header, content) {
      let mode = "input";
      if(header.format == "number") return;
      this.$emit("changeTableContent", index, header, mode, content);
    },
    /**thay đổi nội dung của input date 
     * @param index: index của row
     * @param header: header hiện tại
     * @param date: nội dung ô hiện tại
     * CreatedBY: nvdien(9/10/2021)
    */
    changeDate(index, header, date) {
      let mode = "date";
      this.$emit("changeTableContent", index, header, mode, date);
    },
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
      if(tableHeader.type == "normal" 
      && tableHeader.format == "number"){
        cellData = this.formatMoney(cellData);
      }
      return cellData;
    },
    /**
     * format footer
     * CreatedBy: nvdien(10/10/2021)
     */
    formatFooter(header){
      if(header.format == "number"){
        return this.formatMoney(header.footerValue);
      }
      return header.footerValue;
    },
    /**
     * hiển thị context menu
     * @param event: sự kiện
     * @param tableContent nội dung hàng hiện tại
     * @param index: index hàng
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
    /**Xem khi ấn vào nút xem trong context menu
     * @param tableContent: nội dung hàng hiện tại
     * CreatedBy: nvdien(2/10/2021)
     */
    showInContext(tableContent) {
      this.$emit("showInContext", tableContent);
    },
    /**trả về custom style cho cell 
     * @param tableContent: nội dung hàng hiện tại
     * @param tableHeader: nội dung header
     * CreatedBy: nvdien(9/10/2021)
    */
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
    /**Xóa 1 dòng 
     * @param index: index hàng hiện tại
     * CreatedBy: nvdien(9/10/2021)
    */
    deleteRow(index) {
      this.$emit("deleteRow", index);
    },
    /**click vào 1 ô
     * @param tableHeader : header của cột đó
     * CreatedBy: nvdien(9/10/2021)
     */
    handleClickCell(tableHeader, tableContent) {
      if ("hasClick" in tableHeader) {
        this.$emit(`clickCell${tableHeader["hasClick"]}`, tableContent);
      }
    },
    /**double click vào 1 dòng
     * param tableContent nội dung dòng đó
     * CreatedBy: nvdien(9/10/2021)
     */
    handleDoubleClickRow(tableContent) {
      this.$emit("handleDoubleClickRow", tableContent);
    },
    /**render combobox props 
     * @param header: header table hiện tại
     * @param index: index hàng hiện tại
     * CreatedBY: nvdien( 9/10/2021 )
    */
    renderComboboxProps(header, index) {
      if (header.type == "comboboxapi") {
        let comboboxProps = { ...header.combobox };
        comboboxProps["rowTable"] = index;
        return comboboxProps;
      }
      if (header.type == "comboboxmanual") {
        if (this.tableContents[index][header.combobox.contentFields]) {
          let content =
            this.tableContents[index][header.combobox.contentFields];
          let comboboxProps = {};
          Object.assign(comboboxProps, header.combobox);
          comboboxProps.tableContents = content;
          return comboboxProps;
        }
      }
    },
     /**bind dữ liệu lên combobox
     * @param index: index của row
     * @param header: header hiện tại
     * @param content: nội dung ô hiện tại
     * CreatedBY: nvdien(9/10/2021)
    */
    bindingCombobox(index, header, content) {
      let mode = "comboboxapi";
      this.$emit("changeTableContent", index, header, mode, content);
    },
    /**combobox manual value 
     * @param tableHeader: header hiện tại
     * @param index: index hàng hiện tại
     * @param content: nội dung ô hiện tại
     * CreatedBY: nvdien(9/10/2021)
    */
    renderComboboxManualValue(tableHeader, index, content) {
      if (
        content[tableHeader.combobox.fieldName] !=
        this.tableContents[index][tableHeader.combobox.contentFields]
      ) {
        let mode = "comboboxmanual";
        this.$emit("changeTableContent", index, tableHeader, mode, content);
      }
    },
    /**blur input 
     * @param index: index của row
     * @param header: header hiện tại
     * @param content: nội dung ô hiện tại
     * CreatedBY: nvdien(9/10/2021)
    */
    blurInput(index, header, content) {
      let mode = "blur";
      this.$emit("changeTableContent", index, header, mode, content);
    },
  },
  computed: {
    /**
     * Table đang hiển thị
     * CreatedBy: nvdien (08/10/2021)
     */
    tableDisplay() {
      var tableData = this.tableContents.filter((item) => {
        return item["state"] != this.$resourcesVN.mode.DELETE;
      });
      return tableData;
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/table.css");
</style>
