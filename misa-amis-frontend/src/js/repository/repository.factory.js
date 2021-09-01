import EmployeeApi from '../component/employeeapi.js';
const repositories = {
    employees: EmployeeApi,
}
export const RepositoryFactory = {
    get: name => repositories[name]
}