<template>
  <div class="ms-table-wrap">
    <table class="ms-table">
      <thead class="ms-thead">
        <tr>
          <th>
            <!-- <div class="checkbox">
              <i class="fas fa-check"></i>
            </div> -->
            <base-checkbox />
          </th>
          <th v-for="(tableHeader, index) in tableHeaders" :key="index">
            {{ Object.values(tableHeader)[0] }}
          </th>
          <th>CHỨC NĂNG</th>
        </tr>
      </thead>
      <tbody class="ms-tbody">
        <tr
          v-for="(tableContent, index) in tableContents"
          :key="index"
          @dblclick="editEntity(tableContents[index]['EmployeeId'])"
        >
          <td>
            <base-checkbox />
          </td>
          <td
            v-for="(tableHeader, index) in tableHeaders"
            :key="index"
            :class="alignContent(index)"
          >
            <span>{{ formatTableContent(tableContent, tableHeader) }}</span>
          </td>
          <td>
            <context-menu
              :deleteData="tableContents[index]"
              @loadTable="loadTable"
              @editEntity="editEntity"
              @copyEntity="copyEntity"
            />
          </td>
        </tr>
      </tbody>
    </table>
    <base-loading :class="{ 'ms-loading--show': isLoading }" />
  </div>
</template>


<script>
import CommonMethods from "../../mixins/methods.js";
import axios from "axios";
import ContextMenu from "../views/contextmenu/ContextMenu.vue";
import BaseLoading from "../base/BaseLoading.vue";
import BaseCheckbox from "../base/BaseCheckbox.vue";
export default {
  name: "BaseTable",
  components: {
    ContextMenu,
    BaseLoading,
    BaseCheckbox,
  },
  mixins: [CommonMethods],
  props: {
    tableHeaders: {
      type: Array,
      default() {
        return [];
      },
    },
    urlAPI: {
      type: String,
      default: "",
    },
    forceLoadTable: {
      type: Boolean,
      default() {
        return false;
      },
    },
  },
  mounted() {
    this.loadTable();
  },
  data() {
    return {
      tableContents: [],
      isLoading: false,
    };
  },
  methods: {
    /**
     * Định dạng dữ liệu trong ô của table bên trái, giữa hay phải
     *  @param {Int} index : chỉ số ứng với header của table
     *  @return chuỗi class định dạng
     * CreatedBy: nvdien(29/8/2021)
     */
    alignContent(index) {
      let typeFormat = this.tableHeaders[index].type;
      switch (typeFormat) {
        case "0":
          return "text-align-left";
        case "1":
          return "text-align-center";
        case "2":
          return "text-align-right";
        default:
          return "";
      }
    },

    /**
     * Lấy giá trị của ô trong table và định dạng theo convention ngày, tiền lương
     * @param {Object} tableContent : chứa thông tin api trả về
     * @param {Object} tableHeader: chứa thông tin  Header của bảng
     * return chuỗi chứa giá trị được đổ lên bảng theo đúng định dạng
     * CreatedBy: nvdien(28/8/2021)
     */
    formatTableContent(tableContent, tableHeader) {
      let cellData;
      if (Object.keys(tableHeader)[0] == "Gender") {
        cellData = this.showGenderName(
          tableContent[Object.keys(tableHeader)[0]]
        );
      } else {
        cellData = tableContent[Object.keys(tableHeader)[0]];
      }
      let typeFormat = Object.values(tableHeader)[1];
      if (typeFormat === "1") {
        // định dạng ngày
        return this.formatDate(cellData, "/");
      }
      if (typeFormat === "2") {
        // định dạng tiền lương
        return this.formatMoney(cellData);
      }
      return cellData;
    },

    /**
     * cập nhật lại nội dung bẳng
     * CreatedBy: nvdien(29/8/2021)
     */
    loadTable() {
      axios
        .get(this.urlAPI)
        .then((response) => {
          this.tableContents = response.data["Employees"];
          this.$emit("getTableData", response.data);
          this.isLoading = false;
        })
        .catch((response) => {
          console.log(response);
          this.isLoading = true;
        });
      this.isLoading = true;
    },
    /**
     * Chỉnh sửa đối tượng
     * @param {string} entityId: Id của đối tượng
     * CreadtedBy: nvdien(1/9/2021)
     */
    editEntity(entityId){
      this.$emit("editEntity",entityId);
    },
    /**
     * Nhân bản đối tượng
     * @param {object} đối tượng nhân bản
     * CreatedBY: nvdien(2/9/2021)
     */
    copyEntity(entity){
      this.$emit("copyEntity",entity);
    } 
  },
  watch: {
    urlAPI: function () {
      this.loadTable();
    },
    forceLoadTable: function () {
      this.loadTable();
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/table.css");
</style>
