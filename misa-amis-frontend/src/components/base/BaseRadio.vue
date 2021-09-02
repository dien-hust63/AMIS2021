<template lang="">
<!-- @keydown="radioItemOnKeydown($event, index)" -->
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
				<div class="radio__value">{{ item['GenderName'] }}</div>
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
  },
  data() {
    return {
      currIdx: 0,
    };
  },
  methods: {
    /**
     * Bắt sự kiện nhấn phím mũi tên để di chuyển
     * @param {event} event
     * @param {number} index
     * CreatedBy: NTDUNG (30/08/2021)
     */
    // radioItemOnKeydown(event, index) {
    // 	switch(event.key) {
    // 		case 'ArrowRight':
    // 			if (this.currIdx != this.options.length - 1)
    // 				this.currIdx++;
    // 			else
    // 				this.currIdx = 0;
    // 			this.$refs['radio' + this.currIdx][0].focus();
    // 			break;
    // 		case 'ArrowLeft':
    // 			if (this.currIdx != 0)
    // 				this.currIdx--;
    // 			else
    // 				this.currIdx = this.options.length - 1;
    // 			this.$refs['radio' + this.currIdx][0].focus();
    // 			break;
    // 		case 'Enter':
    // 			this.currIdx = index;
    // 			break;
    // 	}
    // }
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
     * CreatedBy: NTDUNG (31/08/2021)
     */
    value: function (value) {
      if (value !== null) this.currIdx = value;
    },
  },
};
</script>
<style scoped>
@import url("../../css/base/radio.css");
</style>
