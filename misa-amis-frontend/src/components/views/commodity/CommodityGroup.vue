<template>
  <div class="commodity-group-wrap" v-show="isShowCommodityGroup">
    <div
      class="commodity-group"
      :class="{ 'commodity-group-full': isFullScreen }"
    >
      <div class="ms-popup__header">
        <div class="ms-popup-title">
          <div class="popup-title">Chọn tính chất hàng hóa dịch vụ</div>
        </div>
        <div class="ms-popup-close">
          <div class="popup-help-icon">
            <div class="mi mi-24 mi-help"></div>
          </div>
          <div class="popup-close-icon" @click="closeCommodityGroup">
            <div class="mi mi-24 mi-close"></div>
          </div>
        </div>
      </div>
      <div
        class="resize flex"
        :class="{
          'is-full-screen-true': isFullScreen,
          'is-full-screen-false': !isFullScreen,
        }"
        @click="changeFullScreen"
      >
        <div class="mi mi-16 mi-chevron-left"></div>
      </div>
      <div class="commodity-group-content">
        <div class="row-input" @click="showCommodityDetail(item)" v-for="(item, index) in commodityGroups" :key="index">
          <div class="commodity-group-icon">
            <div class="mi mi-85" :class="[item.icon]"></div>
          </div>
          <div class="commodity-group-text">
            <div>{{ item['commodity_group_name']}}</div>
            <div>{{ item['description']}}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { RepositoryFactory } from "../../../js/repository/repository.factory";
const CommodityGroupRepository = RepositoryFactory.get("commoditygroups");
export default {
  name: "CommodityGroup",
  data() {
    return {
      isFullScreen: false,
      isShowCommodityGroup: false,
      listMainIconGroup: ["mi-inventory-item-category", 'mi-construct-category', "mi-matarial-category",'mi-cloth-category', "mi-su-category"],
      commodityGroups:[],
      rowTable:-1,
    };
  },
  methods: {
    /**Chuyển form ra full màn hình và ngược lại
     * CreadtedBy: nvdien(4/10/2021)
     */
    changeFullScreen() {
      this.isFullScreen = !this.isFullScreen;
    },
    /**đóng form
     * CreatedBy: nvdien(4/10/2021)
     */
    closeCommodityGroup() {
      this.isShowCommodityGroup = false;
    },
    /**
     * Hiển thị form chi tiết hàng hóa
     * CreatedBy: nvdien(4/10/2021)
     */
    showCommodityDetail(item) {
      this.$eventBus.$emit("showCommodityDetail", item, this.rowTable);
      this.isShowCommodityGroup = false;
    },
  },
  created() {
    this.$eventBus.$on("showCommodityGroup", (content) => {
      this.rowTable = content;
      //get main commodity group
      CommodityGroupRepository.getMainGroup()
        .then((response) => {
          this.commodityGroups = [...response.data];
          for(let i=0; i< this.listMainIconGroup.length; ++i){
            this.commodityGroups[i]["icon"] = this.listMainIconGroup[i];
          }
          this.isShowCommodityGroup = true;
        })
        .catch((response) => {
          console.log(response);
          this.isShowCommodityGroup = true;
        });
    });
  },
  destroyed() {
    this.$eventBus.$off("showCommodityGroup");
  },
};
</script>

<style scoped>
@import url("../../../css/views/commodity/commoditygroup.css");
</style>