import EmployeeApi from '../component/employeeapi.js';
import DepartmentApi from '../component/department.js';
const repositories = {
    employees: EmployeeApi,
    departments: DepartmentApi,
}
export const RepositoryFactory = {
    get: name => repositories[name]
}