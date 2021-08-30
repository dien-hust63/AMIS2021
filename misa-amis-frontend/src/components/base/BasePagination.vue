<template>
  <div class="ms-pagination">
    <div class="ms-pagination--left">
      <div class="total-record">Tổng số: <b>61</b> bản ghi</div>
    </div>
    <div class="ms-pagination--right">
      <div class="ms-pagination-options">
        <div class="selected-options">
          <span>{{optionValue}} bản ghi trên 1 trang</span>
        </div>
        <div class="dropdown-icon" @click="toogleList">
          <div class="mi mi-16 mi-arrow-dropdown"></div>
        </div>
        <ul class="selected-options__list" :class="{'selected-options__list--show':isShowList}">
          <li
            v-for="(number, index) in numberItemsPerPage"
            :key="index"
            class="selected-options__item"
            @click="chooseOptions(index)"
            :class="{'selected-options__item--active': currentIndex == index}"
          >
            {{ number }} bản ghi trên 1 trang
          </li>
        </ul>
      </div>
      <div class="ms-pagination-page"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BasePagination",
  data() {
    return {
      numberItemsPerPage: [10, 20, 30, 50, 100],
      currentIndex:0,
      isShowList:false,
      optionValue: 10,
    };
  },
  methods:{
      /**
       * set index hiện tại của mảng số bản ghi trên trang
       * @param index: chỉ số của mảng numberItemsPerPage
       * CreatedBy: nvdien(30/8/2021)
       */
      chooseOptions(index){
          this.currentIndex = index;
          this.isShowList = false;
          this.optionValue = this.numberItemsPerPage[index];
      },

    /**
     * ẩn hiện danh sách options khi click vào icon dropdown
     * CreatedBy: nvdien(30/8/2021)
     */
      toogleList(){
          this.isShowList = !this.isShowList;
      }
  }
};
</script>

<style scoped>
@import url("../../css/base/pagination.css");
</style>