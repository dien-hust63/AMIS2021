<template>
  <div class="ms-table-wrap">
    <table class="ms-table">
      <thead class="ms-thead">
        <tr>
          <th v-for="(item, index) in tableHeaders" :key="index">
            <base-checkbox v-if="item.type == 'checkbox'" />
            <div v-if="item.label">{{ item.label }}</div>
          </th>
        </tr>
      </thead>
      <tbody class="ms-tbody">
        <tr v-for="(tableContent, index) in tableContents" :key="index">
          <td v-for="(tableHeader, index) in tableHeaders" :key="index">
            <base-checkbox v-if="tableHeader.type == 'checkbox'" />
            <div v-if="tableHeader.type == 'normal'">
              {{ tableContent[tableHeader.fieldName] }}
            </div>
            <div v-if="tableHeader.type == 'contextmenu'">
              <div class="row-context-menu">
                <div class="row-context-menu__text">Xem</div>
                <div class="row-context-menu__icon"  @click="toogleContextMenu($event)" >
                  <div class="mi mi-16 mi-arrow-up--blue"></div>
                </div>
              </div>
            </div>
            <div v-if="tableHeader.type == 'number'">{{index + 1}}</div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>


<script>
import BaseCheckbox from "./BaseCheckbox.vue";
export default {
  name: "BaseTable",
  components: {
    BaseCheckbox,
  },
  props: {
    tableHeaders: {
      type: Array,
      default() {
        return [];
      },
    },
    tableContents:{
      type: Array,
      default(){
        return [];
      }
    }
  },
  data() {
    return {
      isLoading: false,
    };
  },
  methods: {
    /**Hiển thị context menu */
    toogleContextMenu(event){
      this.$emit("toogleContextMenu", event);
    }
  },

};
</script>

<style scoped>
@import url("../../css/base/table.css");
</style>
