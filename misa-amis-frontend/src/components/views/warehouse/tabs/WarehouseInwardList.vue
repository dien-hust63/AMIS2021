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
      <div
        class="mi mi-24 mi-refresh toolbar-refresh"
      ></div>
      <div class="mi mi-24 mi-excel toolbar-excel" @click="exportData"></div>
    </div>
    <div class="warehouse-table">
        <base-table
          :tableHeaders="tableInwardListHeaders"
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
export default {
  name: "WarehouseInwardList",
  components: {
    BaseTable,
    BasePagination,
    BaseInput
  },
  data(){
      return{
          tableInwardListHeaders: this.$resourcesVN.tableInwardListHeaders,
      }
    
  }
};
</script>

<style scoped>
@import url("../../../../css/views/warehouse/warehouseinwardlist.css");
</style>