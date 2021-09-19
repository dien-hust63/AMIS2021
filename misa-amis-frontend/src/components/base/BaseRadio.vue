<template lang="">
	<div class="radio">
		<div v-if="label" class="radio__label">{{ label }}</div>
		<div class="radio__list">
			<div
				v-for="(item, index) in options"
				class="radio__item"
				:class="{ 'radio__item--selected': index == currIdx }"
        @click="chooseRadio(index)"
				:key="index"
				tabindex="0"
				:ref="'radio' + index"
			>
				<div class="radio__border" >
					<div class="radio__main">
						<div class="radio__icon"></div>
					</div>
				</div>
				<div class="radio__value">{{ item['value'] }}</div>
			</div>
		</div>
	</div>
</template>
<script>
export default {
  name: "BaseRadio",
  props: {
    options: {
      type: Array,
      default: function () {
        return [];
      },
    },
    label: {
      type: String,
      default: "",
    },
    value: {
      type: Number,
      default: null,
    },
    radioReset: {
      type: Boolean,
      default: false,
    }
  },
  data() {
    return {
      currIdx: 0,
    };
  },
  methods: {
    /**
     * Chọn item
     * CreatedBy: nvdien(2/9/2021)
     */
    chooseRadio(index) {
      this.currIdx = index;
      this.$emit("chooseRadio", this.options[index]);
    },
  },
  watch: {
    /**
     * Bắt sự kiện thay đổi giá trị truyền vào
     * @param {number} value
     * CreatedBy: nvdien (31/08/2021)
     */
    value: function (newValue) {
      if (newValue != null) this.currIdx = this.options.findIndex(x => x['data'] === newValue);
    },
    /**
     * reset radio về nam
     * CreatedBy: nvdien(3/9/2021)
     */
    radioReset: function(newValue){
      if(newValue){
        this.currIdx = 0;
      }
    }
  },
};
</script>
<style scoped>
@import url("../../css/base/radio.css");
</style>
