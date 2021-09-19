<template>
  <div class="commodity-group-wrap">
    <div class="commodity-group" :class="{ 'commodity-group-full': isFullScreen }">
      <div class="ms-popup__header">
        <div class="ms-popup-title">
          <div class="popup-title">Chọn tính chất hàng hóa dịch vụ</div>
        </div>
        <div class="ms-popup-close">
          <div class="popup-help-icon">
            <div class="mi mi-24 mi-help"></div>
          </div>
          <div class="popup-close-icon">
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
        <div class="row-input">
          <div class="commodity-group-icon">
            <div class="mi mi-85 mi-inventory-item-category"></div>
          </div>
          <div class="commodity-group-text">
            <div>Hàng hóa</div>
            <div>Sản phẩm bạn mua và bán lại cho khách hàng</div>
          </div>
        </div>
        <div class="row-input">
          <div class="commodity-group-icon">
            <div class="mi mi-85 mi-construct-category"></div>
          </div>
          <div class="commodity-group-text">
            <div>Hàng hóa</div>
            <div>Sản phẩm bạn mua và bán lại cho khách hàng</div>
          </div>
        </div>
        <div class="row-input">
          <div class="commodity-group-icon">
            <div class="mi mi-85 mi-matarial-category"></div>
          </div>
          <div class="commodity-group-text">
            <div>Hàng hóa</div>
            <div>Sản phẩm bạn mua và bán lại cho khách hàng</div>
          </div>
        </div>
        <div class="row-input">
          <div class="commodity-group-icon">
            <div class="mi mi-85 mi-cloth-category"></div>
          </div>
          <div class="commodity-group-text">
            <div>Hàng hóa</div>
            <div>Sản phẩm bạn mua và bán lại cho khách hàng</div>
          </div>
        </div>
        <div class="row-input">
          <div class="commodity-group-icon">
            <div class="mi mi-85 mi-su-category"></div>
          </div>
          <div class="commodity-group-text">
            <div>Hàng hóa</div>
            <div>Sản phẩm bạn mua và bán lại cho khách hàng</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "CommodityGroup",
  data(){
      return{
          isFullScreen: false,
      }
  },
  methods:{
      /**Chuyển form ra full màn hình và ngược lại*/
      changeFullScreen(){
          this.isFullScreen = !this.isFullScreen;
      }
  }
};
</script>

<style scoped>
@import url("../../../css/views/commodity/commoditygroup.css");
</style>