<template lang="">
	<div
		class="context-menu-container"
		:class="{ 'selectbox--show': contextMenuState, 'selectbox--up': isUp }"
		:style="positionOfContextMenu"
	>
		<ul ref="contextMenu" class="selectbox__list">
			<li
				v-for="(item, index) in contextFunction"
				@click="choose(index)"
				class="selectbox__item"
				:key="index"
			>
				{{ item["name"] }}
			</li>
		</ul>
	</div>
</template>
<script>
	export default {
		name: "BaseContextMenu",
		data() {
			return {
				contextMenuState: false,
				contextFunction: [],
				isUp: false,
				top: -100,
				left: -100,
                topChange: 0,
                leftChange: 0,
			};
		},	
        computed: {
            /**
             * Tính toán vị trí aủa contextmenu
             * CreatedBy: nvdien (23/09/2021)
             */
            positionOfContextMenu() {
                if (this.isUp) {
                    return {
                        top: this.top + 'px',
                        left: this.left + this.leftChange+ 'px'
                    }
                } else {
                    return {
                        top: this.top + this.topChange +'px',
                        left: this.left + this.leftChange + 'px'
                    }
                }
            }
        },
		created() {
			/**
			 * Tạo event hiển thị context menu
			 * CreatedBy: nvdien (20/09/2021)
			 */
			this.$eventBus.$on("showContextMenu", (data) => {
				this.contextMenuState = true;
				this.contextFunction = data["contextFunction"];

                this.top = data['pos']['top'];
                this.left = data['pos']['left'];
                this.topChange = data['pos']['topChange'];
                this.leftChange = data['pos']['leftChange'];

                this.$nextTick(() => {
                    this.setPositionOfContextMenu();
                });
			});
			/**
			 * Tạo event ẩn context menu
			 * CreatedBy: nvdien (20/09/2021)
			 */
			this.$eventBus.$on("hideContextMenu", () => {
				setTimeout(() => {	
					this.contextMenuState = false;
				}, 100);
			});
		},
		methods: {
            /**
             * Đặt vị trí cho contextmenu
             * CreatedBy: nvdien (23/09/2021)
             */
            setPositionOfContextMenu() {
                if (this.$refs.contextMenu) {
                    var bottom = this.$refs.contextMenu.getBoundingClientRect().bottom;
                    if (bottom + 46 >= window.innerHeight) {
                        this.isUp = true;
                    } else {
                        this.isUp = false;
                    }
                } 
			},
			/**
			 * Chọn chức năng trên context menu
			 * @param {Number} index
			 * CreatedBy: nvdien (20/09/2021)
			 */
			choose(index) {
				this.contextFunction[index]['function']();
				this.contextMenuState = false;
			},
		},
		destroyed() {
			/**
			 * Huỷ các sự kiện
			 * CreatedBy: nvdien (20/09/2021)
			 */
			this.$eventBus.$off("showContextMenu");
			this.$eventBus.$off("hideContextMenu");
		},
	};
</script>
<style>
	@import url("../../css/base/contextmenu.css");
</style>
