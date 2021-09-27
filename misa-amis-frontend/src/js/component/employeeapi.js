import axios from "axios";
import BaseAPI from "../base/baseapi"
class EmployeeAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Employees"
    }

    /**
    * lọc và phân trang nhân viên
    * url phân trang
    * @returns promise
    * CreatedBy: nvdien(24/9/2021)
    */
     getEmployeePagingFilter(searchData,pageIndex, pageSize) {
        let url = this.baseApiUrl + this.controller + `/filter?searchData=${searchData}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        return axios.get(url);
    }

    /**
     * Lấy mã nhân viên mới
     * @return promise
     * CreatedBy: nvdien(26/8/2021)
     */
    getNewEmployeeCode() {
        let api = this.baseApiUrl + this.controller + "/NewEmployeeCode";
        return axios.get(api);
    }
    
    /**
     * Kiểm tra mã nhân viên có trùng không
     * @param {string} employeeCode  mã nhân viên
     * @returns promise
     * CreatedBy: nvdien(1/9/2021)
     */
    checkEmployeeCodeExist(employeeCode){
        let api = this.baseApiUrl + this.controller + `/EmployeeCodeExist?employeeCode=${employeeCode}`;
        return axios.get(api);
    }

    /**
     * Xuất dữ liệu ra file excel
     * @returns promise
     * CreatedBy: nvdien(31/8/2021)
     */
    export() {
        let api = this.baseApiUrl + this.controller + "/export";
        return axios.get(api, {responseType: "blob"});
    }
}
export default new EmployeeAPI();