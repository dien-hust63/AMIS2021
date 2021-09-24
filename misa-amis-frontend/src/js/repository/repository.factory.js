import EmployeeApi from '../component/employeeapi.js';
import DepartmentApi from '../component/department.js';
import VoucherApi from "../component/voucher.js";
const repositories = {
    employees: EmployeeApi,
    departments: DepartmentApi,
    vouchers: VoucherApi
}
export const RepositoryFactory = {
    get: name => repositories[name]
}