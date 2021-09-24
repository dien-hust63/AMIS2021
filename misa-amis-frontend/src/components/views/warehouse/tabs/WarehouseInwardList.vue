<template>
  <div class="inward-list">
    <div class="toolbar">
      <div class="toolbar__search">
        <base-input
          placeholder="Nhập từ khóa tìm kiếm"
          class="input-search"
          v-model="inputSearchValue"
        />
      </div>
      <div class="mi mi-24 mi-refresh toolbar-refresh"></div>
      <div class="mi mi-24 mi-excel toolbar-excel" @click="exportData"></div>
    </div>
    <div class="warehouse-table">
      <base-table :tableHeaders="tableInwardListHeaders" :tableContents="tableInwardListContents"/>
      <context-menu
        :contextContent="contextContent"
        :contextPosition="contextPosition"
        v-show="isShowContext"
      />
      <div class="warehouse-content-pagination">
        <base-pagination />
      </div>
    </div>
  </div>
</template>

<script>
import BaseTable from "../../../base/BaseTable.vue";
import BasePagination from "../../../base/BasePagination.vue";
import BaseInput from "../../../base/BaseInput.vue";
import ContextMenu from "../../contextmenu/ContextMenu.vue";
import { RepositoryFactory } from "../../../../js/repository/repository.factory";
const VoucherRepository = RepositoryFactory.get("vouchers");
export default {
  name: "WarehouseInwardList",
  components: {
    BaseTable,
    BasePagination,
    BaseInput,
    ContextMenu,
  },
  mounted() {
    //lấy danh sách chứng từ
     this.loadData();
  },
  data() {
    return {
      tableInwardListHeaders: this.$resourcesVN.tableInwardListHeaders,
      /**contextmenu */
      contextContent: [
        { text: "Nhân bản", callbackFunc: this.duplicate },
        { text: "Xóa", callbackFunc: this.delete },
      ],
      contextPosition: {
        top: 0,
        left: 0,
      },
      isShowContext: false,
      tableInwardListContents: [],
    };
  },
  methods: {
    /**hiển thị context menu */
    toogleContextMenu(event) {
      console.log("tesst");
      this.isShowContext = !this.isShowContext;
      if (this.isShowContext) {
        let currentPos = event.target.getBoundingClientRect();
        this.contextPosition.top = currentPos.top + event.target.clientHeight;
        this.contextPosition.left = currentPos.left;
      }
    },
    /**lấy danh sách chứng từ */
    loadData(){
      VoucherRepository.getAccountVoucherPagingFilter('','','','','',1,10)
        .then((response) => {
          console.log(response.data['Vouchers']);
          this.tableInwardListContents = response.data['Vouchers'];
          // this.tableInwardListContents = response.data['Vouchers'];
        })
        .catch((response) => {
          console.log(response);
        });
    }
  },
};
</script>

<style scoped>
@import url("../../../../css/views/warehouse/warehouseinwardlist.css");
</style>