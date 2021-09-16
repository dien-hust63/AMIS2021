<template>
  <div class="ms-pagination">
    <div class="ms-pagination--left">
      <div class="total-record">
        Tổng số: <b>{{ totalRecord }}</b> bản ghi
      </div>
    </div>
    <div
      class="ms-pagination--right"
      :class="{ 'selected-options--show': isShowList }"
    >
      <div
        class="ms-pagination-options"
        :class="{ 'seleted-options--active': isFocusOptions }"
        @blur="blurOptions"
        @focus="focusOptions"
        tabindex="0"
      >
        <div class="selected-options">
          <span>{{ pageSize }} bản ghi trên 1 trang</span>
        </div>
        <div class="dropdown-icon" @click="toogleList">
          <div class="mi mi-16 mi-arrow-dropdown"></div>
        </div>
        <ul class="selected-options__list">
          <li
            v-for="(number, index) in numberItemsPerPage"
            :key="index"
            class="selected-options__item"
            @click="chooseOptions(index)"
            :class="{ 'selected-options__item--active': currentIndex == index }"
          >
            {{ number }} bản ghi trên 1 trang
          </li>
        </ul>
      </div>
      <div class="ms-pagination-page">
        <div
          class="prev-page"
          @click="backPage"
          :class="{ disable: pageIndex == 1 }"
        >
          Trước
        </div>
        <div
          v-for="(item, index) in pageArray"
          :key="index"
          class="page-number"
          :class="{ 'page--selected': item == pageIndex }"
          @click="choosePage(item)"
        >
          {{ item }}
        </div>
        <div
          class="next-page"
          @click="nextPage"
          :class="{ disable: pageIndex == totalPage }"
        >
          Sau
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BasePagination",
  props: {
    totalRecord: {
      type: Number,
      default() {
        return 0;
      },
    },
    totalPage: {
      type: Number,
      default() {
        return 0;
      },
    },
    hasSearch: {
      type: Boolean,
      default: false,
    }
  },
  data() {
    return {
      numberItemsPerPage: [10, 20, 30, 50, 100],
      currentIndex: 0,
      isShowList: false,
      pageSize: 10,
      pageIndex: 1,
      isFocusOptions: false,
    };
  },
  methods: {
    /**
     * set index hiện tại của mảng số bản ghi trên trang
     * @param index: chỉ số của mảng numberItemsPerPage
     * CreatedBy: nvdien(30/8/2021)
     */
    chooseOptions(index) {
      this.currentIndex = index;
      this.isShowList = false;
      this.pageSize = this.numberItemsPerPage[index];
      this.pageIndex = 1;
      this.$emit("getPagingData", this.pageIndex, this.pageSize);
    },
    /**
     * Xử lí khi blur ra khỏi box lựa chọn số bản ghi
     * CreatedBy: nvdien(31/8/2021)
     */
    blurOptions() {
      this.isShowList = false;
      this.isFocusOptions = false;
    },
    /**
     * Xử lí khi focus vào box lựa chọn số bản ghi
     * CreatedBy: nvdien(31/8/2021)
     */
    focusOptions() {
      this.isFocusOptions = true;
    },

    /**
     * ẩn hiện danh sách options khi click vào icon dropdown
     * CreatedBy: nvdien(30/8/2021)
     */
    toogleList() {
      this.isShowList = !this.isShowList;
    },
    /**
     * sinh ra dãy số trang
     * CreatedBy:nvdien(30/8/2021)
     */
    pagination(c, m) {
      var current = c,
        last = m,
        delta = 1,
        left = current - delta,
        right = current + delta + 1,
        range = [],
        rangeWithDots = [],
        l;

      for (let i = 1; i <= last; i++) {
        if (i == 1 || i == last || (i >= left && i < right)) {
          range.push(i);
        }
      }

      for (let i of range) {
        if (l) {
          if (i - l === 2) {
            rangeWithDots.push(l + 1);
          } else if (i - l !== 1) {
            rangeWithDots.push("...");
          }
        }
        rangeWithDots.push(i);
        l = i;
      }

      return rangeWithDots;
    },
    /**
     * Xử lí khi chọn trang
     * CreatedBy:nvdien(30/8/2021)
     */
    choosePage(item) {
      if (item == "...") {
        return;
      } else {
        this.pageIndex = item;
        this.$emit("getPagingData", this.pageIndex, this.pageSize);
      }
    },
    /**
     * Sang trang tiếp theo
     * CreatedBy:nvdien(30/8/2021)
     */
    nextPage() {
      if (this.pageIndex < this.totalPage) {
        this.pageIndex++;
        this.$emit("getPagingData", this.pageIndex, this.pageSize);
      }
    },
    /**
     *quay lại trang trước
     *CreatedBy:nvdien(30/8/2021)
     */
    backPage() {
      if (this.pageIndex > 1) {
        this.pageIndex--;
        this.$emit("getPagingData", this.pageIndex, this.pageSize);
      }
    },
  },
  computed: {
    pageArray: function () {
      return this.pagination(this.pageIndex, this.totalPage);
    },
  },
  watch: {
    hasSearch: function(){
      this.pageIndex = 1;
    }
  }
};
</script>

<style scoped>
@import url("../../css/base/pagination.css");
</style>