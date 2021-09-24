<template>
  <div class="inward-detail-wrap">
    <div class="inward-detail">
      <div class="inward-detail-header">
        <div class="recent-log-btn">
          <div class="mi mi-24 mi-recent-log"></div>
        </div>
        <div class="title">Phiếu nhập kho BK025</div>
        <div class="header-detail-input">
          <base-dropdown
            v-model="dropdownData"
            :dropdownList="inwardTypeList"
            :defaultData="dropdownInwardTypeDefault"
          />
          <div class="combobox"></div>
        </div>
        <div class="header-detail-button">
          <div class="mi mi-24 mi-tour"></div>
          <div class="tour-label">Hướng dẫn</div>
          <div class="button-setting button-group">
            <div class="mi mi-24 mi-setting__detail"></div>
          </div>
          <div class="button-help button-group">
            <div class="mi mi-24 mi-help"></div>
          </div>
          <div class="button-close button-group">
            <div
              class="mi mi-24 mi-close"
              @click="closeInwardDetail()"
            ></div>
          </div>
        </div>
      </div>
      <div class="inward-detail-body">
        <div class="main-infomation flex">
          <div class="w-3/4">
            <div class="basic-information flex">
              <div class="w-4/5">
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input label="Khách hàng" />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input label="Địa chỉ" />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input label="Người giao hàng" />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input label="Diễn giải" />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input label="Nhân viên bán hàng" />
                  </div>
                  <div class="w-4/7 px-13 border-box">
                    <base-input label="Kèm theo" />
                  </div>
                </div>
                <div class="row-input flex">
                  <div class="reference-title">Tham chiếu</div>
                  <div class="reference-show-more">...</div>
                </div>
              </div>
              <div class="w-1/5">
                <div class="row-input-right">
                  <base-input label="Ngày hạch toán" />
                </div>
                <div class="row-input-right">
                  <base-input label="Ngày chứng từ" />
                </div>
                <div class="row-input-right voucher-number">
                  <base-input label="Số chứng từ" />
                </div>
              </div>
            </div>
          </div>
          <div class="w-1/4 summary-info">
            <div class="summary-info-title">Tổng tiền</div>
            <div class="summary-info-number">0,00</div>
          </div>
        </div>
        <div class="tab-inward-detail">
          <div class="inward-detail-table">
            <div class="table-above">
              <div class="table-above-title">Hàng tiền</div>
              <div class="table-above-control">
                <div>Đơn giá nhập kho</div>
                <base-dropdown
                  :dropdownList="inwardMethodList"
                  :defaultData="dropdownInwardMethodDefault"
                />
              </div>
            </div>
            <base-table :tableHeaders="tableInwardDetailHeaders" />
            <div class="inward-detail-pagination">
              <base-pagination />
            </div>
          </div>
        </div>
        <div class="grid-control-item">
          <div class="btn-grid-control">
            <base-button value="Thêm dòng" />
            <base-button value="Thêm ghi chú" />
            <base-button value="Xóa hết dòng" />
          </div>
          <div class="input-grid-control">
            <div class="flex">
              <div class="mi mi-18 mi-attach"></div>
              <div class="mi-attach-title">Đính kèm</div>
              <div class="capacity-attach">Dung lượng tối đa 5MB</div>
            </div>
            <div class="input-attach-content">
              Kéo/Thả tệp vào đây hoặc bấm vào đây
            </div>
          </div>
        </div>
      </div>
      <div class="inward-detail-footer">
        <div class="left-group-button">
          <base-button value="Hủy" class="ms-button-secondary" />
        </div>
        <div class="right-group-button">
          <base-button value=" Cất" class="ms-button-secondary" />
          <base-button value="Cất và in" class="ms-button-primary" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";
import BaseButton from "../../base/BaseButton.vue";
import BaseTable from "../../base/BaseTable.vue";
import BasePagination from "../../base/BasePagination.vue";
import BaseDropdown from "../../base/BaseDropdown.vue";
export default {
  name: "InwardDetail",
  components: {
    BaseInput,
    BaseButton,
    BaseTable,
    BasePagination,
    BaseDropdown,
  },
  data() {
    return {
      tableInwardDetailHeaders: this.$resourcesVN.tableInwardDetailHeaders,
      dropdownInwardTypeDefault: this.$resourcesVN.inwardTypeList[1],
      dropdownInwardTypeData: this.$resourcesVN.inwardTypeList[1].data,
      inwardTypeList: this.$resourcesVN.inwardTypeList,

      dropdownInwardMethodDefault: this.$resourcesVN.inwardMethodList[0],
      inwardMethodList: this.$resourcesVN.inwardMethodList,
      isShowInwardDetail: false,
    };
  },
  methods: {
    /**Đóng form popup
     * CreaedBy: nvdien(17/9/2021)
     */
    closeInwardDetail() {
      this.dropdownData = 20;
      this.$emit("closeInwardDetail");
    },
  },
};
</script>

<style scoped>
@import url("../../../css/views/warehouse/inwarddetail.css");
</style>