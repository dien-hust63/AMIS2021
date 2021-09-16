import Vue from 'vue'
import VueRouter from 'vue-router'
import EmployeeContent from "../components/views/employees/EmployeeContent.vue";
//warehouse
import WarehousePage from "../components/views/warehouse/WarehousePage";
import WarehouseProcess from "../components/views/warehouse/tabs/WarehouseProcess";
import WarehouseInwardOutwardList from "../components/views/warehouse/tabs/WarehouseInwardOutwardList";
import WarehouseReport from "../components/views/warehouse/tabs/WarehouseReport";
Vue.use(VueRouter)

const routes = [
    { path: "/", redirect: "/warehouse" },
    {path: "/employees", name:"EmployeeContent", component: EmployeeContent},
    {
		path: "/warehouse",
		redirect: "/warehouse/process",
		component: WarehousePage,
		children: [
			{ path: "/warehouse/process", component: WarehouseProcess },
			{ path: "/warehouse/InwardOutwardList", component: WarehouseInwardOutwardList },
			{ path: "/warehouse/report", component: WarehouseReport },
		],
	},
]

const router = new VueRouter({
    mode: 'history',
    routes : routes 
})

export default router;