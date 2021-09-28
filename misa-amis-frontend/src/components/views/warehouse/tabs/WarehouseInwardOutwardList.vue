<template lang="">
    <div class="warehouse">
      <div class="header-list">
        <div class="header-list__title">Nhập, xuất kho</div>
        <div class="header-group-button"> 
          <div class="mi mi-24 mi-tour"></div>
          <div class="tour-label">Hướng dẫn</div>
          <div
            class="base-button-custom"
          >
            <div class="button-custom-left ms-button-primary" @click="addInwardForm">
              <div class="buttom-custom-text" >Thêm phiếu nhập</div>
            </div>
            <div class="button-custom-right ms-button-primary">
              <div class="line"></div>
              <div class="mi mi-16 mi-arrow-up--white"></div>
            </div>
          </div>
        </div>
      </div>
      <div class="warehouse-content-body">
        <base-tab :currTab="currTab" :listTab="listTab" v-model="currTab" />
        <router-view></router-view>
      </div>
    </div>
</template>
<script>
import BaseTab from "../../../base/BaseTab.vue";
export default {
  name: "WarehouseInwardOutwardList",
  components: {
    BaseTab,
  },
  data() {
    return {
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
    };
  },
  methods: {
    /**
     * thêm phiếu nhập
     * CreadtedBy: nvdien(26/9/2021)
     */
    addInwardForm(){
       this.$eventBus.$emit('showInwardDetail', this.$resourcesVN.mode.ADD);
    }
  },
};
</script>
<style scoped>
@import url("../../../../css/views/warehouse/warehouseinwardoutwardlist.css");
</style>