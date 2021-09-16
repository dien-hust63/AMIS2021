<template lang="">
	<div class="content-tab">
		<base-tab :currTab="currTab" :listTab="listTab" v-model="currTab" />
		<router-view />
	</div>
</template>
<script>
	import BaseTab from "../../base/BaseTab.vue";
	export default {
		name: "WarehousePage",
		components: {
			BaseTab,
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
					{
						name: this.$resourcesVN.WAREHOUSE_TABS.Report,
						path: "/warehouse/report",
					},
				],
			};
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
