export default {
	methods: {
		/**
		 * Tạo listener cho context menu
		 * @param {Array} contextFunction
		 * @returns {Listener}
		 *  CreatedBy: nvdien (20/09/2021)
		 */
		contextMenuListeners(contextMenuData) {
			var show = false;
			return Object.assign({}, this.$listener, {
				click: (event) => {
					let element = this.findParentByClass(event.target, "row-context-menu__icon");
					if (!show) {
						show = true;
						element.classList.add('context-menu--selected');
						var elementRect = element.getBoundingClientRect();

						var elementPos = {
							top: elementRect.top,
							left: elementRect.left,
							topChange: contextMenuData['topChange'],
							leftChange: contextMenuData['leftChange']
						};
						this.$eventBus.$emit("showContextMenu", {
							contextFunction: contextMenuData['contextFunction'],
							pos: elementPos,
						});
					} else {
						show = false;
						this.$eventBus.$emit('hideContextMenu');
						element.classList.remove('context-menu--selected');
					}

				},
				// focus: (event) => {
				// 	var element = event.target;
				// 	element.classList.add('context-menu--selected');
				// },
				// blur: (event) => {
				// 	let element = this.findParentByClass(event.target, "row-context-menu__icon");

				// 	element.classList.remove('context-menu--selected');

				// 	this.$eventBus.$emit('hideContextMenu');
				// }
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
		}
	}
}