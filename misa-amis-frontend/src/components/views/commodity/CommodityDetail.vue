<template>
  <div class="commodity-detail-wrap">
    <div
      class="commodity-detail"
      :class="{ 'commodity-detail-full': isFullScreen }"
    >
      <div class="ms-popup__header">
        <div class="ms-popup-title">
          <div class="popup-title">Thông tin vật tư, hàng hóa, dịch vụ</div>
          <div class="popup-title-detail">
            <div class="sub-title-icon">
              <div class="mi mi-50 mi-inventory-item-category-detail"></div>
            </div>
            <div class="sub-title">Hàng hóa</div>
            <div class="change-attribute">Thay đổi tính chất</div>
          </div>
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
      <div class="commodity-detail-content">
        <div class="divide-line"></div>
        <div class="w-2/3">
          <div class="row-input">
            <base-input label="Tên" :required="true" />
          </div>
          <div class="row-input">
            <div class="w-1/4 p-r-6">
              <base-input label="Mã" :required="true" />
            </div>
            <div class="w-3/4">
              <base-input label="Nhóm VTHH" />
            </div>
          </div>
          <div class="row-input main-unit">
            <div class="w-1/3">
              <base-input label="Đơn vị tính chính" />
            </div>
          </div>
        </div>
        <div class="divide-line"></div>
        <div class="row-input commodity-description">
          <base-input typeInput="textarea" label="Mô tả" />
        </div>
        <div class="row-input">
          <base-input label="Diễn giải khi mua" />
        </div>
        <div class="row-input sell-description">
          <base-input label="Diễn giải khi bán" />
        </div>
        <div class="commodity-feature flex">
          <div class="mi mi-16 mi-arrow-right--black" @click="showImplicitInfoSection"></div>
          <div class="commodity-feature-text">Thông tin ngầm định</div>
        </div>
        <div class="content-drop-section" :class="{'content-drop-section--show': isShowInmplicitSection}">
          <div class="w-150 p-r-26 border-box">
            <div class="row-input">
              <base-input label="Kho ngầm định" />
            </div>
            <div class="row-input">
              <base-input label="Tài khoản kho" />
            </div>
            <div class="row-input">
              <base-input label="Tài khoản doanh thu" />
            </div>
            <div class="row-input">
              <base-input label="Tài khoản chiết khấu" />
            </div>
          </div>
           <div class="w-150 p-r-26 border-box">
            <div class="row-input">
              <base-input label="TK giảm giá" />
            </div>
            <div class="row-input">
              <base-input label="TK trả lại" />
            </div>
            <div class="row-input">
              <base-input label="TK chi phí" />
            </div>
            <div class="row-input">
              <base-input label="Tỉ lệ CKMH (%)" />
            </div>
          </div>
          <div class="w-center"></div>
          <div class="w-220 p-r-26 border-box">
              <div class="row-input">
                  <base-input label="Đơn giá mua cố định" />
              </div>
              <div class="row-input">
                  <base-input label="Đơn giá mua gần nhất" />
              </div>
              <div class="row-input">
                  <base-input label="Đơn giá bán" />
              </div>
          </div>
          <div class="w-220 p-r-26 border-box">
              <div class="row-input">
                  <base-input label="Thuế suất GTGT(%)" />
              </div>
              <div class="row-input">
                  <base-input label="Thuế suất thuế NK(%)" />
              </div>
              <div class="row-input">
                  <base-input label="Thuế suất thuế XK(%)" />
              </div>
              <div class="row-input">
                  <base-input label="Nhóm HHDV chịu thuế TTĐB" />
              </div>
          </div>
        </div>
        <div class="commodity-feature flex">
          <div class="mi mi-16 mi-arrow-right--black"></div>
          <div class="commodity-feature-text">Đơn vị chuyển đổi</div>
        </div>
        <div class="content-drop-section"></div>
      </div>
      <div class="popup-footer">
        <div class="popup-footer-divide"></div>
        <div class="popup-footer-content">
          <div class="popup-footer--right">
            <div class="ms-button ms-button-secondary">Hủy</div>
          </div>
          <div class="popup-footer--left">
            <div class="ms-button ms-button-secondary first-right-button">
              Cất
            </div>
            <div class="ms-button ms-button-primary">Cất và thêm</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";

export default {
  name: "CommodityDetail",
  components: {
    BaseInput,
  },
  data() {
    return {
      isFullScreen: false,
      isShowInmplicitSection: false,
    };
  },
  methods: {
    /**Chuyển form ra full màn hình và ngược lại
    * CreatedBy: nvdien(20/9/2021)
    */
    changeFullScreen() {
      this.isFullScreen = !this.isFullScreen;
    },
    /*Toogle thông tin ngầm định
    * CreatedBy: nvdien(20/9/2021)
    */
    showImplicitInfoSection(){
      this.isShowInmplicitSection = !this.isShowInmplicitSection;
    }
    
  },
};
</script>

<style scoped>
@import url("../../../css/views/commodity/commoditydetail.css");
</style>