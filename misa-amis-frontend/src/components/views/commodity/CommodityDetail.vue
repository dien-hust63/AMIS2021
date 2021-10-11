<template>
  <div class="commodity-detail-wrap" v-show="isShowCommodityDetail">
    <div
      class="commodity-detail"
      :class="{ 'commodity-detail-full': isFullScreen }"
    >
      <div class="ms-popup__header">
        <div class="ms-popup-title">
          <div class="popup-title">Thông tin vật tư, hàng hóa, dịch vụ</div>
          <div class="popup-title-detail">
            <div class="sub-title-icon">
              <div class="mi mi-50" :class="[icon]"></div>
            </div>
            <div class="sub-title">
              {{ currentCommodityGroup["commodity_group_name"] }}
            </div>
            <div class="change-attribute" @click="showCommodityGroup">
              Thay đổi tính chất
            </div>
          </div>
        </div>
        <div class="ms-popup-close">
          <div class="popup-help-icon">
            <div class="mi mi-24 mi-help"></div>
          </div>
          <div class="popup-close-icon" @click="closeCommodityDetail">
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
            <base-input
              label="Tên"
              :required="true"
              formName="Commodity"
              v-model="commodityData['commodity_name']"
              ref="commodityname"
              fieldName="Tên hàng hóa"
            />
          </div>
          <div class="row-input">
            <div class="w-1/4 p-r-6">
              <base-input
                label="Mã"
                :required="true"
                formName="Commodity"
                v-model="commodityData['commodity_code']"
              />
            </div>
            <div class="w-3/4">
              <base-combobox-custom
                label="Nhóm VTHH"
                v-model="commodityData['commodity_group_name']"
                :comboboxProps="commodityGroupComboboxProps"
                :hasFooter="false"
                @getDataEventBus="bindingCommodityGroup"
              />
            </div>
          </div>
          <div class="row-input main-unit">
            <div class="w-1/3">
              <base-combobox-custom
                label="Đơn vị tính chính"
                v-model="commodityData['unit_name']"
                :comboboxProps="CommodityUnitComboboxProps"
                :hasFooter="false"
                @getDataEventBus="bindingCommodityUnit"
                :hasHeader="false"
              />
            </div>
          </div>
        </div>
        <div class="divide-line"></div>
        <div class="row-input commodity-description">
          <base-input typeInput="textarea" label="Mô tả" />
        </div>
        <div class="row-input">
          <base-input label="Diễn giải khi mua" v-model="buyDescription" />
        </div>
        <div class="row-input sell-description">
          <base-input label="Diễn giải khi bán" v-model="sellDescription" />
        </div>
        <div class="commodity-feature flex">
          <div
            class="mi mi-16 mi-arrow-right--black"
            @click="showImplicitInfoSection"
          ></div>
          <div class="commodity-feature-text">Thông tin ngầm định</div>
        </div>
        <div
          class="content-drop-section"
          :class="{ 'content-drop-section--show': isShowInmplicitSection }"
        >
          <div class="w-150 p-r-26 border-box">
            <div class="row-input">
              <base-combobox-custom
                label="Kho ngầm định"
                v-model="commodityData['warehouse_code']"
                :comboboxProps="comboboxWarehouseProps"
                :hasFooter="false"
                @getDataEventBus="bindingWarehouse"
              />
            </div>
            <div class="row-input">
              <base-combobox-custom
                label="Tài khoản kho"
                v-model="commodityData['debit_account_number']"
                :comboboxProps="comboboxAccountProps"
                :hasFooter="false"
                @getDataEventBus="bindingDebitAccount"
              />
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
              <base-combobox-custom
                label="Tài khoản chi phí"
                v-model="commodityData['credit_account_number']"
                :comboboxProps="comboboxAccountProps"
                :hasFooter="false"
                @getDataEventBus="bindingCreditAccount"
              />
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
              <base-input
                label="Đơn giá mua gần nhất"
                :value="commodityData['debit_amount']"
                @blurInput="blurDebitAmount"
                format="number"
                class="text-right"
              />
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
          <div
            class="mi mi-16 mi-arrow-right--black"
            @click="showUnitSection"
          ></div>
          <div class="commodity-feature-text">Đơn vị chuyển đổi</div>
        </div>
        <div
          class="content-drop-section unit-section"
          :class="{ 'content-drop-section--show': isShowUnitSection }"
        >
          <div class="unit-section-title">
            <div class="mi mi-24 mi-exclamation--success"></div>
            <div class="text">
              Trường hợp vật tư, hàng hóa sử dụng đơn vị tính khác nhau khi
              nhập/xuất thì khai báo thêm các đơn vị tính ở đây.<br />VD: 1 tạ =
              100kg, 1 két bia = 20 chai , ...
            </div>
          </div>
          <div class="unit-table">
            <base-table
              :tableHeaders="tableUnitHeaders"
              :tableContents="tableUnitContents"
              @deleteRow="deleteRow"
              @changeTableContent="changeCommodityDetail"
              formName="CommodityDetail"
            />
          </div>
          <div class="btn-grid-control">
            <base-button value="Thêm dòng" @clickButton="addNewRow" />
            <base-button
              value="Xóa hết dòng"
              @clickButton="warningDeleteAllRow"
            />
          </div>
        </div>
        <div class="space-insert"></div>
      </div>
      <div class="popup-footer">
        <div class="popup-footer-divide"></div>
        <div class="popup-footer-content">
          <div class="popup-footer--right">
            <div class="ms-button ms-button-secondary">Hủy</div>
          </div>
          <div class="popup-footer--left">
            <div
              class="ms-button ms-button-secondary first-right-button"
              @click="saveCommodity"
            >
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
import BaseComboboxCustom from "../../base/BaseComboboxCustom.vue";
import BaseTable from "../../base/BaseTable.vue";
import BaseButton from "../../base/BaseButton.vue";
import { RepositoryFactory } from "../../../js/repository/repository.factory";
const CommodityRepository = RepositoryFactory.get("commoditys");
import MixinMethod from "../../../mixins/methods";
export default {
  name: "CommodityDetail",
  mixins: [MixinMethod],
  components: {
    BaseInput,
    BaseComboboxCustom,
    BaseTable,
    BaseButton,
  },
  data() {
    return {
      isFullScreen: false,
      isShowInmplicitSection: false,
      isShowCommodityDetail: false,
      commodityData: {},
      currentCommodityGroup: {},
      commodityIcon: "",
      commodityGroupComboboxProps:
        this.$resourcesVN.commodityGroupComboboxProps,
      CommodityUnitComboboxProps: this.$resourcesVN.CommodityUnitComboboxProps,
      comboboxWarehouseProps: this.$resourcesVN.comboboxWarehouseProps,
      rowTable: -1,
      /**table unit */
      tableUnitHeaders: this.$resourcesVN.tableUnitHeaders,
      tableUnitContents: [{ ...this.$resourcesVN.tableUnitContents[0] }],
      isShowUnitSection: false,
      /**prop combobox tài khoản */
      comboboxAccountProps: this.$resourcesVN.comboboxAccountProps,
    };
  },
  methods: {
    /**
     * thay đổi đơn giá mua
     * @param {string} content
     * CreatedBy: nvdien(11/10/2021)
     */
    blurDebitAmount(content){
      this.$set(this.commodityData, "debit_amount", content);
    },
    /**
     * trả về mô tả đơn vị tính
     * CreatedBy: nvdien(7/10/2021)
     */
    computeUnitDescription(unitName, rate, calculation) {
      if (this.commodityData["unit_name"] != null && unitName != null) {
        let rateDescription = rate;
        if (calculation == 2) {
          rateDescription = `1/${rate}`;
        }
        return `1 ${unitName} = ${rateDescription} ${this.commodityData["unit_name"]}`;
      }
      return "";
    },
    /**
     * Thay đổi nội dung form commodity detail
     * CreatedBY: nvdien(7/10/2021)
     */
    changeCommodityDetail(index, header, mode, content) {
      switch (mode) {
        case "comboboxapi": {
          this.$set(
            this.tableUnitContents[index],
            header.fieldName,
            content[header.fieldName]
          );
          this.$set(
            this.tableUnitContents[index],
            header.dataField,
            content[header.dataField]
          );
          let unitDescription = this.computeUnitDescription(
            content[header.fieldName],
            this.tableUnitContents[index]["rate"],
            this.tableUnitContents[index]["calculate_value"]
          );
          this.$set(
            this.tableUnitContents[index],
            "description",
            unitDescription
          );
          break;
        }
        case "comboboxmanual": {
          this.$set(
            this.tableUnitContents[index],
            header.fieldName,
            content[header.fieldName]
          );
          this.$set(
            this.tableUnitContents[index],
            header.dataField,
            content[header.dataField]
          );
          let unitDescription = this.computeUnitDescription(
            this.tableUnitContents[index]["unit_name"],
            this.tableUnitContents[index]["rate"],
            content[header.dataField]
          );
          this.$set(
            this.tableUnitContents[index],
            "description",
            unitDescription
          );
          break;
        }
        case "blur": {
          if (header.fieldName == "rate") {
            let unitDescription = this.computeUnitDescription(
              this.tableUnitContents[index]["unit_name"],
              content,
              this.tableUnitContents[index]["calculate_value"]
            );
            this.$set(
              this.tableUnitContents[index],
              "description",
              unitDescription
            );
            this.$set(this.tableUnitContents[index], "rate", content);
          }
          break;
        }
      }
    },
    /**Xóa 1 dòng
     * @param index: index của dòng xóa
     * CreatedBy: nvdien(5/10/2021)
     */
    deleteRow(index) {
      //Xóa dòng đó trên giao diện
      this.tableUnitContents.splice(index, 1);
    },
    /**
     * Thêm dòng mới ở bảng unit
     * CreatedBy: nvdien(7/10/2021)
     */
    addNewRow() {
      let totalRow = this.tableUnitContents.length;
      if (totalRow > 0) {
        let newRowContent = Object.assign(
          {},
          this.tableUnitContents[totalRow - 1]
        );
        //Cập nhật giao diện
        this.$set(this.tableUnitContents, totalRow, newRowContent);
      } else {
        this.tableUnitContents = [];
        this.tableUnitContents[0] = {
          ...this.$resourcesVN.tableUnitContents[0],
        };
      }
    },
    /**Đóng message box
     * CreatedBy: nvdien(5/10/2021)
     */
    closeMessageBox() {
      this.$eventBus.$emit("hideMessageBox");
    },
    /**xóa hết dòng
     * CreatedBy: nvdien(3/10/2021)
     */
    warningDeleteAllRow() {
      let messageText = this.formatString(
        this.$resourcesVN.message.messageDeleteWarning,
        "tất cả các dòng đã nhập"
      );
      this.$eventBus.$emit("showMessageBox", {
        icon: "mi-exclamation-warning-48",
        messageText: messageText,
        buttons: [
          {
            feature: "left ms-button-secondary",
            callback: () => {
              //đóng form cảnh báo
              this.closeMessageBox();
            },
            value: "Không",
          },
          {
            feature: "right-first ms-button-primary",
            callback: () => {
              this.deleteAllRow();
            },
            value: "Có",
          },
        ],
      });
    },
    /**Xóa tất cả các dòng
     * CreatedBy: nvdien(3/10/2021)
     */
    deleteAllRow() {
      //Xóa trên giao diện, set lại mảng default
      this.tableUnitContents = [];
      this.tableUnitContents[0] = { ...this.$resourcesVN.tableUnitContents[0] };
      this.closeMessageBox();
    },
    /**
     * Gán nội dung kho ngầm định combobox
     * CreatedBy: nvdien(5/10/2021)
     */
    bindingWarehouse(content) {
      this.$set(
        this.commodityData,
        "warehouse_code",
        content["warehouse_code"]
      );
      this.$set(this.commodityData, "warehouse_id", content["warehouse_id"]);
    },
    /**
     * Gán nội dung commodity unit combobox
     * CreatedBy: nvdien(5/10/2021)
     */
    bindingCommodityUnit(content) {
      this.$set(this.commodityData, "unit_name", content["unit_name"]);
      this.$set(this.commodityData, "unit_id", content["unit_id"]);
    },
    /**
     * Gán nội dung tài khoản kho
     * @param content: nội dung emit lên
     * CreatedBy: nvdien(8/10/2021)
     */
    bindingDebitAccount(content) {
      this.$set(this.commodityData, "debit_account", content["account_id"]);
      this.$set(
        this.commodityData,
        "debit_account_number",
        content["account_number"]
      );
    },
    /**
     * Gán nội dung tài khoản có
     * @param content: nội dung emit lên
     * CreatedBy: nvdien(8/10/2021)
     */
    bindingCreditAccount(content) {
      this.$set(this.commodityData, "credit_account", content["account_id"]);
      this.$set(
        this.commodityData,
        "credit_account_number",
        content["account_number"]
      );
    },
    /**
     * Gán nội dung commodity group combobox
     * createdBy: nvdien(5/10/2021)
     */
    bindingCommodityGroup(content) {
      this.$set(
        this.commodityData,
        "commodity_group_name",
        content["commodity_group_name"]
      );
      this.$set(
        this.commodityData,
        "commoditygroup_id",
        content["commoditygroup_id"]
      );
    },
    /**Chuyển form ra full màn hình và ngược lại
     * CreatedBy: nvdien(20/9/2021)
     */
    changeFullScreen() {
      this.isFullScreen = !this.isFullScreen;
    },
    /*Toogle thông tin ngầm định
     * CreatedBy: nvdien(20/9/2021)
     */
    showImplicitInfoSection() {
      this.isShowInmplicitSection = !this.isShowInmplicitSection;
    },
    /**
     * Toogle Đơn vị chuyển đổi
     * CreatedBy: nvdien(7/10/2021)
     */
    showUnitSection() {
      this.isShowUnitSection = !this.isShowUnitSection;
    },
    /**Thêm mới hàng hóa
     * CreatedBy: nvdien(3/10/2021)
     */
    saveCommodity() {
      /**thêm đơn vị tính chính vào dữ liệu */
      //Kiểm tra có đơn vị tính chính không
      if (this.commodityData.unit_id != null) {
        let mainUnit = {
          unit_id: this.commodityData.unit_id,
          unit_name: this.commodityData.unit_name,
          is_main_unit: 1,
          rate: 1,
        };
        this.tableUnitContents.push(mainUnit);
        for (let i = 0; i < this.tableUnitContents.length; ++i) {
          let unit = this.tableUnitContents[i];
          if (unit["unit_id"] != null) continue;
          else {
            //loại bỏ đơn vị tính chuyển đổi đó
            this.tableUnitContents.splice(i, 1);
          }
        }
        this.commodityData.units = this.tableUnitContents;
      }

      CommodityRepository.addCommodity(this.commodityData)
        .then((response) => {
          console.log(response);
          this.isShowCommodityDetail = false;
          this.$eventBus.$emit(
            "getCommodityDetail",
            response.data.Data,
            this.rowTable
          );
        })
        .catch((response) => console.log(response));
    },
    /**
     * Đóng form chi tiết hàng hóa
     * CreatedBy: nvdien(4/10/2021)
     */
    closeCommodityDetail() {
      this.isShowCommodityDetail = false;
      this.commodityData = {};
      this.isShowInmplicitSection = false;
    },
    /**
     * hiển thị form group
     * CreatedBy: nvdien(5/10/2021)
     */
    showCommodityGroup() {
      this.closeCommodityDetail();
    },
  },
  computed: {
    icon: function () {
      return this.commodityIcon + "-detail";
    },
    sellDescription() {
      return this.commodityData["commodity_name"];
    },
    buyDescription() {
      return this.commodityData["commodity_name"];
    },
  },
  created() {
    this.$eventBus.$on("showCommodityDetail", (content, rowTable) => {
      this.rowTable = rowTable;
      this.currentCommodityGroup = { ...content };
      this.commodityIcon = content["icon"];

      CommodityRepository.getNewCode().then((response) => {
        this.commodityData = { commodity_code: response.data };
        //bind lên combobox commodity group
        this.$set(
          this.commodityData,
          "commodity_group_name",
          content["commodity_group_name"]
        );
        this.$set(
          this.commodityData,
          "commoditygroup_id",
          content["commoditygroup_id"]
        );
        this.$refs.commodityname.focusInput();
        this.isShowCommodityDetail = true;
      });
    });
  },
  destroyed() {
    this.$eventBus.$off("showCommodityDetail");
  },
};
</script>

<style scoped>
@import url("../../../css/views/commodity/commoditydetail.css");
</style>