<template>
  <div class="inward-detail-wrap">
    <div class="inward-detail">
      <div class="inward-detail-header">
        <div class="recent-log-btn">
          <div class="mi mi-24 mi-recent-log"></div>
        </div>
        <div class="title">Phiếu nhập kho BK025</div>
        <div class="header-detail-input">
          <div class="combobox"></div>
          <div class="combobox"></div>
        </div>
        <div class="header-detail-button">
          <div class="mi mi-24 mi-tour"></div>
          <div class="tour-label">Hướng dẫn</div>
          <div class="mi mi-24 mi-setting__detail"></div>
          <div class="mi mi-24 mi-help"></div>
          <div class="mi mi-24 mi-close"></div>
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
                  <div class="w-4/7">
                    <base-input label="Địa chỉ" />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input label="Người giao hàng" />
                  </div>
                  <div class="w-4/7">
                    <base-input label="Diễn giải" />
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/7">
                    <base-input label="Nhân viên bán hàng" />
                  </div>
                  <div class="w-4/7">
                    <base-input label="Kèm theo" />
                  </div>
                </div>
              </div>
              <div class="w-1/5">
                <div class="row-input">
                  <base-input label="Ngày hạch toán" />
                </div>
                <div class="row-input">
                  <base-input label="Ngày chứng từ" />
                </div>
                <div class="row-input">
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
              <div class="table-above-title">
                Hàng tiền
              </div>
              <div class="table-above-control flex">
                <div>Đơn giá nhập kho</div>
                <div class="base-dropdown"></div>
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
            <base-button value="Thêm dòng"/>
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
export default {
  name: "InwardDetail",
  components: {
    BaseInput,
    BaseButton,
    BaseTable,
    BasePagination,
  },
  data() {
    return {
      tableInwardDetailHeaders: [
        { EmployeeCode: "MÃ NHÂN VIÊN", type: "0" },
        { FullName: "TÊN NHÂN VIÊN", type: "0" },
        { Gender: "GIỚI TÍNH", type: "0" },
        { DateOfBirth: "NGÀY SINH", type: "1" },
        { IdentityNumber: "SỐ CMND", type: "0" },
        { PositionName: "CHỨC DANH", type: "0" },
        { DepartmentName: "TÊN ĐƠN VỊ", type: "0" },
        { BankAccount: "SỐ TÀI KHOẢN", type: "0" },
        { BankName: "TÊN NGÂN HÀNG", type: "0" },
        { BankBranch: "CHI NHÁNH TK NGÂN HÀNG", type: "0" },
      ],
    };
  },
};
</script>

<style scoped>
@import url("../../../css/views/warehouse/inwarddetail.css");
</style>