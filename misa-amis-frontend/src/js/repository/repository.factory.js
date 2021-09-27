import EmployeeApi from '../component/employeeapi.js';
import DepartmentApi from '../component/department.js';
import VoucherApi from "../component/voucher.js";
import AccountObjectApi from "../component/accountobject.js";
const repositories = {
    employees: EmployeeApi,
    departments: DepartmentApi,
    vouchers: VoucherApi,
    accountobjects: AccountObjectApi
}
export const RepositoryFactory = {
    get: name => repositories[name]
}