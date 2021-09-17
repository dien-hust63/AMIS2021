<template lang="">
	<div class="warehouse">
		<div class="warehouse__process">
			<div class="warehouse__tutorial">
				<div class="warehouse__tutorial-title"> {{ $resourcesVN.WAREHOUSE_TUTORIAL_TITLE }}</div>
				<div class="warehouse__tutorial-map">
					<div class="warehouse__node">
						Lệnh sản xuất
					</div>
					<div class="warehouse__node">
						Lắp đặt, tháo dỡ
					</div>
					<div class="warehouse__node">
						Xuất kho
					</div>
					<div class="warehouse__node" @click="showInwardDetail">
						Nhập kho
					</div>
					<div class="warehouse__node">
						Chuyển kho
					</div>
					<div class="warehouse__node">
						Tính giá xuất kho
					</div>
					<div class="warehouse__node">
						Kiểm kê
					</div>
				</div>
				<div class="warehouse__tutorial-function">
					<div
						v-for="(item, index) in WarehouseProcessFunction"
						class="warehouse__tutorial-function-item"
						:key="index"
					>
						<div
							class="warehouse__tutorial-function-icon"
							:style="{
								'background-position': item['position'],
								width: item['width'],
								height: item['height'],
							}"
						></div>
						<div class="warehouse__tutorial-function-label">
							{{ item["name"] }}
						</div>
					</div>
				</div>
			</div>
			<div class="warehouse__report">
				<div class="warehouse__report-title"> Báo cáo</div>
				<ul class="warehouse__report-list">
					<li class="warehouse__report-item"> Sổ chi tiết vật tư hàng hoá</li>
					<li class="warehouse__report-item"> Tổng hợp tồn kho</li>
					<li class="warehouse__report-item"> Tổng hợp tồn kho theo nhiều đơn vị tính</li>
					<li class="warehouse__report-item"> Tổng hợp tồn trên nhiều kho (Dạng bảng chéo)</li>
					<li class="warehouse__report-item"> Tổng hợp nhập xuất tồn trên nhiều kho</li>
				</ul>
				<div class="warehouse__report-all">Tất cả báo cáo</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import WarehouseProcessFunction from "../../../../mixins/warehouse/warehouseProcessFunction";

	export default {
		name: "WarehouseProcess",
		mixins: [WarehouseProcessFunction],
		methods:{
			showInwardDetail(){
				this.$emit("showInwardDetail");
			}
		}
	};
</script>
<style>
	@import url("../../../../css/views/warehouse/warehouseprocess.css");
</style>
