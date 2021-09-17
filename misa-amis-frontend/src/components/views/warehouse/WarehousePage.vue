<template lang="">
	<div class="content-tab">
		<base-tab :currTab="currTab" :listTab="listTab" v-model="currTab" />
		<router-view @showInwardDetail="showInwardDetail"/>
		<inward-detail v-show="isShowInwardDetail" @closeInwardDetail = "closeInwardDetail"/>
	</div>
</template>
<script>
	import BaseTab from "../../base/BaseTab.vue";
	import InwardDetail from "./InwardDetail.vue";
	export default {
		name: "WarehousePage",
		components: {
			BaseTab,
			InwardDetail
		},
		data() {
			return {
				currTab: 0,
				listTab: [
					{
						name: this.$resourcesVN.WAREHOUSE_TABS.Process,
						path: "/warehouse/process",
					},
					{
						name: this.$resourcesVN.WAREHOUSE_TABS.InwardOutwardList,
						path: "/warehouse/InwardOutwardList",
					},
				],
				isShowInwardDetail: false,
			};
		},
		methods: {
			showInwardDetail(){
				this.isShowInwardDetail = true;
			},
			closeInwardDetail(){
				this.isShowInwardDetail = false;
			}
		},
		watch: {
			/**
			 * Khi chuyển tab thì redirect trang
			 * @param {Number} value
			 * CreatedBy: nvdien (16/09/2021)
			 */
			currTab: function(value) {
				this.$router.push(this.listTab[value].path);
			},
		},
	};
</script>
<style>
	@import url("../../../css/views/warehouse/warehouse.css");
</style>
