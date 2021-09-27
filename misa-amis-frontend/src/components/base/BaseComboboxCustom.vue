<template>
  <div class="ms-combobox-custom">
    <label class="combobox-custom-label">
      {{ label }} <span v-if="required"><b class="text--red">*</b></span>
    </label>
    <div class="combobox-custom-content">
      <div class="combobox-custom-input">
        <input type="text" v-on="inputListeners" :value="value"/>
      </div>
      <div class="combobox-custom-actions">
        <div class="btn-add btn-actions" @click="showAddForm">
          <div class="mi mi-16 mi-plus--success"></div>
        </div>
        <div class="btn-dropdown btn-actions" @click="showComboDropdown($event)">
          <div class="mi mi-16 mi-arrow-dropdown"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseComboboxCustom",
  props: {
    required: {
      type: Boolean,
      default() {
        return false;
      },
    },
    label: {
      type: String,
      default: "",
    },
    value:{
      type: String,
      default: "",
    }
  },
  data(){
    return{
      isShowComboDropdown: false,
      inputValue:"",
    }
  },
 
  methods:{
      /**
       * Hiển thị combo dropdown panel
       * CreatedBy: nvdien(26/9/2021)
       */
      showComboDropdown(event){
        this.isShowComboDropdown = !this.isShowComboDropdown;
        if(this.isShowComboDropdown){
          this.$emit("showComboDropdown",event);
        }
        else{
          this.$eventBus.$emit("hideComboDropdown");
        }
        
      },
      /**Hiển thị form thêm */
      showAddForm(){
        this.$emit("showAddForm");
      }
      
  },
  computed: {
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.$emit("input", event.target.value);
        },
        focus: function (event) {
          event.target.select();
        },
      });
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/comboboxcustom.css");
</style>