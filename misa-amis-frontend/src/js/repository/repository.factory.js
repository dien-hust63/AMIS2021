import EmployeeApi from '../component/employeeapi.js';
import DepartmentApi from '../component/department.js';
import VoucherApi from "../component/voucher.js";
import AccountObjectApi from "../component/accountobject.js";
import CommodityApi from "../component/commodity";
import CommodityGroupApi from "../component/commoditygroup";
import WarehouseApi from "../component/warehouse";
const repositories = {
    employees: EmployeeApi,
    departments: DepartmentApi,
    vouchers: VoucherApi,
    accountobjects: AccountObjectApi,
    commoditys: CommodityApi,
    commoditygroups: CommodityGroupApi,
    warehouses: WarehouseApi
}
export const RepositoryFactory = {
    get: name => repositories[name]
}