import axios from "axios";
import BaseAPI from "../base/baseapi"
class EmployeeAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Employees"
    }

    /**
     * lọc và phân trang nhân viên
     * @param {string} searchVal dữ liệu search
     * @param {string} departmentVal Id phòng ban
     * @param {string} positionVal ID vị trí
     * @param {int} pageIndex index trang 
     * @param {int} pageSize số bản ghi trên trang
     * @returns promise
     * CreatedBy: nvdien(26/8/2021)
     */
    getEmployeePaging(pageIndex, pageSize, searchVal = "", departmentVal = "", positionVal = "") {
        let api = this.baseApiUrl + this.controller;
        api += `/Filter?searchData=${searchVal}&departmentId=${departmentVal}&positionId=${positionVal}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        return axios.get(api);
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