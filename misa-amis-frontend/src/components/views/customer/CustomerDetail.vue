<template>
  <div
    class="customer-detail-wrap"
    :class="{ 'customer-popup--show': isShowCustomerDetail }"
  >
    <div class="customer-popup">
      <div class="customer-popup__header">
        <div class="customer-popup-title">
          <div class="popup-title">Thông tin khách hàng</div>
          <base-radio :options="customerRadioData" />
          <base-checkbox label="Là nhà cung cấp" />
        </div>
        <div class="customer-popup-close">
          <div class="mi mi-24 mi-help popup-help-icon"></div>
          <div
            class="mi mi-24 mi-close popup-close-icon"
            @click="closeCustomerDetail"
          ></div>
        </div>
      </div>
      <div class="customer-popup__content">
        <div class="popup-content">
          <div class="row-input">
            <div class="row-input--left w-1/2">
              <div class="w-2/5 p-r-6 border-box">
                <base-input label="Mã số thuế" />
              </div>
              <div class="w-3/5">
                <base-input label="Mã khách hàng" :required="true" />
              </div>
            </div>
            <div class="row-input--right w-1/2">
              <div class="w-2/5 p-r-6 border-box">
                <base-input label="Điện thoại" />
              </div>
              <div class="w-3/5">
                <base-input label="Website" />
              </div>
            </div>
          </div>
          <div class="row-input">
            <div class="row-input--left w-1/2">
              <base-input label="Tên khách hàng" :required="true" />
            </div>
            <div class="row-input--right w-1/2">
              <base-combobox-custom label="Nhóm khách hàng" />
            </div>
          </div>
          <div class="row-input">
            <div class="row-input--left w-1/2">
              <base-input
                label="Địa chỉ"
                class="input-address"
                typeInput="textarea"
                placeholder="VD: Số 82 Duy Tân, Dịch Vọng Hậu, Cầu Giấy, Hà Nội"
              />
            </div>
            <div class="row-input--right w-1/2">
              <base-combobox-custom
                label="Nhân viên bán hàng"
                @showComboDropdown="showEmployeeDropdownPanel"
                @showAddForm="addEmployee"
                v-model="employeeComboboxValue"
                @keyup="searchEmployeeCombobox($event)"
              />
            </div>
          </div>
          <div class="w-full contact-content">
            <div class="contact-content__title">Liên hệ</div>
            <div class="contact-content__content flex">
              <div class="w-1/2 contact-content__content--left">
                <div class="row-input">
                  <div class="w-2/5 contact-person">
                    <base-input label="Người liên hệ" placeholder="Xưng hô" />
                  </div>
                  <div class="w-3/5 label--hidden">
                    <base-input placeholder="Họ và tên" label="Họ và tên" />
                  </div>
                </div>
                <div class="row-input">
                  <base-input placeholder="Email" />
                </div>
                <div class="row-input w-1/2">
                  <base-input placeholder="Số điện thoại" />
                </div>
                <div class="row-input">
                  <base-input
                    placeholder="Đại diện theo PL"
                    label="Đại diện theo PL"
                  />
                </div>
              </div>
              <div class="w-1/2">
                <div class="row-input">
                  <base-input
                    label="Người nhận hóa đơn điện tử"
                    placeholder="Họ và tên"
                  />
                </div>
                <div class="row-input">
                  <base-input
                    placeholder="Email, ngăn cách nhiều email bởi dấu phẩy"
                  />
                </div>
                <div class="row-input">
                  <base-input placeholder="Số điện thoại" />
                </div>
              </div>
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
              <div class="ms-button ms-button-secondary first-right-button">
                Cất
              </div>
              <div class="ms-button ms-button-primary">Cất và thêm</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../../base/BaseInput.vue";
import BaseCheckbox from "../../base/BaseCheckbox.vue";
import BaseRadio from "../../base/BaseRadio.vue";
import BaseComboboxCustom from "../../base/BaseComboboxCustom.vue";
import { RepositoryFactory } from "../../../js/repository/repository.factory";
const EmployeeRepository = RepositoryFactory.get("employees");
export default {
  name: "CustomerDetail",
  components: {
    BaseInput,
    BaseCheckbox,
    BaseRadio,
    BaseComboboxCustom,
  },
  data() {
    return {
      customerRadioData: [
        { data: 1, value: "Tổ chức" },
        { data: 2, value: "Cá nhân" },
      ],
      isShowCustomerDetail: false,
      /**combo employee */
      timeDelaySearch: null,
      employeeComboboxValue: "",
    };
  },
  methods: {
    /**đóng form */
    closeCustomerDetail() {
      this.isShowCustomerDetail = false;
    },
    //#region Combobox employee

    /**hiển thị combo dropdown panel của khách hàng */
    showEmployeeDropdownPanel(event) {
      let element = event.target;
      let elementRect = element.getBoundingClientRect();
      let leftChange =
        -this.findParentByClass(element, "combobox-custom-content")
          .clientWidth +
        element.clientWidth +
        6;
      let elementPos = {
        top: elementRect.top,
        left: elementRect.left,
        topChange: 30,
        leftChange: leftChange,
      };
      EmployeeRepository.getEmployeePagingFilter("", 1, 20)
        .then((response) => {
          let comboDropdownData = {
            tableHeaders: this.$resourcesVN.tableEmployeeComboboxHeaders,
            tableContents: response.data["Employees"],
            hasFooter: false,
            position: elementPos,
          };
          this.$eventBus.$emit("showComboDropdown", comboDropdownData);
        })
        .catch((response) => console.log(response));
    },
    /**filter combobox khách hàng */
    searchEmployeeCombobox(event) {
      if (this.timeDelaySearch) {
        clearTimeout(this.timeDelaySearch);
      }
      this.timeDelaySearch = setTimeout(() => {
        let element = event.target;
        let elementRect = element.getBoundingClientRect();
        let elementPos = {
          top: elementRect.top,
          left: elementRect.left,
          topChange: 30,
          leftChange: -10,
        };
        EmployeeRepository.getEmployeePagingFilter(
          this.customerComboboxValue,
          1,
          20
        )
          .then((response) => {
            let comboDropdownData = {
              tableHeaders: this.$resourcesVN.tableEmployeeComboboxHeaders,
              tableContents: response.data["Employees"],
              hasFooter: false,
              position: elementPos,
              isLoading: false,
            };
            this.$eventBus.$emit("showComboDropdown", comboDropdownData);
          })
          .catch((response) => console.log(response));
        this.$eventBus.$emit("showComboDropdown", {
          position: elementPos,
          isLoading: true,
        });
      }, 500);
    },
    /**
     * Hàm tính ra element cha chứa lớp được truyền vào
     * @param {Element} childE
     * @param {String} parentClass
     */
    findParentByClass(childE, parentClass) {
      var parentE = childE;
      if (parentE) {
        // Nếu không chứa class thì tiếp tục vòng lặp
        while (parentE.classList.contains(parentClass) == false) {
          // Đi ra một element cha
          parentE = parentE.parentElement;
          // Khi đã duyệt hết mà không có thì set null và thoát vòng lặp
          if (parentE.tagName == "BODY") {
            parentE = null;
            break;
          }
        }
      }
      return parentE;
    },
    /**Thêm mới khách hàng */
    addEmployee() {
      this.$eventBus.$emit("showEmployeeDetail");
    },
    //#endregion
  },
  created() {
    this.$eventBus.$on("showCustomerDetail", () => {
      this.isShowCustomerDetail = true;
    });
    /**
     * Tạo event hiển thị context menu
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$on("bindComboboxValue", (data) => {
      this.employeeComboboxValue = data["employee_name"];
      this.$eventBus.$emit("hideComboDropdown");
    });
  },
  destroyed() {
    this.$eventBus.$off("showCustomerDetail");
    /**
     * Huỷ các sự kiện
     * CreatedBy: nvdien (20/09/2021)
     */
    this.$eventBus.$off("bindComboboxValue");
  },
};
</script>

<style scoped>
@import url("../../../css/views/customer/customerdetail.css");
</style>