<template lang="">
    <div class="warehouse">
      <div class="header-list">
        <div class="header-list__title">Nhập, xuất kho</div>
        <div class="header-group-button"> 
          <div class="mi mi-24 mi-tour"></div>
          <div class="tour-label">Hướng dẫn</div>
          <base-button-custom value="Thêm phiếu nhập" color="ms-button-primary"/>
        </div>
      </div>
      <div class="warehouse-content-body">
        <base-tab :currTab="currTab" :listTab="listTab" v-model="currTab" />
        <router-view></router-view>
      </div>
    </div>
</template>
<script>

import BaseButtonCustom from "../../../base/BaseButtonCustom.vue";
import BaseTab from "../../../base/BaseTab.vue";
export default {
  name: "WarehouseInwardOutwardList",
  components: {
    BaseButtonCustom,
    BaseTab
  },
  data(){
    return{
      currTab: 1,
      listTab: [
          {
						name: "Tất cả",
						path: "/warehouse/InwardOutwardList/INInwardList",
					},
					{
						name: "Nhập kho",
						path: "/warehouse/InwardOutwardList/INInwardList",
					},
				],
    }
  },
  methods: {
    /**export data
     * CreatedBy: ndien(17/9/2021)
     */
    exportData() {
      console.log("export Data");
    },
  },
};
</script>
<style scoped>
@import url("../../../../css/views/warehouse/warehouseinwardoutwardlist.css");
</style>