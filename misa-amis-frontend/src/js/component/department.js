import BaseAPI from "../base/baseapi"
class DepartmentAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Departments"
    }
}
export default new DepartmentAPI();