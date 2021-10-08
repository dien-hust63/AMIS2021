<template>
  <div
    class="warehouse-add-wrap"
    :class="{ 'warehouse-add--show': isShowWarehouseAdd }"
  >
    <div class="warehouse-add">
      <div class="customer-popup__header">
        <div class="customer-popup-title">
          <div class="popup-title">Thêm kho</div>
        </div>
        <div class="customer-popup-close">
          <div class="mi mi-24 mi-help popup-help-icon"></div>
          <div
            class="mi mi-24 mi-close popup-close-icon"
            @click="closeWarehouseAdd"
          ></div>
        </div>
      </div>
      <div class="warehouse-add__content">
        <div class="popup-content">
          <div class="row-input">
            <div class="w-2/5 p-r-12 border-box">
              <base-input
                label="Mã"
                :required="true"
                ref="warehousecode"
                v-model="warehouseData['warehouse_code']"
              />
            </div>
            <div class="w-3/5">
              <base-input
                label="Tên"
                :required="true"
                v-model="warehouseData['warehouse_name']"
              />
            </div>
          </div>
          <div class="row-input">
            <div class="w-2/5 p-r-12 border-box">
              <base-combobox-custom label="Tài khoản kho" :hideAddIcon="true" />
            </div>
          </div>
          <div class="row-input">
            <base-input
              label="Địa chỉ"
              typeInput="textarea"
              class="address"
              v-model="warehouseData['warehouse_address']"
            />
          </div>
        </div>
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
              @click="saveWarehouse"
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
import { RepositoryFactory } from "../../../js/repository/repository.factory";
const WarehouseRepository = RepositoryFactory.get("warehouses");
export default {
  components: { BaseComboboxCustom, BaseInput },
  name: "WarehouseAdd",
  data() {
    return {
      isShowWarehouseAdd: false,
      warehouseData: {},
      rowTable: -1,
    };
  },
  methods: {
    /**
     * Lưu thông tin kho
     * createdBy: nvdien(6/10/2021)
     */
    saveWarehouse() {
      WarehouseRepository.post(this.warehouseData).then((response)=>{
        this.$eventBus.$emit("getWarehouseAdd", response.data.Data, this.rowTable);
        this.isShowWarehouseAdd = false;
        
      }).catch(response => console.log(response));
    },
    /**Đóng form thêm kho
     * CreatedBy: nvdien(6/10/2021)
     */
    closeWarehouseAdd() {
      this.isShowWarehouseAdd = false;
    },
  },
  created() {
    this.$eventBus.$on("showWarehouseAdd", (rowTable) => {
      this.rowTable = rowTable;
      //lấy mã mới
      WarehouseRepository.getNewCode()
        .then((response) => {
          this.$set(this.warehouseData, "warehouse_code", response.data);
          this.isShowWarehouseAdd = true;
          this.$refs.warehousecode.focusInput();
        })
        .catch((response) => {
          console.log(response);
          this.isShowWarehouseAdd = true;
          this.$refs.warehousecode.focusInput();
        });

    });
  },
  destroyed() {
    this.$eventBus.$off("showWarehouseAdd");
  },
};
</script>

<style scoped>
@import url("../../../css/views/warehouse/warehouseadd.css");
</style>