<template lang="">
	<div
		v-show="tooltipState"
		class="ms-tooltip tooltip--arrow"
		:style="positionOfTooltip"
	>
		{{ tooltipContent }}
	</div>
</template>
<script>
	// LIBRARY

	export default {
		name: "BaseTooltip",
		data() {
			return {
				tooltipContent: "Nguyễn Văn Diện",
				tooltipState: false,
				tooltipTop: -100,
				tooltipLeft: -100,
				tooltipHeight: 23,
				tooltipWidth: 130
			};
		},
		computed: {
			/**
			 * Tính toán vị trí của tooltip
			 * @returns {object} vị trí của tooltip
			 * CreatedBy: nvdien (20/09/2021)
			 */
			positionOfTooltip() {
				// Kiểm tra nếu vượt quá màn hình
				var top, left, right = "unset";

				// Kiểm tra top
				if (this.tooltipTop + 20 + this.tooltipHeight > window.innerHeight) 
					top = this.tooltipTop - 20 + 'px';
				else {
					top = this.tooltipTop + 20 + 'px';
				}

				// Kiểm tra left
				if (this.tooltipLeft + this.tooltipWidth > window.innerWidth) {
					left = "unset";
					right = 0;
				} else {
					left = this.tooltipLeft + 'px';
				}

				return {top: top, left: left, right: right};
			}
		},
		created() {
			/**
			 * Lắng nghe sự kiện hiển thị tooltip
			 * CreatedBy: nvdien (20/09/2021)
			 */
			this.$eventBus.$on("showTooltip", (data) => {
				this.tooltipState = true;
				this.tooltipContent = data['message'];
                this.tooltipTop = data['top'];
                this.tooltipLeft = data['left'];
			});
			/**
			 * Lắng nghe sự kiện ẩn tooltip
			 * CreatedBy: nvdien (20/09/2021)
			 */
			this.$eventBus.$on("hideTooltip", () => {
				this.tooltipState = false;
			});
		},
		destroy() {
			// Huỷ các sự kiện
			this.$eventBus.$off("hideTooltip");
			this.$eventBus.$off("showTooltip");
		},
	};
</script>
<style>
	@import url('../../css/base/tooltip.css');
</style>
