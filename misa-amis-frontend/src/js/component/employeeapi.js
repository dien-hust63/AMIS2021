import axios from "axios";
import BaseAPI from "../base/baseapi"
class EmployeeAPI extends BaseAPI{
    constructor(){
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
     * author: nvdien(26/8/2021)
     */
    getEmployeePaging(pageIndex, pageSize, searchVal="", departmentVal="", positionVal=""){
        let api = this.baseApiUrl + this.controller;
        api += `/Filter?searchData=${searchVal}&departmentId=${departmentVal}&positionId=${positionVal}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        return axios.get(api);
    }

    /**
     * Lấy mã nhân viên mới
     * @return promise
     * author: nvdien(26/8/2021)
     */
    getNewEmployeeCode(){
        let api = this.baseApiUrl + this.controller + "/NewEmployeeCode";
        return axios.get(api);
    }
}
export default new EmployeeAPI();