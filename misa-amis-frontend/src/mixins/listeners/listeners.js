export default{
    methods: {
        /**Lắng nghe sự kiện contextmenu */
        contextMenuListeners(){
            return Object.assign({}, this.$listener, {
				click: (event) => {
                    let currentElementPosition = event.tartget.getBoundingClientRect();
                    console.log(currentElementPosition);
                }
			});
        },
        /**
		 * Lắng nghe sự kiện cho tooltip
		 * @param {string} message
		 * CreatedBy: nvdien (20/9/2021)
		 */
		tooltipListeners(message) {
			return Object.assign({}, this.$listener, {
				mouseenter: (event) => {
					// Truyền nội dung và vị trí cho tooltip
					var tooltipStyle = {
						message: message,
						top: event.clientY,
						left: event.clientX,
					};
					// Hiện tooltip
					this.$eventBus.$emit("showTooltip", tooltipStyle);
				},
				mouseleave: () => {
					// Ẩn tooltip
					this.$eventBus.$emit("hideTooltip");
				},
			});
		},
    }
}